namespace ModbusExtension.Models;

public class Huawei : Base
{
    public int SN { get; set; }
    public string? ReadWrite { get; set; }
    public string? Gain { get; set; }
    public string? Range { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, SN: {SN}, ReadWrite: {ReadWrite}, Gain: {Gain}, Range: {Range}, Description: {Description}";
    }
}