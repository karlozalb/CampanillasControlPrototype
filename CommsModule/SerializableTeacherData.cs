using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableTeacherData : ISerializable
    {
        public int mId;
        public string mName;
        public List<ClockInDataNode> mClockins;

        public SerializableTeacherData()
        {

        }

        public void addData(DateTime pdate,TimeSpan ptime, TimeSpan pactualtime,int pdelayminutes,bool pisclockin)
        {
            if (mClockins == null) mClockins = new List<ClockInDataNode>();

            ClockInDataNode newNode = new ClockInDataNode();

            newNode.actualEntranceHour = pactualtime;
            newNode.entranceHour = ptime;
            newNode.day = pdate;
            newNode.delayMinutes = pdelayminutes;
            newNode.isClockIn = pisclockin;

            mClockins.Add(newNode);
        }

        public SerializableTeacherData(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mId = (int)info.GetValue("mId", typeof(int));
            mName = (string)info.GetValue("mName", typeof(string));
            mClockins = (List<ClockInDataNode>)info.GetValue("mClockins", typeof(List<ClockInDataNode>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("mId", mId);
            info.AddValue("mName", mName);
            info.AddValue("mClockins", mClockins);
        }

        [Serializable()]
        public class ClockInDataNode : ISerializable
        {
            public DateTime day;
            public TimeSpan entranceHour,actualEntranceHour;
            public int delayMinutes;
            public bool isClockIn;

            public ClockInDataNode()
            {

            }

            public ClockInDataNode(SerializationInfo info, StreamingContext ctxt)
            {
                //Get the values from info and assign them to the appropriate properties
                day = (DateTime)info.GetValue("day", typeof(DateTime));
                entranceHour = (TimeSpan)info.GetValue("entranceHour", typeof(TimeSpan));
                actualEntranceHour = (TimeSpan)info.GetValue("actualEntranceHour", typeof(TimeSpan));
                delayMinutes = (int)info.GetValue("delayMinutes", typeof(int));
                isClockIn = (bool)info.GetValue("isClockIn", typeof(bool));
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("day", day);
                info.AddValue("entranceHour", entranceHour);
                info.AddValue("actualEntranceHour", actualEntranceHour);
                info.AddValue("delayMinutes", delayMinutes);
                info.AddValue("isClockIn", isClockIn);
            }
        }
    }
}
