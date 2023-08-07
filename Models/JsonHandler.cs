using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using Application.Models;
using Modbus.Net;
using Modbus.Net.Modbus;
using System.Runtime.InteropServices;

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

    public static List<AddressUnit> AddressUnitCreator(List<Base> jsonDataArray)
{
    var BaseAddressUnits = new List<AddressUnit>();

    for (int i = 0; i < jsonDataArray.Count; i++) // Use '<' instead of '<='
    {
        var addressUnit = new AddressUnit
        {
            Id = (i + 1).ToString(),
            CommunicationTag = jsonDataArray[i].Name,
            DataType = ConvertDataType(jsonDataArray[i].DataType!)
        }; 

        var Area = ConvertArea(jsonDataArray[i].Address);
        var Address = ConvertAddress(jsonDataArray[i].Address);
        addressUnit.Area = Area;
        addressUnit.Address = Address;

        BaseAddressUnits.Add(addressUnit); // Add the AddressUnit to the list
    }

    return BaseAddressUnits;
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

    public static string? ConvertArea(int Address)
    {
        try
        {
            if( (int) (Address/10000) == 4)
            {
                return "4X";
            }
            else if((int) (Address/10000) == 3)
            {
                return "3X";
            }
            else if((int) (Address/10000) == 1)
            {
                return "1X";
            }
            else if ((int) (Address/10000) == 0)
            {
                return "0X";
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Cannot Convert Address!");
        }
        return null;
    }

    public static int ConvertAddress(int Address)
    {
        return (int)(Address%10000)+1;
    }

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