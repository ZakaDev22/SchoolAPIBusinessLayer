using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsEmailsTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public emailTypesDTO emailTypeDTO
        {
            get
            {
                return new emailTypesDTO(this.ID, this.Name);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public clsEmailsTypes(emailTypesDTO typesDTO, enMode mode)
        {
            Mode = mode;
            ID = typesDTO.ID;
            Name = typesDTO.Name;
        }

        private async Task<bool> _AddNewAsync(emailTypesDTO emailDTO)
        {
            this.ID = await clsEmailsTypesData.AddAsync(emailDTO);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(emailTypesDTO emailDTO)
        {
            return await clsEmailsTypesData.UpdateAsync(emailDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(emailTypeDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(emailTypeDTO);
            }
            return false;
        }


        public static async Task<IEnumerable<emailTypesDTO>> GetAllAsync()
        {
            var list = await clsEmailsTypesData.GetAllAsync();

            return list == null ? Enumerable.Empty<emailTypesDTO>() : list;
        }

        public static async Task<clsEmailsTypes> GetByIDAsync(int ID)
        {
            var emailType = await clsEmailsTypesData.GetByIdAsync(ID);

            return emailType != null ? new clsEmailsTypes(emailType, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsEmailsTypesData.DeleteAsync(ID);
        }
    }
}
