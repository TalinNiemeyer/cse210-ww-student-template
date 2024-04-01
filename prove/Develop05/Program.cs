using System;
using System.Collections.Generic;

public class Goal
{
    public string Name { get; }
    public bool Completed { get; set; }

    public Goal(string name)
    {
        Name = name;
        Completed = false;
    }

    public virtual int GetValue()
    {
        return 0;
    }
}

public class SimpleGoal : Goal
{
    public int Points { get; }

    public SimpleGoal(string name, int points) : base(name)
    {
        Points = points;
    }

    public override int GetValue()
    {
        return Completed ? Points : 0;
    }
}

public class EternalGoal : Goal
{
    public int Points { get; }

    public EternalGoal(string name, int points) : base(name)
    {
        Points = points;
    }

    public override int GetValue()
    {
        return Completed ? Points : 0;
    }
}

public class ChecklistGoal : Goal
{
    public int PointsPerCompletion { get; }
    public int TotalCompletions { get; }
    public int Completions { get; private set; }
    public int BonusPoints { get; }

    public ChecklistGoal(string name, int pointsPerCompletion, int totalCompletions, int bonusPoints) : base(name)
    {
        PointsPerCompletion = pointsPerCompletion;
        TotalCompletions = totalCompletions;
        BonusPoints = bonusPoints;
    }

    public void RecordCompletion()
    {
        Completions++;
        if (Completions == TotalCompletions)
            Completed = true;
    }

    public override int GetValue()
    {
        int value = Completions * PointsPerCompletion;
        if (Completed)
            value += BonusPoints;
        return value;
    }
}

public class User
{
    public string Name { get; }
    public List<Goal> Goals { get; } = new List<Goal>();
    public int Score { get; private set; } = 0;

    public User(string name)
    {
        Name = name;
    }

    public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        foreach (Goal goal in Goals)
        {
            if (goal.Name == goalName)
            {
                goal.Completed = true;
                Score += goal.GetValue();
                break;
            }
        }
    }

    public void DisplayGoals()
    {
        foreach (Goal goal in Goals)
        {
            string completionStatus = goal.Completed ? "Completed" : "Not Completed";
            string displayString = goal is ChecklistGoal ?
                $"{goal.Name}: {completionStatus} {((ChecklistGoal)goal).Completions}/{((ChecklistGoal)goal).TotalCompletions} times" :
                $"{goal.Name}: {completionStatus}";
            Console.WriteLine(displayString);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        User user = new User("John");

        user.AddGoal(new SimpleGoal("Run a Marathon", 1000));
        user.AddGoal(new EternalGoal("Read Scriptures", 100));
        user.AddGoal(new ChecklistGoal("Attend Temple", 50, 10, 500));

        user.RecordEvent("Run a Marathon");
        user.RecordEvent("Read Scriptures");
        user.RecordEvent("Attend Temple");
        user.RecordEvent("Attend Temple");
        user.RecordEvent("Attend Temple");
        user.RecordEvent("Attend Temple");
        user.RecordEvent("Attend Temple");
        user.RecordEvent("Attend Temple");
        user.RecordEvent("Attend Temple");
        user.RecordEvent("Attend Temple");
        user.RecordEvent("Attend Temple");

        user.DisplayGoals();
        Console.WriteLine($"Score: {user.Score}");
    }
}
