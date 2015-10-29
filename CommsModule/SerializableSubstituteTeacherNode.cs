using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableSubstituteTeacherNode : ISerializable
    {
        public int mMissingId, mSubstituteId;
        public string mMissingName, mSubstituteName;

        public SerializableSubstituteTeacherNode()
        {

        }

        public SerializableSubstituteTeacherNode(SerializationInfo info, StreamingContext context)
        {
            mMissingId = info.GetInt32("mMissingId");
            mSubstituteId = info.GetInt32("mSubstituteId");
            mMissingName = info.GetString("mMissingName");
            mSubstituteName = info.GetString("mSubstituteName");
        }    

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mMissingId", mMissingId);
            info.AddValue("mSubstituteId", mSubstituteId);
            info.AddValue("mMissingName", mMissingName);
            info.AddValue("mSubstituteName", mSubstituteName);
        }

        public override string ToString()
        {
            return mSubstituteId + " - " + mSubstituteName + " sustituye a " + mMissingId + " - " + mMissingName;
        }
    }
}
