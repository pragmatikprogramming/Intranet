using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;

namespace CMS.Domain.Abstract
{
    public interface IEmployeeDirectoryRepository
    {
        void Create(Employee m_Employee);
        Employee RetrieveOne(int id);
        List<Employee> RetrieveAll();
        void Update(Employee m_Employee);
        void Delete(int id);
        string getLocation(int id);
        string getJobTitle(int id);
    }
}
