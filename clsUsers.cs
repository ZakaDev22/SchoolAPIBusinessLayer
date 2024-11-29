
using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsUsers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public UserDTO UserDTO
        {
            get
            {
                return new UserDTO(this.userID, this.personID, this.userName, this.permissions, this.AddedByUserID);
            }
        }

        public FullUserDTO fullUserDTO
        {
            get
            {
                return new FullUserDTO(this.userID, this.personID, this.userName, this.passwordHash, this.permissions, this.AddedByUserID);
            }
        }

        public int userID { get; set; }
        public int personID { get; set; }
        public string userName { get; set; }
        public string passwordHash { get; set; }
        public int permissions { get; set; }
        public int? AddedByUserID { get; set; }

        public clsUsers(enMode mode, FullUserDTO fullUserDTO)
        {
            Mode = mode;
            this.userID = fullUserDTO.UserID;
            this.personID = fullUserDTO.PersonID;
            this.userName = fullUserDTO.UserName;
            this.passwordHash = fullUserDTO.Password;
            this.permissions = fullUserDTO.Permissions;
            this.AddedByUserID = fullUserDTO.AddedByUserID;
        }

        // Method to get all users
        public static async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await clsUsersData.GetAllAsync();
        }

        // Method to get a user by ID
        public static async Task<clsUsers> GetUserByIdAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than zero.");

            var fullUserDTO = await clsUsersData.GetByIdAsync(userId);

            return fullUserDTO != null ? new clsUsers(enMode.Update, fullUserDTO) : null;

        }

        // Method to add a new user
        private async Task<bool> _AddNewAsync(FullUserDTO userDTO)
        {
            this.userID = await clsUsersData.AddAsync(userDTO);

            return (this.userID != -1);
        }

        private async Task<bool> _UpdateAsync(FullUserDTO userDTO)
        {
            return await clsUsersData.UpdateAsync(userDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(fullUserDTO))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(fullUserDTO);
            }
            return false;
        }

        // Method to delete a user by ID
        public static async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than zero.");

            return await clsUsersData.DeleteAsync(userId);
        }

        // Method to check if a user exists by ID
        public static async Task<bool> UserExistsAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than zero.");

            return await clsUsersData.ExistsByID(userId);
        }

        // Method to authenticate a user by username and password
        public static async Task<clsUsers> AuthenticateUserAsync(string userName, string passwordHash)
        {
            var fulUserDTO = await clsUsersData.FindByUserNameAndPasswordAsync(userName, passwordHash);

            return fulUserDTO != null ? new clsUsers(enMode.Update, fulUserDTO) : null;
        }
    }
}
