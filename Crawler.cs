using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace github_index_helper
{
    class Crawler
    {
        public static void LoopThroughFolders(string folderPath, bool scanSubfolders)
        {
            string outputFile = $"{folderPath}/README.md";
            string rootFolder = folderPath;
            string rootFolderName = Path.GetFileName(rootFolder);
            string[] foldersToSkip = { ".vscode", ".git" };
            string[] subFolders = Directory.GetDirectories(rootFolder);
            List<string> rows = new List<string>();

            foreach (string subFolder in subFolders)
            {
                
                string subFolderName = Path.GetFileName(subFolder);

                if (Array.IndexOf(foldersToSkip, subFolderName) >= 0)
                {
                    // skips the remaining code within the current iteration of the loop and proceeds to the next iteration.
                    continue;
                };


                if (scanSubfolders)
                {
                    string readmePath = Path.Combine(subFolder, "README.md");
                    if (File.Exists(readmePath))
                    {

                        string content = File.ReadAllText(readmePath);
                        string[] dataArray = ExtractContent(readmePath, content, subFolderName, rootFolderName);
                        string row = GetRowFullInfo(dataArray);
                        rows.Add(row);

                    }
                }
                else
                {
                    rootFolderName = Path.GetFileName(rootFolder);
                    string row = GetRowBasicInfo(subFolderName, rootFolderName);
                    rows.Add(row);
                }
            }


            InsertTableIntoFile(outputFile, rows, scanSubfolders);

        }

        static string[] ExtractContent(string readmeFilePath, string fileContents, string subFolderName, string rootFolder)
        {
            string regexPattern = @"# Provider([\s\S]*?)# Description([\s\S]*?)# Link([\s\S]*)";
            Match match = Regex.Match(fileContents, regexPattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                Group providerGroup = match.Groups[1];
                Group linkGroup = match.Groups[3];

                string provider = providerGroup.Value.Trim();
                string link = linkGroup.Value.Trim();

                string[] dataArray = new string[] { provider, link, subFolderName, rootFolder };
                return dataArray;
            }

            return new string[0];
        }

        static string GetRowFullInfo(string[] dataArray)
        {
            string provider = dataArray[0];
            string link = dataArray[1];
            string subFolderName = dataArray[2];
            string rootFolderName = dataArray[3];

            string row = $" {provider} | {link} | [{subFolderName}]({Uri.EscapeDataString(subFolderName)}/) | https://demos.raydean.tech/{Uri.EscapeDataString(rootFolderName)}/{Uri.EscapeDataString(subFolderName)} |";

            return row;
        }

        static string GetRowBasicInfo(string subFolderName, string rootFolderName)
        {
            string row = $"[{subFolderName}]({Uri.EscapeDataString(subFolderName)}/) | https://demos.raydean.tech/{Uri.EscapeDataString(rootFolderName)}/{Uri.EscapeDataString(subFolderName)} |";
            return row;
        }

        static void InsertTableIntoFile(string outputFile, List<string> rows, bool scanSubfolders)
        {
            string headerRow;

            if (scanSubfolders)
            {
                headerRow = "# Index" + "\n" + "| Provider | Course Link | GitHub Code | Live Demo |\n" + "| --- | --- | --- | --- |";
            }
            else
            {
                headerRow = "# Index" + "\n" + "GitHub Code | Live Demo |\n" + "| --- | --- |";
            };

            // Create a StringBuilder to accumulate content
            StringBuilder contentBuilder = new StringBuilder();

            // Append lines to the StringBuilder
            contentBuilder.AppendLine(headerRow);

            // Append each row to the StringBuilder
            foreach (string row in rows)
            {
                contentBuilder.AppendLine(row);
            };

            // Convert the StringBuilder to a string
            string strBuilderContent = contentBuilder.ToString();

            Console.WriteLine("=== Content intended for insertion ===");
            Console.WriteLine(strBuilderContent);


            string marker = "# Index";

            // Read the existing content of the file
            string fileContent = File.ReadAllText(outputFile);

            // Find the position of the marker
            int markerPosition = fileContent.IndexOf(marker);

            if (markerPosition != -1)
            {

                // Insert the new content at the marker position
                fileContent = fileContent.Substring(0, markerPosition) + strBuilderContent;

                // Write the updated content back to the file
                File.WriteAllText(outputFile, fileContent);
            };


        }

    }

}
