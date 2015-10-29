using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableAddSubstituteTeacherMessage : ISerializable
    {
        public int mMissingTeacherId, mSubstituteTeacherId;

        public SerializableAddSubstituteTeacherMessage()
        {

        }

        public SerializableAddSubstituteTeacherMessage(SerializationInfo info, StreamingContext context)
        {
            mMissingTeacherId = info.GetInt32("mMissingTeacherId");
            mSubstituteTeacherId = info.GetInt32("mSubstituteTeacherId");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mMissingTeacherId", mMissingTeacherId);
            info.AddValue("mSubstituteTeacherId", mSubstituteTeacherId);
        }
    }
}
