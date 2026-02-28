using System;
using Microsoft.Data.SqlClient;
using System.Data;
public class DatabaseHelper
{
    private readonly string _connectionString;

    // constructor reads connection string from appsettings
    public DatabaseHelper(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<string>> GetSourcesAsync()
    {
        List<string> sources = new List<string>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_GetSources", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                sources.Add(reader.GetString(0));
            }
        }

        return sources;
    }

    public async Task<List<string>> GetDestinationsAsync()
    {
        List<string> list = new List<string>();

        using SqlConnection con = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("sp_GetDestinations", con);

        cmd.CommandType = CommandType.StoredProcedure;

        await con.OpenAsync();
        var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            list.Add(reader.GetString(0));

        return list;
    }

    public async Task<List<FlightResult>> SearchFlightsAsync(
    string source, string destination, int persons)
    {
        List<FlightResult> flights = new();

        using SqlConnection con = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("sp_SearchFlights", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Source", source);
        cmd.Parameters.AddWithValue("@Destination", destination);
        cmd.Parameters.AddWithValue("@Persons", persons);

        await con.OpenAsync();

        var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            flights.Add(new FlightResult
            {
                FlightId = (int)reader["FlightId"],
                FlightName = reader["FlightName"].ToString(),
                FlightType = reader["FlightType"].ToString(),
                Source = reader["Source"].ToString(),
                Destination = reader["Destination"].ToString(),
                TotalCost = (decimal)reader["TotalCost"]
            });
        }

        return flights;
    }

    public async Task<List<FlightHotelResult>> SearchFlightsWithHotelsAsync(string source,string destination,int persons)
    {
        List<FlightHotelResult> results = new();

        using SqlConnection con = new SqlConnection(_connectionString);
        using SqlCommand cmd =
            new SqlCommand("sp_SearchFlightsWithHotels", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Source", source);
        cmd.Parameters.AddWithValue("@Destination", destination);
        cmd.Parameters.AddWithValue("@Persons", persons);

        await con.OpenAsync();

        var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new FlightHotelResult
            {
                FlightId = (int)reader["FlightId"],
                FlightName = reader["FlightName"].ToString(),
                Source = reader["Source"].ToString(),
                Destination = reader["Destination"].ToString(),
                HotelName = reader["HotelName"].ToString(),
                TotalCost = (decimal)reader["TotalCost"]
            });
        }

        return results;
    }
}