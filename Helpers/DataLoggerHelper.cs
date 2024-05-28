using ModbusExtension.Models.Enumerations;

namespace ModbusExtension.Helpers;

public static class DataLoggerHelper
{
    public static string GetActivePower(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Active Power",
            DataLoggerTypeEnum.Sungrow => "Active Power",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetReactivePower(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Reactive Power",
            DataLoggerTypeEnum.Sungrow => "Reactive Power",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetVoltageL1(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Uab",
            DataLoggerTypeEnum.Sungrow => "Uab",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetVoltageL2(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Ubc",
            DataLoggerTypeEnum.Sungrow => "Ubc",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetVoltageL3(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Uca",
            DataLoggerTypeEnum.Sungrow => "Uca",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetDeviceStatus(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Plant Status 1",
            DataLoggerTypeEnum.Sungrow => "Plant Status 1",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetLogStatus(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Plant Status 2",
            DataLoggerTypeEnum.Sungrow => "Plant Status 2",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetPowerSetPointLevel1(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Active Adjustment 1",
            DataLoggerTypeEnum.Sungrow => "Active Adjustment 1",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetPowerSetPointLevel2(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Active Adjustment 2",
            DataLoggerTypeEnum.Sungrow => "Active Adjustment 2",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetPowerSetPointLevelByPercentage(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Active Power Adjustement By Percentage",
            DataLoggerTypeEnum.Sungrow => "Active Power Adjustement By Percentage",
            _ => "Unrecognised DataLoggerType."
        };
    // --------------------------Set--------------------------
    public static string SetPowerSetPointLevel1(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Active Adjustment 1",
            DataLoggerTypeEnum.Sungrow => "Active Adjustment 1",
            _ => "Unrecognised DataLoggerType."
        };

    public static string SetPowerSetPointLevel2(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Active Adjustment 2",
            DataLoggerTypeEnum.Sungrow => "Active Adjustment 2",
            _ => "Unrecognised DataLoggerType."
        };

    public static string SetPowerSetPointLevelByPercentage(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "Active Power Adjustement By Percentage",
            DataLoggerTypeEnum.Sungrow => "Active Power Adjustement By Percentage",
            _ => "Unrecognised DataLoggerType."
        };

    public static string GetServerTime(this DataLoggerTypeEnum DataLoggerType) =>
        DataLoggerType switch
        {
            DataLoggerTypeEnum.Huawei => "The Local Time",
            DataLoggerTypeEnum.Sungrow => "The Local Time",
            _ => "Unrecognised DataLoggerType."
        };
}