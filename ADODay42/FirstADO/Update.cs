using Microsoft.Data.SqlClient;
class Update
{
    static void Main()
    {
        string cs = @"Server=.\SQLEXPRESS;Database=TrainingDB;Trusted_Connection=True;TrustServerCertificate=True;";
        string sql = @"UPDATE dbo.Employees SET Salary=@salary WHERE EmployeeId=@id";

Console.Write("EmployeeId: "); int id = int.Parse(Console.ReadLine() ?? "0");
Console.Write("New Salary: "); decimal salary = decimal.Parse(Console.ReadLine() ?? "0");

using var con = new SqlConnection(cs);
using var cmd = new SqlCommand(sql, con);

cmd.Parameters.AddWithValue("@id", id);
cmd.Parameters.AddWithValue("@salary", salary);

con.Open();
int rows = cmd.ExecuteNonQuery();

Console.WriteLine($"Updated rows: {rows}");
        }
}