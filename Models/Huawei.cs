namespace Application.Models;

public class Huawei : Base, IPlantServiceInterface
{
    public int SN { get; set; }
    public string? ReadWrite { get; set; }
    public string? Gain { get; set; }
    public string? Range { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, SN: {SN}, ReadWrite: {ReadWrite}, Gain: {Gain}, Range: {Range}, Description: {Description}";
    }

    public string GetActivePower()
    {
        return "Active Power";
    }

    public string GetReactivePower()
    {
        return "Reactive Power";
    }

    public string GetVoltageL1()
    {
        return "Uab";
    }

    public string GetVoltageL2()
    {
        return "Ubc";
    }

    public string GetVoltageL3()
    {
        return "Uca";
    }

    public string GetDeviceStatus()
    {
        return "Plant Status";
    }

    public string GetLogStatus()
    {
        return "";//?
    }

    public string GetPowerSetPointLevel()
    {
        return "";//?
    }

}
