using System;
using System.Reflection.PortableExecutable;
using Modbus.Net;
using Modbus.Net.Modbus;
using System.Runtime.InteropServices;

//  List<AddressUnit> addressUnits = new List<AddressUnit>
//         {
//             new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)}, //Id is mandatory
//             new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)}, // each address unit has each own id
//             new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
//             new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
//         };

namespace ModbusMachineExtended
{
    public class ModbusMachineExtended<TKey, TUnitKey> : ModbusMachine<TKey, TUnitKey> where TKey : IEquatable<TKey>
    where TUnitKey : IEquatable<TUnitKey>
    {
        
        private readonly ModbusType _connectionType;
        private readonly string _connectionString;

        // Constructor 1
        public ModbusMachineExtended(TKey id, ModbusType connectionType, string connectionString,
            IEnumerable<AddressUnit<TUnitKey>> getAddresses, bool keepConnect, byte slaveAddress, byte masterAddress,
            Endian endian = Endian.BigEndianLsb)
            : base(id, connectionType, connectionString, getAddresses, keepConnect, slaveAddress, masterAddress, endian)
        {
            // Store the values in class-level fields
            _connectionType = connectionType;
            _connectionString = connectionString;
        }

        // Constructor 2
        public ModbusMachineExtended(TKey id, ModbusType connectionType, string connectionString,
            IEnumerable<AddressUnit<TUnitKey>> getAddresses, byte slaveAddress, byte masterAddress,
            Endian endian = Endian.BigEndianLsb)
            : this(id, connectionType, connectionString, getAddresses, true, slaveAddress, masterAddress, endian)
        {
        }

        // Mask Function
        public async Task<ReturnStruct<byte[]>> GetDatasByCommunicationTag(List<AddressUnit> addressUnits, string communicationTag)
        {
            try
            {
                // var machine = new ModbusMachine<string, string>("1", ModbusType.Tcp, "127.0.0.1:502", addressUnits, 1, 0);
                var machine = new ModbusMachine<TKey, TUnitKey>(base.Id, _connectionType, _connectionString, base.GetAddresses, base.SlaveAddress, base.MasterAddress);
                // var ans = new Dictionary<string, ReturnUnit<double>>();

                //Detect and connect devices
                if (!machine.IsConnected)
                {
                    await machine.ConnectAsync();
                }
                //Terminate if unable to connect
                if (!machine.IsConnected)
                {
                    return new ReturnStruct<byte[]>()
                    {
                        Datas = new byte[0],
                        IsSuccess = false,
                        ErrorCode = -1,
                        ErrorMsg = "Connection Error"
                    };
                }
                
                foreach (AddressUnit addressUnit in addressUnits)
                {
                    if (addressUnit.CommunicationTag == communicationTag)
                    {
                        var returnGetObject = await machine.BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync(addressUnit.Area+" "+addressUnit.Address, Marshal.SizeOf(addressUnit.DataType)); //sizeof(addressUnit.DataType)||
                        // machine.Disconnect();

                        return returnGetObject;
                    }
                }  
                return new ReturnStruct<byte[]>()
                        {
                            Datas = new byte[0],
                            IsSuccess = false,
                            ErrorCode = -500,
                            ErrorMsg = "No such CommunicationTag in AddressUnit List"
                        };                       
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ReturnStruct<byte[]>()
                {
                    Datas = new byte[0],
                    IsSuccess = false,
                    ErrorCode = -100,
                    ErrorMsg = "Unknown Exception"
                };            
            }
        }
    }
}