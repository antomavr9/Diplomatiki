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

        var addressUnits = new List<AddressUnit<string, int, int>>();
        switch (DataLoggerType)
        {
            case DataLoggerTypeEnum.Huawei:
                List<Huawei> jsonDataArrayHuawei = JsonHandlerService.LoadFromFileHuawei(jsonFilePath)!;
                addressUnits = JsonHandlerService.AddressUnitCreator(jsonDataArrayHuawei!)!;
                break;
            case DataLoggerTypeEnum.Sungrow:
                List<Sungrow> jsonDataArraySungrow = JsonHandlerService.LoadFromFileSungrow(jsonFilePath)!;
                addressUnits = JsonHandlerService.AddressUnitCreator(jsonDataArraySungrow!)!;
                break;
            case DataLoggerTypeEnum.AiStratis:
                List<AiStratis> jsonDataArrayAiStratis = JsonHandlerService.LoadFromFilejsonListAiStratis(jsonFilePath)!;
                addressUnits = JsonHandlerService.AddressUnitCreator(jsonDataArrayAiStratis!)!;
                break;
            default:
                throw new Exception($"Unsupported DataLoggerType: {DataLoggerType}");
        }

        return new ModbusMachineExtendedService<string, string, string, string>("1", ModbusType.Tcp, ip, addressUnits, false, 1, 0, Endian.BigEndianLsb);
    }

    // public static ModbusMachineExtendedService<string, string, string, string> CreateSimulationMachine(string ip)
    // {
    //     List<Base>? jsonDataArrayBase = JsonHandlerService.LoadFromFileBase("JsonData/Huawei.json");
    //     List<AddressUnit<string, int, int>>? BaseAddressUnits = JsonHandlerService.AddressUnitCreator(jsonDataArrayBase!);
    //     return new ModbusMachineExtendedService<string, string, string, string>("1", ModbusType.Tcp, ip, BaseAddressUnits, false, 1, 0, Endian.BigEndianLsb);
    // }
}