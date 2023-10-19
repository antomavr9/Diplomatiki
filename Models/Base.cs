namespace ModbusExtension;

public class Base
{
    // private variables
    private int _address;
    private string _dataType = null!;
    private int _quantity;

    // Properties
    public string Name { get; set; } = null!;
    public string? Unit { get; set; }
    public string? Description { get; set; }
    public string DataType
    {
        get => _dataType;
        set => _dataType = value switch
        {
            "U16" or "U32" or "U64" or "I16" or "I32" or "I64" or "STR" or "MLD" or "N/A" => value,
            _ => throw new ArgumentException("Invalid value for DataType property.")
        };
    }
    public int Address
    {
        get => _address;
        set
        {
            // Enforce a value range (between 0 and 1000000)
            if (value >= 0 && value <= 465537)
            {
                _address = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Address must be between 0 and 1000000");
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
