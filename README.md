# Scripture Memorizer App
This is a C# console application that helps users memorize scriptures by hiding random words in the text and prompting the user to recall the missing words. The difficulty of the game is customizable, and the scriptures are loaded from a JSON file.

## Installation
To run the Journal App, you need to have the .NET Framework installed on your computer. You can download the latest version of the .NET Framework from the Microsoft website. Once you have installed the .NET Framework, you can download or clone the Journal App source code from the repository on GitHub.

The program will prompt the user to enter the path to a JSON file containing a list of scriptures. The JSON file should be an array of objects, where each object represents a scripture and has the following format:

'''
{
  "reference": "John 3:16",
  "text": "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."
}
'''

## Usage
When the program is running, it will display a randomly selected scripture from the list loaded from the JSON file. The user can then choose a difficulty level by entering a number between 1 and 3. The difficulty level determines the number of words that will be hidden from the scripture.

After the user chooses a difficulty level, the program will begin hiding words from the scripture. The user must then try to recall the hidden words in the correct order. If the user gets stuck, they can type "quit" to exit the program.