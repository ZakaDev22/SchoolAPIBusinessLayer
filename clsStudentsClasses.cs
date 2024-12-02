using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStudentsClasses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public studentClassDTO sClassDTO
        {
            get
            {

                return new studentClassDTO(this.ID, this.StudentID, this.ClassID);
            }
        }

        public int ID { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }

        public clsStudentsClasses(studentClassDTO sDTO, enMode mode)
        {
            _Mode = mode;
            ID = sDTO.ID;
            StudentID = sDTO.StudentID;
            ClassID = sDTO.ClassID;
        }

        private async Task<bool> _AddNewAsync(studentClassDTO studentClassDTO)
        {
            this.ID = await clsStudentsClassesData.AddAsync(studentClassDTO);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(studentClassDTO studentClassDTO)
        {
            return await clsStudentsClassesData.UpdateAsync(studentClassDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(sClassDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(sClassDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<studentClassDTO>> GetAllAsync()
        {
            var list = await clsStudentsClassesData.GetAllAsync();


            return list == null ? Enumerable.Empty<studentClassDTO>() : list;
        }

        public static async Task<clsStudentsClasses> GetByIDAsync(int ID)
        {
            var studentClassDTO = await clsStudentsClassesData.GetByIdAsync(ID);

            return studentClassDTO != null ? new clsStudentsClasses(studentClassDTO, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsStudentsClassesData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsStudentsClassesData.IsExistsAsync(ID);
        }
    }


}
