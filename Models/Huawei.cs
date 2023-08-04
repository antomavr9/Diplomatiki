using System;
using System.IO;
using System.Text.Json;

public class Huawei : Base
{
    public int SN { get; set; }
    public string? ReadWrite { get; set; }
    public int Gain { get; set; }
    public string? Range { get; set; }

    public bool ValidateJsonHuawei(string jsonFilePath)
    {
        if (!ValidateJson(jsonFilePath))
        {
            return false;
        }

        try
        {
            using (StreamReader reader = new StreamReader(jsonFilePath))
            {
                string jsonString = reader.ReadToEnd();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<Huawei[]>(jsonString, options);

                foreach (var item in data)
                {
                    if (string.IsNullOrEmpty(item.ReadWrite) || item.Gain == 0 ||
                        string.IsNullOrEmpty(item.Range) || item.SN == 0)
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