﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    public interface ICommController
    {

        SerializableTeacherData getTeacherInfo(int pteacherid,DateTime pinit,DateTime pend);
        SerializableTeacherList getTeachersList();
        
    }
}
