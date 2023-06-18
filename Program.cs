using System;
using System.IO;

namespace github_index_helper
{
    class Program
    {
        static void Main()
        {

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            string folderPath = string.Empty;

            while (string.IsNullOrEmpty(folderPath))
            {
                // Prompt for Folder path
                Console.WriteLine($"Please provide a Folder path [{currentDirectory}]:");
                string folderPathInput = Console.ReadLine();

                // Use default directory if no Folder path is provided
                folderPath = string.IsNullOrWhiteSpace(folderPathInput) ? currentDirectory : folderPathInput.Trim();

                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("Error: Invalid Folder path");
                    folderPath = string.Empty;
                }
            }

            bool scanSubfolders = false;

            while (true)
            {
                // Prompt for scanning subfolders
                Console.WriteLine("Would you like me to scan subfolders for README.md files? Y/[N]:");
                string scanSubfoldersInput = Console.ReadLine().ToUpper();

                if (scanSubfoldersInput == "Y")
                {
                    scanSubfolders = true;
                    break; // Exit the loop if "Y" is selected
                }
                else if (scanSubfoldersInput == "N" || string.IsNullOrEmpty(scanSubfoldersInput))
                {
                    break; // Exit the loop if "N" or empty input is selected
                }
                else
                {
                    Console.WriteLine("Error: Invalid Input.");
                }
            }

            // Console.WriteLine("Folder path: " + folderPath);
            // Console.WriteLine("Scan subfolders: " + scanSubfolders);

            Crawler.LoopThroughFolders(folderPath, scanSubfolders);

        }
    }
}
