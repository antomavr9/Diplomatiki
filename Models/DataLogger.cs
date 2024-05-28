namespace ModbusExtension.Models;

public class DataLogger
{
    public string? Ip { get; set; }
    public string? DataLoggerType { get; set; }

    public static string GetActivePower(string DataLoggerType) =>
    DataLoggerType switch
    {
        "Huawei" => "Active Power",
        "Sungrow" => "Active Power",
        _ => "Unrecognised DataLoggerType."
    };
    public static string GetReactivePower(string DataLoggerType) =>
    DataLoggerType switch
    {
        "Huawei" => "Reactive Power",
        "Sungrow" => "Reactive Power",
        _ => "Unrecognised DataLoggerType."
    };
    public static string GetVoltageL1(string DataLoggerType) =>
    DataLoggerType switch
    {
        "Huawei" => "Uab",
        "Sungrow" => "Uab",
        _ => "Unrecognised DataLoggerType."
    };
    public static string GetVoltageL2(string DataLoggerType) =>
    DataLoggerType switch
    {
        "Huawei" => "Ubc",
        "Sungrow" => "Ubc",
        _ => "Unrecognised DataLoggerType."
    };
    public static string GetVoltageL3(string DataLoggerType) =>
        DataLoggerType switch
        {
            "Huawei" => "Uca",
            "Sungrow" => "Uca",
            _ => "Unrecognised DataLoggerType"
        };
    public static string GetDeviceStatus(string DataLoggerType) =>
        DataLoggerType switch
        {
            "Huawei" => "Plant Status 1",
            "Sungrow" => "Plant Status 1",
            _ => "Unrecognised Data Logger Type"
        };
    public static string GetLogStatus(string DataLoggerType) =>
        DataLoggerType switch
        {
            "Huawei" => "Plant Status 2",
            "Sungrow" => "Plant Status 2",
            _ => "Unrecognised DataLoggerType"
        };
    public static string GetPowerSetPointLevel1(string DataLoggerType) =>
        DataLoggerType switch
        {
            "Huawei" => "Active Adjustment 1",
            "Sungrow" => "Active Adjustment 1",
            _ => "Unrecognised Data Logger Type"
        };
    public static string GetPowerSetPointLevel2(string DataLoggerType) =>
        DataLoggerType switch
        {
            "Huawei" => "Active Adjustment 2",
            "Sungrow" => "Active Adjustment 2",
            _ => "Unrecognised Data Logger Type"
        };
    public static string GetPowerSetPointLevelByPercentage(string DataLoggerType) =>
        DataLoggerType switch
        {
            "Huawei" => "Active Power Adjustement By Percentage",
            "Sungrow" => "Active Power Adjustement By Percentage",
            _ => "Unrecognised Data Logger Type"
        };
    // --------------------------Set--------------------------
    public static string SetPowerSetPointLevel1(string DataLoggerType) =>
    DataLoggerType switch
    {
        "Huawei" => "Active Adjustment 1",
        "Sungrow" => "Active Adjustment 1",
        _ => "Unrecognised Data Logger Type"
    };
    public static string SetPowerSetPointLevel2(string DataLoggerType) =>
    DataLoggerType switch
    {
        "Huawei" => "Active Adjustment 2",
        "Sungrow" => "Active Adjustment 2",
        _ => "Unrecognised Data Logger Type"
    };
    public static string SetPowerSetPointLevelByPercentage(string DataLoggerType) =>
    DataLoggerType switch
    {
        "Huawei" => "Active Power Adjustement By Percentage",
        "Sungrow" => "Active Power Adjustement By Percentage",
        _ => "Unrecognised Data Logger Type"
    };
    public static string GetServerTime(string DataLoggerType) =>
        DataLoggerType switch
        {
            "Huawei" => "The Local Time",
            "Sungrow" => "The Local Time",
            _ => "Unrecognised Data Logger Type"
        };
}