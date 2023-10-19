using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using Application.Models;
using Modbus.Net;
using Modbus.Net.Modbus;
using System.Runtime.InteropServices;

namespace JsonHandlerName;
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

    public static List<AddressUnit<string, int, int>> AddressUnitCreator(List<Base> jsonDataArray)
    {
        var addressUnits  = new List<AddressUnit<string, int, int>>();

        for (int i = 0; i < jsonDataArray.Count; i++)
        {
            var addressUnit = new AddressUnit<string, int, int>
            {
                Id = (i + 1).ToString(),
                CommunicationTag = jsonDataArray[i].Name,
                DataType = ConvertDataType(jsonDataArray[i].DataType!)
            }; 

            var area = ConvertArea(jsonDataArray[i].Address);
            var address = ConvertAddress(jsonDataArray[i].Address);
            addressUnit.Area = area;
            addressUnit.Address = address;

            addressUnits .Add(addressUnit); // Add the AddressUnit to the list
        }

        return addressUnits ;
    }

    public static List<AddressUnit<string, int, int>>? AddressUnitCreator(List<Huawei> jsonDataArray)
    {
        var addressUnits  = new List<AddressUnit<string, int, int>>();

        for (int i = 0; i < jsonDataArray.Count; i++)
        {
            var addressUnit = new AddressUnit<string, int, int>
            {
                Id = (i + 1).ToString(),
                CommunicationTag = jsonDataArray[i].Name,
                DataType = ConvertDataType(jsonDataArray[i].DataType!)
            }; 

            var area = ConvertArea(jsonDataArray[i].Address);
            var address = ConvertAddress(jsonDataArray[i].Address);
            addressUnit.Area = area;
            addressUnit.Address = address;

            addressUnits .Add(addressUnit); // Add the AddressUnit to the list
        }

        return addressUnits ;
    }

    public static List<AddressUnit<string, int, int>>? AddressUnitCreator(List<Sungrow> jsonDataArray)
    {
        var addressUnits  = new List<AddressUnit<string, int, int>>();

        for (int i = 0; i < jsonDataArray.Count; i++) // Use '<' instead of '<='
        {
            var addressUnit = new AddressUnit<string, int, int>
            {
                Id = (i + 1).ToString(),
                CommunicationTag = jsonDataArray[i].Name,
                DataType = ConvertDataType(jsonDataArray[i].DataType!)
            }; 

            var area = ConvertArea(jsonDataArray[i].Address);
            var address = ConvertAddress(jsonDataArray[i].Address);
            addressUnit.Area = area;
            addressUnit.Address = address;

            addressUnits .Add(addressUnit); // Add the AddressUnit to the list
        }

        return addressUnits ;
    }


    public static Type ConvertDataType(string DataType)
    {
        return DataType switch
        {
            "U16" => typeof(UInt16),
            "U32" => typeof(UInt32),
            "U64" => typeof(UInt64),
            "I16" => typeof(Int16),
            "I32" => typeof(Int32),
            "I64" => typeof(Int64),
            "STR" => typeof(string),
            "MLD" => typeof(Byte),
            "N/A" => typeof(bool),
            _ => typeof(bool)
        };
    }

    public static string ConvertArea(int address)
    {
        try
        {
            int areaCode = address / 10000;
            string result = areaCode switch
            {
                4 => "4X",
                3 => "3X",
                1 => "1X",
                0 => "0X",
                _ => "Invalid Area"
            };
            return result;
        }
        catch (Exception)
        {
            return "Cannot Convert Address!";
        }
    }

    public static int ConvertAddress(int address)
    {
        return address % 10000 + 1;
    }

    public override string ToString()
    {
        return "Handles the json data.";
    }
}