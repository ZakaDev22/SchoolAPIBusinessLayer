using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsClasses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public classDTO classDTO
        {
            get
            {
                return new classDTO(this.ID, this.Name, this.Code, this.Capacity, this.SchoolID);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public short Capacity { get; set; }
        public int SchoolID { get; set; }

        public clsClasses(classDTO clsDTO, enMode mode)
        {
            Mode = mode;
            ID = clsDTO.ID;
            Name = clsDTO.Name;
            Code = clsDTO.Code;
            Capacity = clsDTO.Capacity;
            SchoolID = clsDTO.SchoolID; ;
        }

        private async Task<bool> _AddNewAsync(classDTO clsDTO)
        {
            this.ID = await clsClassesData.AddAsync(clsDTO);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(classDTO clsDTO)
        {
            return await clsClassesData.UpdateAsync(clsDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(classDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(classDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<classDTO>> GetAllAsync()
        {
            var list = await clsClassesData.GetAllAsync();

            return list == null ? Enumerable.Empty<classDTO>() : list;
        }

        public static async Task<clsClasses> GetByIDAsync(int ID)
        {
            var @class = await clsClassesData.GetByIdAsync(ID);

            return @class != null ? new clsClasses(@class, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsClassesData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsClassesData.IsExistsAsync(ID);
        }
    }
}
