using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableGetNoSchoolDaysMessage : ISerializable
    {
        public SerializableGetNoSchoolDaysMessage()
        {

        }

        public SerializableGetNoSchoolDaysMessage(SerializationInfo info, StreamingContext context)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
