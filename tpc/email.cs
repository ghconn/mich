using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace tpc
{
    #region
    public class Email
    {
        private MailMessage mMailMessage;   //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
        private SmtpClient mSmtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）
        private int mSenderPort;   //发送邮件所用的端口号（htmp协议默认为25）
        private string mSenderServerHost;    //发件箱的邮件服务器地址（IP形式或字符串形式均可）
        private string mSenderPassword;    //发件箱的密码
        private string mSenderUsername;   //发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        private bool mEnableSsl;    //是否对邮件内容进行socket层加密传输
        private bool mEnablePwdAuthentication;  //是否对发件人邮箱进行密码验证
        private Attachment attachment;

        ///<summary>
        /// 构造函数
        ///</summary>
        ///<param name="server">发件箱的邮件服务器地址</param>
        ///<param name="toMail">收件人地址（可以是多个收件人，程序中是以","进行分隔）</param>
        /// <param name="bcc">密送地址（可以是多个，程序中是以","进行分隔）</param>
        ///<param name="fromMail">发件人地址</param>
        ///<param name="subject">邮件标题</param>
        ///<param name="emailBody">邮件内容（可以以html格式进行设计）</param>
        ///<param name="username">发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）</param>
        ///<param name="password">发件人邮箱密码</param>
        ///<param name="port">发送邮件所用的端口号（htmp协议默认为25）</param>
        ///<param name="sslEnable">true表示对邮件内容进行socket层加密传输，false表示不加密</param>
        ///<param name="pwdCheckEnable">true表示对发件人邮箱进行密码验证，false表示不对发件人邮箱进行密码验证</param>
        public Email(string server, string toMail, string bcc, string fromMail, string displayName, string subject, string emailBody, string username, string password, string port, bool sslEnable, bool pwdCheckEnable)
        {
            try
            {
                mMailMessage = new MailMessage();
                if (!string.IsNullOrEmpty(bcc))
                    mMailMessage.Bcc.Add(bcc);
                mMailMessage.To.Add(toMail);
                mMailMessage.From = new MailAddress(fromMail, displayName);
                mMailMessage.Subject = subject;
                mMailMessage.Body = emailBody;
                mMailMessage.IsBodyHtml = true;
                mMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mMailMessage.Priority = MailPriority.Normal;
                this.mSenderServerHost = server;
                this.mSenderUsername = username;
                this.mSenderPassword = password;
                this.mSenderPort = Convert.ToInt32(port);
                this.mEnableSsl = sslEnable;
                this.mEnablePwdAuthentication = pwdCheckEnable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    attachment = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    mMailMessage.Attachments.Add(attachment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ///<summary>
        /// 邮件的发送
        ///</summary>
        public void Send()
        {
            try
            {
                if (mMailMessage != null)
                {
                    mSmtpClient = new SmtpClient();
                    //mSmtpClient.Host = "smtp." + mMailMessage.From.Host;
                    mSmtpClient.Host = this.mSenderServerHost;
                    mSmtpClient.Port = this.mSenderPort;
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.EnableSsl = this.mEnableSsl;
                    if (this.mEnablePwdAuthentication)
                    {
                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //NTLM: Secure Password Authentication in Microsoft Outlook Express
                        mSmtpClient.Credentials = nc.GetCredential(mSmtpClient.Host, mSmtpClient.Port, "NTLM");//""
                    }
                    else
                    {
                        mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                    }
                    mSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //用于外部释放附件文件资源
        public Attachment Attachment
        {
            get { return this.attachment; }
        }
    } 
    #endregion

    #region
    public class Email2
    {
        string smtpHost = string.Empty;
        string Sendmailaddress = string.Empty;
        string Sendpassword = string.Empty;
        string SendDisplayname = string.Empty;
        string Recevivemailaddress = string.Empty;
        string ReceviveDisplayname = string.Empty;
        string Bccmailaddress = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host">主机号</param>
        /// <param name="sendmailaddress">发送人邮箱地址</param>
        /// <param name="sendpassword">发送邮箱密码</param>
        /// <param name="sendDisplayname">发送人显示名称</param>
        /// <param name="recevivemailaddress">接收人邮箱地址</param>
        /// <param name="receviveDisplayname">接收人显示名称</param>
        public Email2(string host, string sendmailaddress, string sendpassword, string sendDisplayname, string recevivemailaddress, string receviveDisplayname, string bccmailaddress)
        {
            smtpHost = host;
            Sendmailaddress = sendmailaddress;
            Sendpassword = sendpassword;
            SendDisplayname = sendDisplayname;
            Recevivemailaddress = recevivemailaddress;
            ReceviveDisplayname = receviveDisplayname;
            Bccmailaddress = bccmailaddress;
        }

        /// <summary>
        /// 发送邮件功能
        /// </summary>
        /// <param name="mailsubject">邮件标题</param>
        /// <param name="mailbody">邮件主要内容</param>
        /// <param name="isadddocument">是否添加附件</param>
        /// <param name="documentpath">添加附件的文件路径列表</param>
        /// <returns></returns>
        public bool Sendmail(string mailsubject, string mailbody, bool isadddocument, IEnumerable<string> documentpath)
        {
            bool sendstatus = false;
            try
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(smtpHost);  //确定smtp服务器地址。实例化一个Smtp客户端
                MailAddress from = new MailAddress(Sendmailaddress, SendDisplayname, Encoding.UTF8);//构造一个发件人地址对象
                //MailAddress to = new MailAddress(Recevivemailaddress, ReceviveDisplayname, Encoding.UTF8);//构造一个收件人地址对象                
                MailMessage message = new MailMessage();//构造一个Email的Message对象

                message.From = from;
                message.To.Add(Recevivemailaddress);
                if (!string.IsNullOrEmpty(Bccmailaddress))
                    message.Bcc.Add(Bccmailaddress);
                message.Subject = mailsubject;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;

                //设置邮件的信息
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = false;

                //如果服务器支持安全连接，则将安全连接设为true。
                //如果是gmail则一定要将其设为true
                if (smtpHost == "smpt.gmail.com")
                    client.EnableSsl = true;
                else
                    client.EnableSsl = false;

                if (isadddocument == true)
                {
                    AddDocument(message, documentpath);
                }
                client.UseDefaultCredentials = false;
                //用户登陆信息
                System.Net.NetworkCredential myCredentials = new System.Net.NetworkCredential(Sendmailaddress, Sendpassword);
                client.Credentials = myCredentials;
                //发送邮件
                client.Send(message);
                sendstatus = true;
            }
            catch { throw; }
            return sendstatus;
        }

        /// <summary>
        /// 添加附件功能
        /// </summary>
        /// <param name="message">Mailmessage对象</param>
        /// <param name="Documentpath">附件路径列表</param>
        private void AddDocument(MailMessage message, IEnumerable<string> documentpath)
        {
            foreach (var filepath in documentpath)
            {
                try
                {
                    if (File.Exists(filepath)) //判断文件是否存在
                    {
                        Attachment attach = new Attachment(filepath);    //构造一个附件对象
                        ContentDisposition disposition = attach.ContentDisposition;   //得到文件的信息
                        disposition.CreationDate = System.IO.File.GetCreationTime(filepath);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(filepath);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(filepath);
                        message.Attachments.Add(attach);   //向邮件添加附件
                    }
                }
                catch { }
            }
        }
    } 
    #endregion
}
