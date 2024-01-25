using Modbus.Net;
using Modbus.Net.Modbus;

namespace ModbusExtension;

public class CreateMachine
{
    public static ModbusMachineExtended<string, string, string,string> CreateModbusMachine(string ip, string DataLoggerType)
    {
        string jsonFilePath = "JsonData/"+DataLoggerType+".json";
        List<Base>? jsonDataArrayBase = JsonHandler.LoadFromFileBase(jsonFilePath);
        List<AddressUnit<string,int,int>>? BaseAddressUnits = JsonHandler.AddressUnitCreator(jsonDataArrayBase!);
        return new ModbusMachineExtended<string,string,string,string>("1", ModbusType.Tcp, ip, BaseAddressUnits, false, 1, 0, Endian.BigEndianLsb);
    }
}