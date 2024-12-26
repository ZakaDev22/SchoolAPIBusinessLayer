using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStudentGrades
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public sGradeDTO sGradeDTO
        {
            get
            {

                return new sGradeDTO(this.ID, this.Name, this.SchoolID);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int SchoolID { get; set; }

        public clsStudentGrades(sGradeDTO sGrade, enMode mode)
        {
            _Mode = mode;
            ID = sGrade.ID;
            Name = sGrade.Name;
            SchoolID = sGrade.SchoolID;
        }

        private async Task<bool> _AddNewAsync(sGradeDTO sDTO)
        {
            this.ID = await clsStudentGradesData.AddAsync(sDTO);

            return this.ID != -1;
        }

        private async Task<bool> _UpdateAsync(sGradeDTO sDTO)
        {
            return await clsStudentGradesData.UpdateAsync(sDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(sGradeDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(sGradeDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<sGradeDTO>> GetAllAsync()
        {
            var list = await clsStudentGradesData.GetAllAsync();

            return list == null ? Enumerable.Empty<sGradeDTO>() : list;
        }

        public static async Task<clsStudentGrades> GetByIDAsync(int ID)
        {
            var sgLog = await clsStudentGradesData.GetByIdAsync(ID);

            return sgLog != null ? new clsStudentGrades(sgLog, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsStudentGradesData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsStudentGradesData.IsExistsAsync(ID);
        }
    }
}
