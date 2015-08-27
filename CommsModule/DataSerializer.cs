using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    class DataSerializer
    {
        public byte[] serialize(Object pobject)
        {
            BinaryFormatter bin = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            bin.Serialize(mem, pobject);
            return mem.GetBuffer();
        }

        public Object deserialize(byte[] pobject)
        {
            BinaryFormatter bin = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            mem.Write(pobject, 0, pobject.Length);
            mem.Seek(0, 0);
            return bin.Deserialize(mem);
        }


    }
}
