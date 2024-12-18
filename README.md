   # Advent of Code

   Welcome to the **Advent of Code** repository! This project contains solutions to the Advent of Code challenges, written in C#.

   ## Description

   Advent of Code is an annual event that provides small programming puzzles for a variety of skill sets and skill levels that can be solved in any programming language. This repository includes my solutions to these challenges.

   ## Installation

   To run the solutions locally, you need to have the .NET SDK installed on your machine. You can download it from the [official .NET website](https://dotnet.microsoft.com/download).

   1. Clone the repository:
      ```bash
      git clone https://github.com/Agrael11/Advent-Of-Code.git
      cd Advent-Of-Code
      ```

   2. Restore the dependencies:
      ```bash
      dotnet restore
      ```

   ## Usage

   ### Running the Graphical UI

   The project uses Avalonia for the graphical user interface (GUI). You can start the GUI with the following command:

   ```bash
   dotnet run --project <path_to_ui_project>
   ```

   To run the console version, use the `--console` or `-c` argument:

   ```bash
   dotnet run --project <path_to_ui_project> -- --console
   dotnet run --project <path_to_ui_project> -c
   ```

   Replace `<path_to_ui_project>` with the actual path to the UI project within the repository.

   ### Running Specific Days' Challenges

   Each day of the challenge is organized as a library project with a namespace structure like `advent_of_code.Year2015.Day01` for the year 2015, day 01.

   Each part of the challenge has two static classes: `Challange1` and `Challange2`, each containing a static method `DoChallange` that takes a string input and returns a result. The `DoChallange` method is automatically invoked by the UI.

   ```csharp
   // Example method signature
   public static <return_type> DoChallange(string inputData)
   ```

   ### Platform Compatibility

   This AoC Launcher has been tested to work with Android, Windows, and Linux builds.

   ### Automatic Input Download

   You can provide a browser cookie to automatically download inputs for the challenges. 

   ## Contributing

   Contributions to Launcher are welcome! If you have suggestions or improvements, feel free to open an issue or submit a pull request.
   If you noticed a bug in one of solutions, ot it doesn't work for your input, please let me know :)

   ## Author

   **Agrael11** / **Tachi**

   ## License

   This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

   ## Acknowledgments

   - [Advent of Code](https://adventofcode.com/) for providing these wonderful challenges.
