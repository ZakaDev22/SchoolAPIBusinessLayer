using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsUserRegister
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public userRegisterDTO RegisterDTO
        {
            get
            {
                return new userRegisterDTO(this.RegisterID, this.UserID, this.LoginTime, this.LogoutTime, this.IPAdrress, this.SessionDuration);
            }
        }


        public int RegisterID { get; set; }
        public int UserID { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string IPAdrress { get; set; }
        public int? SessionDuration { get; set; }


        public clsUserRegister(userRegisterDTO userRegisterDTO, enMode mode)
        {
            Mode = mode;

            RegisterID = userRegisterDTO.RegisterID;
            UserID = userRegisterDTO.UserID;
            LoginTime = userRegisterDTO.LoginTime;
            LogoutTime = userRegisterDTO.LogoutTime;
            IPAdrress = userRegisterDTO.IPAddress;
            SessionDuration = userRegisterDTO.SessionDuration;
        }

        private async Task<bool> _AddNewAsync(userRegisterDTO sDTO)
        {
            this.RegisterID = await clsUserRegisterData.AddAsync(sDTO);

            return this.RegisterID != -1;
        }

        private async Task<bool> _UpdateAsync(int registerID)
        {
            return await clsUserRegisterData.UpdateAsync(registerID);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(RegisterDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(RegisterID);
            }
            return false;
        }

        public static async Task<IEnumerable<userRegisterDTO>> GetAllAsync()
        {
            var list = await clsUserRegisterData.GetAllAsync();

            return list == null ? Enumerable.Empty<userRegisterDTO>() : list;
        }

        public static async Task<clsUserRegister> GetByIDAsync(int ID)
        {
            var register = await clsUserRegisterData.GetByIdAsync(ID);

            return register != null ? new clsUserRegister(register, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsUserRegisterData.DeleteAsync(ID);
        }



    }
}
