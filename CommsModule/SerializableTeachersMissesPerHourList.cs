using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableTeachersMissesPerHourList : ISerializable
    {

        public const byte C_FALTA_COMPLETA = 1;
        public const byte C_SE_VA_ANTES = 2;
        public const byte C_PRESENTE = 3;
        public const byte C_LLEGA_TARDE = 4;
        public const byte C_SE_VA_ANTES_Y_LLEGA_TARDE = 5;
        public const byte C_NO_NECESARIA_PRESENCIA = 6;

        public List<MissesPerHourTeacherNode> mMissesList;        

        public SerializableTeachersMissesPerHourList()
        {
            mMissesList = new List<MissesPerHourTeacherNode>();
        }

        public SerializableTeachersMissesPerHourList(SerializationInfo info, StreamingContext context)
        {
            mMissesList = (List<MissesPerHourTeacherNode>)info.GetValue("mMissesList",typeof(List<MissesPerHourTeacherNode>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mMissesList", mMissesList);
        }

        [Serializable()]
        public class MissesPerHourTeacherNode : ISerializable
        {
            public string mTeacherName;
            public int mId;
            public List<KeyValuePair<DateTime, byte[]>> mDateHourValues;

            public MissesPerHourTeacherNode()
            {
                mDateHourValues = new List<KeyValuePair<DateTime, byte[]>>();
            }

            public MissesPerHourTeacherNode(SerializationInfo info, StreamingContext context)
            {
                mTeacherName = info.GetString("mTeacherName");
                mId = info.GetInt32("mId");
                mDateHourValues = (List<KeyValuePair<DateTime, byte[]>>)info.GetValue("mDateHourValues", typeof(List<KeyValuePair<DateTime, byte[]>>));
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("mTeacherName", mTeacherName);
                info.AddValue("mId", mId);
                info.AddValue("mDateHourValues", mDateHourValues);
            }
        }

    }
}
