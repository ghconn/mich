using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class SplitF
    {
        public static void splitF()
        {
            var fullname = @"E:\tool\Git-2.27.0-64-bit.exe";
            using (FileStream fs = new FileStream(fullname, FileMode.Open, FileAccess.Read))
            {
                for (long i = 0; i < fs.Length; i += 5 * 1024 * 1024) // 每次5M
                {
                    #region bytes长度
                    long length = 5 * 1024 * 1024L;
                    if (i + length > fs.Length)
                    {
                        length = fs.Length - i;
                    }
                    if (length == 0)
                    {
                        break;
                    }
                    #endregion
                    byte[] bts = new byte[length];
                    // 读取fs的指针已经在目标位置，offset的值为0即可，如果有其它扰乱指针位置的操作，在每次Read前使用fs.Seek(0, SeekOrigin.Begin)，offset的值为(int)i
                    fs.Read(bts, 0, (int)length);

                    using (Stream stream = new FileStream($@"E:\tool\Git-2.27.0-64-bit-{i}.exe", FileMode.Create))
                    {
                        stream.Write(bts, 0, bts.Length);
                    }
                }
            }
        }

        public static void mergeF()
        {
            var lst = new List<byte[]>();
            for (var i = 0; i < 10; i++) // 文件已经分割成10部分
            {
                var fullname = $@"E:\tool\Git-2.27.0-64-bit-{i}.exe";
                using (FileStream fs = new FileStream(fullname, FileMode.Open, FileAccess.Read))
                {
                    var bts = new byte[fs.Length];
                    fs.Read(bts, 0, bts.Length);
                    lst.Add(bts);
                }
            }
            using (Stream stream = new FileStream($@"E:\tool\Git-2.27.0-64-bit-merge.exe", FileMode.Create))
            {
                foreach (var bts in lst)
                {
                    stream.Write(bts, 0, bts.Length);
                }
            }
        }
    }
}
