using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace mdl
{
    [Serializable]
    [DataContract]
    public class chartpie<T>
    {
        [DataMember]
        public List<dataset<T>> datasets { get; set; }

        [DataMember]
        public string[] labels { get; set; }
    }

    public class dataset<T>
    {
        [DataMember]
        public T[] data { get; set; }

        [DataMember]
        public string[] backgroundColor { get; set; }
    }
}
