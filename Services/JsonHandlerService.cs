using System.Text.Json;
using System.Text.Json.Serialization;
using Modbus.Net;
using ModbusExtension.Models;
using ModbusExtension.Models.Enumerations;

namespace ModbusExtension.Services;

public class JsonHandlerService
{
    public static List<Base>? LoadFromFileBase(string jsonFilePath)
    {
        try
        {
            // Read the JSON file as a string
            string json = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON string to a JsonHandler array
            List<Base>? jsonListBase = JsonSerializer.Deserialize<List<Base>>(json, new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            });
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

    public static List<DataLogger>? LoadFromFileDataLogger(string jsonFilePath)
    {
        try
        {
            // Deserialize the JSON string to a JsonHandler array
            List<DataLogger>? jsonServersList = JsonSerializer.Deserialize<List<DataLogger>>(File.ReadAllText(jsonFilePath),
                options: new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });
            return jsonServersList;
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
        var addressUnits = new List<AddressUnit<string, int, int>>();

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

            addressUnits.Add(addressUnit); // Add the AddressUnit to the list
        }

        return addressUnits;
    }

    public static List<AddressUnit<string, int, int>>? AddressUnitCreator(List<Huawei> jsonDataArray)
    {
        var addressUnits = new List<AddressUnit<string, int, int>>();

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

            addressUnits.Add(addressUnit); // Add the AddressUnit to the list
        }

        return addressUnits;
    }

    public static List<AddressUnit<string, int, int>>? AddressUnitCreator(List<Sungrow> jsonDataArray)
    {
        var addressUnits = new List<AddressUnit<string, int, int>>();

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

            addressUnits.Add(addressUnit); // Add the AddressUnit to the list
        }

        return addressUnits;
    }

    public static List<AddressUnit<string, int, int>>? AddressUnitCreator(List<AiStratis> jsonDataArray)
    {
        var addressUnits = new List<AddressUnit<string, int, int>>();

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

            addressUnits.Add(addressUnit); // Add the AddressUnit to the list
        }

        return addressUnits;
    }

    public static Type ConvertDataType(DataTypeEnum DataType)
    {
        return DataType switch
        {
            DataTypeEnum.U16 => typeof(UInt16),
            DataTypeEnum.U32 => typeof(UInt32),
            DataTypeEnum.F32 => typeof(float),
            DataTypeEnum.U64 => typeof(UInt64),
            DataTypeEnum.I16 => typeof(Int16),
            DataTypeEnum.I32 => typeof(Int32),
            DataTypeEnum.I64 => typeof(Int64),
            DataTypeEnum.STR => typeof(string),
            DataTypeEnum.MLD => typeof(Byte),
            DataTypeEnum.BOOL => typeof(bool),
            DataTypeEnum.NA => typeof(bool),
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
                // note: this might change depending on the version of the modbus protocol that is used, eg. tcp, udp, etc
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
        return address + 1; // json Address + 1
    }

    public override string ToString()
    {
        return "Handles the json data.";
    }
}