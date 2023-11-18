using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

class Goal
{
    public string Name { get; set; }
    public string Action { get; set; }

    public Goal(string name, string action)
    {
        Name = name;
        Action = action;
    }

    public override string ToString()
    {
        return $"Goal: {Name}\nAction: {Action}\n";
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();
    private List<Goal> goals = new List<Goal>();

    public void AddEntry(string prompt, string response, string date)
    {
        Entry newEntry = new Entry(prompt, response, date);
        entries.Add(newEntry);
    }

    public void AddGoal(string name, string action)
    {
        Goal newGoal = new Goal(name, action);
        goals.Add(newGoal);
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            Console.WriteLine(goal);
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            // Guardar entradas
            foreach (var entry in entries)
            {
                sw.WriteLine($"Entry|{entry.Date}|{entry.Prompt}|{entry.Response}");
            }

            // Guardar metas
            foreach (var goal in goals)
            {
                sw.WriteLine($"Goal|{goal.Name}|{goal.Action}");
            }
        }
    }

    public void LoadFromFile(string fileName)
    {
        entries.Clear(); // Clear existing entries before loading
        goals.Clear();   // Clear existing goals before loading

        using (StreamReader sr = new StreamReader(fileName))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 3)
                {
                    if (parts[0] == "Entry")
                    {
                        entries.Add(new Entry(parts[2], parts[3], parts[1]));
                    }
                    else if (parts[0] == "Goal")
                    {
                        goals.Add(new Goal(parts[1], parts[2]));
                    }
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Journal journal = new Journal();

        List<string> prompts = new List<string>
        {
            "What was the most important thing that happened to you today?",
            "What made you feel special today?",
            "How did I see the hand of the Lord in my life today?",
            "What did you do to remember the Savior today?",
            "If you had to give one piece of advice to your offspring today, what would it be?",
            "Who did you pray for today?",
            "Something you want to say to the person you love?"
        };

        while (true)
        {
            Console.WriteLine("1. Write about your day");
            Console.WriteLine("2. Display your journal");
            Console.WriteLine("3. Write a goal");
            Console.WriteLine("4. Display your goals");
            Console.WriteLine("5. Save your journal to a file");
            Console.WriteLine("6. Load your journal from a file");
            Console.WriteLine("7. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Selecting random prompt...");
                    string randomPrompt = prompts[new Random().Next(prompts.Count)];
                    Console.WriteLine($"Prompt: {randomPrompt}");
                    Console.Write("Enter your response: ");
                    string response = Console.ReadLine();
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    journal.AddEntry(randomPrompt, response, currentDate);
                    Console.WriteLine("Entry added!\n");
                    break;

                case "2":
                    Console.WriteLine("Displaying journal entries...\n");
                    journal.DisplayEntries();
                    break;

                case "3":
                    Console.Write("Enter the name of the goal: ");
                    string goalName = Console.ReadLine();
                    Console.Write($"What are you going to do to achieve {goalName}? ");
                    string goalAction = Console.ReadLine();
                    journal.AddGoal(goalName, goalAction);
                    Console.WriteLine("Goal added!\n");
                    break;

                case "4":
                    Console.WriteLine("Displaying goals...\n");
                    journal.DisplayGoals();
                    break;

                case "5":
                    Console.Write("Enter the file name to save the journal: ");
                    string saveFileName = Console.ReadLine();
                    journal.SaveToFile(saveFileName);
                    Console.WriteLine($"Journal saved to {saveFileName}\n");
                    break;

                case "6":
                    Console.Write("Enter the file name to load the journal: ");
                    string loadFileName = Console.ReadLine();
                    journal.LoadFromFile(loadFileName);
                    Console.WriteLine($"Journal loaded from {loadFileName}\n");
                    break;

                case "7":
                    Console.WriteLine("Exiting program. See u tomorrow!");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.\n");
                    break;
            }
        }
    }
}
