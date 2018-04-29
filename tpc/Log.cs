using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;

namespace tpc
{
    public class log
    {
        static Mutex _mux = new Mutex();
        #region 写日志
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="V_Content">主体日志内容</param>
        /// <param name="V_Status">操作业务后的状态（如：失败；成功)</param>
        public static void WriteLog(string V_Content, string V_Status)
        {
            try
            {
                object obj = new object();
                lock (obj)
                {
                    string path = common.GetCurrentPhysicalPath + "/Temp";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path += "/" + DateTime.Now.ToString("yyyyMMdd") + ".nqh";
                    StreamWriter sw = new StreamWriter(path, true, Encoding.Default);
                    string log = DateTime.Now.ToString() + "\t" + V_Content + "\t" + V_Status;
                    sw.WriteLine(log);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {

            }

        } 
        #endregion

        #region 写日志
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="V_Path">日志内容保存路径</param>
        /// <param name="V_Content">主体日志内容</param>
        /// <param name="V_Status">操作业务后的状态（如：失败；成功)</param>
        public static void WriteLog(string V_Path, string V_Content, string V_Status)
        {
            try
            {
                object obj = new object();
                lock (obj)
                {
                    string logpath = common.GetCurrentPhysicalPath + "/Temp/" + V_Path;
                    if (!Directory.Exists(logpath))
                    {
                        Directory.CreateDirectory(logpath);
                    }
                    logpath += "/" + DateTime.Now.ToString("yyyyMMdd") + ".nqh";
                    StreamWriter sw = new StreamWriter(logpath, true, Encoding.Default);
                    string log = DateTime.Now.ToString() + "\t" + V_Content + "\t" + V_Status;
                    sw.WriteLine(log);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {

            }
        } 
        #endregion

        #region ShouWriteLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="V_Path"></param>
        /// <param name="V_Content"></param>
        public static void ShouWriteLog(string V_Path, string V_Content)
        {
            try
            {
                _mux.WaitOne();
                string realpath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\" + V_Path;
                if (!Directory.Exists(realpath))
                {
                    Directory.CreateDirectory(realpath);
                }
                realpath += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".nqh";
                StreamWriter writer = new StreamWriter(realpath, true);
                string str = DateTime.Now.ToString() + "\t" + V_Content;
                writer.WriteLine(str);
                writer.Flush();
                writer.Close();
                _mux.ReleaseMutex();
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("Shou"))
                {
                    EventLog.CreateEventSource("Shou", "Shou");
                }
                EventLog myLog = new EventLog();
                myLog.Source = "Shou";
                myLog.WriteEntry(ex.Message);
            }
        } 
        #endregion

        #region ShouWriteLog
        /// <summary>
        /// ShouWriteLog固定路径
        /// </summary>
        /// <param name="V_Path"></param>
        /// <param name="V_Content"></param>
        public static void ShouWriteLog(string V_Path, string V_FileName, string V_Content)
        {
            try
            {
                _mux.WaitOne();
                string realpath = V_Path;
                if (!Directory.Exists(realpath))
                {
                    Directory.CreateDirectory(realpath);
                }
                realpath += "\\" + V_FileName + ".txt";
                StreamWriter writer = new StreamWriter(realpath, true);
                string str = DateTime.Now.ToString() + "\t" + V_Content;
                writer.WriteLine(str);
                writer.Flush();
                writer.Close();
                _mux.ReleaseMutex();
            }
            catch (Exception ex)
            {
                if (!EventLog.SourceExists("Shou"))
                {
                    EventLog.CreateEventSource("Shou", "Shou");
                }
                EventLog myLog = new EventLog();
                myLog.Source = "Shou";
                myLog.WriteEntry(ex.Message);
            }
        }
        #endregion
    }
}
