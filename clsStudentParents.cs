using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStudentParents
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public studentParentDTO sParentDTO
        {
            get
            {

                return new studentParentDTO(this.ID, this.StudentID, this.ParentID);
            }
        }

        public int ID { get; set; }
        public int StudentID { get; set; }
        public int ParentID { get; set; }

        public clsStudentParents(studentParentDTO studentParentDTO, enMode mode)
        {
            _Mode = mode;
            ID = studentParentDTO.ID;
            StudentID = studentParentDTO.StudentID;
            ParentID = studentParentDTO.ParentID;
        }

        private async Task<bool> _AddNewAsync(studentParentDTO studentParentDTO)
        {
            this.ID = await clsStudentParentsData.AddAsync(studentParentDTO);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(studentParentDTO studentParentDTO)
        {
            return await clsStudentParentsData.UpdateAsync(studentParentDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(sParentDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(sParentDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<studentParentDTO>> GetAllAsync()
        {
            var list = await clsStudentParentsData.GetAllAsync();


            return list == null ? Enumerable.Empty<studentParentDTO>() : list;
        }

        public static async Task<clsStudentParents> GetByIDAsync(int ID)
        {
            var studentparentDTO = await clsStudentParentsData.GetByIdAsync(ID);

            return studentparentDTO != null ? new clsStudentParents(studentparentDTO, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsStudentParentsData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsStudentParentsData.IsExistsAsync(ID);
        }
    }
}
