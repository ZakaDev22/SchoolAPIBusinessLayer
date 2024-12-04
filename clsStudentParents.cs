using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStudentParents
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public studentParentDTO sClassDTO
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

        public static async Task<IEnumerable<studentParentDTO>> GetAllAsync()
        {
            var list = await clsStudentParentsData.GetAllAsync();


            return list == null ? Enumerable.Empty<studentParentDTO>() : list;
        }
    }
}
