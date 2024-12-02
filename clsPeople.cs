
using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{

    public class clsPeople
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public PersonDTO pDTO
        {
            get
            {

                return new PersonDTO(this.ID, this.FirstName, this.LastName, this.DateOfBirth, this.Gender,
                                        this.SchoolID, this.AddressID);
            }
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public int SchoolID { get; set; }
        public int AddressID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public clsPeople(PersonDTO personDTO, enMode cMode = enMode.AddNew)
        {


            this.ID = personDTO.ID;
            this.FirstName = personDTO.FirstName;
            this.LastName = personDTO.LastName;
            this.DateOfBirth = personDTO.DateOfBirth;
            this.Gender = personDTO.Gender;
            this.SchoolID = personDTO.SchoolID;
            this.AddressID = personDTO.AddressID;

            Mode = cMode;
        }

        private async Task<bool> _AddNewAsync(PersonDTO personDTO)
        {
            this.ID = await clsPeopleData.AddAsync(personDTO);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(PersonDTO personDTO)
        {
            return await clsPeopleData.UpdateAsync(personDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(pDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(pDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var list = await clsPeopleData.GetAllAsync();

            return list == null ? Enumerable.Empty<PersonDTO>() : list;
        }

        public static async Task<clsPeople> GetByIDAsync(int ID)
        {
            PersonDTO personDTO = await clsPeopleData.GetByIdAsync(ID);

            return personDTO != null ? new clsPeople(personDTO, enMode.Update) : null;
        }

        public static async Task<clsPeople> GetByNameAsync(string firstName, string lastName)
        {
            PersonDTO personDTO = await clsPeopleData.GetByNameAsync(firstName, lastName);

            return personDTO != null ? new clsPeople(personDTO, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsPeopleData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsByIDAsync(int ID)
        {
            return await clsPeopleData.IsExistsByIDAsync(ID);
        }

        public static async Task<bool> IsExistsByNameAsync(string firstName, string lastName)
        {
            return await clsPeopleData.IsExistsByNameAsync(firstName, lastName);
        }


    }




}
