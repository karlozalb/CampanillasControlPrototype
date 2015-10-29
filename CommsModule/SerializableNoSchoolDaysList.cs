using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableNoSchoolDaysList : ISerializable
    {
        public List<DateTime> mDaysList;

        public SerializableNoSchoolDaysList()
        {
            mDaysList = new List<DateTime>();
        }

        public SerializableNoSchoolDaysList(SerializationInfo info, StreamingContext context)
        {
            mDaysList = (List<DateTime>)info.GetValue("mDaysList",typeof(List<DateTime>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mDaysList", mDaysList);
        }
    }
}
