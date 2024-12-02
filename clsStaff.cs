using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStaff
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public staffDTO staffDTO
        {
            get
            {

                return new staffDTO(this.ID, this.PersonID, this.JobTitleID, this.DepartmentID, this.SchoolID, this.StaffSalaryID);
            }
        }

        public int ID { get; set; }
        public int PersonID { get; set; }
        public int JobTitleID { get; set; }
        public int DepartmentID { get; set; }
        public int SchoolID { get; set; }
        public int StaffSalaryID { get; set; }

        public clsStaff(staffDTO staff, enMode mode)
        {
            _Mode = mode;

            ID = staff.ID;
            PersonID = staff.PersonID;
            JobTitleID = staff.JobTitleID;
            DepartmentID = staff.DepartmentID;
            SchoolID = staff.SchoolID;
            StaffSalaryID = staff.StaffSalaryID;

        }

        private async Task<bool> _AddNewAsync(staffDTO staff)
        {
            this.ID = await clsStaffData.AddAsync(staffDTO);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(staffDTO staff)
        {
            return await clsStaffData.UpdateAsync(staff);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(staffDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(staffDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<staffDTO>> GetAllAsync()
        {
            var list = await clsStaffData.GetAllAsync();

            return list == null ? Enumerable.Empty<staffDTO>() : list;
        }

        public static async Task<clsStaff> GetByIDAsync(int ID)
        {
            var staff = await clsStaffData.GetByIdAsync(ID);

            return staff != null ? new clsStaff(staff, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsStaffData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsStaffData.IsExistsAsync(ID);
        }

        public static async Task<bool> IsExistsByPersonIDAsync(int ID)
        {
            return await clsStaffData.IsExistsByPersonIDAsync(ID);
        }
    }
}
