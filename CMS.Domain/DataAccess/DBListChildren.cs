using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBListChildren
    {
        public static void CreateChild(ListChild m_Child)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_ListChildren(listId, childLabel, childLink) VALUES(@listId, @childLabel, @childLink)";
            SqlCommand insChild = new SqlCommand(queryString, conn);
            insChild.Parameters.AddWithValue("listId", m_Child.ListId);
            insChild.Parameters.AddWithValue("childLabel", m_Child.Label);
            insChild.Parameters.AddWithValue("childLink", m_Child.Link);
            insChild.ExecuteNonQuery();

            conn.Close();
        }

        public static ListChild RetrieveChild(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_ListChildren WHERE id = @id";
            SqlCommand getChild = new SqlCommand(queryString, conn);
            getChild.Parameters.AddWithValue("id", id);
            SqlDataReader childReader = getChild.ExecuteReader();

            ListChild m_Child = new ListChild();

            if(childReader.Read())
            {
                m_Child.Id = childReader.GetInt32(0);
                m_Child.ListId = childReader.GetInt32(1);
                m_Child.Label = childReader.GetString(2);
                m_Child.Link = childReader.GetString(3);
            }
            else
            {
                m_Child = null;
            }

            conn.Close();

            return m_Child;
        }

        public static List<ListChild> RetrieveAllChildren(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_ListChildren WHERE listId = @id";
            SqlCommand getChildren = new SqlCommand(queryString, conn);
            getChildren.Parameters.AddWithValue("id", id);
            SqlDataReader childReader = getChildren.ExecuteReader();

            List<ListChild> m_Children = new List<ListChild>();

            while(childReader.Read())
            {
                ListChild m_Child = new ListChild();

                m_Child.Id = childReader.GetInt32(0);
                m_Child.ListId = childReader.GetInt32(1);
                m_Child.Label = childReader.GetString(2);
                m_Child.Link = childReader.GetString(3);

                m_Children.Add(m_Child);
            }

            conn.Close();

            return m_Children;
        }

        public static void UpdateChild(ListChild m_Child)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_ListChildren SET childLabel = @childLabel, childLink = @childLink WHERE id = @id";
            SqlCommand updChild = new SqlCommand(queryString, conn);
            updChild.Parameters.AddWithValue("childLabel", m_Child.Label);
            updChild.Parameters.AddWithValue("childLink", m_Child.Link);
            updChild.Parameters.AddWithValue("id", m_Child.Id);
            updChild.ExecuteNonQuery();

            conn.Close();
        }

        public static void DeleteChild(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_ListChildren WHERE id = @id";
            SqlCommand delChild = new SqlCommand(queryString, conn);
            delChild.Parameters.AddWithValue("id", id);
            delChild.ExecuteNonQuery();

            conn.Close();
        }
    }
}