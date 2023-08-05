using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using JsonClasses;
using Modbus.Net;
using Modbus.Net.Modbus;

public class JsonHandler
{
    public static List<Base>? LoadFromFileBase(string jsonFilePath)
    {
        try
        {
            // Read the JSON file as a string
            string json = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON string to a JsonHandler array
            List<Base>? jsonListBase = JsonSerializer.Deserialize<List<Base>>(json);
            return jsonListBase;
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

    public static List<Huawei>? LoadFromFileHuawei(string jsonFilePath)
    {
        try
        {
            // Read the JSON file as a string
            string json = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON string to a JsonHandler array
            List<Huawei>? jsonListHuawei = JsonSerializer.Deserialize<List<Huawei>>(json);
            return jsonListHuawei;
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

    public static List<Sungrow>? LoadFromFileSungrow(string jsonFilePath)
    {
        try
        {
            // Read the JSON file as a string
            string json = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON string to a JsonHandler array
            List<Sungrow>? jsonListSungrow = JsonSerializer.Deserialize<List<Sungrow>>(json);
            return jsonListSungrow;
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

    // public static List<AddressUnit>? AddressUnitCreator(List<Base> jsonDataArray)
    // {
    //     return ;
    // }

    // public static List<AddressUnit>? AddressUnitCreator(List<Huawei> jsonDataArray)
    // {
    //     return ;
    // }

    // public static List<AddressUnit>? AddressUnitCreator(List<Sungrow> jsonDataArray)
    // {
    //     return ;
    // }


    public override string ToString()
    {
        return "Handles the json data.";
    }
}