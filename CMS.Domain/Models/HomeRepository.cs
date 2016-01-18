using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using CMS.Domain.Abstract;
using CMS.Domain.Entities;
using CMS.Domain.DataAccess;

namespace CMS.Domain.Models
{
    public class HomeRepository : IHomeRepository
    {
        public List<Page> MainMenu()
        {
            List<Page> m_Pages = DBHome.MainMenu();
            return m_Pages;            
        }

        public WidgetContainer getContainer(int id)
        {
            WidgetContainer m_Container = DBWidgetContainer.RetrieveOne(id);
            return m_Container;
        }

        public List<BlogPost> NewsAnnouncements()
        {
            List<BlogPost> m_Posts = new List<BlogPost>();
            return m_Posts;
        }

        public List<Event> FeaturedEvents()
        {
            List<Event> m_Events = DBEvent.getFeaturedEvents();
            return m_Events;
        }

        public List<MenuItem> NonSystemMenu(int id)
        {
            List<MenuItem> m_MenuItems = DBMenuItem.RetrieveAll(id);
            return m_MenuItems;
        }

        public List<BlogPost> GetBlog(int id)
        {
            List<BlogPost> m_BlogPosts = DBBlogPost.RetrieveAllByCategory(id);
            return m_BlogPosts;
        }

        public List<BlogPost> GetNews()
        {
            List<BlogPost> m_BlogPosts = DBBlogPost.getNewsRotator();
            return m_BlogPosts;
        }

        public BlogPost SwapNews(int id)
        {
            BlogPost m_BlogPost = DBBlogPost.RetrieveOne(id);
            return m_BlogPost;
        }

        public void SubmitComment(BlogPostComment m_Comment)
        {
            DBHome.SubmitComment(m_Comment);

            /*MailMessage m_Message = new MailMessage("website@solanolibrary.com", "jwiggint@gmail.com");
            m_Message.Subject = "New Blog Post for Approval";
            m_Message.Body = @"Go approve the comment";
            SmtpClient client = new SmtpClient("localhost");
            client.UseDefaultCredentials = true;

            client.Send(m_Message);*/
        }

        public List<SearchResult> Search(string searchParam)
        {
            int flag = 0;
            string newSearchParam = "";
            string newSearchParamOr = "";
            string[] words = searchParam.Split(' ');
            foreach(string word in words)
            {
                if (flag == 0)
                {
                    newSearchParam += @"""" + word + @""" ";
                    newSearchParamOr += @"""" + word + @""" ";
                    flag = 1;
                }
                else
                {
                    newSearchParam += @"and """ + word + @""" ";
                    newSearchParamOr += @"or """ + word + @""" ";
                }
            }

            newSearchParam.Trim();
            newSearchParamOr.Trim();

            //string newSearchParam = @"""" + searchParam + @"""";
            List<SearchResult> m_SearchResults = DBHome.Search(newSearchParam, searchParam, newSearchParamOr);

            for (int i = 0; i < m_SearchResults.Count; i++ )
            {
                if (m_SearchResults[i].ContentType == "Performer")
                {
                    string[] m_Words = m_SearchResults[i].Content.Split('^');
                    string m_Content = "";

                    foreach (string word in m_Words)
                    {
                        if (word.Length > 0)
                        {
                            m_Content += word + "<br />";
                        }
                    }

                    m_SearchResults[i].Content = m_Content;
                }
            }

            return m_SearchResults;
        }
    }
}