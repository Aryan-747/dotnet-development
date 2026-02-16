using Microsoft.Data.SqlClient;
using System;
using System.Data;

class Program
{
    static string connectionString = @"Server=.\SQLEXPRESS;
                    Database=TrainingDB;
                    Trusted_Connection=True;
                    TrustServerCertificate=True;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== Employee Management (Disconnected) =====");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. Display");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Insert();
                    break;
                case 2:
                    Update();
                    break;
                case 3:
                    Delete();
                    break;
                case 4:
                    Display();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void Insert()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Department: ");
        string dept = Console.ReadLine();

        Console.Write("Enter Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            Console.WriteLine("Invalid salary.");
            return;
        }

        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];

        DataRow row = table.NewRow();
        row["FullName"] = name;
        row["Department"] = dept;
        row["Salary"] = salary;

        table.Rows.Add(row);
        adapter.Update(ds, "Employees");

        Console.WriteLine("Insert successful.");
    }

    static void Update()
    {
        Console.Write("Enter Employee ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Enter New Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter New Department: ");
        string dept = Console.ReadLine();

        Console.Write("Enter New Salary: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            Console.WriteLine("Invalid salary.");
            return;
        }

        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];

        bool found = false;

        foreach (DataRow row in table.Rows)
        {
            if ((int)row["EmployeeId"] == id)
            {
                row["FullName"] = name;
                row["Department"] = dept;
                row["Salary"] = salary;
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        adapter.Update(ds, "Employees");
        Console.WriteLine("Update successful.");
    }

    static void Delete()
    {
        Console.Write("Enter Employee ID to Delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];

        bool found = false;

        foreach (DataRow row in table.Rows)
        {
            if ((int)row["EmployeeId"] == id)
            {
                row.Delete();
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        adapter.Update(ds, "Employees");
        Console.WriteLine("Delete successful.");
    }

    static void Display()
    {
        using SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Employees");

        DataTable table = ds.Tables["Employees"];

        Console.WriteLine("\n--- Employee List ---");

        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"{row["EmployeeId"]} | {row["FullName"]} | {row["Department"]} | {row["Salary"]}");
        }
    }
}
