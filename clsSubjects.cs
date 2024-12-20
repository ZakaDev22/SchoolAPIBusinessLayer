﻿using SchoolAPiDataAccessLayer;

namespace SchoolBusinessLayer
{
    public class clsSubjects
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode _Mode = enMode.AddNew;

        public subjectDTO sbjDTO
        {
            get
            {

                return new subjectDTO(this.ID, this.Name, this.Code, this.SchoolID);
            }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int SchoolID { get; set; }

        public clsSubjects(subjectDTO subject, enMode mode)
        {
            _Mode = mode;

            ID = subject.ID;
            Name = subject.Name;
            Code = subject.Code;
            SchoolID = subject.SchoolID;


        }

        private async Task<bool> _AddNewAsync(subjectDTO sDTO)
        {
            this.ID = await clsSubjectsData.AddAsync(sDTO);

            return this.ID != -1;
        }

        private async Task<bool> _UpdateAsync(subjectDTO sDTO)
        {
            return await clsSubjectsData.UpdateAsync(sDTO);
        }

        public async Task<bool> SaveAsync()
        {
            switch (_Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewAsync(sbjDTO))
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateAsync(sbjDTO);
            }
            return false;
        }

        public static async Task<IEnumerable<subjectDTO>> GetAllAsync()
        {
            var list = await clsSubjectsData.GetAllAsync();

            return list == null ? Enumerable.Empty<subjectDTO>() : list;
        }

        public static async Task<clsSubjects> GetByIDAsync(int ID)
        {
            var subject = await clsSubjectsData.GetByIdAsync(ID);

            return subject != null ? new clsSubjects(subject, enMode.Update) : null;
        }
        public static async Task<clsSubjects> GetBySubjectCodeAsync(string subjectCode)
        {
            var subject = await clsSubjectsData.GetBySubjectCodeAsync(subjectCode);

            return subject != null ? new clsSubjects(subject, enMode.Update) : null;
        }

        public static async Task<bool> DeleteAsync(int ID)
        {
            return await clsSubjectsData.DeleteAsync(ID);
        }

        public static async Task<bool> IsExistsAsync(int ID)
        {
            return await clsSubjectsData.IsExistsAsync(ID);
        }
    }
}
