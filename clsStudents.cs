using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStudents
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public studentDTO sDTO
        {
            get
            {

                return new studentDTO(this.StudentID, this.PersonID, this.StudentGradeID, this.SchoolID, this.EnrollmentDate, this.IsActive);
            }
        }

        public int StudentID { get; set; }
        public int PersonID { get; set; }
        public int StudentGradeID { get; set; }
        public int SchoolID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }

        public clsStudents(studentDTO studentDTO, enMode mode)
        {
            _Mode = mode;
            StudentID = studentDTO.StudentID;
            PersonID = studentDTO.PersonID;
            StudentGradeID = studentDTO.StudentGradeID;
            SchoolID = studentDTO.SchoolID;
            EnrollmentDate = studentDTO.EnrollmentDate;
            IsActive = studentDTO.IsActive;
        }

        private async Task<bool> _AddNewAsync(studentDTO studentDTO)
        {
            this.StudentID = await clsStudentsData.AddAsync(studentDTO);

            return (this.StudentID != -1);
        }

        private async Task<bool> _UpdateAsync(studentDTO studentDTO)
        {
            return await clsStudentsData.UpdateAsync(studentDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(sDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(sDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<studentDTO>> GetAllAsync()
        {
            var list = await clsStudentsData.GetAllAsync();


            return list == null ? Enumerable.Empty<studentDTO>() : list;
        }

        public static async Task<clsStudents> GetByIDAsync(int ID)
        {
            studentDTO studentDTO = await clsStudentsData.GetByIdAsync(ID);

            return studentDTO != null ? new clsStudents(studentDTO, enMode.Update) : null;
        }

        public static async Task<clsStudents> GetByPersonIDAsync(int PersonID)
        {
            var studentDTO = await clsStudentsData.GetByPersonIDAsync(PersonID);

            return studentDTO != null ? new clsStudents(studentDTO, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsStudentsData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsStudentsData.IsExistsAsync(ID);
        }
    }
}
