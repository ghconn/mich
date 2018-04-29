using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public class DllRef
    {
        public static void GetRef()
        {
            var dllname = "DeliveryServicesProxy.dll";//要找出其引用的dll的dll，放在桌面
            var sourcefolder = @"\\172.16.10.251\soft2\康勇\dll\0829\";
            var destfolder = @"E:\0Source\SFWeb\trunk\storeWeb\packages\";

            var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), dllname);
            var assemblynames = System.Reflection.Assembly.LoadFile(filename).GetReferencedAssemblies();

            foreach (var assemblyname in assemblynames)
            {
                var sourcedll = assemblyname.Name + ".dll";
                if (File.Exists(sourcefolder + sourcedll) && !File.Exists(destfolder + sourcedll))
                    File.Copy(sourcefolder + sourcedll, destfolder + sourcedll);
            }
        }
    }
}
