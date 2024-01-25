using Modbus.Net;

namespace ModbusExtension;

public class Sungrow : Base
{
    public int No { get; set; }
    public string? DataRange { get; set; }
    public string? Note { get; set; }
    public int FinalAddress { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, No: {No}, DataRange: {DataRange}, Note: {Note}, FinalAddress: {FinalAddress}, Description: {Description}";
    }
}