using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;

namespace CMS.Domain.Models
{
    public class JobTitleRepository : IJobTitleRepository
    {
        public void Create(JobTitles m_JobTitle)
        {
            DBJobTitle.Create(m_JobTitle);
        }

        public JobTitles RetrieveOne(int id)
        {
            JobTitles m_JobTitle = DBJobTitle.RetrieveOne(id);
            return m_JobTitle;
        }

        public List<JobTitles> RetrieveAll()
        {
            List<JobTitles> m_JobTitle = DBJobTitle.RetrieveAll();
            return m_JobTitle;
        }

        public void Update(JobTitles m_JobTitle)
        {
            DBJobTitle.Update(m_JobTitle);
        }

        public void Delete(int id)
        {
            DBJobTitle.Delete(id);
        }
    }
}