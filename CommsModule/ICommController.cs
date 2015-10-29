using System;
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
        void addNewAd(SerializableAd pnewad);
        SerializableAdList getAdList();
        void deleteAd(SerializableDeleteAd messageReceived);
        SerializableMissingTeachersList getMissingTeachers(SerializableGetMissingTeachersMessage messageReceived);
        SerializableTeacherDataList getBadClockInsTeachersData(SerializableGetBadClockInsTeachersListMessage messageReceived);
        void addSubstitute(SerializableAddSubstituteTeacherMessage messageReceived);
        SerializableSubstitutionList getSubstituteList();
        void deleteSubstitute(SerializableDeleteSubstituteMessage messageReceived);
        SerializableLateClockInsList getLateClockInsList(SerializableGetLateClockInsListMessage messageReceived);
        SerializableNoSchoolDaysList getNoSchoolDaysList();
        void addNoShoolDay(SerializableAddDayMessage messageReceived);
        void deleteNoShoolDay(SerializableDeleteDayMessage messageReceived);
        SerializableTeachersMissesPerHourList getMissesPerHourList(SerializableGetMissesPerHourMessage messageReceived);
    }
}
