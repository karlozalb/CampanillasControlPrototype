using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableDeleteDayMessage : SerializableAddDayMessage
    {
        public SerializableDeleteDayMessage() : base()
        {

        }

        public SerializableDeleteDayMessage(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
    }
}
