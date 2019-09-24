using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Threading.Tasks;
using mdl;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace tpc
{
    public class h
    {
        #region 定义成员
        Microsoft.Practices.EnterpriseLibrary.Data.Database mDatabase = null;//定义微软企业库
        DbConnection Connection = null;//定义连接
        DbTransaction Transaction = null;//定义事务
        DbCommand mDbCommand = null;//定义Command 
        #endregion

        #region 构造函数
        public h(string configuration_connectionstring_node_name)
        {
            mDatabase = DatabaseFactory.CreateDatabase(configuration_connectionstring_node_name);
            Connection = mDatabase.CreateConnection();
        }
        #endregion

        #region 数据库名
        public string DbName
        {
            get { return Connection.Database; }
        }
        #endregion

        #region 创建参数集
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="DbType"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(String ParameterName, DbType DbType, ParameterDirection Direction)
        {
            return CreateParameter(ParameterName, DbType, null, -1, Direction);
        }

        /// <summary>
        /// 创建参数集
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(String ParameterName, DbType DbType, Int32 Size, ParameterDirection Direction)
        {
            return CreateParameter(ParameterName, DbType, null, Size, Direction);
        }

        /// <summary>
        /// 创建参数集
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Value">参数值</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(String ParameterName, DbType DbType, Object Value, Int32 Size, ParameterDirection Direction)
        {

            IDbDataParameter Param = System.Data.Common.DbProviderFactories.GetFactory("SqlClient").CreateParameter();
            //SqlParameter Param=new SqlParameter();
            try
            {
                Param.ParameterName = ParameterName;
                Param.Value = Value;
                if (Size != -1)
                    Param.Size = Size;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            Param.DbType = DbType;
            Param.Direction = Direction;
            return Param;
        }

        /// <summary>
        /// 创建参数集
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="Value">参数值</param>
        /// <param name="Direction">参数方向</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(String ParameterName, Object Value, ParameterDirection Direction)
        {
            return CreateParameter(ParameterName, DbType.String, Value, -1, Direction);
        }

        /// <summary>
        /// 创建参数集
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Value">参数值</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(String ParameterName, DbType DbType, Object Value)
        {
            return CreateParameter(ParameterName, DbType, Value, -1, ParameterDirection.Input);
        }
        /// <summary>
        /// 创建参数集
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="Value">参数值</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(String ParameterName, Object Value)
        {
            return CreateParameter(ParameterName, DbType.String, Value, -1, ParameterDirection.Input);
        }
        #endregion

        #region 连接
        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (Connection.State != ConnectionState.Closed && Transaction == null)
                Connection.Close();
        }
        #endregion

        #region 事务
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            Open();
            Transaction = Connection.BeginTransaction();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            Transaction.Commit();
            Transaction = null;
            Close();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBackTransaction()
        {
            Transaction.Rollback();
            Transaction = null;
            Close();
        }
        #endregion

        #region 执行sql语句数据添加、修改、删除
        /// <summary>
        /// ExecuteNonQuery 数据添加、修改、删除
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params IDbDataParameter[] commandParameters)
        {
            try
            {
                this.Open();
                if (cmdType == CommandType.StoredProcedure)
                    mDbCommand = mDatabase.GetStoredProcCommand(cmdText);
                else
                    mDbCommand = mDatabase.GetSqlStringCommand(cmdText);
                mDbCommand.Connection = Connection;
                if (Transaction != null)
                    mDbCommand.Transaction = Transaction;
                if ((commandParameters != null) && (commandParameters.Length > 0))
                    foreach (IDbDataParameter mParameter in commandParameters)
                        mDatabase.AddParameter(mDbCommand, mParameter.ParameterName, mParameter.DbType, mParameter.Direction, mParameter.SourceColumn, mParameter.SourceVersion, mParameter.Value);
                return mDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Transaction == null)
                    this.Close();
            }
        }
        #endregion

        #region 执行sql语句返回结果集DataSet
        /// <summary>
        /// ExecuteDataSet  返回结果集DataSet
        /// </summary>
        /// <param name="commandType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="commandText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的DataSet；失败则返回null</returns>
        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params IDbDataParameter[] commandParameters)
        {
            try
            {
                DataSet ds = new DataSet();
                Open();
                if (cmdType == CommandType.StoredProcedure)
                    mDbCommand = mDatabase.GetStoredProcCommand(cmdText);
                else
                    mDbCommand = mDatabase.GetSqlStringCommand(cmdText);

                if ((commandParameters != null) && (commandParameters.Length > 0))
                    foreach (IDbDataParameter mParameter in commandParameters)
                        mDatabase.AddParameter(mDbCommand, mParameter.ParameterName, mParameter.DbType, mParameter.Direction, mParameter.SourceColumn, mParameter.SourceVersion, mParameter.Value);
                ds = mDatabase.ExecuteDataSet(mDbCommand);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region  执行sql语句返回实体对像
        /// <summary>
        /// 查询返回对象实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public T DataAdapter<T>(string cmdText, CommandType cmdType, params IDbDataParameter[] commandParameters)
        {

            try
            {
                List<T> lst = DataAdapters<T>(cmdText, cmdType, commandParameters);
                return lst.Count > 0 ? lst[0] : default(T);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }
        /// <summary>
        /// 查询返回对象实体List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">SQL语句、存储过程名称</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public List<T> DataAdapters<T>(string cmdText, CommandType cmdType, params IDbDataParameter[] commandParameters)
        {

            try
            {
                DataSet ds = new DataSet();
                this.Open();
                if (cmdType == CommandType.StoredProcedure)
                    mDbCommand = mDatabase.GetStoredProcCommand(cmdText);
                else
                    mDbCommand = mDatabase.GetSqlStringCommand(cmdText);
                if ((commandParameters != null) && (commandParameters.Length > 0))
                    foreach (IDbDataParameter mParameter in commandParameters)
                        mDatabase.AddParameter(mDbCommand, mParameter.ParameterName, mParameter.DbType, mParameter.Direction, mParameter.SourceColumn, mParameter.SourceVersion, mParameter.Value);
                ds = mDatabase.ExecuteDataSet(mDbCommand);
                return DataTableConvertList<T>(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        ///  根据传入的表名、视图或者SQL语句返回分页数据,大数据量时使用,使用二分法分页，效率比较高,返回泛型结果集
        /// </summary>
        /// <typeparam name="T">返回实体</typeparam>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="tblName">表名、视图名或者SQL语句</param>
        /// <param name="fldName">字段名，必须填</param>
        /// <param name="where ">查询条件(注意: 不要加where)</param>
        /// <param name="fldSort">排序字段</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="page">查看的页面序号</param>
        /// <param name="Counts">总记录条数</param>
        /// <returns></returns>
        public List<T> DataAdaptersPage<T>(string storedProcedureName, string tblName, string fldName, string where, string fldSort, int pageSize, int page, ref int Counts)
        {
            try
            {
                DataSet ds = new DataSet();
                Open();
                mDbCommand = mDatabase.GetStoredProcCommand(storedProcedureName);
                mDbCommand.Connection = Connection;
                mDatabase.AddInParameter(mDbCommand, "@TableName", DbType.AnsiString, tblName);
                mDatabase.AddInParameter(mDbCommand, "@SelectFields", DbType.AnsiString, fldName);
                mDatabase.AddInParameter(mDbCommand, "@Where", DbType.AnsiString, where);
                mDatabase.AddInParameter(mDbCommand, "@OrderField", DbType.AnsiString, fldSort);
                mDatabase.AddInParameter(mDbCommand, "@PageSize", DbType.Int32, pageSize);
                mDatabase.AddInParameter(mDbCommand, "@PageIndex", DbType.Int32, page);
                mDatabase.AddOutParameter(mDbCommand, "@RowCount", DbType.Int32, Counts);

                ds = mDatabase.ExecuteDataSet(mDbCommand);
                //pageCount = Convert.ToInt32(mDatabase.GetParameterValue(mDbCommand, "@pageCount"));
                Counts = Convert.ToInt32(mDatabase.GetParameterValue(mDbCommand, "@RowCount"));
                //return ModelHelper.ModelListInTable<T>(ds.Tables[0]);
                return DataTableConvertList<T>(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// DATATABLE转化成LIST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mTable"></param>
        /// <returns></returns>
        private List<T> DataTableConvertList<T>(DataTable mTable)
        {
            try
            {
                List<T> mList = new List<T>();
                var mT = default(T);
                string mTempName = string.Empty;
                foreach (DataRow mRow in mTable.Rows)
                {
                    mT = Activator.CreateInstance<T>();
                    var mProperTypes = mT.GetType().GetProperties();
                    foreach (var mPro in mProperTypes)
                    {
                        mTempName = mPro.Name;
                        if (mTable.Columns.Contains(mTempName))
                        {
                            var mValue = mRow[mTempName];
                            if (mValue != DBNull.Value)
                                mPro.SetValue(mT, mValue, null);
                        }
                    }
                    mList.Add(mT);
                }
                return mList;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        #endregion

        #region 执行Sql语句返回结果集的第一行第一列的值对象
        /// <summary>
        /// 执行Sql语句返回结果集的第一行第一列的值对象
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回结果集的第一行第一列的值对象</returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params IDbDataParameter[] commandParameters)
        {
            try
            {
                this.Open();
                if (cmdType == CommandType.StoredProcedure)
                    mDbCommand = mDatabase.GetStoredProcCommand(cmdText);
                else
                    mDbCommand = mDatabase.GetSqlStringCommand(cmdText);
                mDbCommand.Connection = Connection;
                if (Transaction != null)
                    mDbCommand.Transaction = Transaction;
                if ((commandParameters != null) && (commandParameters.Length > 0))
                    foreach (IDbDataParameter mParameter in commandParameters)
                        mDatabase.AddParameter(mDbCommand, mParameter.ParameterName, mParameter.DbType, mParameter.Direction, mParameter.SourceColumn, mParameter.SourceVersion, mParameter.Value);
                return mDbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Transaction == null)
                    this.Close();
            }
        }
        #endregion

        #region 返回指定SQL查询参数的单行结果集
        /// <summary>
        /// 返回指定SQL查询参数的单行结果集
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程名， T-SQL语句</param>
        /// <param name="SQLParameters">可选 如果是存储过程或参数化SQL请输入相应的参数</param>
        /// <returns>DataRow如果没有记录则为null</returns>
        public DataRow ExecuteDataRow(CommandType cmdType, string cmdText, params  IDbDataParameter[] commandParameters)
        {
            try
            {
                DataRow tr = null;
                DataTable table = ExecuteDataSet(cmdType, cmdText, commandParameters).Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    tr = table.Rows[0];
                }
                return tr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 返回指定SQL查询参数的单行结果集
        /// </summary>
        /// <param name="cmdText">T-SQL语句</param>
        /// <param name="SQLParameters">可选 如果是存储过程或参数化SQL请输入相应的参数</param>
        /// <returns>DataRow如果没有记录则为null</returns>
        public DataRow ExecuteDataRow(string cmdText, params  IDbDataParameter[] commandParameters)
        {
            try
            {
                DataRow tr = null;
                DataTable table = ExecuteDataSet(CommandType.Text, cmdText, commandParameters).Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    tr = table.Rows[0];
                }
                return tr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region
        public int ExeDbCommand(List<DbParameter> mList, string StoredProcName, ref string Msg)
        {
            try
            {
                DataSet ds = new DataSet();
                Open();
                mDbCommand = mDatabase.GetStoredProcCommand(StoredProcName);
                mDbCommand.Connection = Connection;
                //mDatabase.AddInParameter(mDbCommand, "@TableName", DbType.AnsiString, tblName);
                //mDatabase.AddInParameter(mDbCommand, "@SelectFields", DbType.AnsiString, fldName);
                //mDatabase.AddInParameter(mDbCommand, "@Where", DbType.AnsiString, where);
                //mDatabase.AddInParameter(mDbCommand, "@OrderField", DbType.AnsiString, fldSort);
                //mDatabase.AddInParameter(mDbCommand, "@PageSize", DbType.Int32, pageSize);
                //mDatabase.AddInParameter(mDbCommand, "@PageIndex", DbType.Int32, page);
                //mDatabase.AddOutParameter(mDbCommand, "@RowCount", DbType.Int32, Counts);
                int i = 0;
                foreach (DbParameter mItem in mList)
                {
                    i++;
                    if (i < mList.Count)
                        mDatabase.AddInParameter(mDbCommand, mItem.ParameterName, mItem.DbType, mItem.Value);
                    else
                    {
                        mDatabase.AddOutParameter(mDbCommand, mItem.ParameterName, mItem.DbType, 50);
                    }

                }


                //ds = mDatabase.ExecuteDataSet(mDbCommand);
                int mCount = mDatabase.ExecuteNonQuery(mDbCommand);
                //pageCount = Convert.ToInt32(mDatabase.GetParameterValue(mDbCommand, "@pageCount"));

                //int Counts = Convert.ToInt32(mDatabase.GetParameterValue(mDbCommand, "@RowCount"));
                //return ModelHelper.ModelListInTable<T>(ds.Tables[0]);
                string mMsg = mDatabase.GetParameterValue(mDbCommand, mList[mList.Count - 1].ParameterName).ToString();
                Msg = mMsg;
                return mCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region
        public DataTable PageInfo(string tblName, string fldName, string where, string fldSort, int pageSize, int page, ref int Counts)
        {
            try
            {
                DataSet ds = new DataSet();
                Open();
                mDbCommand = mDatabase.GetStoredProcCommand("Proc_Pagination");
                mDbCommand.Connection = Connection;
                mDatabase.AddInParameter(mDbCommand, "@TableName", DbType.AnsiString, tblName);
                mDatabase.AddInParameter(mDbCommand, "@SelectFields", DbType.AnsiString, fldName);
                mDatabase.AddInParameter(mDbCommand, "@Where", DbType.AnsiString, where);
                mDatabase.AddInParameter(mDbCommand, "@OrderField", DbType.AnsiString, fldSort);
                mDatabase.AddInParameter(mDbCommand, "@PageSize", DbType.Int32, pageSize);
                mDatabase.AddInParameter(mDbCommand, "@PageIndex", DbType.Int32, page);
                mDatabase.AddOutParameter(mDbCommand, "@RowCount", DbType.Int32, Counts);

                ds = mDatabase.ExecuteDataSet(mDbCommand);

                //pageCount = Convert.ToInt32(mDatabase.GetParameterValue(mDbCommand, "@pageCount"));

                Counts = Convert.ToInt32(mDatabase.GetParameterValue(mDbCommand, "@RowCount"));
                //return ModelHelper.ModelListInTable<T>(ds.Tables[0]);


                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region
        public List<T> GetListPageInfo<T>(PageInfo pageInfo)
        {
            int mCount = 0;
            List<T> mlist = DataAdaptersPage<T>("Proc_Pagination"
                , pageInfo.TableName
                , pageInfo.SelectFields
                , pageInfo.Filter
                , pageInfo.SortField
                , pageInfo.PageSize
                , pageInfo.CurrentPage
                , ref mCount);
            pageInfo.Counts = mCount;
            return mlist;
        }
        #endregion
    }
}

#region proc
//USE [XH_TelSys]
//GO
///****** Object:  StoredProcedure [dbo].[Proc_Pagination]    Script Date: 05/25/2016 10:51:26 ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO

// /*通用分页存储过程 MS SQL Server 2005*/  
//ALTER PROCEDURE [dbo].[Proc_Pagination]  
//(  
//    @TableName VARCHAR(255),            -- 表名/视图名  
//    @SelectFields VARCHAR(1000) = '*',  -- 需要返回的列  
//    @Where VARCHAR(3000) = '1=1',       -- 查询条件(注意: 不要加where)  
//    @OrderField VARCHAR(255)='',        -- 排序字段  
//    @PageSize INT = 10,                 -- 页尺寸  
//    @PageIndex INT = 1,                 -- 当前页码(从第页开始)  
//    @RowCount INT OUTPUT                -- 记录总数  
//)   
//AS BEGIN  
//    --定义变量,SQL语句  
//    DECLARE @SQL_SELECT NVARCHAR(4000)  
//    DECLARE @SQL_COUNT NVARCHAR(4000)  
//    --统计总数据量  
//    SET @SQL_COUNT = N'SELECT @RowCount= COUNT(*) FROM ' + @TableName + ' WHERE ' + @WHERE  
//    EXEC SP_EXECUTESQL @SQL_COUNT,N'@RowCount INT OUTPUT',@RowCount=@RowCount OUTPUT  
//    --执行分页查询  
//    SET @SQL_SELECT = 'SELECT * FROM  
//     (  
//       SELECT ROW_NUMBER() OVER(ORDER BY ' + @OrderField + ') AS RowNumber,' + @SelectFields + '  
//       FROM ' + @TableName + '   
//       WHERE ' + @Where + '  
//     )AS TempTable  
//     WHERE RowNumber > ' + CONVERT(VARCHAR,(@PageIndex - 1) * @PageSize) + 'AND  
//        RowNumber <= ' + CONVERT(VARCHAR,@PageIndex * @PageSize)  
//    EXEC (@SQL_SELECT)  
//END  
#endregion