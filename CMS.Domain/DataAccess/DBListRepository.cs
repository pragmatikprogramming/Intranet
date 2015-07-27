using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBListRepository
    {
        public static void Create(CMSList m_List)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_Lists(listName, listType) VALUES(@listName, @listType)";
            SqlCommand insList = new SqlCommand(queryString, conn);
            insList.Parameters.AddWithValue("listName", m_List.ListName ?? "");
            insList.Parameters.AddWithValue("listType", m_List.ListType);
            insList.ExecuteNonQuery();

            conn.Close();
        }

        public static CMSList RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_Lists WHERE id = @id";
            SqlCommand getList = new SqlCommand(queryString, conn);
            getList.Parameters.AddWithValue("id", id);
            SqlDataReader listReader = getList.ExecuteReader();

            CMSList m_List = new CMSList();

            if (listReader.Read())
            {
                m_List.Id = listReader.GetInt32(0);
                m_List.ListName = listReader.GetString(1);
                m_List.ListType = listReader.GetInt32(2);
            }

            conn.Close();

            return m_List;
        }

        public static List<CMSList> RetrieveAll()
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_Lists";
            SqlCommand getLists = new SqlCommand(queryString, conn);
            SqlDataReader listReader = getLists.ExecuteReader();

            List<CMSList> m_Lists = new List<CMSList>();

            while(listReader.Read())
            {
                CMSList m_List = new CMSList();
                m_List.Id = listReader.GetInt32(0);
                m_List.ListName = listReader.GetString(1);
                m_List.ListType = listReader.GetInt32(2);

                m_Lists.Add(m_List);
            }

            conn.Close();

            return m_Lists;
        }

        public static void Update(CMSList m_List)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_Lists SET listName = @listName, listType = @listType WHERE id = @id";
            SqlCommand updList = new SqlCommand(queryString, conn);
            updList.Parameters.AddWithValue("listName", m_List.ListName ?? "");
            updList.Parameters.AddWithValue("listType", m_List.ListType);
            updList.Parameters.AddWithValue("id", m_List.Id);

            updList.ExecuteNonQuery();

            conn.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_ListChildren WHERE listId = @id";
            SqlCommand delChild = new SqlCommand(queryString, conn);
            delChild.Parameters.AddWithValue("id", id);
            delChild.ExecuteNonQuery();

            queryString = "DELETE FROM CMS_Lists WHERE id = @id";
            SqlCommand delList = new SqlCommand(queryString, conn);
            delList.Parameters.AddWithValue("id", id);
            delList.ExecuteNonQuery();

            conn.Close();
        }
    }
}