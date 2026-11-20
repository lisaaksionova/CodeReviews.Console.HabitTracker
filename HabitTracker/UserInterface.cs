using HabitTracker.Controllers;
using HabitTracker.Enums;
using Spectre.Console;

namespace HabitTracker;

public class UserInterface
{
    private readonly HabitController _habitController = new();
    private readonly HabitLogController _habitLogController = new();

    public void MainMenu()
    {
        do
        {
            Console.Clear();

            var entityChoice = AnsiConsole.Prompt(
                new SelectionPrompt<Entity>()
                    .Title("Choose habit or log")
                    .AddChoices(Enum.GetValues<Entity>()));

            var operationChoice = AnsiConsole.Prompt(
                new SelectionPrompt<Operation>()
                    .Title("Choose the operation")
                    .AddChoices(Enum.GetValues<Operation>()));

            switch (operationChoice)
            {
                case Operation.Create:
                    CreateEntity(entityChoice);
                    break;
                case Operation.Read:
                    ReadEntity(entityChoice);
                    break;
                case Operation.Update:
                    UpdateEntity(entityChoice);
                    break;
                case Operation.Delete:
                    DeleteEntity(entityChoice);
                    break;
            }
        } while (true);
    }

    private void CreateEntity(Entity entity)
    {
        switch (entity)
        {
            case Entity.Habit:
                _habitController.AddHabit();
                break;
            case Entity.HabitLog:
                _habitLogController.AddHabitLog();
                break;
        }
    }
    
    private void ReadEntity(Entity entity)
    {
        switch (entity)
        {
            case Entity.Habit:
                _habitController.ListHabits();
                break;
            case Entity.HabitLog:
                _habitLogController.ListHabitLogs();
                break;
        }
    }
    
    private void UpdateEntity(Entity entity)
    {
        switch (entity)
        {
            case Entity.Habit:
                _habitController.UpdateHabit();
                break;
            case Entity.HabitLog:
                _habitLogController.UpdateHabitLog();
                break;
        }
    }
    
    private void DeleteEntity(Entity entity)
    {
        switch (entity)
        {
            case Entity.Habit:
                _habitController.DeleteHabit();
                break;
            case Entity.HabitLog:
                _habitLogController.DeleteHabitLog();
                break;
        }
    }
}