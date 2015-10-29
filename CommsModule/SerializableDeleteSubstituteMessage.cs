using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableDeleteSubstituteMessage : ISerializable
    {
        public int mSubstituteId, mMissingId;

        public SerializableDeleteSubstituteMessage()
        {

        }

        public SerializableDeleteSubstituteMessage(SerializationInfo info, StreamingContext context)
        {
            mSubstituteId = info.GetInt32("mSubstituteId");
            mMissingId = info.GetInt32("mMissingId");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mSubstituteId", mSubstituteId);
            info.AddValue("mMissingId", mMissingId);
        }
    }
}
