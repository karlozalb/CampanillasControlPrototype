using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableGenericOKMessage : ISerializable
    {
        int mOk;

        public SerializableGenericOKMessage()
        {
            
        }

        public SerializableGenericOKMessage(SerializationInfo info, StreamingContext context)
        {
            mOk = info.GetInt32("mOk");
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mOk", mOk,typeof(int));
        }
    }
}
