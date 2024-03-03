using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep3 World!");

        Random random = new Random();
        int magicNumber = random.Next(1, 101); // Generate a random number between 1 and 100
        int guess;
        int attempts = 0;

        do
        {
            Console.WriteLine("What is your guess?");
            guess = int.Parse(Console.ReadLine());
            attempts++;

            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        } while (guess != magicNumber);

        Console.WriteLine($"You made {attempts} guesses.");

        Console.WriteLine("Do you want to play again? (yes/no)");
        string playAgain = Console.ReadLine().ToLower();

        while (playAgain == "yes")
        {
            magicNumber = random.Next(1, 101);
            attempts = 0;

            do
            {
                Console.WriteLine("What is your guess?");
                guess = int.Parse(Console.ReadLine());
                attempts++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            } while (guess != magicNumber);

            Console.WriteLine($"You made {attempts} guesses.");
            Console.WriteLine("Do you want to play again? (yes/no)");
            playAgain = Console.ReadLine().ToLower();
        }
        
    }
}