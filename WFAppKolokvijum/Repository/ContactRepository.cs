using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFAppKolokvijum.DBContext;
using WFAppKolokvijum.Interface;
using WFAppKolokvijum.Model;

namespace WFAppKolokvijum.Repository
{
    internal class ContactRepository : IContactRepository
    {


        public bool AddContact(Contact contact)
        {

            var con = new SqlConnection(DataContext.ConnectionString);
            try
            {
                var command = new SqlCommand();
                command.Connection = con;
                con.Open();
                command.CommandText = @"INSERT INTO Kontakti (FirstName, LastName,
                    Email, Phone) VALUES (@FirstName, @LastName, @Email, @Phone)";
                command.Parameters.AddWithValue("@FirstName", contact.FirstName.ToString());
                command.Parameters.AddWithValue("@LastName", contact.LastName.ToString());
                command.Parameters.AddWithValue("@Email", contact.Email.ToString());
                command.Parameters.AddWithValue("@Phone", contact.Phone.ToString());
                command.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
            return true;

        }

        public bool DeleteContact(int id)
        {
            

            
                var con = new SqlConnection(DataContext.ConnectionString);
                try
                {
                    var command = new SqlCommand();
                    command.Connection = con;
                    con.Open();
                    command.CommandText = @"DELETE FROM Kontakti WHERE Id = " + id + "";
                    command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
                return true;
            
           
        }

        public List<Contact> GetContact(string name)
        {
            
            var contacts = new List<Contact>();
            var con = new SqlConnection(DataContext.ConnectionString);
            try
            {
                con.Open();
                var querry = "SELECT * FROM Kontakti WHERE FirstName LIKE '%" + name + "%' OR LastName LIKE '%"+ name +"%'";
                var command = new SqlCommand(querry, con);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var c = new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };
                    contacts.Add(c);
                   
                }
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting data from the database.");
            }
            finally
            {
                con.Close();
            }

            return contacts;
        }

        public List<Contact> GetContacts()
        {
            var contacts = new List<Contact>();
            var con = new SqlConnection(DataContext.ConnectionString);
            try
            {
                con.Open();
                var querry = "SELECT * FROM Kontakti";
                var command = new SqlCommand(querry, con);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var c = new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };
                    contacts.Add(c);
                }
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error while getting data from the database.");
            }
            finally 
            { 
                con.Close(); 
            }

            return contacts;
        }

        public bool UpdateContact(Contact contact)
        {
            
                var con = new SqlConnection(DataContext.ConnectionString);
                try
                {
                    var command = new SqlCommand();
                    command.Connection = con;
                    con.Open();
                    command.CommandText = @"UPDATE Kontakti SET FirstName='" + contact.FirstName + "', LastName='" + contact.LastName + "',Email='" + contact.Email + "', Phone='" + contact.Phone + "'WHERE Id = " + contact.Id + "";
                    command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
                return true;
            
        }
    }
}
