using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using BaseClass;
using SungrowClass;
using HuaweiClass;

public class JsonHandler
{
    public static List<Base>? LoadFromFile(string jsonFilePath)
    {
        try
        {
            // Read the JSON file as a string
            string json = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON string to a JsonHandler array
            List<Base>? jsonList = JsonSerializer.Deserialize<List<Base>>(json);

            return jsonList;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. Please provide a valid JSON file path.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Invalid JSON format. Please check your JSON file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return null;
    }

    public override string ToString()
    {
        return "Handles the json data.";
    }
}