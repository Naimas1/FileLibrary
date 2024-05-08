using System;
using System.IO;
using System.Text;

namespace FileLibrary
{
    public static class FileHandler
    {
        public static void CopyFile(string sourceFile, string destinationFile)
        {
            File.Copy(sourceFile, destinationFile);
        }

        public static void CopyDirectory(string sourceDirectory, string destinationDirectory)
        {
            Directory.CreateDirectory(destinationDirectory);
            foreach (string file in Directory.GetFiles(sourceDirectory))
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationDirectory, fileName);
                File.Copy(file, destFile, true);
            }
            foreach (string subDirectory in Directory.GetDirectories(sourceDirectory))
            {
                string directoryName = Path.GetFileName(subDirectory);
                string destDirectory = Path.Combine(destinationDirectory, directoryName);
                CopyDirectory(subDirectory, destDirectory);
            }
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static void DeleteFiles(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                File.Delete(fileName);
            }
        }

        public static void DeleteFilesWithPattern(string directoryPath, string searchPattern)
        {
            foreach (string file in Directory.GetFiles(directoryPath, searchPattern))
            {
                File.Delete(file);
            }
        }

        public static void MoveFile(string sourceFile, string destinationFile)
        {
            File.Move(sourceFile, destinationFile);
        }

        public static void SearchWordInFile(string filePath, string wordToSearch)
        {
            string[] lines = File.ReadAllLines(filePath);
            int occurrences = 0;
            foreach (string line in lines)
            {
                if (line.Contains(wordToSearch))
                {
                    occurrences++;
                }
            }

            string reportFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_report.txt";
            string reportFilePath = Path.Combine(Path.GetDirectoryName(filePath), reportFileName);

            using (StreamWriter writer = new StreamWriter(reportFilePath))
            {
                writer.WriteLine($"Word '{wordToSearch}' was found {occurrences} times in file {filePath}");
            }
        }

        public static void SearchWordInFolder(string folderPath, string wordToSearch)
        {
            string[] files = Directory.GetFiles(folderPath);
            StringBuilder reportContent = new StringBuilder();

            foreach (string file in files)
            {
                string[] lines = File.ReadAllLines(file);
                int occurrences = 0;
                foreach (string line in lines)
                {
                    if (line.Contains(wordToSearch))
                    {
                        occurrences++;
                    }
                }

                reportContent.AppendLine($"Word '{wordToSearch}' was found {occurrences} times in file {file}");
            }

            string reportFileName = $"{Path.GetFileName(folderPath)}_report.txt";
            string reportFilePath = Path.Combine(folderPath, reportFileName);

            using (StreamWriter writer = new StreamWriter(reportFilePath))
            {
                writer.WriteLine(reportContent.ToString());
            }
        }

        public static void Main(string[] args)
        {
            // Example usage of the FileHandler class methods
            string sourceFilePath = "source.txt";
            string destinationFilePath = "destination.txt";
            string folderPath = "folder";
            string wordToSearch = "example";

            // Copy file
            CopyFile(sourceFilePath, destinationFilePath);
            Console.WriteLine("File copied successfully!");

            // Delete file
            DeleteFile(destinationFilePath);
            Console.WriteLine("File deleted successfully!");

            // Copy directory
            CopyDirectory(folderPath, "copy_of_folder");
            Console.WriteLine("Directory copied successfully!");

            // Delete files with pattern
            DeleteFilesWithPattern("folder", "*.txt");
            Console.WriteLine("Files with pattern deleted successfully!");

            // Search word in file
            SearchWordInFile(sourceFilePath, wordToSearch);
            Console.WriteLine("Word search in file completed!");

            // Search word in folder
            SearchWordInFolder(folderPath, wordToSearch);
            Console.WriteLine("Word search in folder completed!");
        }
    }
}
