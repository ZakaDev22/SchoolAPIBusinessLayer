using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsPhoneNumbers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public phoneNumberDTO phoneNumberDTO
        {
            get
            {
                return new phoneNumberDTO(this.ID, this.Number, this.PersonID, this.PhoneTypeID, this.IsPrimary);
            }
        }

        public int ID { get; set; }
        public string Number { get; set; }
        public int PersonID { get; set; }
        public int PhoneTypeID { get; set; }
        public bool IsPrimary { get; set; }

        public clsPhoneNumbers(phoneNumberDTO number, enMode mode)
        {
            Mode = mode;
            ID = number.ID;
            Number = number.Number;
            PersonID = number.PersonID;
            PhoneTypeID = number.PhoneTypeID;
            IsPrimary = number.IsPrimary;
        }


        private async Task<bool> _AddNewAsync(phoneNumberDTO phoneDTO)
        {
            this.ID = await clsPhoneNumbersData.AddAsync(phoneDTO);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(phoneNumberDTO phoneDTO)
        {
            return await clsPhoneNumbersData.UpdateAsync(phoneDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(phoneNumberDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(phoneNumberDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<phoneNumberDTO>> GetAllAsync()
        {
            var list = await clsPhoneNumbersData.GetAllAsync();

            return list == null ? Enumerable.Empty<phoneNumberDTO>() : list;
        }

        public static async Task<clsPhoneNumbers> GetByIDAsync(int ID)
        {
            var number = await clsPhoneNumbersData.GetByIdAsync(ID);

            return number != null ? new clsPhoneNumbers(number, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsPhoneNumbersData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsPhoneNumbersData.IsExistsAsync(ID);
        }
    }
}
