using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public JournalEntry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date}: {Prompt}\n{Response}\n";
    }
}

class Journal
{
    private List<JournalEntry> entries;
    private string[] customPrompts;

    public Journal(string[] prompts)
    {
        entries = new List<JournalEntry>();
        customPrompts = prompts;
    }

    public void AddEntry(string prompt, string response, string date)
    {
        JournalEntry entry = new JournalEntry(prompt, response, date);
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (JournalEntry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (JournalEntry entry in entries)
            {
                writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    entries.Add(new JournalEntry(parts[1], parts[2], parts[0]));
                }
            }
        }
    }

    public string[] GetCustomPrompts()
    {
        return customPrompts;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string[] customPrompts = {
            "What is one thing I learned today?",
            "What is a goal I want to achieve tomorrow?",
            "What made me laugh today?",
            "What is something I am grateful for today?",
            "What is a new experience I had today?"
        };

        Journal journal = new Journal(customPrompts);

        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    string[] prompts = journal.GetCustomPrompts();
                    string prompt = prompts[new Random().Next(prompts.Length)];
                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("Response: ");
                    string response = Console.ReadLine();
                    string date = DateTime.Now.ToString("MM/dd/yyyy");
                    journal.AddEntry(prompt, response, date);
                    break;
                case 2:
                    journal.DisplayEntries();
                    break;
                case 3:
                    Console.Write("Enter filename to save: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveToFile(saveFilename);
                    break;
                case 4:
                    Console.Write("Enter filename to load: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
