using System;
using System.Threading;

public abstract class Activity
{
    protected string Name;
    protected string Description;
    protected int Duration;

    public Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"\nStarting {Name}...");
        Console.WriteLine(Description);
        SetDuration();
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000); 
    }

    public void EndActivity(double startTime)
    {
        double endTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        int duration = (int)(endTime - startTime);
        Console.WriteLine($"\nGood job! You have completed {Name} for {duration} seconds.");
        Thread.Sleep(3000); 
    }

    protected void SetDuration()
    {
        Console.Write("Enter duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
    }

    protected void Pause(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000); 
        }
        Console.WriteLine();
    }

    public abstract void PerformActivity();
}

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by guiding you through slow breathing.")
    {
    }

    public override void PerformActivity()
    {
        StartActivity();
        Console.WriteLine("Clear your mind and focus on your breathing.");
        for (int i = 0; i < Duration; i++)
        {
            Console.WriteLine("Breathe in...");
            Pause(3);
            Console.WriteLine("Breathe out...");
            Pause(3);
        }
        EndActivity(DateTimeOffset.Now.ToUnixTimeSeconds());
    }
}

public class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
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

    public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    public override void PerformActivity()
    {
        StartActivity();
        Console.WriteLine("Clear your mind and reflect on the prompts.");
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Length)];
        Console.WriteLine(prompt);

        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Pause(3);
        }

        EndActivity(DateTimeOffset.Now.ToUnixTimeSeconds());
    }
}

public class ListingActivity : Activity
{
    private string[] listPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void PerformActivity()
    {
        StartActivity();
        Random rand = new Random();
        string listPrompt = listPrompts[rand.Next(listPrompts.Length)];
        Console.WriteLine($"Think about: {listPrompt}");
        Console.WriteLine("You have several seconds to start listing items...");
        Pause(5);

        Console.WriteLine("Enter the items (one per line). Type 'done' when finished:");
        int count = 0;
        while (true)
        {
            string item = Console.ReadLine();
            if (item.ToLower() == "done")
                break;
            count++;
        }
        Console.WriteLine($"You listed {count} items.");

        EndActivity(DateTimeOffset.Now.ToUnixTimeSeconds());
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nChoose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1-4): ");
            int choice = int.Parse(Console.ReadLine());

            Activity activity;
            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.PerformActivity();
        }
    }
}
