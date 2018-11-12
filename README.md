# README #

## Changes to the code ##

### General ###

* Removed the ConsolePrinter class. This was done because the only time it was used in the code was 
    to immediately call ToString after instantiation. The object can simply be replaced by a Console.WriteLine call in 
    all use cases which removes the overhead of instantiating a new object and storing a value temporarily.
    
    
### Main ###
#### General ####
* Moved global variables into their own functions where needed.
* Removed getEnteredKey function which can be replaced by Console.ReadKey().KeyChar.
* Removed the PrintResults function which is replaced by new functions in the UserInterface class.
* Removed the GetRandomJokes function because it was unnecessary after refactoring the JsonFeed class.
* Removed the getCategories function because it was unnecessary after refactoring the JsonFeed class.
* Removed the GetNames function because it was unnecessary after refactoring the JsonFeed class.
* Simplified the logic by refactoring each step out to functions in the various helper classes.


### JsonFeed ###
#### General ####
* Renamed to JokeService which better describes what it represents.
* Made the class completely static because none of its state is never changed after instantiation.
* Intialized the HttpClient in the constructor rather than repeatedly initializing on every method call. This saves the
    overhead associated with instantiating and destroying objects multiple time.
#### GetRandomJokes function
* Refactored to use a query string generated through the HttpUtility. This allows us to simply set the parameters instead 
    of worrying about whether there is or isn't already a parameter in the URI.
* Now returns a list containing just the jokes themselves instead of the raw JSON strings for each joke which had other
    unnecessary text.
    
#### GetNames function ####
* Moved this function to a new class because it calls a different external service.

### New Classes ###

#### NameService ####
* Class to represent the service which fetches random people's names. Created to provide separation from the JokeService.

#### UserInterface ####
* Utility class to handle all display of the user interface. Created to separate the formatting of the output from the
    logic of the application.