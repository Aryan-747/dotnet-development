using Microsoft.Data.SqlClient;
class Insert
{
    static void Main()
    {
        string cs = "Data Source=ARYAN\\SQLEXPRESS; Initial Catalog = TrainingDB; Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;";
        string sql = @"INSERT INTO dbo.Employees(FullName, Department, Salary)
               VALUES (@name, @dept, @salary)";

        Console.Write("Name: "); string name = Console.ReadLine() ?? "";
        Console.Write("Dept: "); string dept = Console.ReadLine() ?? "";
        Console.Write("Salary: "); decimal salary = decimal.Parse(Console.ReadLine() ?? "0");

        using var con = new SqlConnection(cs);
        using var cmd = new SqlCommand(sql, con);

        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@dept", dept);
        cmd.Parameters.AddWithValue("@salary", salary);

        con.Open();
        int rows = cmd.ExecuteNonQuery();

        Console.WriteLine(rows == 1 ? "✅ Inserted" : "⚠️ Not inserted");
        }
}