using Modbus.Net;
using Modbus.Net.Modbus;
using ModbusExtension.Models;
using ModbusExtension.Models.Enumerations;

namespace ModbusExtension.Services;

public class CreateMachineService
{
    public static ModbusMachineExtendedService<string, string, string, string> CreateModbusMachine(string ip, DataLoggerTypeEnum DataLoggerType)
    {
        string jsonFilePath = "JsonData/" + DataLoggerType + ".json";
        List<Base>? jsonDataArrayBase = JsonHandlerService.LoadFromFileBase(jsonFilePath);
        List<AddressUnit<string, int, int>>? BaseAddressUnits = JsonHandlerService.AddressUnitCreator(jsonDataArrayBase!);
        return new ModbusMachineExtendedService<string, string, string, string>("1", ModbusType.Tcp, ip, BaseAddressUnits, false, 1, 0, Endian.BigEndianLsb);
    }

    public static ModbusMachineExtendedService<string, string, string, string> CreateSimulationMachine(string ip)
    {
        List<Base>? jsonDataArrayBase = JsonHandlerService.LoadFromFileBase("JsonData/Huawei.json");
        List<AddressUnit<string, int, int>>? BaseAddressUnits = JsonHandlerService.AddressUnitCreator(jsonDataArrayBase!);
        return new ModbusMachineExtendedService<string, string, string, string>("1", ModbusType.Tcp, ip, BaseAddressUnits, false, 1, 0, Endian.BigEndianLsb);
    }
}