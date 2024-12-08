using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsEmails
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public EmailDTO emailDTO
        {
            get
            {
                return new EmailDTO(this.ID, this.Address, this.PersonID, this.EmailTypeID, this.IsPrimary);
            }
        }

        public int ID { get; set; }
        public string Address { get; set; }
        public int PersonID { get; set; }
        public int EmailTypeID { get; set; }
        public bool IsPrimary { get; set; }

        public clsEmails(EmailDTO email, enMode mode)
        {
            Mode = mode;
            ID = email.ID;
            Address = email.Address;
            PersonID = email.PersonID;
            EmailTypeID = email.EmailTypeID;
            IsPrimary = email.IsPrimary;
        }


        public static async Task<IEnumerable<EmailDTO>> GetAllAsync()
        {
            var list = await clsEmailsData.GetAllAsync();

            return list == null ? Enumerable.Empty<EmailDTO>() : list;
        }

        public static async Task<clsEmails> GetByIDAsync(int ID)
        {
            var email = await clsEmailsData.GetByIdAsync(ID);

            return email != null ? new clsEmails(email, enMode.Update) : null;
        }

    }
}
