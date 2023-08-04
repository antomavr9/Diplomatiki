using System;
using System.IO;
using System.Text.Json;
using BaseClass;

namespace SungrowClass
{
    public class Sungrow : Base
    {
        public int No { get; set; }
        public string? DataRange { get; set; }
        public string? Note { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, No: {No}, DataRange: {DataRange}, Note: {Note}";
        }
    }
}