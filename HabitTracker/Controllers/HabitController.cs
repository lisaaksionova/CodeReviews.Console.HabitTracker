using HabitTracker.Database;
using HabitTracker.Exceptions;
using HabitTracker.Models;
using Spectre.Console;

namespace HabitTracker.Controllers;

public class HabitController
{
    private readonly HabitSql _habitSql = new HabitSql();
    public void AddHabit()
    {
        var habit = new Habit();
        habit.Name = AnsiConsole.Ask<string>("Enter the [green]habit name[/]:");
        habit.Unit = AnsiConsole.Ask<string>("Enter the [green]habit unit of measurement[/]:");
        
        _habitSql.Create(habit);
    }

    public void UpdateHabit()
    {
        var habit = new Habit();
        int id = habit.Id = AnsiConsole.Ask<int>("Enter the [green]habit ID[/] to update:");
        
        if (_habitSql.Read(id) is null)
            throw new NotFoundException(nameof(Habit), id);
        
        habit.Name = AnsiConsole.Ask<string>("Enter the [green]habit name[/]:");
        habit.Unit = AnsiConsole.Ask<string>("Enter the [green]habit unit of measurement[/]:");
        
        _habitSql.Update(habit);
    }

    public void DeleteHabit()
    {
        var id = AnsiConsole.Ask<int>("Enter the [green]habit ID[/] to delete:");

        if (_habitSql.Read(id) is null)
            throw new NotFoundException(nameof(Habit), id);
        
        _habitSql.Delete(id);
    }

    public void ListHabits()
    {
        var habits = _habitSql.Read();

        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Unit");

        foreach (var habit in habits)
        {
            table.AddRow(habit.Id.ToString(), habit.Name, habit.Unit);
        }
        
        AnsiConsole.Write(table);
    }
}