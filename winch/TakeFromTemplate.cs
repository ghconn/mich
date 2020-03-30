using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace winch
{
    public partial class TakeFromTemplate : DialogBase
    {
        tpc.h h;
        DbType DbType;
        bool _lowerFirstChar = false;

        string connStr = "server=172.18.132.136;port=3306;database=attendmn;uid=root;pwd=pactera;charset=utf8";
        string folder = "FromTemplate";
        string mydbname = "";

        public TakeFromTemplate()
        {
            InitializeComponent();
            connStr = System.Configuration.ConfigurationManager.AppSettings["mysql"];
            _lowerFirstChar = System.Configuration.ConfigurationManager.AppSettings["lowerFirstChar"] == "1";
        }

        public TakeFromTemplate(tpc.h h, DbType dbType = DbType.sqlserver) : this()
        {
            this.h = h;
            this.DbType = dbType;

            switch (this.DbType)
            {
                case DbType.sqlserver:
                        TileLabel("");
                        break;
                case DbType.mysql:
                    connStr = System.Configuration.ConfigurationManager.AppSettings["mysql"];
                    TileLabelMySql("");
                    break;
                default:
                    break;
            }
        }

        private void TileLabel(string key)
        {
            var tablenames = h.ExecuteScalar(CommandType.Text, $"SELECT top 100 [Name] + ',' FROM {h.DbName}..SysObjects Where XType='U' and [name] like '%{key}%' ORDER BY Name FOR XML PATH('')")?.ToString().TrimEnd(',');
            if (tablenames == null)
            {
                return;
            }
            var a_tablename = tablenames.Split(',');
            var count = a_tablename.Length;
            for (var i = 0; i < count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Location = new Point(i % 3 * 200 + 30, i / 3 * 30 + 10);
                cb.Text = a_tablename[i];
                cb.Width = 200;
                panel3.Controls.Add(cb);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "<挂起>")]
        private void TileLabelMySql(string key)
        {
            DataSet set = new DataSet();

            mydbname = "ghsystem";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string sql = "select database()";
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                    mySqlDataAdapter.Fill(set);
                }
                conn.Close();
            }

            if (set.Tables.Count > 0)
            {
                mydbname = set.Tables[0].DefaultView[0][0]?.ToString();
            }

            set = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                key = key.Replace("_", "/_");
                string sql = $"select table_name from information_schema.tables where table_schema='{mydbname}' and (table_type='BASE TABLE' or table_type='base table') and table_name like '%{key}%' escape '/'";
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                    mySqlDataAdapter.Fill(set);
                }
                conn.Close();
            }

            var dts = set.Tables[0];
            var dv = dts.DefaultView;
            
            var count = dv.Count;
            for (var i = 0; i < count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Location = new Point(i % 3 * 200 + 30, i / 3 * 30 + 10);
                cb.Text = dv[i][0].ToString();
                cb.Width = 200;
                panel3.Controls.Add(cb);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            foreach (Control ctrl in panel3.Controls)
            {
                var cb = ctrl as CheckBox;
                if (cb.Checked)
                {
                    switch (this.DbType)
                    {
                        case DbType.sqlserver:
                            SelectTo(cb.Text);
                            break;
                        case DbType.mysql:
                            SelectToMySql(cb.Text);
                            break;
                        default:
                            break;
                    }                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            switch (this.DbType)
            {
                case DbType.sqlserver:
                    TileLabel(textBox1.Text);
                    break;
                case DbType.mysql:
                    TileLabelMySql(textBox1.Text);
                    break;
                default:
                    break;
            }
        }

        string br = "\r\n";//换行
        private void SelectTo(string tname)
        {
            var sb = new StringBuilder();
            sb.Append("SELECT * FROM (");
            sb.Append("SELECT  obj.name AS 表名,");
            sb.Append("col.colorder AS 序号,");
            sb.Append("col.name AS 列名,");
            sb.Append("ISNULL(ep.[value], '') AS 列说明,");
            sb.Append("t.name AS 数据类型,");
            sb.Append("col.length AS 长度,");
            sb.Append("ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS 小数位数,");
            sb.Append("CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识,");
            sb.Append("CASE WHEN EXISTS (SELECT 1 ");
            sb.Append("FROM dbo.sysindexes si ");
            sb.Append("INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id ");
            sb.Append("AND si.indid = sik.indid ");
            sb.Append("INNER JOIN dbo.syscolumns sc ON sc.id = sik.id ");
            sb.Append("AND sc.colid = sik.colid ");
            sb.Append("INNER JOIN dbo.sysobjects so ON so.name = si.name ");
            sb.Append("AND so.xtype = 'PK' ");
            sb.Append("WHERE sc.id = col.id ");
            sb.Append("AND sc.colid = col.colid ) THEN '√' ");
            sb.Append("ELSE '' END AS 主键,");
            sb.Append("CASE WHEN col.isnullable = 1 THEN '√' ELSE '' END AS 允许空,");
            sb.Append("ISNULL(comm.text, '') AS 默认值 ");
            sb.Append("FROM dbo.syscolumns col ");
            sb.Append("LEFT JOIN dbo.systypes t ON col.xtype = t.xusertype ");
            sb.Append("INNER JOIN dbo.sysobjects obj ON col.id = obj.id AND obj.xtype = 'U' AND obj.status >= 0 ");
            sb.Append("LEFT JOIN dbo.syscomments comm ON col.cdefault = comm.id ");
            sb.Append("LEFT JOIN sys.extended_properties ep ON col.id = ep.major_id AND col.colid = ep.minor_id AND ep.name = 'MS_Description' ");
            sb.Append("LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id AND epTwo.minor_id = 0 AND epTwo.name = 'MS_Description'");
            sb.Append(") AS t WHERE 表名 = @tname order by 序号");

            var dtcolumn = h.ExecuteDataSet(CommandType.Text, sb.ToString(), new SqlParameter("@tname", tname)).Tables[0];
            if (dtcolumn.Rows.Count == 0)
            {
                return;
            }

            var dv = dtcolumn.DefaultView;            

            sb = new StringBuilder();

            for (var i = 0; i < dv.Count; ++i)
            {
                var row = dv[i];
                sb.Append(textBox2.Text.Replace("[表名]", tname.ToLowerFirstChar(_lowerFirstChar))
                    .Replace("[列名]", row["列名"].ToString().ToLowerFirstChar(_lowerFirstChar))
                    .Replace("[列说明]", row["列说明"].ToString())
                    .Replace("[序号]", row["序号"].ToString())
                    .Replace("[默认值]", row["默认值"].ToString())
                    .Replace("[数据类型]", row["数据类型"].ToString())
                    .Replace("[长度]", row["长度"].ToString()));
                //[表名][列名][列说明][序号][默认值][数据类型][长度]
                if (checkBox1.Checked)
                    sb.Append(br);
            }

            tpc.f.TextCreateToFile(folder + "\\" + tname.ToLowerFirstChar(_lowerFirstChar) + ".txt", sb.ToString());
        }
        
        private void SelectToMySql(string text)
        {            
            var settbdesc = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string sqltbdesc = $"select table_comment from information_schema.tables where table_schema='{mydbname}' and (table_type='BASE TABLE' or table_type='base table') and table_name='{text}'";
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sqltbdesc, conn))
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                    mySqlDataAdapter.Fill(settbdesc);
                }
                conn.Close();
            }
            var dttbdesc = settbdesc.Tables[0];
            var dvtbdesc = dttbdesc.DefaultView;

            var sql = $@"
            SELECT TABLE_NAME AS '表名',
            COLUMN_NAME AS '列名',
            ORDINAL_POSITION AS '序号',
            COLUMN_DEFAULT AS '默认值',
            case IS_NULLABLE when 'YES' then '√' else '' end AS '空值',
            DATA_TYPE AS '数据类型',
            CHARACTER_MAXIMUM_LENGTH AS '长度',
            NUMERIC_PRECISION AS '数值最大位数',
            NUMERIC_SCALE AS '小数',
            case COLUMN_KEY when 'PRI' then '√' else '' end as '主键',
            case EXTRA when 'AUTO_INCREMENT' then '√' else '' end AS '标识',
            COLUMN_COMMENT AS '列说明'
        FROM
            information_schema.`COLUMNS`
        WHERE
            TABLE_SCHEMA = '{mydbname}' and TABLE_NAME='{text}'
        ORDER BY
            TABLE_NAME,
            ORDINAL_POSITION";

            var set = new DataSet();
            using (MySqlConnection conn2 = new MySqlConnection(connStr))
            {
                conn2.Open();
                using (MySqlCommand cmd2 = new MySqlCommand(sql, conn2))
                {
                    MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(cmd2);
                    mySqlDataAdapter2.Fill(set);
                }
                conn2.Close();
            }

            var dtcols = set.Tables[0];
            var dv = dtcols.DefaultView;

            if (dv.Count == 0)
            {
                return;
            }

            var sb = new StringBuilder();

            for(var i = 0; i < dv.Count; ++i)
            {
                var row = dv[i];
                sb.Append(textBox2.Text.Replace("[表名]", text.ToLowerFirstChar(_lowerFirstChar))
                    .Replace("[列名]", row["列名"].ToString().ToLowerFirstChar(_lowerFirstChar))
                    .Replace("[列说明]", row["列说明"].ToString())
                    .Replace("[序号]", row["序号"].ToString())
                    .Replace("[默认值]", row["默认值"].ToString())
                    .Replace("[数据类型]", row["数据类型"].ToString())
                    .Replace("[长度]", row["长度"].ToString()));
                //[表名][列名][列说明][序号][默认值][数据类型][长度]
                if (checkBox1.Checked)
                    sb.Append(br);
            }

            tpc.f.TextCreateToFile(folder + "\\" + text + ".txt", sb.ToString());
        }        
    }

    public static class Extend
    {
        public static string ToLowerFirstChar(this string source, bool flag)
        {
            if (!flag)
            {
                return source;
            }
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException();
            }
            return source.Substring(0, 1).ToLower() + source.Substring(1);
        }
    }
}
