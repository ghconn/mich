using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 引用System.Data.SQLite.dll并生成，生成后将SQLite.Interop.dll（runtimes\win-x86\native\netstandard2.0版本）放到生成的目标目录，即可正常运行
 * 运行完此Demo后，生成的test.db可以用Navicat Premium 12连接，不需要用户名密码，指定文件路径即可
 */

namespace CTest
{
    public class SqliteDemo
    {
        public static void _main()
        {
            //可以不存在test.db文件，运行以下代码将自动创建test.db文件，只要目录存在即可
            string conn_str = $@"Data Source =C:\Users\kan_y\Desktop\新建文件夹\test.db";

            SQLiteConnection conn = new SQLiteConnection(conn_str);
            //conn.SetPassword(":)z");
            //打开数据库，若文件不存在会自动创建 
            conn.Open();
            //建表语句 
            string sql = "CREATE TABLE IF NOT EXISTS student(id INTEGER PRIMARY KEY AUTOINCREMENT, name varchar(20), sex varchar(2));";
            //创建sql执行指令对象
            SQLiteCommand com = new SQLiteCommand(sql, conn);
            //如果不带参数时, 使用一下语句赋值
            //com.CommandText = sql;
            //com.Connection = con;
            //执行sql指令创建建数据表,如果表不存在，创建数据表 
            com.ExecuteNonQuery();

            //为了示例重复演示
            com.CommandText = "delete from student";
            com.ExecuteNonQuery();

            //给表添加数据
            //1. 使用sql语句逐行添加
            com.CommandText = "INSERT INTO student VALUES(null, '小红', '男')";
            com.ExecuteNonQuery();
            com.CommandText = "INSERT INTO student VALUES(null, '小李', '女')";
            com.ExecuteNonQuery();
            //2. 使用事务添加
            //实例化一个事务对象
            SQLiteTransaction tran = conn.BeginTransaction();
            //把事务对象赋值给com的transaction属性
            com.Transaction = tran;
            //设置带参数sql语句
            com.CommandText = "INSERT INTO student VALUES(null, @name, @sex)";
            for (int i = 0; i < 10; i++)
            {
                //添加参数
                com.Parameters.AddRange(new[] {//添加参数
                    new SQLiteParameter("@name", "test" + i),
                    new SQLiteParameter("@sex", i % 2 == 0 ? "男" : "女")
                });
                //执行添加
                com.ExecuteNonQuery();
            }
            //提交事务
            tran.Commit();

            //读取数据
            sql = "select * from student";
            //实例化sql指令对象
            com = new SQLiteCommand(sql, conn);
            //存放读取数值
            SQLiteDataReader reader = com.ExecuteReader();
            //读取每一行数据
            while (reader.Read())
            {
                //读取并赋值给控件
                Console.WriteLine(reader.GetInt32(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
            }
            //关闭数据库
            conn.Close();
        }
    }
}
