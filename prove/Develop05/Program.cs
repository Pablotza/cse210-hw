using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    public Goal(string name)
    {
        Name = name;
        Points = 0;
        IsCompleted = false;
    }

    public virtual void RecordEvent()
    {
        Points += CalculatePoints();
        CheckCompletion();
    }

    protected abstract int CalculatePoints();

    protected virtual void CheckCompletion()
    {
    }

    public override string ToString()
    {
        return $"{Name} - {(IsCompleted ? "[X]" : "[ ]")}";
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name) : base(name) { }

    protected override int CalculatePoints() => 1000;
}

public class EternalGoal : Goal
{
    public EternalGoal(string name) : base(name) { }

    protected override int CalculatePoints() => 100;
}

public class ChecklistGoal : Goal
{
    private int targetCount;
    private int completedCount;

    public ChecklistGoal(string name, int targetCount) : base(name)
    {
        this.targetCount = targetCount;
        completedCount = 0;
    }

    protected override int CalculatePoints()
    {
        completedCount++;
        return 50;
    }

    protected override void CheckCompletion()
    {
        if (completedCount >= targetCount)
        {
            Points += 500;
            IsCompleted = true;
        }
    }

    public override string ToString()
    {
        return base.ToString() + $" - Completed {completedCount}/{targetCount} times";
    }
}

class Program
{
    static void Main()
    {
        List<Goal> goals = LoadGoals() ?? new List<Goal>();

        while (true)
        {
            Console.WriteLine("1. View Goals");
            Console.WriteLine("2. Add Goal");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save and Exit");
            Console.Write("Select an option: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        DisplayGoals(goals);
                        break;

                    case 2:
                        AddGoal(goals);
                        break;

                    case 3:
                        RecordEvent(goals);
                        break;

                    case 4:
                        SaveGoals(goals);
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.WriteLine();
        }
    }

    static void DisplayGoals(List<Goal> goals)
    {
        Console.WriteLine("Your Goals:");
        foreach (var goal in goals)
        {
            Console.WriteLine(goal);
        }
        Console.WriteLine($"Your total score: {goals.Sum(g => g.Points)}");
        Console.WriteLine();
    }

    static void AddGoal(List<Goal> goals)
    {
        Console.Write("Enter the name of the goal: ");
        string goalName = Console.ReadLine();

        Console.WriteLine("Select the type of goal:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            Goal newGoal = choice switch
            {
                1 => new SimpleGoal(goalName),
                2 => new EternalGoal(goalName),
                3 => CreateChecklistGoal(goalName),
                _ => null,
            };

            if (newGoal != null)
            {
                goals.Add(newGoal);
                Console.WriteLine("Goal added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid choice. Goal not added.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Goal not added.");
        }

        Console.WriteLine();
    }

    static ChecklistGoal CreateChecklistGoal(string goalName)
    {
        Console.Write("Enter the number of times the goal must be accomplished: ");
        if (int.TryParse(Console.ReadLine(), out int targetCount))
        {
            return new ChecklistGoal(goalName, targetCount);
        }
        else
        {
            Console.WriteLine("Invalid input. Creating a default Checklist Goal.");
            return new ChecklistGoal(goalName, 5);
        }
    }

    static void RecordEvent(List<Goal> goals)
    {
        Console.WriteLine("Select the goal to record an event for:");
        DisplayGoals(goals);

        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordEvent();
            Console.WriteLine("Event recorded successfully!");
        }
        else
        {
            Console.WriteLine("Invalid input. Event not recorded.");
        }

        Console.WriteLine();
    }

    static void SaveGoals(List<Goal> goals)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter("goals.json"))
            {
                string json = JsonSerializer.Serialize(goals);
                sw.Write(json);
            }

            Console.WriteLine("Goals saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    static List<Goal> LoadGoals()
    {
        try
        {
            using (StreamReader sr = new StreamReader("goals.json"))
            {
                string json = sr.ReadToEnd();
                return JsonSerializer.Deserialize<List<Goal>>(json);
            }
        }
        catch (FileNotFoundException)
        {
            return null; // No saved goals
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
            return null;
        }
    }
}
