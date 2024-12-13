using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsPhoneTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public phoneTypesDTO phoneTypeDTO
        {
            get
            {
                return new phoneTypesDTO(this.ID, this.Name);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public clsPhoneTypes(phoneTypesDTO phoneTypeDTO, enMode mode)
        {
            Mode = mode;
            ID = phoneTypeDTO.ID;
            Name = phoneTypeDTO.Name;
        }

        private async Task<bool> _AddNewAsync(phoneTypesDTO phoneDTO)
        {
            this.ID = await clsPhoneTypesData.AddAsync(phoneDTO);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(phoneTypesDTO phoneDTO)
        {
            return await clsPhoneTypesData.UpdateAsync(phoneDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(phoneTypeDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(phoneTypeDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<phoneTypesDTO>> GetAllAsync()
        {
            var list = await clsPhoneTypesData.GetAllAsync();

            return list == null ? Enumerable.Empty<phoneTypesDTO>() : list;
        }

        public static async Task<clsPhoneTypes> GetByIDAsync(int ID)
        {
            var phoneType = await clsPhoneTypesData.GetByIdAsync(ID);

            return phoneType != null ? new clsPhoneTypes(phoneType, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsPhoneTypesData.DeleteAsync(ID);
        }
    }
}
