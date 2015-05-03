using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBAudience
    {
        public static void Create(Audience m_Audience)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_Audiences(Audience) VALUES(@audience)";
            SqlCommand insAud = new SqlCommand(queryString, conn);
            insAud.Parameters.AddWithValue("audience", m_Audience.Name ?? "");
            insAud.ExecuteNonQuery();

            conn.Close();
        }

        public static Audience RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT id, audience FROM CMS_Audiences WHERE id = @id";
            SqlCommand getAud = new SqlCommand(queryString, conn);
            getAud.Parameters.AddWithValue("id", id);
            SqlDataReader audReader = getAud.ExecuteReader();

            Audience m_Audience = new Audience();

            if(audReader.Read())
            {
                m_Audience.Id = audReader.GetInt32(0);
                m_Audience.Name = audReader.GetString(1);
            }
            else
            {
                m_Audience.Id = 0;
            }

            conn.Close();

            return m_Audience;
        }

        public static List<Audience> RetrieveAll()
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT id, audience FROM CMS_Audiences ORDER BY audience";
            SqlCommand getAud = new SqlCommand(queryString, conn);
            SqlDataReader audReader = getAud.ExecuteReader();

            List<Audience> m_Audiences = new List<Audience>();

            while(audReader.Read())
            {
                Audience m_Audience = new Audience();
                m_Audience.Id = audReader.GetInt32(0);
                m_Audience.Name = audReader.GetString(1);

                m_Audiences.Add(m_Audience);
            }

            conn.Close();

            return m_Audiences;
        }

        public static void Update(Audience m_Audience)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_Audiences SET audience = @audience WHERE id = @id";
            SqlCommand updAud = new SqlCommand(queryString, conn);
            updAud.Parameters.AddWithValue("audience", m_Audience.Name);
            updAud.Parameters.AddWithValue("id", m_Audience.Id);
            updAud.ExecuteNonQuery();

            conn.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_Audiences WHERE id = @id";
            SqlCommand delAud = new SqlCommand(queryString, conn);
            delAud.Parameters.AddWithValue("id", id);
            delAud.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_ActsToAudiences WHERE audienceId = @id";
            SqlCommand delA2A = new SqlCommand(queryString, conn);
            delA2A.Parameters.AddWithValue("id", id);
            delA2A.ExecuteNonQuery();

            conn.Close();
        }
    }
}