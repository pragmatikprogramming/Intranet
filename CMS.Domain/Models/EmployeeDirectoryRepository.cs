﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;

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
    }
}