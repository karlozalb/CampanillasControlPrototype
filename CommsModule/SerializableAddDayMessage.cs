using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableAddDayMessage : ISerializable
    {
        public List<DateTime> mDays;

        public SerializableAddDayMessage()
        {
            mDays = new List<DateTime>();
        }

        public SerializableAddDayMessage(SerializationInfo info, StreamingContext context)
        {
            mDays = (List<DateTime>)info.GetValue("mDays", typeof(List<DateTime>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mDays", mDays);
        }
    }
}
