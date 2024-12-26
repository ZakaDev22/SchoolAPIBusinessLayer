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
    }
}
