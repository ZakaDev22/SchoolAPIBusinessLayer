using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsDepartments
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public departmentDTO departmentDTO
        {
            get
            {
                return new departmentDTO(this.ID, this.Name, this.SchoolID);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int SchoolID { get; set; }

        public clsDepartments(departmentDTO depDTO, enMode mode)
        {
            Mode = mode;
            ID = depDTO.ID;
            Name = depDTO.Name;
            SchoolID = depDTO.SchoolID;
        }

        public static async Task<IEnumerable<departmentDTO>> GetAllAsync()
        {
            var list = await clsDepartmentsData.GetAllAsync();

            return list == null ? Enumerable.Empty<departmentDTO>() : list;
        }

        public static async Task<clsDepartments> GetByIDAsync(int ID)
        {
            var email = await clsDepartmentsData.GetByIdAsync(ID);

            return email != null ? new clsDepartments(email, enMode.Update) : null;
        }

    }
}
