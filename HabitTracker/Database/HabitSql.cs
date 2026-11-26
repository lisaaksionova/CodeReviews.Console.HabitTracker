using HabitTracker.Models;
using Microsoft.Data.Sqlite;

namespace HabitTracker.Database;

public class HabitSql : SqlBase
{
    public void Create(Habit habit)
    {
        string insertHabitSql = """
                                INSERT INTO Habits (Name, Unit)
                                VALUES (@Name, @Unit);
                                """;
        
        (string Name, object Value)[] parameters =
        [
            ("@Name", habit.Name),
            ("@Unit", habit.Unit),
        ];

        ExecuteCommand(insertHabitSql, parameters);
    }

    public List<Habit> Read()
    {
        string selectHabitsSql = """
                                 SELECT * FROM Habits;
                                 """;

        var habits = ExecuteQuery<Habit>(selectHabitsSql, reader => new Habit
        {
            Id = Convert.ToInt32(reader.GetString(0)),
            Name = reader.GetString(1),
            Unit = reader.GetString(2)
        });

        return habits;
    }

    public Habit? Read(int id)
    {
        string selectHabitsSql = """
                                 SELECT * FROM Habits
                                 WHERE Id = @Id;
                                 """;
        
        (string Name, object Value)[] parameters =
        [
            ("@Id", id),
        ];

        var habit = ExecuteQueryReturnSingle<Habit>(selectHabitsSql, reader => new Habit
        {
            Id = Convert.ToInt32(reader.GetString(0)),
            Name = reader.GetString(1),
            Unit = reader.GetString(2)
        }, parameters);

        return habit;
    }

    public void Update(Habit habit)
    {
        string updateHabitSql = """
                                UPDATE Habits
                                SET Name = @Name, Unit = @Unit
                                WHERE Id = @Id;
                                """;

        (string Name, object Value)[] parameters =
        [
            ("@Id", habit.Id),
            ("@Name", habit.Name),
            ("@Unit", habit.Unit),
        ];

        ExecuteCommand(updateHabitSql, parameters);
    }

    public void Delete(int id)
    {
        string deleteHabitSql = """
                                DELETE FROM Habits
                                WHERE Id = @Id;
                                """;
        
        (string Name, object Value)[] parameters =
        [
            ("@Id", id),
        ];
        
        ExecuteCommand(deleteHabitSql, parameters);
    }
}