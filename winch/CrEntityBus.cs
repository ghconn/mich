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
    public partial class CrEntityBus : DialogBase
    {
        tpc.h h;
        DbType DbType;

        string connStr = "server=172.18.132.136;port=3306;database=attendmn;uid=root;pwd=pactera;charset=utf8";
        string folder = "Model";
        string mydbname = "";

        public CrEntityBus()
        {
            InitializeComponent();
            connStr = System.Configuration.ConfigurationManager.AppSettings["mysql"];
        }

        public CrEntityBus(tpc.h h, DbType dbType = DbType.sqlserver) : this()
        {
            this.h = h;
            this.DbType = dbType;

            switch (this.DbType)
            {
                case DbType.sqlserver:
                        TileLabel("");
                        break;
                case DbType.mysql:
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

        private void TileLabelMySql(string key)
        {
            DataSet set = new DataSet();

            mydbname = "ghsystem";
            using(MySqlConnection conn = new MySqlConnection(connStr))
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
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
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
                            //model
                            SelectToMySql(cb.Text);
                            //controller
                            CreateController(cb.Text);
                            //vue
                            CreateVue(cb.Text);
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
        string BR = "\r\n\r\n";//换行并加一个空行
        string tab = "    ";
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

            var sql = $"SELECT ds.value 描述 FROM sysobjects tbs left join sys.extended_properties ds ON tbs.id=ds.major_id and ds.minor_id=0 WHERE tbs.xtype='U' and tbs.name='{tname}'";
            var tabledesc = h.ExecuteScalar(CommandType.Text, sql);

            sb.Clear();
            sb.Append("using System;")
                .Append(br)
                .Append("using System.ComponentModel;")
                .Append(BR)
                .Append("namespace Model")
                .Append(br)
                .Append("{")
                .Append(br)
                .Append(tab).Append("/// <summary>")
                .Append(br)
                .Append(tab).Append($"/// {tabledesc}")
                .Append(br)
                .Append(tab).Append("/// </summary>")
                .Append(br)
                .Append(tab).Append($"public class {tname} : IModel")
                .Append(br)
                .Append(tab).Append("{")
                .Append(br);
            for (var i = 0; i < dtcolumn.Rows.Count; i++)
            {
                var row = dtcolumn.Rows[i];
                sb.Append(tab + tab).Append("/// <summary>")
                .Append(br)
                .Append(tab + tab).Append($"/// {row["列说明"]}")
                .Append(br)
                .Append(tab + tab).Append("/// </summary>")
                .Append(br)
                .Append(tab + tab).Append($"[Description(\"{row["列说明"]}\")]")
                .Append(br)
                .Append(tab + tab).Append("public ");

                var fieldtype = "";
                switch (row["数据类型"].ToString())
                {
                    case "int":
                    case "tinyint":
                    case "smallint":
                        fieldtype = "int";
                        break;
                    case "bigint":
                        fieldtype = "Int64";
                        break;
                    case "decimal":
                    case "float":
                    case "real":
                    case "money":
                    case "smallmoney":
                        fieldtype = "decimal";
                        break;
                    case "datetime":
                        fieldtype = "DateTime";
                        break;
                    case "bit":
                        fieldtype = "bool";
                        break;
                    default:
                        fieldtype = "string";
                        break;
                }
                if (fieldtype != "string" && row["允许空"].ToString() == "√")
                {
                    fieldtype = fieldtype + "?";
                }
                sb.Append(fieldtype).Append($" {row["列名"]} {{ get; set; }}")
                    .Append(br);
            }
            sb.Append(tab).Append("}")
                .Append(br)
                .Append("}");

            tpc.f.TextCreateToFile(folder + "\\" + tname + ".cs", sb.ToString());
        }
        
        private void SelectToMySql(string text)
        {            
            var settbdesc = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string sqltbdesc = $"select table_comment from information_schema.tables where table_schema='{mydbname}' and " +
                    $"(table_type='base table' or table_type='BASE TABLE') and table_name='{text}'";
                conn.Open();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                using (MySqlCommand cmd = new MySqlCommand(sqltbdesc, conn))
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
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
            CHARACTER_MAXIMUM_LENGTH AS '长度MY',
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
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                using (MySqlCommand cmd2 = new MySqlCommand(sql, conn2))
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
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
            sb.Append("using System;")
                .Append(br)
                .Append("using System.ComponentModel;")
                .Append(BR)
                .Append("namespace Model")
                .Append(br)
                .Append("{")
                .Append(br)
                .Append(tab).Append("/// <summary>")
                .Append(br)
                .Append(tab).Append($"/// {dvtbdesc[0][0]}")
                .Append(br)
                .Append(tab).Append("/// </summary>")
                .Append(br)
                .Append(tab).Append($"public class {text} : IModel")
                .Append(br)
                .Append(tab).Append("{")
                .Append(br);
            for (var i = 0; i < dv.Count; i++)
            {
                var row = dv[i];
                sb.Append(tab + tab).Append("/// <summary>")
                .Append(br)
                .Append(tab + tab).Append($"/// {row["列说明"]}")
                .Append(br)
                .Append(tab + tab).Append("/// </summary>")
                .Append(br)
                .Append(tab + tab).Append($"[Description(\"{row["列说明"]}\")]")
                .Append(br)
                .Append(tab + tab).Append("public ");

                var fieldtype = "";
                switch (row["数据类型"].ToString())
                {
                    case "int":
                        fieldtype = "int";
                        break;
                    case "smallint":
                        fieldtype = "short";
                        break;
                    case "tinyint":
                        fieldtype = "byte";
                        break;
                    case "bigint":
                        fieldtype = "long";
                        break;
                    case "decimal":
                    case "float":
                    case "real":
                    case "money":
                    case "smallmoney":
                        fieldtype = "decimal";
                        break;
                    case "double":
                        fieldtype = "double";
                        break;
                    case "datetime":
                        fieldtype = "DateTime";
                        break;
                    case "bit":
                        fieldtype = "bool";
                        break;
                    default:
                        fieldtype = "string";
                        break;
                }
                if (fieldtype != "string" && row["空值"].ToString() == "√")
                {
                    fieldtype = fieldtype + "?";
                }
                sb.Append(fieldtype).Append($" {row["列名"]} {{ get; set; }}")
                    .Append(br);
            }
            sb.Append(tab).Append("}")
                .Append(br)
                .Append("}");
            
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folder + "\\" + text);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            tpc.f.TextCreateToFile(folder + "\\" + text + "\\" + text + ".cs", sb.ToString());
        }

        #region controller
        private void CreateVue(string text)
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(CrEntityBus));

            var templateText = resources.GetString("ControllerTemplate");

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folder + "\\" + text);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            tpc.f.TextCreateToFile(folder + "\\" + text + "\\" + text + "Controller.cs", templateText.Replace("[entityname]", text));
        }
        #endregion

        #region vue
        private void CreateController(string text)
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(CrEntityBus));

            var templateText = resources.GetString("VueTemplate");

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folder + "\\" + text);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            #region 表描述
            var settbdesc = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string sqltbdesc = $"select table_comment from information_schema.tables where table_schema='{mydbname}' and " +
                    $"(table_type='base table' or table_type='BASE TABLE') and table_name='{text}'";
                conn.Open();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                using (MySqlCommand cmd = new MySqlCommand(sqltbdesc, conn))
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                    mySqlDataAdapter.Fill(settbdesc);
                }
                conn.Close();
            }
            var dttbdesc = settbdesc.Tables[0];
            var dvtbdesc = dttbdesc.DefaultView; 
            #endregion

            #region cols-template
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
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                using (MySqlCommand cmd2 = new MySqlCommand(sql, conn2))
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
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
            #endregion
            
            var sb_cols = new StringBuilder();
            var sb_formitems = new StringBuilder();

            var space6 = "      ";
            var space8 = "        ";
            var space10 = "          ";
            for (var i = 0; i < dv.Count; ++i)
            {
                var row = dv[i];

                if (row["列名"].ToString().ToLower() != "id" 
                    && row["列名"].ToString().ToLower() != "isdel"
                     && row["列名"].ToString().ToLower() != "createtime"
                      && row["列名"].ToString().ToLower() != "updatetime"
                       && row["列名"].ToString().ToLower() != "createuserid"
                        && row["列名"].ToString().ToLower() != "updateuserid"
                         && row["列名"].ToString().ToLower() != "createusername"
                          && row["列名"].ToString().ToLower() != "updateusername")
                {
                    sb_cols.Append(br).Append(space6).Append($"<el-table-column label=\"{row["列说明"]}\" prop=\"{row["列名"]}\"></el-table-column>");
                    sb_formitems.Append(br).Append(space8).Append($"<el-form-item label=\"{row["列说明"]}\" prop=\"{row["列名"]}\">")
                        .Append(br)
                        .Append(space10).Append($"<el-input v-model=\"detaildialog.{row["列名"]}\" placeholder=\"请输入\" clearable></el-input>")
                        .Append(br)
                        .Append(space8).Append($"</el-form-item>");
                }
            }

            var re = templateText.Replace("[entityname]", text).Replace("[entitydesc]", dvtbdesc[0][0].ToString())
                .Replace("{{0}}", sb_cols.ToString())
                .Replace("{{1}}", sb_formitems.ToString());

            tpc.f.TextCreateToFile(folder + "\\" + text + "\\" + text + ".vue", re);
        } 
        #endregion

    }
}
