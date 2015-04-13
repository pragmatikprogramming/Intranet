using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Domain.Entities;
using CMS.Domain.HelperClasses;
using System.Data.SqlClient;

namespace CMS.Domain.DataAccess
{
    public class DBSkillsRegistry
    {
        public static void Create(SkillsRegistry m_Skill)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "INSERT INTO CMS_SkillsRegistry(skill) VALUES(@skill)";
            SqlCommand insSkill = new SqlCommand(queryString, conn);
            insSkill.Parameters.AddWithValue("skill", m_Skill.SkillName);
            insSkill.ExecuteNonQuery();

            conn.Close();
        }

        public static SkillsRegistry RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_SkillsRegistry WHERE id = @id";
            SqlCommand getSkill = new SqlCommand(queryString, conn);
            getSkill.Parameters.AddWithValue("id", id);
            SqlDataReader skillReader = getSkill.ExecuteReader();

            SkillsRegistry m_Skill = new SkillsRegistry();

            if(skillReader.Read())
            {
                m_Skill.Id = skillReader.GetInt32(0);
                m_Skill.SkillName = skillReader.GetString(1);
            }
            else
            {
                m_Skill = null;
            }

            conn.Close();

            return m_Skill;
        }

        public static List<SkillsRegistry> RetrieveAll()
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "SELECT * FROM CMS_SkillsRegistry ORDER BY skill";
            SqlCommand getSkills = new SqlCommand(queryString, conn);
            SqlDataReader skillsReader = getSkills.ExecuteReader();

            List<SkillsRegistry> m_Skills = new List<SkillsRegistry>();

            while(skillsReader.Read())
            {
                SkillsRegistry m_Skill = new SkillsRegistry();
                m_Skill.Id = skillsReader.GetInt32(0);
                m_Skill.SkillName = skillsReader.GetString(1);
                m_Skills.Add(m_Skill);
            }

            conn.Close();

            return m_Skills;
        }

        public static void Update(SkillsRegistry m_Skill)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "UPDATE CMS_SkillsRegistry SET skill = @skill WHERE id = @id";
            SqlCommand updSkill = new SqlCommand(queryString, conn);
            updSkill.Parameters.AddWithValue("skill", m_Skill.SkillName);
            updSkill.Parameters.AddWithValue("id", m_Skill.Id);
            updSkill.ExecuteNonQuery();

            conn.Close();
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.DbConnect();
            conn.Open();

            string queryString = "DELETE FROM CMS_SkillsRegistry WHERE id = @id";
            SqlCommand delSkill = new SqlCommand(queryString, conn);
            delSkill.Parameters.AddWithValue("id", id);
            delSkill.ExecuteNonQuery();

            conn.Close();
        }
    }
}