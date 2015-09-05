using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableDeleteAd : ISerializable
    {
        public int mId;

        public SerializableDeleteAd()
        {
        }

        public SerializableDeleteAd(int pid)
        {
            mId = pid;
        }

        public SerializableDeleteAd(SerializationInfo info, StreamingContext context)
        {
            mId = info.GetInt32("mId");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mId", mId);
        }
    }
}
