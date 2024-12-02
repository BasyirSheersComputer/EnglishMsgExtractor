// Path to the input and output files
using System.Text.RegularExpressions;

string inputFilePath = @"D:\KSP_SKLDemo\set\Eng\message.ini";
string outputFilePath = @"D:\KSP_SKLDemo\set\Eng\message_output_contents.txt";

// Regular expression to match content within quotes for various header formats
string pattern = @"(?:CICO|CI|CO|WI)-\d{3}-\d{1}-\d{1}=""([^""]*)""";

// Regular expression to detect possible file or folder paths
string pathPattern = @"[A-Za-z]:\\(?:[\w\s]+\\)*[\w\s]+(?:\.[a-zA-Z]{2,})?";

try
{
    // Read all lines from the input file
    string[] lines = File.ReadAllLines(inputFilePath);

    // Use a HashSet to store unique lines
    HashSet<string> uniqueContents = new HashSet<string>();

    foreach (string line in lines)
    {
        // Find content within quotes in the current line
        Match match = Regex.Match(line, pattern);

        // If there's a match, add the content to the HashSet if it's not a path
        if (match.Success)
        {
            string content = match.Groups[1].Value;

            // Check if the content is a path
            if (!Regex.IsMatch(content, pathPattern))
            {
                uniqueContents.Add(content);
            }
        }
    }

    // Open the output file for writing
    using (StreamWriter writer = new StreamWriter(outputFilePath))
    {
        writer.WriteLine("Extracted Unique Content (Paths Removed)");
        writer.WriteLine("=========================================");
        writer.WriteLine();

        // Number each line in the output
        int lineNumber = 1;
        foreach (string content in uniqueContents)
        {
            writer.WriteLine($"{lineNumber}. {content}");
            lineNumber++;
        }
    }

    Console.WriteLine($"Processing complete. Unique results have been written to {outputFilePath}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}