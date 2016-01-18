using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using CMS.Domain.HelperClasses;
using CMS.Domain.Entities;

namespace CMS.Domain.DataAccess
{
    public class DBHome
    {
        public static List<Page> MainMenu()
        {
            List<Page> m_Pages = DBPage.RetrieveAll(0);
            return m_Pages;
        }

        public static void SubmitComment(BlogPostComment m_Comment)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_BlogPostComments(comment, name, blogId, pageWorkFlowState) VALUES(@comment, @name, @blogId, 2)";
            SqlCommand insertComment = new SqlCommand(queryString, conn);
            insertComment.Parameters.AddWithValue("comment", m_Comment.Comment);
            insertComment.Parameters.AddWithValue("name", m_Comment.Name);
            insertComment.Parameters.AddWithValue("blogId", m_Comment.BlogId);
            insertComment.ExecuteNonQuery();

            conn.Close();
        }

        public static List<SearchResult> Search(string SearchParam, string oldSearchParam, string searchParamOr)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "select id, pageId as m_Id, pageTitle as title, 'Page' as contentType, navigationName, content from CMS_Pages where pageWorkFlowState = 2 and (contains((content, pageTitle, navigationName), @searchParam)) union all select id, blogId as m_Id, title, 'Blog' as contentType, ' ' as navigationName, content from CMS_BlogPosts where pageWorkFlowState = 2 and contains(content, @searchParam) union all select p.id, p.pageId as m_Id, p.pageTitle as title, 'Page' as contentType, p.navigationName, h.content from CMS_Pages p inner join CMS_WidgetContainers w on p.widgetContainerId = w.id inner join CMS_ContainerToWidgets c on w.id = c.containerId inner join CMS_HTMLWidget h on c.widgetId = h.id where p.pageWorkFlowState = 2 and h.content like '%' + @oldSearchParam + '%' union all select id, '' as m_Id, firstName + ' ' + lastName as title, 'Employee Directory' as contentType, '' as navigationName, '' as content from CMS_EmployeeDirectory where contains((firstName, lastName, info, about), @searchParamOr) union all select p.id, p.pageId as m_Id, p.pageTitle as title, 'FAQ' as contentType, p.navigationName, cast(q.faqQuestion as varchar(max)) + ' ' + cast(q.faqAnswer as varchar(max)) as content from CMS_Pages as p inner join CMS_FAQs f on p.pageTypeID = f.id inner join CMS_FAQQuestions q on f.id = q.faqID where p.pageType = 3 and p.pageWorkFlowState = 2 and contains((q.faqQuestion, q.faqAnswer), @searchParam) union all select id, id as m_Id, firstName + ' ' + lastName as title, 'Performer' as contentType, '' as navigationName, address + '^' + phone + '^' + fax + '^' + email + '^' + website as content from CMS_Performers where contains((firstName, lastName), @searchParam) union all select id, id as m_Id, programTitle as title, 'Act' as contentType, '' as navigationName, [description] as content from CMS_Acts where contains((programTitle, [description], notes), @searchParam)";
            SqlCommand getSearchResults = new SqlCommand(queryString, conn);
            getSearchResults.Parameters.AddWithValue("searchParam", SearchParam);
            getSearchResults.Parameters.AddWithValue("oldSearchParam", oldSearchParam);
            getSearchResults.Parameters.AddWithValue("searchParamOr", searchParamOr);
            SqlDataReader searchResultsReader = getSearchResults.ExecuteReader();

            List<SearchResult> m_SearchResults = new List<SearchResult>();

            while(searchResultsReader.Read())
            {
                SearchResult m_SearchResult = new SearchResult();
                m_SearchResult.Id = searchResultsReader.GetInt32(0);
                m_SearchResult.TypeId = searchResultsReader.GetInt32(1);
                m_SearchResult.Title = searchResultsReader.GetString(2);
                m_SearchResult.ContentType = searchResultsReader.GetString(3);
                m_SearchResult.NavigationName = searchResultsReader.GetString(4);
                m_SearchResult.Content = searchResultsReader.GetString(5);
                

                m_SearchResult.Content = Regex.Replace(m_SearchResult.Content, @"<[^>]+>|&nbsp;", "").Trim();
                m_SearchResult.Content = Regex.Replace(m_SearchResult.Content, @"\s{2,}", " ");

                if(m_SearchResult.Title.ToLower().Contains(oldSearchParam.ToLower()))
                {
                    if(m_SearchResult.Content.Length > 100)
                    {
                        m_SearchResult.Content = m_SearchResult.Content.Substring(0, 100);
                    }
                }
                else if(m_SearchResult.NavigationName.ToLower().Contains(oldSearchParam.ToLower()))
                {
                    if (m_SearchResult.Content.Length > 100)
                    {
                        m_SearchResult.Content = m_SearchResult.Content.Substring(0, 100);
                    }
                }
                else if(m_SearchResult.Content.ToLower().Contains(oldSearchParam.ToLower()))
                {
                    string[] separators = new string[] { oldSearchParam.ToLower() };
                    string[] words = m_SearchResult.Content.ToLower().Split(separators, StringSplitOptions.None);

                    string begin = words[0];
                    string end = words[1];

                    if (begin.Length > 100)
                    {
                        begin = begin.Substring((begin.Length - 100), 100);
                    }
                    if (end.Length > 100)
                    {
                        end = end.Substring(0, 100);
                    }

                    m_SearchResult.Content = begin + " <b>" + oldSearchParam + "</b> " + end;
                }
                else
                {
                    if (m_SearchResult.Content.Length > 100)
                    {
                        m_SearchResult.Content = m_SearchResult.Content.Substring(0, 100);
                    }
                }

                m_SearchResults.Add(m_SearchResult);
            }

            conn.Close();

            return m_SearchResults;
        }

        
    }
}