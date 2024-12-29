using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsCountries
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public countryDTO countryDTO
        {
            get
            {
                return new countryDTO(this.ID, this.Name, this.Code);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public clsCountries(countryDTO countryDTO, enMode mode)
        {
            Mode = mode;
            ID = countryDTO.ID;
            Name = countryDTO.Name;
            Code = countryDTO.Code;
        }

        public static async Task<IEnumerable<countryDTO>> GetAllAsync()
        {
            var list = await clsCountriesData.GetAllAsync();

            return list == null ? Enumerable.Empty<countryDTO>() : list;
        }

        public static async Task<clsCountries> GetByIDAsync(int ID)
        {
            var country = await clsCountriesData.GetByIdAsync(ID);

            return country != null ? new clsCountries(country, enMode.Update) : null;
        }

        public static async Task<clsCountries> GetByCountryNameAsync(string Name)
        {
            var country = await clsCountriesData.GetByCountryNameAsync(Name);

            return country != null ? new clsCountries(country, enMode.Update) : null;
        }

        public static async Task<clsCountries> GetByCountryCodeAsync(string Code)
        {
            var country = await clsCountriesData.GetByCountryCodeAsync(Code);

            return country != null ? new clsCountries(country, enMode.Update) : null;
        }

    }
}
