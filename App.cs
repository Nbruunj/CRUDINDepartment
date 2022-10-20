using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Transactions;
using System.Data.Common;
using System.Xml.Linq;
using System.Collections;
using System.Windows.Input;
using System.Data;

namespace Compulsory_Assignment
{
    public class App
    {
        private const string ConnectionString = "Data Source=localhost;Initial Catalog=Company;User ID=cowboy;Password=insetpassword";
        


        public void Start()
        {
            
            Console.WriteLine("1: do you wont to add a Department");
            Console.WriteLine("2: do you wont to delete a Department");
            Console.WriteLine("3: do you wont to see all Department");
           

            var select = Console.ReadLine();

            switch (select)
            {
                case "1":
                    AddDepartment();



                    break;
                case "2":
                    DeleteDepartment();


                    break;
                case "3":
                    GetAll();
                    break;
            }
        }

        
        public void AddDepartment()
        {
            Console.WriteLine("insert new department name");
            var dname = Console.ReadLine();
            Console.WriteLine("the new department name is" + dname);
            Console.WriteLine("insert your MGRSSN number ");
            var mgrssn = Console.ReadLine();
            
            using (SqlConnection connection = new SqlConnection(
                           ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("CreateDepartment", connection))
                {
                    
                    command.Parameters.AddWithValue("@DName", dname);
                    command.Parameters.AddWithValue("@MgrSSN", int.Parse(mgrssn));
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();

                    
                }
                Console.WriteLine("the department has ben createt with the name " + dname);
            }
        }

        public void DeleteDepartment()
        {
            Console.WriteLine("what department do you like to delete insert DNumber");
            var dnumber = Console.ReadLine();
            


            using (SqlConnection connection = new SqlConnection(
                          ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteDepartment", connection);
                command.Parameters.AddWithValue("@Dnumber", SqlDbType.Int);
                command.Parameters["@Dnumber"].Value = dnumber;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
               
                       
                Console.WriteLine("the department has ben Delete with the DNumber " + dnumber);
            }
                    
        }
        public void GetAll()
        {

            using (SqlConnection connection = new SqlConnection(
                           ConnectionString))
            {
                connection.Open();
                
                try
                {
                    var reader = new SqlCommand("GetAllDepartments", connection)
                       .ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}", reader.GetString(0),
                                reader.GetInt32(1));
                        }
                    }
                   

                }
                catch (SqlException sqlError)
                {
                    
                }
                connection.Close();

            }
        }
    }
}
