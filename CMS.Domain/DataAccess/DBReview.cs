using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBReview
    {
        public static void Create(Review m_Review)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_Reviews(actId, name, comments, rating) VALUES(@actId, @name, @comments, @rating)";
            SqlCommand insRev = new SqlCommand(queryString, conn);
            insRev.Parameters.AddWithValue("actId", m_Review.ActId);
            insRev.Parameters.AddWithValue("name", m_Review.Name ?? "");
            insRev.Parameters.AddWithValue("comments", m_Review.Comments ?? "");
            insRev.Parameters.AddWithValue("rating", m_Review.Rating);
            insRev.ExecuteNonQuery();

            conn.Close();
        }

        public static Review RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT id, actId, name, comments, rating FROM CMS_Reviews WHERE id = @id";
            SqlCommand getRev = new SqlCommand(queryString, conn);
            getRev.Parameters.AddWithValue("id", id);
            SqlDataReader revReader = getRev.ExecuteReader();

            Review m_Review = new Review();

            if(revReader.Read())
            {
                m_Review.Id = revReader.GetInt32(0);
                m_Review.ActId = revReader.GetInt32(1);
                m_Review.Name = revReader.GetString(2);
                m_Review.Comments = revReader.GetString(3);
                m_Review.Rating = revReader.GetInt32(4);
            }
            else
            {
                m_Review.Id = 0;
            }

            conn.Close();

            return m_Review;
        }

        public static List<Review> RetrieveAll(int actId)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT id, actId, name, comments, rating FROM CMS_Reviews WHERE actId = @actId";
            SqlCommand getRev = new SqlCommand(queryString, conn);
            getRev.Parameters.AddWithValue("actId", actId);
            SqlDataReader revReader = getRev.ExecuteReader();

            List<Review> m_Reviews = new List<Review>();

            while(revReader.Read())
            {
                Review m_Review = new Review();

                m_Review.Id = revReader.GetInt32(0);
                m_Review.ActId = revReader.GetInt32(1);
                m_Review.Name = revReader.GetString(2);
                m_Review.Comments = revReader.GetString(3);
                m_Review.Rating = revReader.GetInt32(4);

                m_Reviews.Add(m_Review);
            }

            conn.Close();

            return m_Reviews;
        }

        public static void Update(Review m_Review)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_Reviews SET actId = @actId, name= @name, comments = @comments, rating = @rating WHERE id = @id";
            SqlCommand updRev = new SqlCommand(queryString, conn);
            updRev.Parameters.AddWithValue("actId", m_Review.ActId);
            updRev.Parameters.AddWithValue("name", m_Review.Name ?? "");
            updRev.Parameters.AddWithValue("comments", m_Review.Comments ?? "");
            updRev.Parameters.AddWithValue("rating", m_Review.Rating);
            updRev.Parameters.AddWithValue("id", m_Review.Id);
            updRev.ExecuteNonQuery();

            conn.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_Reviews WHERE id = @id";
            SqlCommand delRev = new SqlCommand(queryString, conn);
            delRev.Parameters.AddWithValue("id", id);
            delRev.ExecuteNonQuery();

            conn.Close();
        }
    }
}