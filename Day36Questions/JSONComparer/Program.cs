using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

#region Models

public class CustomerApplication
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int? Age { get; set; }
    public string PAN { get; set; }
}

public class RecordError
{
    public int RecordIndex { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}

public class ValidationReport
{
    public int TotalRecords { get; set; }
    public int ValidRecords { get; set; }
    public int InvalidRecords { get; set; }
    public List<RecordError> ErrorDetails { get; set; } = new List<RecordError>();
}

#endregion

public class ValidationPipeline
{
    private static readonly Regex EmailRegex =
        new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    private static readonly Regex PanRegex =
        new Regex(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", RegexOptions.Compiled);

    public ValidationReport ValidateBatch(List<string> jsonPayloads)
    {
        var report = new ValidationReport
        {
            TotalRecords = jsonPayloads.Count
        };

        for (int i = 0; i < jsonPayloads.Count; i++)
        {
            var errors = new List<string>();

            CustomerApplication app = null;

            try
            {
                app = JsonSerializer.Deserialize<CustomerApplication>(jsonPayloads[i]);
            }
            catch
            {
                errors.Add("Invalid JSON format");
                AddError(report, i, errors);
                continue;
            }

            // Mandatory fields
            if (string.IsNullOrWhiteSpace(app?.Name))
                errors.Add("Name is mandatory");

            if (string.IsNullOrWhiteSpace(app?.Email))
                errors.Add("Email is mandatory");

            if (app?.Age == null)
                errors.Add("Age is mandatory");

            if (string.IsNullOrWhiteSpace(app?.PAN))
                errors.Add("PAN is mandatory");

            // Email format
            if (!string.IsNullOrWhiteSpace(app?.Email) &&
                !EmailRegex.IsMatch(app.Email))
            {
                errors.Add("Invalid email format");
            }

            // Age range
            if (app?.Age != null &&
                (app.Age < 18 || app.Age > 60))
            {
                errors.Add("Age must be between 18 and 60");
            }

            // PAN format
            if (!string.IsNullOrWhiteSpace(app?.PAN) &&
                !PanRegex.IsMatch(app.PAN))
            {
                errors.Add("Invalid PAN format");
            }

            if (errors.Count > 0)
            {
                AddError(report, i, errors);
            }
            else
            {
                report.ValidRecords++;
            }
        }

        report.InvalidRecords = report.TotalRecords - report.ValidRecords;
        return report;
    }

    private void AddError(ValidationReport report, int index, List<string> errors)
    {
        report.ErrorDetails.Add(new RecordError
        {
            RecordIndex = index,
            Errors = errors
        });
    }
}

#region Demo

class Program
{
    static void Main()
    {
        var jsonInputs = new List<string>
        {
            @"{""Name"":""Rahul"",""Email"":""rahul@gmail.com"",""Age"":25,""PAN"":""ABCDE1234F""}",
            @"{""Name"":"""",""Email"":""wrong-email"",""Age"":17,""PAN"":""123""}",
            @"{""Name"":""Anita"",""Email"":""anita@mail.com"",""Age"":45,""PAN"":""ABCDE1234Z""}"
        };

        var pipeline = new ValidationPipeline();
        ValidationReport report = pipeline.ValidateBatch(jsonInputs);

        Console.WriteLine($"Total   : {report.TotalRecords}");
        Console.WriteLine($"Valid   : {report.ValidRecords}");
        Console.WriteLine($"Invalid : {report.InvalidRecords}");

        Console.WriteLine("\nError Details:");
        foreach (var error in report.ErrorDetails)
        {
            Console.WriteLine($"Record {error.RecordIndex}:");
            foreach (var msg in error.Errors)
            {
                Console.WriteLine($" - {msg}");
            }
        }
    }
}

#endregion
