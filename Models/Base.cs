using System;
using System.IO;
using System.Text.Json;

namespace BaseClass
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
}