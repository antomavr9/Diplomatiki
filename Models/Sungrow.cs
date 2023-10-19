namespace ModbusExtension;

public class Sungrow : Base, IPlantServiceInterface
{
    public int No { get; set; }
    public string? DataRange { get; set; }
    public string? Note { get; set; }
    public int FinalAddress { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, No: {No}, DataRange: {DataRange}, Note: {Note}, FinalAddress: {FinalAddress}, Description: {Description}";
    }

    public string GetActivePower()
    {
        return "Total Active Power";
    }

    public string GetReactivePower()
    {
        return "Total Reactive Power";
    }

    public string GetVoltageL1()
    {
        return "A-B Line Voltage/Phase A Voltage";
    }

    public string GetVoltageL2()
    {
        return "B-C Line Voltage/Phase B Voltage";
    }

    public string GetVoltageL3()
    {
        return "C-A Line Voltage/Phase C Voltage";
    }

    public string GetDeviceStatus()
    {
        return "";//?
    }

    public string GetLogStatus()
    {
        return "";//?
    }

    public string GetPowerSetPointLevel()
    {
        return "Active Power Regulation Setpoint";
    }
}

