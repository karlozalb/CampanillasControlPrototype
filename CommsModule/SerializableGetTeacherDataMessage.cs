using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableGetTeacherDataMessage : ISerializable
    {
        public int teacherID;
        public DateTime init,end;

        public SerializableGetTeacherDataMessage()
        {

        }

        public SerializableGetTeacherDataMessage(SerializationInfo info, StreamingContext context)
        {
            teacherID = (int)info.GetValue("teacherID", typeof(int));
            init = (DateTime)info.GetValue("init", typeof(DateTime));
            end = (DateTime)info.GetValue("end", typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("teacherID", teacherID);
            info.AddValue("init", init);
            info.AddValue("end", end);
        }
    }
}
