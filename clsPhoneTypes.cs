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
    }
}
