using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using tpc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace winch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void d() { }
        h h1 = new h("mssql");
        private void Form1_Load(object sender, EventArgs e)
        {
            //Func<int> func = () => { return 0; };
            //AsyncCallback asynccallback = (ar) => { };
            //IAsyncResult asyncresult = func.BeginInvoke(asynccallback, null);
            //var result = func.EndInvoke(asyncresult);
            //Text = result.ToString();

            //new Msg(string.Join(br, Enumerable.Range(0, 100).Select(i => new string('*', new Random(Guid.NewGuid().GetHashCode()).Next(100)))));
            //new Msg(string.Join(br, Enumerable.Range(0, 100).Select(i => "xxx" + i.ToString("D2") + tab + "123qer,.")));

            //Clipboard.SetText("123");
            //Task.Run(() =>
            //{
            //    Thread.Sleep(5 * 1000);
            //    while (true)
            //    {
            //        //Thread.Sleep(2000);
            //        //Invoke(new Action(() => { SendKeys.Send("^v"); }));
            //        Invoke(new Action(() =>
            //        {
            //            var s = "ggg pe bg ";
            //            foreach (var c in s)
            //            {
            //                SendKeys.Send(c.ToString());
            //                Application.DoEvents();
            //                Thread.Sleep(300);
            //            }
            //        }));
            //        Application.DoEvents();
            //        Thread.Sleep(5 * 1000);
            //    }
            //});

            //txt1.Text = GetInsSql("cb_Contract", GetIsNotIdentityColumnNames("cb_Contract")) + BR + GetUptSql("cb_Contract");

        }

        private void 设置连接SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnOption co = new ConnOption();
            co.ShowDialog();
        }

        string tablenames = "";
        string columnnames = "";
        string br = "\r\n";//换行
        string BR = "\r\n\r\n";//换行并加一个空行
        string tab = "    ";
        string tab6(object strobj) { return new string('\t', (6 - strobj.ToString().CLength() / 8) > 0 ? (6 - strobj.ToString().CLength() / 8) : 6); }//6个tab
        string spline = new string('-', 200);//分割线

        private void 直接生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 生成到文本文件TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.AppendAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "1.txt"), BuildCodeLines(), Encoding.Default);
        }

        string GetTableNames()
        {
            tablenames = h1.ExecuteScalar(CommandType.Text, "SELECT Name + ',' FROM " + h1.DbName + "..SysObjects Where XType='U' ORDER BY Name FOR XML PATH('')")?.ToString().TrimEnd(',');
            return tablenames ?? "";
        }

        string GetColumnNames(string tableName)//所有列名
        {
            columnnames = h1.ExecuteScalar(CommandType.Text, "select name from syscolumns where id=object_id('" + tableName + "') FOR XML PATH('')").ToString();
            columnnames = columnnames.Replace("<name>", "");
            columnnames = columnnames.Replace("</name>", ",").TrimEnd(',');
            return columnnames;
        }

        string GetIsNotIdentityColumnNames(string tableName)//所有非自增列列名
        {
            columnnames = h1.ExecuteScalar(CommandType.Text, "select name from syscolumns where id=object_id('" + tableName + "') and COLUMNPROPERTY(OBJECT_ID('" + tableName + "'),name,'IsIdentity')!=1 FOR XML PATH('')").ToString();
            columnnames = columnnames.Replace("<name>", "");
            columnnames = columnnames.Replace("</name>", ",").TrimEnd(',');
            return columnnames;
        }

        string GetInsSql(string tableName, string columnNames)
        {
            var sql = string.Format("insert into {0} values ({1})", tableName, string.Join(",", columnnames.Split(',').Select(s => "@" + s)));
            sql = "var _sqlIns=\"" + sql + "\";";
            return sql + br + GetParams(tableName, columnnames);
        }

        string GetUptSql(string tableName)
        {
            var sql = "update " + tableName + " set ";
            sql += string.Join(",", columnnames.Split(',').Select(s => s + "=@" + s));
            var columnBy = GetUpdateColumnBy(tableName);
            sql += " where " + columnBy + "=@" + columnBy;
            sql = "var _sqlUpt=\"" + sql + "\";";
            return sql + br + GetParams(tableName, GetColumnNames(tableName));
        }

        string GetUpdateColumnBy(string tableName)
        {
            var columnName = h1.ExecuteScalar(CommandType.Text, "select name from syscolumns where id=object_id('" + tableName + "') and COLUMNPROPERTY(OBJECT_ID('" + tableName + "'),name,'IsIdentity')=1");
            if (columnName != null)
                return columnName.ToString();
            else
                return h1.ExecuteScalar(CommandType.Text, "select name from syscolumns where id=object_id('" + tableName + "')").ToString();
        }

        string GetParams(string tableName, string columnNames)
        {
            var sqlParams = "new SqlParameter[]" + br + "{" + br + "";
            sqlParams += string.Join("," + br + "", columnNames.Split(',').Select(s => tab + "new SqlParameter(\"@" + s + "\"," + tableName + "." + s + ")"));
            sqlParams += "" + br + "}";
            return "var _params = " + sqlParams + ";";
        }

        string BuildCodeLines()
        {
            if (string.IsNullOrEmpty(tablenames))
                GetTableNames();
            var result = "";
            foreach (var tablename in tablenames.Split(','))
            {
                result += GetInsSql(tablename, GetIsNotIdentityColumnNames(tablename)) + BR;
                result += GetUptSql(tablename) + BR;
            }
            return result;
        }

        private void 批量更改文件名MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatRename fbr = new BatRename();
            fbr.Show();
        }
        
        private void 表名和列描述DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var h = h1;

            #region 表名和描述
            var sqltable = "select tbs.name,ds.value from sysobjects tbs left join sys.extended_properties ds on tbs.id=ds.major_id and ds.minor_id=0 where tbs.xtype='U'";
            //dynamic dm = new { name = "", id = "" };
            //var mlist = h.DataAdapters<dynamic>(sqltable, CommandType.Text);
            var dttable = h.ExecuteDataSet(CommandType.Text, sqltable).Tables[0];
            var jsonstr = j.SerializeObject(dttable);
            var mlist = j.DeserializeJsonToList<dynamic>(jsonstr);
            #endregion

            #region 列名和描述
            var sqlcolumn = new StringBuilder();
            sqlcolumn.Append("SELECT  obj.name AS 表名,");
            sqlcolumn.Append("col.colorder AS 序号,");
            sqlcolumn.Append("col.name AS 列名,");
            sqlcolumn.Append("ISNULL(ep.[value], '') AS 列说明,");
            sqlcolumn.Append("t.name AS 数据类型,");
            sqlcolumn.Append("col.length AS 长度,");
            sqlcolumn.Append("ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS 小数位数,");
            sqlcolumn.Append("CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识,");
            sqlcolumn.Append("CASE WHEN EXISTS (SELECT 1 ");
            sqlcolumn.Append("FROM dbo.sysindexes si ");
            sqlcolumn.Append("INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id ");
            sqlcolumn.Append("AND si.indid = sik.indid ");
            sqlcolumn.Append("INNER JOIN dbo.syscolumns sc ON sc.id = sik.id ");
            sqlcolumn.Append("AND sc.colid = sik.colid ");
            sqlcolumn.Append("INNER JOIN dbo.sysobjects so ON so.name = si.name ");
            sqlcolumn.Append("AND so.xtype = 'PK' ");
            sqlcolumn.Append("WHERE sc.id = col.id ");
            sqlcolumn.Append("AND sc.colid = col.colid ) THEN '√' ");
            sqlcolumn.Append("ELSE '' END AS 主键,");
            sqlcolumn.Append("CASE WHEN col.isnullable = 1 THEN '√' ELSE '' END AS 允许空,");
            sqlcolumn.Append("ISNULL(comm.text, '') AS 默认值 ");
            sqlcolumn.Append("FROM dbo.syscolumns col ");
            sqlcolumn.Append("LEFT JOIN dbo.systypes t ON col.xtype = t.xusertype ");
            sqlcolumn.Append("INNER JOIN dbo.sysobjects obj ON col.id = obj.id AND obj.xtype = 'U' AND obj.status >= 0 ");
            sqlcolumn.Append("LEFT JOIN dbo.syscomments comm ON col.cdefault = comm.id ");
            sqlcolumn.Append("LEFT JOIN sys.extended_properties ep ON col.id = ep.major_id AND col.colid = ep.minor_id AND ep.name = 'MS_Description' ");
            sqlcolumn.Append("LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id AND epTwo.minor_id = 0 AND epTwo.name = 'MS_Description'");

            var dtcolumn = h.ExecuteDataSet(CommandType.Text, sqlcolumn.ToString()).Tables[0];
            #endregion

            #region 将表名描述列名描述追加到文本
            var lines = new StringBuilder();

            foreach (var m in mlist)
            {
                lines.AppendLine(spline);
                lines.AppendLine("" + m.name + tab6(m.name) + m.value);
                lines.AppendLine(br);
                #region m表中所有列描述
                //dtcolumn.WriteXml("1.xml");
                DataRow[] rows = dtcolumn.Select("表名='" + m.name + "'");
                lines.AppendLine("序号,列名,列说明,类型,长度,小数位,标识,主键,允许空,默认值".Replace(",", tab6("")));
                lines.AppendLine(string.Join(br, rows.Select(row => row["序号"].ToString() + tab6(row["序号"]) + row["列名"] + tab6(row["列名"]) + row["列说明"] + tab6(row["列说明"]) + row["数据类型"] + tab6(row["数据类型"]) + row["长度"] + tab6(row["长度"]) + row["小数位数"] + tab6(row["小数位数"]) + row["标识"] + tab6(row["标识"]) + row["主键"] + tab6(row["主键"]) + row["允许空"] + tab6(row["允许空"]) + row["默认值"])));
                #endregion
                lines.AppendLine(spline);
                lines.AppendLine(br);
            }
            #endregion

            #region 将文本写入文件
            File.AppendAllText(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "1.txt"), lines.ToString(), Encoding.Default);
            #endregion
        }

        private void 网页WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new webdoc().Show();
        }

        private void 生成ModelMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CrModel(h1).Show();
        }

        private void 生成ModelMySqlSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CrModel(h1, DbType.mysql).Show();
        }

        private void templateMySqlDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TakeFromTemplate(h1, DbType.mysql).Show();
        }

        private void templatePToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TakeFromTemplate(h1).Show();
        }

        void adodotnetdemo()
        {
            var myConnection = new SqlConnection("server=.;database=q;uid=sa;pwd=95938");
            var myCommand = new SqlCommand("select top 1 course from Sc", myConnection);
            SqlDataAdapter MyAdapter;
            var a_ds = new DataSet();
            DataTable dt;
            MyAdapter = new SqlDataAdapter();
            myCommand.CommandType = CommandType.Text;
            MyAdapter.SelectCommand = myCommand;

            MyAdapter.Fill(a_ds);
            dt = a_ds.Tables[0];

            Text = dt.Rows[0]["course"].ToString();
            myConnection.Close();
        }

    }

    //class Startup
    //{
    //    public void Configuration(IAppBuilder app)
    //    {
    //        app.MapSignalR();
    //    }
    //}

    //public class MyHub : Hub
    //{
    //    static List<string> connectionidlist = new List<string>();

    //    public void Send(string message)
    //    {
    //        Clients.All.LoadMessage(message);
    //    }

    //    public override Task OnConnected()
    //    {
    //        connectionidlist.Add(Context.ConnectionId);
    //        return base.OnConnected();
    //    }
    //    public override Task OnDisconnected(bool stopCalled)
    //    {
    //        connectionidlist.Remove(Context.ConnectionId);
    //        return base.OnDisconnected(stopCalled);
    //    }
    //}
}
