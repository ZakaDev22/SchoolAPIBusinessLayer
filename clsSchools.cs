using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsSchools
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public schoolDTO schoolDTO
        {
            get
            {

                return new schoolDTO(this.ID, this.Name, this.AddressID);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int AddressID { get; set; }

        public clsSchools(schoolDTO school, enMode mode)
        {
            _Mode = mode;

            ID = school.ID;
            Name = school.Name;
            AddressID = school.AddressID;


        }

        private async Task<bool> _AddNewAsync(schoolDTO sDTO)
        {
            this.ID = await clsSchoolsData.AddAsync(sDTO);

            return this.ID != -1;
        }

        private async Task<bool> _UpdateAsync(schoolDTO sDTO)
        {
            return await clsSchoolsData.UpdateAsync(sDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(schoolDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(schoolDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<schoolDTO>> GetAllAsync()
        {
            var list = await clsSchoolsData.GetAllAsync();

            return list == null ? Enumerable.Empty<schoolDTO>() : list;
        }

        public static async Task<clsSchools> GetByIDAsync(int ID)
        {
            var school = await clsSchoolsData.GetByIdAsync(ID);

            return school != null ? new clsSchools(school, enMode.Update) : null;
        }
        public static async Task<clsSchools> GetByNameAsync(string schoolName)
        {
            var school = await clsSchoolsData.GetByNameAsync(schoolName);

            return school != null ? new clsSchools(school, enMode.Update) : null;
        }



        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsSchoolsData.DeleteAsync(ID);
        }

    }
}
