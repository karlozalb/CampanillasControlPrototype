using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableGetSubstituteListMessage : ISerializable
    {
        public SerializableGetSubstituteListMessage()
        {

        }

        public SerializableGetSubstituteListMessage(SerializationInfo info, StreamingContext context)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
