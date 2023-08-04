using System;
using System.IO;
using System.Text.Json;
using BaseClass;

namespace HuaweiClass
{
    public class Huawei : Base
    {
        public int SN { get; set; }
        public string? ReadWrite { get; set; }
        public int Gain { get; set; }
        public string? Range { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, DataType: {DataType}, Unit: {Unit}, Address: {Address}, Quantity: {Quantity}, SN: {SN}, ReadWrite: {ReadWrite}, Gain: {Gain}, Range: {Range}";
        }
    }
}