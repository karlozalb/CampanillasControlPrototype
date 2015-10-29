using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableSubstitutionList : ISerializable
    {
        public List<SerializableSubstituteTeacherNode> mSubstitutesNodes;

        public SerializableSubstitutionList()
        {
            mSubstitutesNodes = new List<SerializableSubstituteTeacherNode>();
        }

        public SerializableSubstitutionList(SerializationInfo info, StreamingContext context)
        {
            mSubstitutesNodes = (List<SerializableSubstituteTeacherNode>)info.GetValue("mSubstitutesNodes", typeof(List<SerializableSubstituteTeacherNode>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mSubstitutesNodes", mSubstitutesNodes);
        }
    }
}
