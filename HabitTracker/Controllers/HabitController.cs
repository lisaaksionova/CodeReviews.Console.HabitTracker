using HabitTracker.Database;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace HabitTracker.Controllers;

public class HabitController
{
    private readonly string _connectionString = "Data Source=HabitTracker.db";
    
    public void AddHabit()
    {
        var name = AnsiConsole.Ask<string>("Enter the [green]habit name[/]:");
        var unit = AnsiConsole.Ask<string>("Enter the [green]habit unit of measurement[/]:");
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        string insertHabitSql = """
                                INSERT INTO Habits (Name, Unit)
                                VALUES (@Name, @Unit);
                                """;
        
        using var command = new SqliteCommand(insertHabitSql, connection);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Unit", unit);
        
        command.ExecuteNonQuery();
    }

    public void UpdateHabit()
    {
        var id = AnsiConsole.Ask<int>("Enter the [green]habit ID[/] to update:");
        var name = AnsiConsole.Ask<string>("Enter the [green]habit name[/]:");
        var unit = AnsiConsole.Ask<string>("Enter the [green]habit unit of measurement[/]:");
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        string updateHabitSql = """
                                UPDATE Habits
                                SET Name = @Name, Unit = @Unit
                                WHERE Id = @Id;
                                """;

        using var command = new SqliteCommand(updateHabitSql, connection);
        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Unit", unit);

        command.ExecuteNonQuery();
    }

    public void DeleteHabit()
    {
        var id = AnsiConsole.Ask<int>("Enter the [green]habit ID[/] to delete:");
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string deleteHabitSql = """
                                DELETE FROM Habits
                                WHERE Id = @Id;
                                """;
        
        using var command = new SqliteCommand(deleteHabitSql, connection);
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }

    public void ListHabits()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        string selectHabitsSql = """
                                 SELECT * FROM Habits;
                                 """;

        using var command = new SqliteCommand(selectHabitsSql, connection);
        using var reader = command.ExecuteReader();

        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Unit");
        
        if(reader.HasRows)
            while (reader.Read())
            {
                var id = reader.GetString(0);
                var name = reader.GetString(1);
                var unit = reader.GetString(2);

                table.AddRow(id, name, unit);
            }
        AnsiConsole.Write(table);
        Console.ReadKey();
    }
}