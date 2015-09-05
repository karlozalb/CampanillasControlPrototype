using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableMissingTeachersList : ISerializable
    {
        public List<TeacherMissingNode> mMissingList;

        public SerializableMissingTeachersList()
        {
            mMissingList = new List<TeacherMissingNode>();
        }

        public SerializableMissingTeachersList(SerializationInfo info, StreamingContext context)
        {
            mMissingList = (List<TeacherMissingNode>)info.GetValue("mMissingList", typeof(List<TeacherMissingNode>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mMissingList", mMissingList);
        }

        [Serializable()]
        public class TeacherMissingNode : ISerializable
        {
            public string NAME;
            public List<DateTime> DAYS;

            public TeacherMissingNode()
            {
                DAYS = new List<DateTime>();
            }

            public TeacherMissingNode(SerializationInfo info, StreamingContext context)
            {
                DAYS = (List<DateTime>)info.GetValue("DAYS",typeof(List<DateTime>));
                NAME = (string)info.GetValue("NAME", typeof(string));
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("NAME",NAME);
                info.AddValue("DAYS", DAYS);
            }
        }
    }
    
}
