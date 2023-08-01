using System;
using System.IO;
using System.Text.Json;

public class JsonHandler
{
    public int SN { get; set; }
    public string? Name { get; set; }
    public string? ReadWrite { get; set; }
    public string? Type { get; set; }
    public string? Unit { get; set; }
    public int Gain { get; set; }
    public int Address { get; set; }
    public int Quantity { get; set; }
    public string? Range { get; set; }

    public static JsonHandler[]? LoadFromFile(string jsonFilePath)
    {
        try
        {
            // Read the JSON file as a string
            string json = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON string to a JsonHandler array
            JsonHandler[]? jsonData = JsonSerializer.Deserialize<JsonHandler[]>(json);

            return jsonData;
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
        return $"SN: {SN}, Name: {Name}, Read/Write: {ReadWrite}, Type: {Type}, Unit: {Unit}, Gain: {Gain}, Address: {Address}, Quantity: {Quantity}, Range: {Range}";
    }
}