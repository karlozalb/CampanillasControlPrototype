using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    [Serializable()]
    public class SerializableTeacherList : ISerializable
    {
        public List<TeacherData> mTeachers;

        public SerializableTeacherList()
        {

        }

        public SerializableTeacherList(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mTeachers = (List<TeacherData>)info.GetValue("teachers", typeof(List<TeacherData>));           
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("teachers", mTeachers);           
        }

        public void add(TeacherData pteacherdata)
        {
            if (mTeachers == null) mTeachers = new List<TeacherData>();
            mTeachers.Add(pteacherdata);
        }

        [Serializable()]
        public class TeacherData : ISerializable
        {
            public string mTeacherName;
            public int mId;

            public TeacherData()
            {

            }

            public TeacherData(SerializationInfo info, StreamingContext ctxt)
            {
                //Get the values from info and assign them to the appropriate properties
                mTeacherName = (string)info.GetValue("mTeacherName", typeof(string));
                mId = (int)info.GetValue("mId", typeof(int));
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("mTeacherName", mTeacherName);
                info.AddValue("mId", mId);
            }

            public override string ToString()
            {
                return mId + "-" + mTeacherName;
            }
        }
    }
}
