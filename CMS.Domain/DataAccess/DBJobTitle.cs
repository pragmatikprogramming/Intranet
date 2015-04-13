using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBJobTitle
    {
        public static void Create(JobTitles m_JobTitle)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_JobTitles(jobTitle) VALUES(@jobTitle)";
            SqlCommand insJob = new SqlCommand(queryString, conn);
            insJob.Parameters.AddWithValue("jobTitle", m_JobTitle.JobTitle);
            insJob.ExecuteNonQuery();

            conn.Close();
        }

        public static JobTitles RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_JobTitles WHERE id = @id";
            SqlCommand getJob = new SqlCommand(queryString, conn);
            getJob.Parameters.AddWithValue("id", id);
            SqlDataReader jobReader = getJob.ExecuteReader();

            JobTitles m_JobTitle = new JobTitles();

            if(jobReader.Read())
            {
                m_JobTitle.Id = jobReader.GetInt32(0);
                m_JobTitle.JobTitle = jobReader.GetString(1);
            }
            else
            {
                m_JobTitle = null;
            }

            conn.Close();

            return m_JobTitle;
        }

        public static List<JobTitles> RetrieveAll()
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_JobTitles ORDER BY jobTitle ASC";
            SqlCommand getJobs = new SqlCommand(queryString, conn);
            SqlDataReader jobsReader = getJobs.ExecuteReader();

            List<JobTitles> m_JobTitles = new List<JobTitles>();

            while(jobsReader.Read())
            {
                JobTitles m_JobTitle = new JobTitles();
                m_JobTitle.Id = jobsReader.GetInt32(0);
                m_JobTitle.JobTitle = jobsReader.GetString(1);

                m_JobTitles.Add(m_JobTitle);
            }

            conn.Close();

            return m_JobTitles;
        }

        public static void Update(JobTitles m_JobTitle)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_JobTitles SET jobTitle = @jobTitle WHERE id = @id";
            SqlCommand updJob = new SqlCommand(queryString, conn);
            updJob.Parameters.AddWithValue("jobTitle", m_JobTitle.JobTitle);
            updJob.Parameters.AddWithValue("id", m_JobTitle.Id);
            updJob.ExecuteNonQuery();

            conn.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_JobTitles WHERE id = @id";
            SqlCommand delJob = new SqlCommand(queryString, conn);
            delJob.Parameters.AddWithValue("id", id);
            delJob.ExecuteNonQuery();

            conn.Close();
        }
    }
}