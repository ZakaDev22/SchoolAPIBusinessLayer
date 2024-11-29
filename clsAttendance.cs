using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsAttendance
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public attendanceDTO attendanceDTO
        {
            get
            {
                return new attendanceDTO(this.AttendanceID, this.StudentID, this.ClassID, this.AttendanceDate, this.Status);
            }
        }

        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool Status { get; set; }

        public clsAttendance(attendanceDTO attendanceDTO, enMode mode)
        {
            Mode = mode;

            AttendanceID = attendanceDTO.AttendanceID;
            StudentID = attendanceDTO.StudentID;
            ClassID = attendanceDTO.ClassID;
            AttendanceDate = attendanceDTO.AttendanceDate;
            Status = attendanceDTO.Status;
        }

        private async Task<bool> _AddNewAsync(attendanceDTO atd)
        {
            this.AttendanceID = await clsAttendanceData.AddAsync(atd);

            return (this.AttendanceID != -1);
        }

        private async Task<bool> _UpdateAsync(attendanceDTO atd)
        {
            return await clsAttendanceData.UpdateAsync(atd);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(attendanceDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(attendanceDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<attendanceDTO>> GetAllAsync()
        {
            var list = await clsAttendanceData.GetAllAsync();

            return list == null ? Enumerable.Empty<attendanceDTO>() : list;
        }

        public static async Task<clsAttendance> GetByIDAsync(int ID)
        {
            var atd = await clsAttendanceData.GetByIdAsync(ID);

            return atd != null ? new clsAttendance(atd, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsAttendanceData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsAttendanceData.IsExistsAsync(ID);
        }
    }
}
