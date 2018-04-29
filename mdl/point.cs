using System;
using System.Collections.Generic;

namespace mdl
{
    public class MyCompareer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            var px = x as point;
            var py = y as point;
            if (px != null && py != null)
                return px.x == px.x && px.y == py.y;
            return false;
        }

        public int GetHashCode(T obj)
        {
            var o = obj as point;
            if (o != null)
                return (int)(o.x) + (int)(o.y);
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// 相等性实现笔记。
    /// 1,如果将class point改为struct point，不用实现接口或重写成员，两个point的x和y分别相等即判定这两个point相等
    /// 2,两种方式实现比较相等性都必须同时写Equals和GetHashCode方法
    /// </summary>
    public class point //: IEqualityComparer<point>
    {
        public Single x { get; set; }

        public Single y { get; set; }

        public point()
        {

        }

        public point(Single x, Single y)
        {
            this.x = x;
            this.y = y;
        }

        #region 重写基类Equals实现比较相等性
        //public override bool Equals(object obj)
        //{
        //    if (obj == null || this.GetType() != obj.GetType())
        //        return false;
        //    return this.x == ((point)obj).x && this.y == ((point)obj).y;
        //    //if (Math.Abs(this.x - ((point)obj).x) < .00000001)
        //    //    return Math.Abs(this.y - ((point)obj).y) < .00000001;
        //    //return false;
        //}

        //public override int GetHashCode()
        //{
        //    return (int)(this.x) + (int)(this.y);
        //}
        #endregion

        #region 继承IEqualityComparer<T>实现比较相等性
        //public bool Equals(point x, point y)
        //{
        //    return x.x == x.x && x.y == y.y;
        //    //if (Math.Abs(this.x - ((point)obj).x) < .00000001)
        //    //    return Math.Abs(this.y - ((point)obj).y) < .00000001;
        //    //return false;
        //}

        //public int GetHashCode(point obj)
        //{
        //    return (int)(obj.x) + (int)(obj.y);
        //}
        #endregion
    }

    public class vvv
    {
        public string n { get; set; }
        public string m { get; set; }
        public decimal area { get; set; }
        
    }
}
