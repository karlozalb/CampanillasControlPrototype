using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableTeacherDataList : ISerializable
    {
        public List<SerializableTeacherData> mTeacherDataList;
           
        public SerializableTeacherDataList()
        {
            mTeacherDataList = new List<SerializableTeacherData>();
        }

        public SerializableTeacherDataList(SerializationInfo info, StreamingContext ctxt)
        {
            mTeacherDataList = (List<SerializableTeacherData>)info.GetValue("mTeacherDataList",typeof(List<SerializableTeacherData>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mTeacherDataList", mTeacherDataList);
        }
    }
}
