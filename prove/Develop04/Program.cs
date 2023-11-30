using System;
using System.Threading;

public class MindfulnessActivity
{
    protected string activityName;
    protected string activityDescription;
    protected int duration;

    public MindfulnessActivity(string name, string description)
    {
        activityName = name;
        activityDescription = description;
    }

    public void StartActivity()
    {
        SetDuration();
        DisplayStartingMessage();

        // Pause before starting
        Pause(3);

        PerformActivity();

        DisplayEndingMessage();
    }

    protected virtual void PerformActivity()
    {
        // Base class does nothing; override in derived classes
    }

    private void SetDuration()
    {
        Console.Write($"Enter the duration for the {activityName} activity in seconds: ");
        duration = int.Parse(Console.ReadLine());
    }

    private void DisplayStartingMessage()
    {
        Console.WriteLine($"Welcome to the {activityName} activity!");
        Console.WriteLine(activityDescription);
        Console.WriteLine($"Get ready to start. Duration: {duration} seconds.");
    }

    private void DisplayEndingMessage()
    {
        Console.WriteLine($"Great job! You've completed the {activityName} activity for {duration} seconds.");
    }

    protected void Pause(int seconds)
    {
        Console.WriteLine("Pausing...");
        // Simulate animation or countdown timer during pause
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000); // Pause for 1 second
        }
        Console.WriteLine();
    }
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Let's begin the breathing activity:");

        for (int i = 0; i < duration; i += 2)
        {
            Console.WriteLine("Breathe in...");
            Pause(2);

            Console.WriteLine("Breathe out...");
            Pause(2);
        }
    }
}

public class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] reflectionQuestions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Let's begin the reflection activity:");

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        foreach (string question in reflectionQuestions)
        {
            Console.WriteLine(question);
            Pause(5); // Pause for 5 seconds after each question
        }
    }
}

public class ListingActivity : MindfulnessActivity
{
    private string[] listingPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Let's begin the listing activity:");

        Random random = new Random();
        string prompt = listingPrompts[random.Next(listingPrompts.Length)];
        Console.WriteLine(prompt);

        Console.WriteLine("You have 10 seconds to list as many items as you can:");
        Pause(10); // Pause for 10 seconds

        int itemCount = GetItemCount(); // Simulate getting the count of items entered
        Console.WriteLine($"You listed {itemCount} items.");
    }

    private int GetItemCount()
    {
        // Simulate counting the items entered by the user
        Random random = new Random();
        return random.Next(1, 11); // Return a random number between 1 and 10
    }
}

class Program
{
    static void Main()
    {
        // Example usage
        MindfulnessActivity breathingActivity = new BreathingActivity();
        breathingActivity.StartActivity();

        MindfulnessActivity reflectionActivity = new ReflectionActivity();
        reflectionActivity.StartActivity();

        MindfulnessActivity listingActivity = new ListingActivity();
        listingActivity.StartActivity();
    }
}
