using ModbusExtension.Models.Enumerations;

namespace ModbusExtension.Models;

public class DataLogger
{
    public string Ip { get; set; }
    public DataLoggerTypeEnum DataLoggerType { get; set; }
}

