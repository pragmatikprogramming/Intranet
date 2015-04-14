﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBEmployeeDirectory
    {
        public static void Create(Employee m_Employee)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_EmployeeDirectory(firstName, lastName, jobTitle, phone, intercom, fax, email, location, info, about";
            if(m_Employee.Photo != null && m_Employee.Photo.Length > 0)
            {
                queryString += ", photo";
            }
            
            queryString += ") VALUES(@firstName, @lastName, @jobTitle, @phone, @intercom, @fax, @email, @location, @info, @about";

            if (m_Employee.Photo != null && m_Employee.Photo.Length > 0)
            {
                queryString += ", @photo";
            }

            queryString += ")";

            SqlCommand insEmp = new SqlCommand(queryString, conn);
            insEmp.Parameters.AddWithValue("firstName", m_Employee.FirstName);
            insEmp.Parameters.AddWithValue("lastName", m_Employee.LastName);
            insEmp.Parameters.AddWithValue("jobTitle", m_Employee.JobTitle);
            insEmp.Parameters.AddWithValue("phone", m_Employee.Phone);
            insEmp.Parameters.AddWithValue("intercom", m_Employee.Intercom ?? "");
            insEmp.Parameters.AddWithValue("fax", m_Employee.Fax ?? "");
            insEmp.Parameters.AddWithValue("email", m_Employee.Email);
            insEmp.Parameters.AddWithValue("location", m_Employee.Location);
            insEmp.Parameters.AddWithValue("info", m_Employee.Info ?? "");
            insEmp.Parameters.AddWithValue("about", m_Employee.About ?? "");

            if (m_Employee.Photo != null && m_Employee.Photo.Length > 0)
            {
                insEmp.Parameters.AddWithValue("photo", m_Employee.Photo);
            }

            insEmp.ExecuteNonQuery();

            queryString = "SELECT IDENT_CURRENT('CMS_EmployeeDirectory')";
            SqlCommand empId = new SqlCommand(queryString, conn);
            int m_Id = (int)(decimal)empId.ExecuteScalar();

            foreach (int i in m_Employee.Skills)
            {
                queryString = "INSERT INTO CMS_EmployeeToSkills(employeeId, skillId) VALUES(@employeeId, @skillId)";
                SqlCommand insSkill = new SqlCommand(queryString, conn);
                insSkill.Parameters.AddWithValue("employeeId", m_Id);
                insSkill.Parameters.AddWithValue("skillId", i);
                insSkill.ExecuteNonQuery();
            }
            
            conn.Close();

        }

        public static Employee RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_EmployeeDirectory WHERE id = @id";
            SqlCommand getEmp = new SqlCommand(queryString, conn);
            getEmp.Parameters.AddWithValue("id", id);
            SqlDataReader empReader = getEmp.ExecuteReader();

            Employee m_Employee = new Employee();

            if(empReader.Read())
            {
                m_Employee.Id = empReader.GetInt32(0);
                m_Employee.FirstName = empReader.GetString(1);
                m_Employee.LastName = empReader.GetString(2);
                m_Employee.JobTitle = empReader.GetInt32(3);
                m_Employee.Phone = empReader.GetString(4);
                m_Employee.Intercom = empReader.GetString(5);
                m_Employee.Fax = empReader.GetString(6);
                m_Employee.Email = empReader.GetString(7);
                m_Employee.Location = empReader.GetInt32(8);
                if (!empReader.IsDBNull(9))
                {
                    m_Employee.Photo = (byte[])empReader[9];
                }
                m_Employee.Info = empReader.GetString(10);
                m_Employee.About = empReader.GetString(11);
            }
            else
            {
                m_Employee = null;
            }

            conn.Close();
            conn.Open();

            queryString = "SELECT skillId FROM CMS_EmployeeToSkills WHERE employeeId = @id";
            SqlCommand getSkills = new SqlCommand(queryString, conn);
            getSkills.Parameters.AddWithValue("id", id);
            SqlDataReader skillsReader = getSkills.ExecuteReader();

            m_Employee.Skills = new List<int>();

            while(skillsReader.Read())
            {
                m_Employee.Skills.Add(skillsReader.GetInt32(0));
            }

            return m_Employee;
        }

        public static List<Employee> RetrieveAll()
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_EmployeeDirectory ORDER BY firstName ASC";
            SqlCommand getEmps = new SqlCommand(queryString, conn);
            SqlDataReader empReader = getEmps.ExecuteReader();

            List<Employee> m_Employees = new List<Employee>();

            while (empReader.Read())
            {
                Employee m_Employee = new Employee();

                m_Employee.Id = empReader.GetInt32(0);
                m_Employee.FirstName = empReader.GetString(1);
                m_Employee.LastName = empReader.GetString(2);
                m_Employee.JobTitle = empReader.GetInt32(3);
                m_Employee.Phone = empReader.GetString(4);
                m_Employee.Intercom = empReader.GetString(5);
                m_Employee.Fax = empReader.GetString(6);
                m_Employee.Email = empReader.GetString(7);
                m_Employee.Location = empReader.GetInt32(8);
                if (!empReader.IsDBNull(9))
                {
                    m_Employee.Photo = (byte[])empReader[9];
                }
                m_Employee.Info = empReader.GetString(10);
                m_Employee.About = empReader.GetString(11);

                m_Employees.Add(m_Employee);
            }

            conn.Close();

            return m_Employees;
        }

        public static void Update(Employee m_Employee)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_EmployeeDirectory SET firstName = @firstname, lastName = @lastName, jobTitle = @jobTitle, phone = @phone, intercom = @intercom, fax = @fax, email = @email, location = @location,"; 

            if(m_Employee.Photo != null && m_Employee.Photo.Length > 0)
            {
                queryString += "photo = @photo, info = @info, about = @about WHERE id = @id";
            }
            else
            {
                queryString += "info = @info, about = @about WHERE id = @id";
            }

            SqlCommand updEmp = new SqlCommand(queryString, conn);
            updEmp.Parameters.AddWithValue("firstName", m_Employee.FirstName);
            updEmp.Parameters.AddWithValue("lastName", m_Employee.LastName);
            updEmp.Parameters.AddWithValue("jobTitle", m_Employee.JobTitle);
            updEmp.Parameters.AddWithValue("phone", m_Employee.Phone);
            updEmp.Parameters.AddWithValue("intercom", m_Employee.Intercom ?? "");
            updEmp.Parameters.AddWithValue("fax", m_Employee.Fax ?? "");
            updEmp.Parameters.AddWithValue("email", m_Employee.Email);
            updEmp.Parameters.AddWithValue("location", m_Employee.Location);
            updEmp.Parameters.AddWithValue("info", m_Employee.Info ?? "");
            updEmp.Parameters.AddWithValue("about", m_Employee.About ?? "");
            updEmp.Parameters.AddWithValue("id", m_Employee.Id);

            if(m_Employee.Photo != null && m_Employee.Photo.Length > 0)
            {
                updEmp.Parameters.AddWithValue("photo", m_Employee.Photo);
            }

            updEmp.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_EmployeeToSkills WHERE employeeId = @id";
            SqlCommand delSkills = new SqlCommand(queryString, conn);
            delSkills.Parameters.AddWithValue("id", m_Employee.Id);
            delSkills.ExecuteNonQuery();

            foreach (int i in m_Employee.Skills)
            {
                queryString = "INSERT INTO CMS_EmployeeToSkills(employeeId, skillId) VALUES(@employeeId, @skillId)";
                SqlCommand insSkill = new SqlCommand(queryString, conn);
                insSkill.Parameters.AddWithValue("employeeId", m_Employee.Id);
                insSkill.Parameters.AddWithValue("skillId", i);
                insSkill.ExecuteNonQuery();
            }

            conn.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_EmployeeDirectory WHERE id = @id";
            SqlCommand delEmp = new SqlCommand(queryString, conn);
            delEmp.Parameters.AddWithValue("id", id);
            delEmp.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_EmployeeToSkills WHERE employeeId = @id";
            SqlCommand delSkills = new SqlCommand(queryString, conn);
            delSkills.Parameters.AddWithValue("id", id);
            delSkills.ExecuteNonQuery();

            conn.Close();
        }
    }
}