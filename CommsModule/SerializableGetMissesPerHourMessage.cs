using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable]
    public class SerializableGetMissesPerHourMessage : ISerializable
    {
        public DateTime mInit, mEnd;
        public int teacherId;

        public SerializableGetMissesPerHourMessage()
        {

        }

        public SerializableGetMissesPerHourMessage(SerializationInfo info, StreamingContext context)
        {
            mInit = info.GetDateTime("mInit");
            mEnd = info.GetDateTime("mEnd");
            teacherId = info.GetInt32("teacherId");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mInit", mInit);
            info.AddValue("mEnd", mEnd);
            info.AddValue("teacherId", teacherId);
        }
    }
}
