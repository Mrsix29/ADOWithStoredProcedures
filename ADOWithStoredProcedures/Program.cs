using ADOWithStoredProcedures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADOWithStoredProcedures
{
    internal class Program
    {
        static SqlConnection connection;
        static SqlCommand command;
        static SqlDataAdapter adapter;
        static string connectionString = @"data source=CA-LG5G07G3\SQLEXPRESS;initial catalog=CGIDb;integrated security=true";
        
        static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            command = new SqlCommand("GetAllCustomers", GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            foreach (DataRow dataRow in dt.Rows)
            {
                Customer customer = new Customer();
                customer.Id = Convert.ToInt32(dataRow["id"]);
                customer.Name = dataRow["name"].ToString();
                customer.Address = dataRow["address"].ToString();
                customer.City = dataRow["city"].ToString();
                customer.State = dataRow["state"].ToString();
                customers.Add(customer);
            }
            return customers;
        }
        static Customer GetCustomerByID(int id)
        {
            Customer customer = new Customer();
            command = new SqlCommand("GetCustomerByID", GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
      
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            DataRow dataRow = dt.Rows[0];
            customer.Name = dataRow["name"].ToString();
            customer.Address = dataRow["address"].ToString();
            customer.City = dataRow["city"].ToString();
            customer.State = dataRow["state"].ToString();

            return customer;
        }

        static void UpdateCustomer(Customer customer)
        {
            using (connection = GetConnection())
            {
                using (command = new SqlCommand("UpdateCustomer"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@id", customer.Id);
                    
                    command.Parameters.AddWithValue("@address", customer.Address);
                    command.Parameters.AddWithValue("@city", customer.City);
                    command.Parameters.AddWithValue("@State", customer.State);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();


                }
            }
        }

        static void InsertCustomer(Customer customer)
        {
            using (connection = GetConnection())
            {
                using (command = new SqlCommand("InsertCustomer"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@id", customer.Id);
                    command.Parameters.AddWithValue("@name", customer.Name);
                    command.Parameters.AddWithValue("@address", customer.Address);
                    command.Parameters.AddWithValue("@city", customer.City);
                    command.Parameters.AddWithValue("@State", customer.State);
                    SqlParameter para1 = new SqlParameter();
                    para1.ParameterName = "@flag";
                    para1.Direction = ParameterDirection.ReturnValue;
                    // para1.ParameterName = para1;
                    para1.SqlDbType = SqlDbType.Int;
                    command.Parameters.Add(para1);
                    connection.Open();
                    command.ExecuteNonQuery();
                    int flag = (int)command.Parameters["@flag"].Value;
                    connection.Close();
                    if (flag == 0)
                    {
                        Console.WriteLine("Record with this id already exist");
                    }
                    else
                        Console.WriteLine("Record inserted");


                }
            }
        }

        static void DeleteCustomer(int id)
        {
            command = new SqlCommand("DeleteCustomer", GetConnection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);

            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
        }

        static void Main(string[] args)
        {

            //Insert
           /*  Customer customer = new Customer() { 
                   Id = 6, Name = "Chen" ,Address = "666 Sky Ave", City = "Edmonton", State = "AB"
               };

             InsertCustomer(customer);*/

            //Update
            // UpdateCustomer(customer);

            //Get ALL
            List<Customer> customers = GetAllCustomers();
            foreach (Customer customer in customers)
            {
                Console.WriteLine($"Name:  {customer.Name}  Address:  {customer.Address}  City:{customer.City} State:{customer.State}");
            }

            //GET BY ID
           /* Customer customer = GetCustomerByID(1);
            Console.WriteLine($"Name:  {customer.Name}  Address:  {customer.Address}  City:{customer.City} State:{customer.State}");*/

            //DELETE BY ID
            //DeleteCustomer(5);

        }

    }
}
