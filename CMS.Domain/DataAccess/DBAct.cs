using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBAct
    {
        public static void Create(Act m_Act)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_Acts(performerId, programTitle, description, cost, duration, notes) VALUES(@performerId, @programTitle, @description, @cost, @duration, @notes)";
            SqlCommand insAct = new SqlCommand(queryString, conn);
            insAct.Parameters.AddWithValue("performerId", m_Act.PerformerId);
            insAct.Parameters.AddWithValue("programTitle", m_Act.ProgramTitle ?? "");
            insAct.Parameters.AddWithValue("description", m_Act.Description ?? "");
            insAct.Parameters.AddWithValue("cost", m_Act.Cost);
            insAct.Parameters.AddWithValue("duration", m_Act.Duration);
            insAct.Parameters.AddWithValue("notes", m_Act.Notes);
            insAct.ExecuteNonQuery();

            queryString = "SELECT IDENT_CURRENT('CMS_Acts')";
            SqlCommand getActId = new SqlCommand(queryString, conn);
            int m_ActId = (int)(decimal)getActId.ExecuteScalar();


            if (m_Act.Audiences != null)
            {
                foreach (int audience in m_Act.Audiences)
                {
                    queryString = "INSERT INTO CMS_ActsToAudiences(actId, audienceId) VALUES(@actId, @audienceId)";
                    SqlCommand insAct2Aud = new SqlCommand(queryString, conn);
                    insAct2Aud.Parameters.AddWithValue("actId", m_ActId);
                    insAct2Aud.Parameters.AddWithValue("audienceId", audience);

                    insAct2Aud.ExecuteNonQuery();
                }
            }

            if (m_Act.Branches != null)
            {
                foreach (int location in m_Act.Branches)
                {
                    queryString = "INSERT INTO CMS_ActsToBranches(branchId, actId) VALUES(@branchId, @actId)";
                    SqlCommand insAct2Branch = new SqlCommand(queryString, conn);
                    insAct2Branch.Parameters.AddWithValue("branchId", location);
                    insAct2Branch.Parameters.AddWithValue("actId", m_ActId);

                    insAct2Branch.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        public static Act RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT id, performerId, programTitle, description, cost, duration, notes FROM CMS_Acts WHERE id = @id";
            SqlCommand getAct = new SqlCommand(queryString, conn);
            getAct.Parameters.AddWithValue("id", id);
            SqlDataReader actReader = getAct.ExecuteReader();

            Act m_Act = new Act();

            if(actReader.Read())
            {
                m_Act.Id = actReader.GetInt32(0);
                m_Act.PerformerId = actReader.GetInt32(1);
                m_Act.ProgramTitle = actReader.GetString(2);
                m_Act.Description = actReader.GetString(3);
                m_Act.Cost = actReader.GetDouble(4);
                m_Act.Duration = actReader.GetDouble(5);
                m_Act.Notes = actReader.GetString(6);
            }
            else
            {
                m_Act.Id = 0;
            }

            conn.Close();
            conn.Open();

            queryString = "SELECT * FROM CMS_ActsToAudiences WHERE actId = @actId";
            SqlCommand getAud = new SqlCommand(queryString, conn);
            getAud.Parameters.AddWithValue("actId", m_Act.Id);
            SqlDataReader audReader = getAud.ExecuteReader();

            m_Act.Audiences = new List<int>();

            while(audReader.Read())
            {
                m_Act.Audiences.Add(audReader.GetInt32(1));
            }

            conn.Close();
            conn.Open();

            queryString = "SELECT * FROM CMS_ActsToBranches WHERE actId = @actId";
            SqlCommand getBranch = new SqlCommand(queryString, conn);
            getBranch.Parameters.AddWithValue("actId", m_Act.Id);
            SqlDataReader branchReader = getBranch.ExecuteReader();

            m_Act.Branches = new List<int>();

            while(branchReader.Read())
            {
                m_Act.Branches.Add(branchReader.GetInt32(0));
            }

            conn.Close();

            return m_Act;
        }

        public static List<Act> RetrieveAll(int performerId)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT id, performerId, programTitle, description, cost, duration, notes FROM CMS_Acts WHERE performerId = @performerId ORDER BY programTitle";
            SqlCommand getAct = new SqlCommand(queryString, conn);
            getAct.Parameters.AddWithValue("performerId", performerId);
            SqlDataReader actReader = getAct.ExecuteReader();

            List<Act> m_Acts = new List<Act>();

            while(actReader.Read())
            {
                Act m_Act = new Act();
                m_Act.Id = actReader.GetInt32(0);
                m_Act.PerformerId = actReader.GetInt32(1);
                m_Act.ProgramTitle = actReader.GetString(2);
                m_Act.Description = actReader.GetString(3);
                m_Act.Cost = actReader.GetDouble(4);
                m_Act.Duration = actReader.GetDouble(5);
                m_Act.Notes = actReader.GetString(6);

                m_Acts.Add(m_Act);
            }

            conn.Close();

            return m_Acts;
        }

        public static void Update(Act m_Act)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_Acts SET performerId = @performerId, programTitle = @programTitle, description = @description, cost = @cost, duration = @duration, notes = @notes WHERE id = @id";
            SqlCommand updAct = new SqlCommand(queryString, conn);
            updAct.Parameters.AddWithValue("performerId", m_Act.PerformerId);
            updAct.Parameters.AddWithValue("programTitle", m_Act.ProgramTitle ?? "");
            updAct.Parameters.AddWithValue("description", m_Act.Description ?? "");
            updAct.Parameters.AddWithValue("cost", m_Act.Cost);
            updAct.Parameters.AddWithValue("duration", m_Act.Duration);
            updAct.Parameters.AddWithValue("notes", m_Act.Notes ?? "");
            updAct.Parameters.AddWithValue("id", m_Act.Id);

            updAct.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_ActsToAudiences WHERE actId = @actId";
            SqlCommand delA2A = new SqlCommand(queryString, conn);
            delA2A.Parameters.AddWithValue("actId", m_Act.Id);
            delA2A.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_ActsToBranches WHERE actId = @actId";
            SqlCommand delA2B = new SqlCommand(queryString, conn);
            delA2B.Parameters.AddWithValue("actId", m_Act.Id);
            delA2B.ExecuteNonQuery();

            if (m_Act.Audiences != null)
            {
                foreach (int audience in m_Act.Audiences)
                {
                    queryString = "INSERT INTO CMS_ActsToAudiences(actId, audienceId) VALUES(@actId, @audienceId)";
                    SqlCommand insAct2Aud = new SqlCommand(queryString, conn);
                    insAct2Aud.Parameters.AddWithValue("actId", m_Act.Id);
                    insAct2Aud.Parameters.AddWithValue("audienceId", audience);

                    insAct2Aud.ExecuteNonQuery();
                }
            }

            if (m_Act.Branches != null)
            {
                foreach (int location in m_Act.Branches)
                {
                    queryString = "INSERT INTO CMS_ActsToBranches(branchId, actId) VALUES(@branchId, @actId)";
                    SqlCommand insAct2Branch = new SqlCommand(queryString, conn);
                    insAct2Branch.Parameters.AddWithValue("branchId", location);
                    insAct2Branch.Parameters.AddWithValue("actId", m_Act.Id);

                    insAct2Branch.ExecuteNonQuery();
                }
            }
            conn.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_Acts WHERE id = @id";
            SqlCommand delAct = new SqlCommand(queryString, conn);
            delAct.Parameters.AddWithValue("id", id);
            delAct.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_Reviews WHERE actId = @id";
            SqlCommand delRev = new SqlCommand(queryString, conn);
            delRev.Parameters.AddWithValue("id", id);
            delRev.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_ActsToAudiences WHERE actId = @id";
            SqlCommand delA2A = new SqlCommand(queryString, conn);
            delA2A.Parameters.AddWithValue("id", id);
            delA2A.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_ActsToBranches WHERE actId = @id";
            SqlCommand delA2B = new SqlCommand(queryString, conn);
            delA2B.Parameters.AddWithValue("id", id);

            conn.Close();
        }

        public static double getAverageRating(int id)
        {
            double avgRating;

            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "select ROUND(AVG(CAST(rating as FLOAT)), 2) from CMS_Reviews WHERE actId = @id";
            SqlCommand getRating = new SqlCommand(queryString, conn);
            getRating.Parameters.AddWithValue("id", id);
            avgRating = (double)getRating.ExecuteScalar();

            conn.Close();

            return avgRating;
        }

        public static int numReviews(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "select COUNT(rating) from CMS_Reviews WHERE actId = @id";
            SqlCommand getRev = new SqlCommand(queryString, conn);
            getRev.Parameters.AddWithValue("id", id);
            int numReviews = (int)getRev.ExecuteScalar();

            conn.Close();

            return numReviews;
        }
    }
}