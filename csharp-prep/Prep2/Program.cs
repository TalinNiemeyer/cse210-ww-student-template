using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep2 World!");

        Console.WriteLine("Enter your grade percentage:");
        int gradePercentage = int.Parse(Console.ReadLine());

        char letter;
        if (gradePercentage >= 90)
        {
            letter = 'A';
        }
        else if (gradePercentage >= 80)
        {
            letter = 'B';
        }
        else if (gradePercentage >= 70)
        {
            letter = 'C';
        }
        else if (gradePercentage >= 60)
        {
            letter = 'D';
        }
        else
        {
            letter = 'F';
        }

        char sign = ' ';
        if (gradePercentage % 10 >= 7)
        {
            sign = '+';
        }
        else if (gradePercentage % 10 < 3 && gradePercentage < 90 && gradePercentage >= 60)
        {
            sign = '-';
        }

        if (letter == 'A' && gradePercentage == 100)
        {
            sign = ' ';
        }
        else if (letter == 'F' && (gradePercentage == 59 || gradePercentage == 58))
        {
            sign = ' ';
        }

        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course with a grade of " + letter + sign);
        }
        else
        {
            Console.WriteLine("Unfortunately, you did not pass the course with a grade of " + letter + sign + ". Better luck next time!");
        }
    }
}