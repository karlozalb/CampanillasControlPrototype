using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableGetLateClockInsListMessage : ISerializable
    {

        public DateTime mInit, mEnd;
        public int mDelay;

        public SerializableGetLateClockInsListMessage()
        {

        }

        public SerializableGetLateClockInsListMessage(SerializationInfo info, StreamingContext context)
        {
            mInit = info.GetDateTime("mInit");
            mEnd = info.GetDateTime("mEnd");
            mDelay = info.GetInt32("mDelay");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mInit", mInit);
            info.AddValue("mEnd", mEnd);
            info.AddValue("mDelay", mDelay);
        }
    }
}
