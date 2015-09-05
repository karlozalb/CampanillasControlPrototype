using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableGetAdList : SerializableGenericOKMessage
    {

        public SerializableGetAdList()
        {

        }

        public SerializableGetAdList(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
