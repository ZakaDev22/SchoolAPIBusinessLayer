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


    }
}
