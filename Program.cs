using System;
using System.IO;
using System.Text.RegularExpressions;

namespace github_index_helper
{
    class Program
    {
        static void Main()
        {

            string outputFile = "C:/Users/Ray/Documents/GitHub/webdev-tutorials/README.md";
            string rootFolder = "C:/Users/Ray/Documents/GitHub/webdev-tutorials";
            string headerRow = "# Index" + "\n" + "| Provider | Course Link | GitHub Code | Live Demo |\n" + "| --- | --- | --- | --- |" + "\n";
            File.WriteAllText(outputFile, headerRow);
            string rootFolderName = Path.GetFileName(rootFolder);
            string[] subFolders = Directory.GetDirectories(rootFolder);


            foreach (string subFolder in subFolders)
            {
                string readmePath = Path.Combine(subFolder, "README.md");

                if (File.Exists(readmePath))
                {

                    string subFolderName = Path.GetFileName(subFolder);
                    string content = File.ReadAllText(readmePath);

                    string[] dataArray = ExtractContent(readmePath, content, subFolderName, rootFolderName);

                    string row = GetRow(dataArray);

                    File.AppendAllText(outputFile, $"{row}\n");


                }


            }





        }


        static string[] ExtractContent(string readmeFilePath, string fileContents, string subFolderName, string rootFolderName)
        {

            string regexPattern = @"# Provider([\s\S]*?)# Description([\s\S]*?)# Link([\s\S]*)";
            Match match = Regex.Match(fileContents, regexPattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                Group providerGroup = match.Groups[1];
                // Group descriptionGroup = match.Groups[2];
                Group linkGroup = match.Groups[3];

                string provider = providerGroup.Value.Trim();
                // string description = descriptionGroup.Value.Trim();
                string link = linkGroup.Value.Trim();

                string[] dataArray = new string[] { provider, link, subFolderName, rootFolderName };

                return dataArray;


            }

            return new string[0];


        }


        static string GetRow(string[] dataArray)
        {

            string provider = dataArray[0];
            string link = dataArray[1];
            string subFolderName = dataArray[2];
            string rootFolderName = dataArray[3];

            return $" {provider} | {link} | [{subFolderName}/]({subFolderName}/) | https://demos.raydean.tech/{rootFolderName}/{subFolderName} |";

        }

    }
}





