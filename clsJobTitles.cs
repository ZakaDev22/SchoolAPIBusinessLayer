using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsJobTitles
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public JobTitleDTO jobTitleDTO
        {
            get
            {
                return new JobTitleDTO(this.ID, this.Name);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public clsJobTitles(JobTitleDTO jobtitleDTO, enMode mode)
        {
            Mode = mode;
            ID = jobtitleDTO.ID;
            Name = jobtitleDTO.Name;
        }

        public static async Task<IEnumerable<JobTitleDTO>> GetAllAsync()
        {
            var list = await clsJobTitlesData.GetAllAsync();

            return list == null ? Enumerable.Empty<JobTitleDTO>() : list;
        }

        public static async Task<clsJobTitles> GetByIDAsync(int ID)
        {
            var jobTitle = await clsJobTitlesData.GetByIdAsync(ID);

            return jobTitle != null ? new clsJobTitles(jobTitle, enMode.Update) : null;
        }

        public static async Task<clsJobTitles> GetByjoobTitleNameAsync(string title)
        {
            var jobTitle = await clsJobTitlesData.GetByJobTitleNameAsync(title);

            return jobTitle != null ? new clsJobTitles(jobTitle, enMode.Update) : null;
        }
    }
}
