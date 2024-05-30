using DeddheModbusClient.Core.Models.Modbus.Enumerations;

namespace ModbusExtension.Models;

public class AiStratis : Base
{
    public int SN { get; set; }
    public string? ReadWrite { get; set; }
    public string? Gain { get; set; }
    public string? Range { get; set; }
    public ModbusDataTypeEnum ModbusDataType { get; set; }

    
    public override string ToString()
    {
        return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, SN: {SN}, ReadWrite: {ReadWrite}, Gain: {Gain}, Range: {Range}, Description: {Description}, ModbusDataType: {ModbusDataType}";
    }
}