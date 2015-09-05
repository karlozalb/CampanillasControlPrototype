using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableAdList : ISerializable
    {
        public List<SerializableAd> mAdList;

        public SerializableAdList()
        {
            mAdList = new List<SerializableAd>();
        }

        public SerializableAdList(SerializationInfo info, StreamingContext context)
        {
            mAdList = (List<SerializableAd>)info.GetValue("mAdList", typeof(List<SerializableAd>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mAdList", mAdList);
        }

    }
}
