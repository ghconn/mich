using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    #region interface
    public delegate void SubcribeHandler(string s);
    public interface ISubcribe
    {
        event SubcribeHandler SubcribeEvent;
    }

    public delegate void PublishHandler(string s);
    public interface IPublish
    {
        event PublishHandler PublishEvent;
        void Notify(string s);
    }
    #endregion

    #region class
    public class SubPubComponet : ISubcribe, IPublish
    {
        public SubPubComponet()
        {
            PublishEvent += new PublishHandler(Notify);
        }

        public PublishHandler PublishEvent;
        event PublishHandler IPublish.PublishEvent
        {
            add { PublishEvent += value; }
            remove { PublishEvent -= value; }
        }

        event SubcribeHandler subcribeEvent;
        event SubcribeHandler ISubcribe.SubcribeEvent
        {
            add { subcribeEvent += value; }
            remove { subcribeEvent -= value; }
        }

        public void Notify(string s)
        {
            subcribeEvent?.Invoke(s);
        }        
    }

    public class Subcriber
    {
        string _subname;
        public Subcriber(string s)
        {
            _subname = s;
        }

        public ISubcribe AddSubcribe { set { value.SubcribeEvent += Show; } }
        public ISubcribe RemoveSubcribe { set { value.SubcribeEvent -= Show; } }

        public void Show(string s)
        {
            Console.WriteLine($"{_subname}收到{s}");
        }
    }

    public class Publisher : IPublish
    {
        string _pubname;
        public Publisher(string s)
        {
            _pubname = s;
        }

        private event PublishHandler PublishEvent;
        event PublishHandler IPublish.PublishEvent
        {
            add { PublishEvent += value; }
            remove { PublishEvent -= value; }
        }

        public void Notify(string s)
        {
            PublishEvent?.Invoke($"{_pubname}发布{s}");
        }
    }
    #endregion

    #region demo
    public class SPDemo
    {
        public static void _Main()
        {
            SubPubComponet spc = new SubPubComponet();
            IPublish pub = new Publisher("A");
            pub.PublishEvent += spc.PublishEvent;
            Subcriber sub1 = new Subcriber("s1");
            Subcriber sub2 = new Subcriber("s2");
            sub1.AddSubcribe = spc;
            sub2.AddSubcribe = spc;

            pub.Notify("123");
        }
    }
    #endregion
}
