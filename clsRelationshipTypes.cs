using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsRelationshipTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public RelationshipTypeDTO relationshipTypeDTO
        {
            get
            {
                return new RelationshipTypeDTO(this.ID, this.Name);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public clsRelationshipTypes(RelationshipTypeDTO relTypeDTO, enMode mode)
        {
            Mode = mode;
            ID = relTypeDTO.ID;
            Name = relTypeDTO.Name;
        }

        public static async Task<IEnumerable<RelationshipTypeDTO>> GetAllAsync()
        {
            var list = await clsRelationshipTypesData.GetAllAsync();

            return list == null ? Enumerable.Empty<RelationshipTypeDTO>() : list;
        }

        public static async Task<clsRelationshipTypes> GetByIDAsync(int ID)
        {
            var relationType = await clsRelationshipTypesData.GetByIdAsync(ID);

            return relationType != null ? new clsRelationshipTypes(relationType, enMode.Update) : null;
        }

    }
}
