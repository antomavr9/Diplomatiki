using System;
using System.IO;
using System.Text.Json;

public class Base
{
    public string? Name { get; set; }
    public string? DataType { get; set; }
    public string? Unit { get; set; }
    public int Address { get; set; }
    public int Quantity { get; set; }

    public bool ValidateJson(string jsonFilePath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(jsonFilePath))
            {
                string jsonString = reader.ReadToEnd();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<Base[]>(jsonString, options);

                foreach (var item in data)
                {
                    if (string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.DataType) ||
                        string.IsNullOrEmpty(item.Unit) || item.Address == 0 || item.Quantity == 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}