using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsParents
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public parentDTO parentDTO
        {
            get
            {

                return new parentDTO(this.ID, this.PersonID, this.RelationshipID, this.StudentID);
            }
        }

        public int ID { get; set; }
        public int PersonID { get; set; }
        public int RelationshipID { get; set; }
        public int StudentID { get; set; }

        public clsParents(parentDTO parent, enMode mode)
        {
            _Mode = mode;

            ID = parent.ID;
            PersonID = parent.PersonID;
            RelationshipID = parent.RelationshipTypeID;
            StudentID = parent.StudentID;

        }

        private async Task<bool> _AddNewAsync(parentDTO parent)
        {
            this.ID = await clsParentsData.AddAsync(parent);

            return (this.ID != -1);
        }

        private async Task<bool> _UpdateAsync(parentDTO parent)
        {
            return await clsParentsData.UpdateAsync(parent);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(parentDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(parentDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<parentDTO>> GetAllAsync()
        {
            var list = await clsParentsData.GetAllAsync();

            return list == null ? Enumerable.Empty<parentDTO>() : list;
        }

        public static async Task<clsParents> GetByIDAsync(int ID)
        {
            var parent = await clsParentsData.GetByIdAsync(ID);

            return parent != null ? new clsParents(parent, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsParentsData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsParentsData.IsExistsAsync(ID);
        }

        public static async Task<bool> IsExistsByPersonIDAsync(int ID)
        {
            return await clsParentsData.IsExistsByPersonIDAsync(ID);
        }
    }
}
