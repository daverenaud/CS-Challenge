using System;
using System.Collections.Generic;

namespace JokeGenerator
{
    internal static class Program
    {
        private static void Main()
        {
            UserInterface.PrintStartupScreen();

            char userSelection;
            while ((userSelection = Console.ReadKey().KeyChar) != 'q')
            {
                switch (userSelection)
                {
                    case 'c':
                        UserInterface.PrintJokeCategories(JokeService.GetCategories());
                        break;
                    case 'r':
                        UserInterface.UserJokeParameters jokeParameters = UserInterface.GetJokeParametersFromUser();
                        List<string> jokes = JokeService.GetRandomJokes(jokeParameters.FirstName, jokeParameters.LastName,
                            jokeParameters.Category, jokeParameters.NumberOfJokes);
                        UserInterface.PrintJokes(jokes);
                        break;
                    case '?':
                        UserInterface.PrintUsageInstructions();
                        break;
                    default:
                        Console.WriteLine("The input selected is not recognized. Please try again. If you need help press '?'");
                        break;
                }
                
                UserInterface.PrintEmptyLines(2);
                Console.WriteLine("Execution Finished. Please make another selection.");
                UserInterface.PrintUsageInstructions();
            }
        }
    }
}
