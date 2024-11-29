using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStudentGradesLog
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public sgLogDTO sgLogDTO
        {
            get
            {

                return new sgLogDTO(this.LogID, this.StudentID, this.SubjectID, this.Grade, this.LogDate, this.Comments);
            }
        }

        public int LogID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public decimal Grade { get; set; }
        public DateTime LogDate { get; set; }
        public string Comments { get; set; }

        public clsStudentGradesLog(sgLogDTO sglDTO, enMode mode)
        {
            _Mode = mode;
            LogID = sglDTO.LogID;
            StudentID = sglDTO.StudentID;
            SubjectID = sglDTO.SubjectID;
            Grade = sglDTO.Grade;
            LogDate = sglDTO.LogDate;
            Comments = sglDTO.Comments;
        }

        private async Task<bool> _AddNewAsync(sgLogDTO sDTO)
        {
            this.LogID = await clsStudentGradesLogData.AddAsync(sDTO);

            return this.LogID != -1;
        }

        private async Task<bool> _UpdateAsync(sgLogDTO sDTO)
        {
            return await clsStudentGradesLogData.UpdateAsync(sDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(sgLogDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(sgLogDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<sgLogDTO>> GetAllAsync()
        {
            var list = await clsStudentGradesLogData.GetAllAsync();

            return list == null ? Enumerable.Empty<sgLogDTO>() : list;
        }

        public static async Task<clsStudentGradesLog> GetByIDAsync(int ID)
        {
            var sgLog = await clsStudentGradesLogData.GetByIdAsync(ID);

            return sgLog != null ? new clsStudentGradesLog(sgLog, enMode.Update) : null;
        }


        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsStudentGradesLogData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsStudentGradesLogData.IsExistsAsync(ID);
        }
    }
}
