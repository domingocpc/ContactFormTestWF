using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Form_Test_WF
{
    public static class DataAccess
    {
        static string ConnectionString = "Data Source = sql15dev; Initial Catalog = TheWorldDb; User ID = sa; Password=C3l5ius;";

        public static void AddContact(string firstName, string lastName, string phoneNum)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("insert into Contact(FirstName, LastName, PhoneNumber) values('{0}', '{1}' , '{2}')", firstName, lastName, phoneNum);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }

        public static Contact FindByFirstName(string firstName)
        {
            Contact contact = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("select top 1 * from contact where FirstName = '{0}'", firstName);

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    contact = new Contact()
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                }
                return contact;
            }
        }

        public static Contact FindByLastName(string lastName)
        {
            Contact contact = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("select top 1 * from contact where LastName like '{0}%'", lastName);

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    contact = new Contact()
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                }
                return contact;
            }
        }

        public static Contact FindByPhoneNumber(string phoneNum)
        {
            Contact contact = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("select top 1 * from contact where PhoneNumber like '{0}%'", phoneNum);

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    contact = new Contact()
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                }
                return contact;
            }
        }

        public static List<Contact> GetAllContacts()
        {
            List<Contact> contactList = null;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("select * from contact");

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                contactList = new List<Contact>();

               
                    while (reader.Read())
                    {
                        string firstName = reader["FirstName"].ToString();
                        string lastName = reader["LastName"].ToString();
                        string phoneNum = reader["PhoneNumber"].ToString();

                        contactList.Add(new Contact
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            PhoneNumber = phoneNum

                        });
                    }
                
                

                return contactList;

            }   
        }

        public static void DeleteByFirstName(string firstName)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("delete from contact where FirstName like '{0}%'", firstName);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteByLastName(string lastName)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("delete from contact where LastName like '{0}%'", lastName);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteByPhoneNumber(string phoneNumber)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("delete from contact where PhoneNumber like '{0}%'", phoneNumber);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void UpdateContact(Contact contact)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("update contact set FirstName = '{0}', LastName = '{1}', PhoneNumber = '{2}' where Id = {3}", 
                    contact.FirstName, contact.LastName, contact.PhoneNumber, contact.Id);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }

        public static void DeleteContact(Contact contact)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("delete from contact where id = {0}", contact.Id);

                conn.Open();

                command.ExecuteNonQuery();
            }
        }

        public static string GetCount()
        {

            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = conn;
                command.CommandText = String.Format("select count(*) from contact");

                conn.Open();

                string count = command.ExecuteScalar().ToString();

                return count;
            }
        }
    }
}
