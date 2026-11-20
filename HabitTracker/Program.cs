using HabitTracker;
using HabitTracker.Database;

DatabaseConfiguration dbConfig = new DatabaseConfiguration();
dbConfig.CreateDatabase();

UserInterface userInterface = new UserInterface();
userInterface.MainMenu();
