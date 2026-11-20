using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace HabitTracker.Controllers;

public class HabitLogController
{
    private readonly string _connectionString = "Data Source=HabitTracker.db";

    public void AddHabitLog()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var habitId = AnsiConsole.Ask<int>("Enter [green]habit id[/]: ");
        var occurrences = AnsiConsole.Ask<int>("Enter [green]occurence count[/]: ");
        var date = AnsiConsole.Ask<string>("Enter [green]date[/] int format 'dd-mm-yyyy': ");

        string insertHabitLogSql = """
                                  INSERT INTO HabitLogs (HabitId, Occurrences, OccurrenceDate)
                                  VALUES (@habitId, @occurrences, @date);
                                  """;
        using var command = new SqliteCommand(insertHabitLogSql, connection);
        command.Parameters.AddWithValue("@habitId", habitId);
        command.Parameters.AddWithValue("@occurrences", occurrences);
        command.Parameters.AddWithValue("@date", date);

        command.ExecuteNonQuery();
    }

    public void UpdateHabitLog()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        var id = AnsiConsole.Ask<int>("Enter [green]log id[/]: ");
        var habitId = AnsiConsole.Ask<int>("Enter [green]habit id[/]: ");
        var occurrences = AnsiConsole.Ask<int>("Enter [green]occurence count[/]: ");
        var date = AnsiConsole.Ask<string>("Enter [green]date[/] int format 'dd-mm-yyyy': ");

        string updateHabitLogSql = """
                                  UPDATE HabitLogs
                                  SET HabitId = @habitId, Occurrences = @occurrences, OccurrenceDate = @date
                                  WHERE Id = @id;
                                  """;
        using var command = new SqliteCommand(updateHabitLogSql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@habitId", habitId);
        command.Parameters.AddWithValue("@occurrences", occurrences);
        command.Parameters.AddWithValue("@date", date);

        command.ExecuteNonQuery();
    }

    public void DeleteHabitLog()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        var id = AnsiConsole.Ask<int>("Enter [green]log id[/]: ");

        string deleteHabitLogSql = """
                                  DELETE FROM HabitLogs
                                  WHERE Id = @id;
                                  """;
        using var command = new SqliteCommand(deleteHabitLogSql, connection);
        command.Parameters.AddWithValue("@id", id);
        
        command.ExecuteNonQuery();
    }

    public void ListHabitLogs()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectHabitLogs = """
                                 SELECT * FROM HabitLogs;
                                 """;
        using var command = new SqliteCommand(selectHabitLogs, connection);
        using var reader = command.ExecuteReader();
        
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("HabitId");
        table.AddColumn("Occurrences");
        table.AddColumn("OccurenceDate");
        
        if(reader.HasRows)
            while (reader.Read())
            {
                var id = reader.GetString(0);
                var habitId = reader.GetString(1);
                var occurrences = reader.GetString(2);
                var occurenceDate = reader.GetString(3);

                table.AddRow(id, habitId, occurrences, occurenceDate);
            }
        AnsiConsole.Write(table);
        Console.ReadKey();
    }
}