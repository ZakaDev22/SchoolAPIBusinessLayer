using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsStates
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public stateDTO stateDTO
        {
            get
            {
                return new stateDTO(this.ID, this.CountryID, this.Name);
            }
        }

        public int ID { get; set; }
        public int CountryID { get; set; }
        public string Name { get; set; }

        public clsStates(stateDTO stateDTO, enMode mode)
        {
            Mode = mode;
            ID = stateDTO.ID;
            CountryID = stateDTO.CountryID;
            Name = stateDTO.Name;
        }

        public static async Task<IEnumerable<stateDTO>> GetAllAsync()
        {
            var list = await clsStatesData.GetAllAsync();

            return list == null ? Enumerable.Empty<stateDTO>() : list;
        }
        public static async Task<IEnumerable<stateDTO>> GetAllStatsByCountryIDAsync(int ID)
        {
            var list = await clsStatesData.GetAllStatesByCountryIDAsync(ID);

            return list == null ? Enumerable.Empty<stateDTO>() : list;
        }

        public static async Task<clsStates> GetByIDAsync(int ID)
        {
            var state = await clsStatesData.GetByIdAsync(ID);

            return state != null ? new clsStates(state, enMode.Update) : null;
        }

        public static async Task<clsStates> GetByStateNameAsync(string Name)
        {
            var state = await clsStatesData.GetByStateNameAsync(Name);

            return state != null ? new clsStates(state, enMode.Update) : null;
        }
    }
}
