using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


    class SqlAdapetr
    {
        static void Main(string[] args)
        
    {
            string cs = @"Server=.\SQLEXPRESS;Database=TrainingDB;Trusted_Connection=True;TrustServerCertificate=True;";
            string sql = "SELECT EmployeeId, FullName, Department, Salary FROM dbo.Employees ORDER BY EmployeeId;SELECT EmployeeId FROM dbo.Employees";

            DataSet ds = new DataSet();
            using (var con = new SqlConnection(cs))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            ds.WriteXml("TestData1");
        }
    }
