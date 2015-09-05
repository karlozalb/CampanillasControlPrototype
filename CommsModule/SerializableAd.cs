using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableAd : ISerializable
    {
        public int mId;
        public string mText;
        public string mDate;       

        public SerializableAd()
        {

        }

        public SerializableAd(SerializationInfo info, StreamingContext context)
        {
            mId = (int)info.GetValue("mId", typeof(int));
            mText = (string)info.GetValue("mText", typeof(string));
            mDate = (string)info.GetValue("mDate", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mId",mId);
            info.AddValue("mText", mText);
            info.AddValue("mDate", mDate);
        }

        public override string ToString()
        {
            return mId + " - " + mDate;
        }
    }
}
