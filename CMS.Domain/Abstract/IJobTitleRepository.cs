using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Domain.Abstract
{
    public interface IJobTitleRepository
    {
        void Create(JobTitles m_JobTitle);
        JobTitles RetrieveOne(int id);
        List<JobTitles> RetrieveAll();
        void Update(JobTitles m_JobTitle);
        void Delete(int id);
    }
}
