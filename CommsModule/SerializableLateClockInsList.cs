using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableLateClockInsList : ISerializable
    {
        public List<SerializableTeacherData> mTeacherList;

        public SerializableLateClockInsList()
        {
            mTeacherList = new List<SerializableTeacherData>();
        }

        public SerializableLateClockInsList(SerializationInfo info, StreamingContext context)
        {
            mTeacherList = (List<SerializableTeacherData>)info.GetValue("mTeacherList",typeof(List<SerializableTeacherData>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mTeacherList", mTeacherList);
        }
    }
}
