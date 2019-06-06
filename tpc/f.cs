using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace tpc
{
    public class f
    {
        #region 获取在指定目录(包括其子目录)里的所有文件的文件名//GetDirectories所有目录//GetFileSystemEntries所有目录及文件
        public static string[] FNameInPath(string path)
        {
            return Directory.GetFiles(path, "*", SearchOption.AllDirectories);
        }
        #endregion

        #region 获取在指定目录里的所有文件的文件名
        public static string[] FNameInPath(string path, SearchOption searchOption)
        {
            return Directory.GetFiles(path, "*", searchOption);
        }
        #endregion

        #region 获取在指定目录(包括其子目录)里的符合搜索条件的文件的文件名，
        public static string[] FNameInPath(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
        }
        #endregion

        #region 将文本追加到桌面1.txt文件中
        /// <summary>
        /// 将文本追加到桌面1.txt文件中
        /// </summary>
        /// <param name="text">要追加的文本</param>
        public static void TextAppendToDeskFile(string text)
        {
            File.AppendAllText(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "1.txt"), text, Encoding.UTF8);
        }
        #endregion

        #region 删除同名文件，指定文件名，将文本保存到该文件中，文件位置在桌面，如果不存在则创建该文件
        /// <summary>
        /// 删除同名文件，指定文件名，将文本保存到该文件中，文件位置在桌面(如果不是绝对路径)，如果不存在则创建该文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="text"></param>
        public static void TextCreateToFile(string fileName, string text)
        {
            var fname = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
            if (File.Exists(fname))
            {
                File.Delete(fname);
            }
            File.AppendAllText(fname, text, Encoding.UTF8);
        }
        #endregion

        #region 指定文件名，将文本追加到该文件中，文件位置在桌面，如果不存在则创建该文件
        /// <summary>
        /// 指定文件名，将文本追加到该文件中，文件位置在桌面(如果不是绝对路径)，如果不存在则创建该文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="text"></param>
        public static void TextAppendToFile(string fileName, string text)
        {
            File.AppendAllText(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName), text, Encoding.UTF8);
        }
        #endregion

        #region 指定文件名，将多行文本追加到该文件中，文件位置在桌面，如果不存在则创建该文件
        /// <summary>
        /// 指定文件名，将多行文本追加到该文件中，文件位置在桌面(如果不是绝对路径)，如果不存在则创建该文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ienumrable"></param>
        public static void TextAppendToFile(string fileName, IEnumerable<string> ienumrable)
        {
            File.AppendAllLines(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName), ienumrable, Encoding.UTF8);
        }
        #endregion

        #region 将多行文本追加到桌面1.txt文件中
        /// <summary>
        /// 将多行文本追加到桌面1.txt文件中
        /// </summary>
        /// <param name="ienumrable"></param>
        public static void TextAppendToDeskFile(IEnumerable<string> ienumrable)
        {
            File.AppendAllLines(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "1.txt"), ienumrable, Encoding.UTF8);
        }
        #endregion

        #region 指定文件名、编码方式，将多行文本追加到该文件中，文件位置在桌面，如果不存在则创建该文件
        /// <summary>
        /// 指定文件名、编码方式，将多行文本追加到该文件中，文件位置在桌面(如果不是绝对路径)，如果不存在则创建该文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ienumrable"></param>
        /// <param name="encoding"></param>
        public static void TextAppendToFile(string fileName, IEnumerable<string> ienumrable, Encoding encoding)
        {
            File.AppendAllLines(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName), ienumrable, encoding);
        }
        #endregion

    }

    public class bof
    {
        #region stream to byte[]
        public static byte[] StreamToBytes(Stream stream)
        {
            using (stream)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    return br.ReadBytes((int)stream.Length);
                }
            }
        }
        #endregion
        
        #region 合并文件
        public static void CombineFile(string destfilename, params string[] paths)
        {
            using (FileStream fsave = new FileStream(destfilename, FileMode.Create))
            {
                foreach (var path in paths)
                {
                    using (FileStream fopen = new FileStream(path, FileMode.Open))
                    {
                        var bts = StreamToBytes(fopen);
                        fsave.Write(bts, 0, bts.Length);
                    }
                }
            }
        }
        #endregion
    }

    public class ftphelper
    {
        #region ftp方式上传
        /// <summary>
        /// ftp方式上传
        /// </summary>
        /// <param name="filePath">要上传的文件的路径，末尾不用带目录分隔符</param>
        /// <param name="filename">要上传的文件名</param>
        /// <param name="ftpServerIP">包括ip和端口（默认21可忽略），不包括"ftp://"</param>
        /// <param name="ftpFilePath">要保存在ftp服务器上的路径，末尾不用带目录分隔符，根目录为""</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        /// <returns></returns>
        public static int UploadFtp(string filePath, string filename, string ftpServerIP, string ftpFilePath, string ftpUserID, string ftpPassword)
        {
            FileInfo fileInf = new FileInfo(filePath + "\\" + filename);
            FtpWebRequest reqFTP;
            var uri = new Uri("ftp://" + ftpServerIP + "/" + ftpFilePath);
            if (!IsExistF(uri, ftpUserID, ftpPassword))
                CreateDirectory(uri, ftpUserID, ftpPassword);
            // Create FtpWebRequest object from the Uri provided 
            ftpFilePath = ftpFilePath == "" ? "" : ftpFilePath + "/";
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + ftpFilePath + fileInf.Name));
            try
            {
                // Provide the WebPermission Credintials 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                // By default KeepAlive is true, where the control connection is not closed 
                // after a command is executed. 
                reqFTP.KeepAlive = false;

                // Specify the command to be executed. 
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type. 
                reqFTP.UseBinary = true;

                // Notify the server about the size of the uploaded file 
                reqFTP.ContentLength = fileInf.Length;

                // The buffer size is set to 2kb 
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded 
                //FileStream fs = fileInf.OpenRead(); 
                FileStream fs = fileInf.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // Stream to which the file to be upload is written 
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time 
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends 
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream 
                strm.Close();
                fs.Close();
                return 1;
            }
            catch
            {
                reqFTP.Abort();
                //  Logging.WriteError(ex.Message + ex.StackTrace);
                throw;
            }
        }
        #endregion

        #region ftp方式下载
        /// <summary>
        /// ftp方式下载
        /// </summary>
        /// <param name="saveFilePath">保存路径(dir), 末尾不用带分隔目录符</param>
        /// <param name="fileName">保存文件名以及在ftp服务器上的文件名</param>
        /// <param name="ftpServerIP">包括ip和端口（默认21可忽略），不包括"ftp://"</param>
        /// <param name="ftpFilePath">在ftp上的路径, 末尾不用带分隔目录符</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        /// <returns>1：成功</returns>
        public static int DownloadFtp(string saveFilePath, string fileName, string ftpServerIP, string ftpFilePath, string ftpUserID, string ftpPassword)
        {
            FtpWebRequest reqFTP;
            try
            {
                //filePath = < <The full path where the file is to be created.>>, 
                //fileName = < <Name of the file to be created(Need not be the name of the file on FTP server).>>
                if (!Directory.Exists(saveFilePath))
                {
                    Directory.CreateDirectory(saveFilePath);
                }
                FileStream outputStream = new FileStream(saveFilePath + Path.DirectorySeparatorChar + fileName, FileMode.Create);

                ftpFilePath = ftpFilePath == "" ? "" : ftpFilePath + "/";
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + ftpFilePath + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return 1;
            }
            catch
            {
                // Logging.WriteError(ex.Message + ex.StackTrace);
                // System.Windows.Forms.MessageBox.Show(ex.Message);
                //return -2;
                throw;
            }
        }
        #endregion

        #region 获取ftp服务器上文件和目录详细信息（可以指定目录，不查找子目录）
        /// <summary>
        /// 获取ftp服务器上文件和目录详细信息（可以指定目录，不查找子目录）
        /// </summary>
        /// <param name="ftpServerIP">例:new Uri("ftp://192.168.2.206/新建文件夹/")</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPW"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string[] GetFileList(Uri ftpServerIP, string ftpUserID, string ftpPW, Encoding encoding)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpServerIP);
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPW);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), encoding);

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取ftp服务器上是否存在指定的文件或目录
        /// <summary>
        /// 获取ftp服务器上是否存在指定的文件或目录
        /// </summary>
        /// <param name="ftpServerIP">例:new Uri("ftp://192.168.2.206/1.txt")</param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPW"></param>
        /// <returns></returns>
        public static bool IsExistF(Uri ftpServerIP, string ftpUserID, string ftpPW)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpServerIP);
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPW);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader.ReadLine() != null;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ftp创建目录
        /// <summary>
        /// ftp创建目录(只能在原有目录上创建下一级目录，不能创建多级目录)
        /// </summary>
        /// <param name="uri">ftp地址（包括要创建的目录）</param>
        /// <param name="UserName">登陆账号</param>
        /// <param name="UserPass">密码</param>
        /// <param name="FileSource"></param>
        /// <param name="FileCategory"></param>
        /// <returns></returns>
        public static bool CreateDirectory(Uri uri, string UserName, string UserPass)
        {
            try
            {
                FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                FTP.Credentials = new NetworkCredential(UserName, UserPass);
                FTP.Proxy = null;
                FTP.KeepAlive = false;
                FTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FTP.UseBinary = true;
                FtpWebResponse response = FTP.GetResponse() as FtpWebResponse;
                response.Close();
                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
