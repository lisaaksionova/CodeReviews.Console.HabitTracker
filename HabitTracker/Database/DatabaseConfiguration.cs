using Microsoft.Data.Sqlite;

namespace HabitTracker.Database;

public class DatabaseConfiguration
{
    private readonly string _connectionString;
    
    public DatabaseConfiguration()
    {
        _connectionString = "Data Source=HabitTracker.db";
    }

    public void CreateDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string createHabitsTableSql = """
                                CREATE TABLE IF NOT EXISTS Habits (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL,
                                Unit TEXT NOT NULL
                                );
                                """;
        
        string createHabitLogsTableSql = """
                                CREATE TABLE IF NOT EXISTS HabitLogs (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                HabitId INTEGER NOT NULL,
                                Occurrences INTEGER NOT NULL,
                                OccurrenceDate TEXT NOT NULL,
                                FOREIGN KEY (HabitId) REFERENCES Habits(Id)
                                );
                                """;
        
        using var createHabitsTableCommand = new SqliteCommand(createHabitsTableSql, connection);
        createHabitsTableCommand.ExecuteNonQuery();
        
        using var createHabitLogsTableCommand = new SqliteCommand(createHabitLogsTableSql, connection);
        createHabitLogsTableCommand.ExecuteNonQuery();
    }
}