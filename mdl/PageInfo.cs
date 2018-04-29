using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace mdl
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    [Serializable]
    [DataContract]
    public class PageInfo
    {

        #region 默认构造函数
        public PageInfo()
        {
            TableName = "";
            SelectFields = "*";
            Filter = "1=1";
            SortField = "";
            CurrentPage = 1;
            PageSize = 20;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 以pageIndex、pageSize初始化分页实体
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PageInfo(int pageIndex, int pageSize) : this()
        {
            CurrentPage = pageIndex;
            PageSize = pageSize;
        }
        #endregion

        #region 查询表名
        /// <summary>
        /// 查询表名
        /// </summary>
        [DataMember]
        public string TableName
        {
            get;
            set;
        }
        #endregion

        #region 要返回的字段，英文半角逗号相连
        /// <summary>
        /// 需要返回的字段 如： id ,Name
        /// </summary>
        [DataMember]
        public string SelectFields
        {
            get;
            set;
        }
        #endregion

        #region 过滤条件
        /// <summary>
        /// 过滤条件 如:name='张三' and tel="13622233421"
        /// </summary>
        [DataMember]
        public string Filter
        {
            get;
            set;
        }
        #endregion

        #region 排序字段
        /// <summary>
        /// 排序字段 如： id desc
        /// </summary>
        [DataMember]
        public string SortField
        {
            get;
            set;
        }
        #endregion

        #region 当前页index
        /// <summary>
        /// 当前页
        /// </summary>
        [DataMember]
        public int CurrentPage
        {
            get;
            set;
        }
        #endregion

        #region 页大小
        /// <summary>
        /// 页大小 
        /// </summary>
        [DataMember]
        public int PageSize
        {
            get;
            set;
        }
        #endregion

        #region 总记录数
        /// <summary>
        /// 总记录数
        /// </summary>
        [DataMember]
        public int Counts
        {
            get;
            set;
        }
        #endregion

    }
}
