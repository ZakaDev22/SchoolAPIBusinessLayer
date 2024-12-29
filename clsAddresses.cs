using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsAddresses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public addressDTO addressDTO
        {
            get
            {
                return new addressDTO(this.ID, this.Street, this.City, this.StateID, this.CountryID);
            }
        }

        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }

        public clsAddresses(addressDTO addsDTO, enMode mode)
        {
            Mode = mode;
            ID = addsDTO.ID;
            Street = addsDTO.Street;
            City = addsDTO.City;
            StateID = addsDTO.StateID;
            CountryID = addsDTO.CountryID;
        }

        private async Task<bool> _AddNewAsync(addressDTO atd)
        {
            this.ID = await clsAddressesData.AddAsync(atd);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(addressDTO atd)
        {
            return await clsAddressesData.UpdateAsync(atd);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(addressDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(addressDTO);
            }
            return false;
        }


        public static async Task<IEnumerable<addressDTO>> GetAllAsync()
        {
            var list = await clsAddressesData.GetAllAsync();

            return list == null ? Enumerable.Empty<addressDTO>() : list;
        }

        public static async Task<IEnumerable<addressDTO>> GetByCityNameAsync(string city)
        {
            var list = await clsAddressesData.GetByCityNameAsync(city);

            return list == null ? Enumerable.Empty<addressDTO>() : list;
        }

        public static async Task<clsAddresses> GetByIDAsync(int ID)
        {
            var address = await clsAddressesData.GetByIdAsync(ID);

            return address != null ? new clsAddresses(address, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsAddressesData.DeleteAsync(ID);
        }

    }
}
