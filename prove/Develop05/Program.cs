using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Base class for goals
public abstract class Goal
{
    public string Name { get; }
    public bool IsCompleted { get; protected set; }

    protected Goal(string name)
    {
        Name = name;
        IsCompleted = false;
    }

    public abstract int RecordEvent();
    public abstract string DisplayStatus();
}

// Simple goal class
public class SimpleGoal : Goal
{
    private readonly int _reward;

    public SimpleGoal(string name, int reward) : base(name)
    {
        _reward = reward;
    }

    public override int RecordEvent()
    {
        IsCompleted = true;
        return _reward;
    }

    public override string DisplayStatus()
    {
        return IsCompleted ? $"[X] {Name}" : $"[ ] {Name}";
    }
}

// Eternal goal class
public class EternalGoal : Goal
{
    private readonly int _reward;

    public EternalGoal(string name, int reward) : base(name)
    {
        _reward = reward;
    }

    public override int RecordEvent()
    {
        return _reward;
    }

    public override string DisplayStatus()
    {
        return $"[ ] {Name}";
    }
}

// Checklist goal class
public class ChecklistGoal : Goal
{
    private readonly int _rewardPerEvent;
    private readonly int _targetCount;
    private int _completedCount;

    public ChecklistGoal(string name, int rewardPerEvent, int targetCount) : base(name)
    {
        _rewardPerEvent = rewardPerEvent;
        _targetCount = targetCount;
        _completedCount = 0;
    }

    public override int RecordEvent()
    {
        _completedCount++;
        if (_completedCount == _targetCount)
        {
            IsCompleted = true;
            return _rewardPerEvent * _completedCount + 500; // Bonus points
        }
        return _rewardPerEvent;
    }

    public override string DisplayStatus()
    {
        return IsCompleted ? $"Completed {_completedCount}/{_targetCount} times - [X] {Name}" : $"Completed {_completedCount}/{_targetCount} times - [ ] {Name}";
    }
}

// Main program class
class Program
{
    static List<Goal> goals = new List<Goal>();

    static void Main(string[] args)
    {
        LoadGoals(); // Load saved goals and scores
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nEternal Quest Program");
            Console.WriteLine("1. Add New Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals and Scores");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddNewGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    DisplayGoals();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    SaveGoals();
                    break;
                case "6":
                    SaveGoals();
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddNewGoal()
    {
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();
        Console.WriteLine("Select the type of goal:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Enter the reward points: ");
                int reward = Convert.ToInt32(Console.ReadLine());
                goals.Add(new SimpleGoal(name, reward));
                break;
            case "2":
                Console.Write("Enter the reward points: ");
                int eternalReward = Convert.ToInt32(Console.ReadLine());
                goals.Add(new EternalGoal(name, eternalReward));
                break;
            case "3":
                Console.Write("Enter the reward points per event: ");
                int rewardPerEvent = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the target count: ");
                int targetCount = Convert.ToInt32(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, rewardPerEvent, targetCount));
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void RecordEvent()
    {
        Console.WriteLine("Select the goal to record event:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
        Console.Write("Enter your choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice > 0 && choice <= goals.Count)
        {
            int pointsEarned = goals[choice - 1].RecordEvent();
            Console.WriteLine($"Event recorded for {goals[choice - 1].Name}. You earned {pointsEarned} points.");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    static void DisplayGoals()
    {
        Console.WriteLine("Current Goals:");
        foreach (var goal in goals)
        {
              Console.WriteLine(goal.DisplayStatus());
        }
    }

    static void DisplayScore()
    {
        int totalScore = goals.Where(g => g.IsCompleted).Sum(g => g.RecordEvent());
        Console.WriteLine($"Total Score: {totalScore}");
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.IsCompleted}");
            }
        }
        Console.WriteLine("Goals and scores saved successfully.");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            goals.Clear();
            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string typeName = parts[0];
                    string name = parts[1];
                    bool isCompleted = Convert.ToBoolean(parts[2]);

                    switch (typeName)
                    {
                        case "SimpleGoal":
                            goals.Add(new SimpleGoal(name, 0) { IsCompleted = isCompleted });
                            break;
                        case "EternalGoal":
                            goals.Add(new EternalGoal(name, 0) { IsCompleted = isCompleted });
                            break;
                        case "ChecklistGoal":
                            goals.Add(new ChecklistGoal(name, 0, 0) { IsCompleted = isCompleted });
                            break;
                    }
                }
            }
            Console.WriteLine("Goals and scores loaded successfully.");
        }
    }
}
         
