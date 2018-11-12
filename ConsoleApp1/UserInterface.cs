using System;
using System.Collections.Generic;
using System.Net;

namespace JokeGenerator
{
    public static class UserInterface
    {
        public class UserJokeParameters
        {
            public string FirstName;
            public string LastName;
            public string Category;
            public int NumberOfJokes = -1;
        }
        
        /// <summary>
        ///     Prints the initial screen shown to the user on application startup to the console.
        /// </summary>
        public static void PrintStartupScreen()
        {
            Console.WriteLine("David Renaud's Awesome Joke Generator");
            PrintEmptyLines(1);
            Console.WriteLine("?: Print the usage instructions.");
            PrintUsageInstructions();
        }

        /// <summary>
        ///     Prints the available joke categories to the console
        /// </summary>
        /// <param name="categories">The list of categories to print</param>
        public static void PrintJokeCategories(List<string> categories)
        {
            PrintEmptyLines(1);
            Console.WriteLine("Categories:");
            Console.WriteLine("- "+ string.Join(Environment.NewLine + "- ", categories));
        }

        /// <summary>
        ///     Prints the usage instructions for the application.
        /// </summary>
        public static void PrintUsageInstructions()
        {
            Console.WriteLine("c: Get a list of joke categories.");
            Console.WriteLine("r: Get a list of random jokes.");
            Console.WriteLine("q: Quit the program.");
            Console.Write("Please select an action from the above list by typing the corresponding character: ");
        }

        /// <summary>
        ///     Prompts the user for all required joke parameters
        /// </summary>
        /// <returns>The joke parameters chosen by the user</returns>
        public static UserJokeParameters GetJokeParametersFromUser()
        {
            UserJokeParameters jokeParameters = new UserJokeParameters();
            
            Console.Write(Environment.NewLine + "Would you like to substitute a random name in the jokes (y/N)? ");
            if (Console.ReadKey().KeyChar == 'y')
            {
                Tuple<string, string> randomName = NameService.GetRandomName();
                jokeParameters.FirstName = randomName?.Item1;
                jokeParameters.LastName = randomName?.Item2;
            }
            
            Console.Write(Environment.NewLine +  "Would you like to specify a category (y/N)? ");
            if (Console.ReadKey().KeyChar == 'y')
            {
                while (jokeParameters.Category == null)
                {
                    List<string> categories = JokeService.GetCategories();
                    PrintJokeCategories(categories);
                    PrintEmptyLines(1);
                    Console.Write("Which category would you like? ");
                    string chosenCategory = Console.ReadLine();
                    if(!categories.Contains(chosenCategory))
                    {
                        Console.WriteLine("Invalid input. Chosen category is not in the list of available categories.");                    
                    }
                    else
                    {
                        jokeParameters.Category = chosenCategory;
                    }
                }
            }
            
            Console.Write(Environment.NewLine + "How many jokes would you like to be generated? ");
            while (jokeParameters.NumberOfJokes < 1 || jokeParameters.NumberOfJokes > 9)
            {
                try
                {
                    jokeParameters.NumberOfJokes = int.Parse(Console.ReadLine());
                    if (jokeParameters.NumberOfJokes < 1 || jokeParameters.NumberOfJokes > 9)
                    {
                        Console.WriteLine("Invalid input. Number of jokes must be an integer from 1 - 9.");
                    }
                }
                catch (Exception e) when (e is ArgumentNullException || e is FormatException)
                {
                    Console.WriteLine("Invalid input. The entered value is null or is not an integer.");
                }
                catch (Exception e) when (e is OverflowException)
                {
                    Console.WriteLine("Invalid input. The entered value is out of the acceptable range. Please choose a number between 1 and " + int.MaxValue);
                }
            }

            return jokeParameters;
        }

        /// <summary>
        ///     Prints the list of jokes to the console.
        /// </summary>
        /// <param name="jokes">The jokes to print</param>
        public static void PrintJokes(List<string> jokes)
        {
            Console.WriteLine("Generated Jokes:");
            Console.WriteLine("- " + string.Join(Environment.NewLine + "- ", jokes));
        }

        /// <summary>
        ///     Prints empty lines to the console to create vertical space
        /// </summary>
        /// <param name="numberOfLines">The number of lines to print</param>
        public static void PrintEmptyLines(int numberOfLines)
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                Console.Write(Environment.NewLine);
            }
        }
    }
}