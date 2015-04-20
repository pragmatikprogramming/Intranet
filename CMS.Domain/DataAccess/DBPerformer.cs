using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBPerformer
    {
        public static void Create(Performer m_Performer)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_Performers(firstName, lastName, address, phone, fax, email, website) VALUES(@firstName, @lastName, @address, @phone, @fax, @email, @website";
            SqlCommand insPerf = new SqlCommand(queryString, conn);
            insPerf.Parameters.AddWithValue("firstName", m_Performer.FirstName ?? "");
            insPerf.Parameters.AddWithValue("lastName", m_Performer.LastName ?? "");
            insPerf.Parameters.AddWithValue("firstName", m_Performer.Address ?? "");
            insPerf.Parameters.AddWithValue("address", m_Performer.Phone ?? "");
            insPerf.Parameters.AddWithValue("phone", m_Performer.Phone ?? "");
            insPerf.Parameters.AddWithValue("fax", m_Performer.Fax ?? "");
            insPerf.Parameters.AddWithValue("email", m_Performer.Email ?? "");
            insPerf.Parameters.AddWithValue("website", m_Performer.Website ?? "");

            insPerf.ExecuteNonQuery();
            conn.Close();            
        }

        public static Performer RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT id, firstName, lastName, address, phone, fax, email, website FROM CMS_Performers WHERE id = @id";
            SqlCommand getPerf = new SqlCommand(queryString, conn);
            getPerf.Parameters.AddWithValue("id", id);
            SqlDataReader perfReader = getPerf.ExecuteReader();

            Performer m_Performer = new Performer();

            if(perfReader.Read())
            {
                m_Performer.Id = perfReader.GetInt32(0);
                m_Performer.FirstName = perfReader.GetString(1);
                m_Performer.LastName = perfReader.GetString(2);
                m_Performer.Address = perfReader.GetString(3);
                m_Performer.Phone = perfReader.GetString(4);
                m_Performer.Fax = perfReader.GetString(5);
                m_Performer.Email = perfReader.GetString(6);
                m_Performer.Website = perfReader.GetString(7);
            }
            else
            {
                m_Performer.Id = 0;
            }

            conn.Close();
            return m_Performer;
        }

        public static List<Performer> RetrieveAll()
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT id, firstName, lastName, address, phone, fax, email, website FROM CMS_Performers ORDER BY firstName";
            SqlCommand getPerf = new SqlCommand(queryString, conn);
            SqlDataReader perfReader = getPerf.ExecuteReader();

            List<Performer> m_Performers = new List<Performer>();

            while(perfReader.Read())
            {
                Performer m_Performer = new Performer();
                m_Performer.Id = perfReader.GetInt32(0);
                m_Performer.FirstName = perfReader.GetString(1);
                m_Performer.LastName = perfReader.GetString(2);
                m_Performer.Address = perfReader.GetString(3);
                m_Performer.Phone = perfReader.GetString(4);
                m_Performer.Fax = perfReader.GetString(5);
                m_Performer.Email = perfReader.GetString(6);
                m_Performer.Website = perfReader.GetString(7);

                m_Performers.Add(m_Performer);
            }

            conn.Close();

            return m_Performers;
        }

        public static void Update(Performer m_Performer)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_Performers SET firstName = @firstName, lastName = @lastName, address = @address, phone = @phone, fax = @fax, email = @email, website = @website WHERE id = @id";
            SqlCommand updPerf = new SqlCommand(queryString, conn);
            updPerf.Parameters.AddWithValue("firstName", m_Performer.FirstName ?? "");
            updPerf.Parameters.AddWithValue("lastName", m_Performer.LastName ?? "");
            updPerf.Parameters.AddWithValue("address", m_Performer.Address ?? "");
            updPerf.Parameters.AddWithValue("phone", m_Performer.Phone ?? "");
            updPerf.Parameters.AddWithValue("fax", m_Performer.Fax ?? "");
            updPerf.Parameters.AddWithValue("email", m_Performer.Email ?? "");
            updPerf.Parameters.AddWithValue("website", m_Performer.Website ?? "");
            updPerf.Parameters.AddWithValue("id", m_Performer.Id);
            updPerf.ExecuteNonQuery();

            conn.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_Performers WHERE id = @id";
            SqlCommand delPerf = new SqlCommand(queryString, conn);
            delPerf.Parameters.AddWithValue("id", id);
            delPerf.ExecuteNonQuery();

            foreach(Act m_Act in DBAct.RetrieveAll(id))
            {
                DBAct.Delete(m_Act.Id);
            }

            conn.Close();
        }
    }
}