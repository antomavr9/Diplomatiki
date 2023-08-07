using System;
using System.IO;
using System.Text.Json;

namespace JsonClasses
{
    public class Base
    {
        public string? Name { get; set; }
        public string? DataType { get; set; }
        public string? Unit { get; set; }
        public int Address { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}";
        }
    }

    public class Huawei : Base
    {
        public int SN { get; set; }
        public string? ReadWrite { get; set; }
        public string? Gain { get; set; }
        public string? Range { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, SN: {SN}, ReadWrite: {ReadWrite}, Gain: {Gain}, Range: {Range}";
        }
    }

    public class Sungrow : Base
    {
        public int No { get; set; }
        public string? DataRange { get; set; }
        public string? Note { get; set; }
        public int FinalAddress { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, No: {No}, DataRange: {DataRange}, Note: {Note}, FinalAddress: {FinalAddress}";
        }
    }
}