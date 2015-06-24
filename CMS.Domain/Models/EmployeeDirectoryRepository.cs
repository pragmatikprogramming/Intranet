using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;
using CMS.Domain.HelperClasses;

namespace CMS.Domain.Models
{
    public class EmployeeDirectoryRepository : IEmployeeDirectoryRepository
    {
        public void Create(Employee m_Employee)
        {
            DBEmployeeDirectory.Create(m_Employee);
        }

        public Employee RetrieveOne(int id)
        {
            Employee m_Employee = DBEmployeeDirectory.RetrieveOne(id);
            return m_Employee;
        }

        public List<Employee> RetrieveAll()
        {
            List<Employee> m_Employees = DBEmployeeDirectory.RetrieveAll();
            return m_Employees;
        }

        public void Update(Employee m_Employee)
        {
            DBEmployeeDirectory.Update(m_Employee);
        }

        public void Delete(int id)
        {
            DBEmployeeDirectory.Delete(id);
        }

        public string getLocation(int id)
        {
            string branchName = Utility.getBranchName(id);
            return branchName;
        }

        public string getJobTitle(int id)
        {
            JobTitles m_Job = DBJobTitle.RetrieveOne(id);
            return m_Job.JobTitle;
        }

        public List<Employee> getEmployeeFiltered(int m_Filter, string m_Order)
        {
            List<Employee> m_Employees = DBEmployeeDirectory.RetrieveFiltered(m_Filter, m_Order);
            return m_Employees;
        }
    }
}