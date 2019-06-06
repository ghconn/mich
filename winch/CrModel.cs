using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace winch
{
    public partial class CrModel : DialogBase
    {
        tpc.h h;
        public CrModel()
        {
            InitializeComponent();
        }

        public CrModel(tpc.h h) : this()
        {
            this.h = h;

            TileLabel("");
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

        private void button2_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Model");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (Control ctrl in panel3.Controls)
            {
                var cb = ctrl as CheckBox;
                if (cb.Checked)
                {
                    SelectTo(cb.Text);
                }
            }

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            TileLabel(textBox1.Text);
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
                .Append(BR)
                .Append("namespace Model")
                .Append(br)
                .Append("{")
                .Append(br)
                .Append(tab).Append("/// <summary>")
                .Append(br)
                .Append(tab).Append($"/// {tabledesc}")
                .Append(br)
                .Append(tab).Append("/// <summary>")
                .Append(br)
                .Append(tab).Append($"public class {tname} : IModel")
                .Append(br)
                .Append(tab).Append("{")
                .Append(br);
            for(var i = 0; i < dtcolumn.Rows.Count; i++)
            {
                var row = dtcolumn.Rows[i];
                sb.Append(tab + tab).Append("/// <summary>")
                .Append(br)
                .Append(tab + tab).Append($"/// {row["列说明"]}")
                .Append(br)
                .Append(tab + tab).Append("/// <summary>")
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
            
            tpc.f.TextCreateToFile("Model\\" + tname + ".cs", sb.ToString());
        }
    }
}
