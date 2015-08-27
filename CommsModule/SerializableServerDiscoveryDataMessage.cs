using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    class SerializableServerDiscoveryDataMessage : ISerializable
    {

        public SerializableServerDiscoveryDataMessage()
        {

        }

        public SerializableServerDiscoveryDataMessage(SerializationInfo info, StreamingContext context)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}
