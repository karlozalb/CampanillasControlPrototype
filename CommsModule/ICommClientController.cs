﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommsModule
{
    public interface ICommClientController
    {
        void addTeachersDataToGUI(SerializableTeacherData pteacherdata);
        void addTeacherListDataToGUI(SerializableTeacherList pteacherlist);
        void showMessage(string ptext);
        void deleteAdResponseReceived(SerializableAdList plist);
        void missingTeachersListReceived(SerializableMissingTeachersList presponse);
    }
}
