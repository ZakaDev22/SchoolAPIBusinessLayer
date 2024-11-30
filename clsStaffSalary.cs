using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStaffSalary
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public staffSalaryDTO staffSalaryDTO
        {
            get
            {

                return new staffSalaryDTO(this.StaffSalaryID, this.StaffID, this.Salary, this.EffectiveDate, this.Bonus, this.Deductions);
            }
        }

        public int StaffSalaryID { get; set; }
        public int StaffID { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }

        public clsStaffSalary(staffSalaryDTO SalaryDTO, enMode mode)
        {
            _Mode = mode;
            StaffSalaryID = SalaryDTO.StaffSalaryID;
            StaffID = SalaryDTO.StaffID;
            Salary = SalaryDTO.Salary;
            EffectiveDate = SalaryDTO.EffectiveDate;
            Bonus = SalaryDTO.Bonus;
            Deductions = SalaryDTO.Deductions;
        }

        private async Task<bool> _AddNewAsync(staffSalaryDTO salaryDTO)
        {
            this.StaffID = await clsStaffSalaryData.AddAsync(salaryDTO);

            return (this.StaffID != -1);
        }

        private async Task<bool> _UpdateAsync(staffSalaryDTO salaryDTO)
        {
            return await clsStaffSalaryData.UpdateAsync(salaryDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(staffSalaryDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(staffSalaryDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<staffSalaryDTO>> GetAllAsync()
        {
            var list = await clsStaffSalaryData.GetAllAsync();

            return list == null ? Enumerable.Empty<staffSalaryDTO>() : list;
        }

        public static async Task<clsStaffSalary> GetByIDAsync(int ID)
        {
            var staffSalary = await clsStaffSalaryData.GetByIdAsync(ID);

            return staffSalary != null ? new clsStaffSalary(staffSalary, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsStaffSalaryData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsStaffSalaryData.IsExistsAsync(ID);
        }


    }
}

