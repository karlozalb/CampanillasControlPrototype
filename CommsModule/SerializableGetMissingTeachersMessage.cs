using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableGetMissingTeachersMessage : ISerializable
    {
        public DateTime init, end;
        public int teacherId;

        public SerializableGetMissingTeachersMessage()
        {

        }

        public SerializableGetMissingTeachersMessage(SerializationInfo info, StreamingContext context)
        {
            init = (DateTime)info.GetValue("init", typeof(DateTime));
            end = (DateTime)info.GetValue("end", typeof(DateTime));
            teacherId = info.GetInt32("teacherId");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("init", init);
            info.AddValue("end", end);
            info.AddValue("teacherId", teacherId);
        }
    }
}
