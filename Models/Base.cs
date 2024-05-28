using ModbusExtension.Models.Enumerations;

namespace ModbusExtension.Models;

public class Base
{
    // private variables
    private int _address;
    private int _quantity;

    // Properties
    public string Name { get; set; } = null!;
    public string? Unit { get; set; }
    public string? Description { get; set; }
    public DataTypeEnum DataType { get; set; }
    public int Address
    {
        get => _address;
        set
        {
            // Enforce a value range (between 0 and 1000000)
            if (value >= 0 && value <= 465537) // 42779
            {
                _address = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Address must be between 0 and 465537");
            }
        }
    }
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }
            _quantity = value;
        }
    }
    public override string ToString()
    {
        return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, Description: {Description}";
    }
}
