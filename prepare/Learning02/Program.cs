using System;
using System.Collections.Generic;

public class Job
{
    private string _company;
    private string _jobTitle;
    private int _startYear;
    private int _endYear;

    public Job(string company, string jobTitle, int startYear, int endYear)
    {
        _company = company;
        _jobTitle = jobTitle;
        _startYear = startYear;
        _endYear = endYear;
    }

    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}

public class Resume
{
    private string _personName;
    private List<Job> _jobs;

    public Resume(string personName)
    {
        _personName = personName;
        _jobs = new List<Job>();
    }

    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }

    public void Display()
    {
        Console.WriteLine($"Name: {_personName}");
        foreach (var job in _jobs)
        {
            job.Display();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job("BYU-I", "Opereation Manager", 2025, 2029);
        Job job2 = new Job("Bestbuy", "Sales Tech", 2022, 2025);

        job1.Display();
        job2.Display();

        Resume myResume = new Resume("Talin Niemeyer");
        myResume.AddJob(job1);
        myResume.AddJob(job2);

        myResume.Display();
    }
}
