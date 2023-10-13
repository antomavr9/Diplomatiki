using System;
using System.Reflection.PortableExecutable;
using Modbus.Net;
using Modbus.Net.Modbus;
using System.Runtime.InteropServices;

namespace ModbusMachineExtended;
//  List<AddressUnit> addressUnits = new List<AddressUnit>
//         {
//             new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)}, //Id is mandatory
//             new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)}, // each address unit has each own id
//             new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
//             new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
//         };

public class ModbusMachineExtended<TKey, TUnitKey> : ModbusMachine<TKey, TUnitKey> where TKey : IEquatable<TKey>
where TUnitKey : IEquatable<TUnitKey>
{
    // Constructor 1
    public ModbusMachineExtended(TKey id, ModbusType connectionType, string connectionString,
        IEnumerable<AddressUnit<TUnitKey>> getAddresses, bool keepConnect, byte slaveAddress, byte masterAddress,
        Endian endian = Endian.BigEndianLsb)
        : base(id, connectionType, connectionString, getAddresses, keepConnect, slaveAddress, masterAddress, endian)
    {
    }

    // Constructor 2
    public ModbusMachineExtended(TKey id, ModbusType connectionType, string connectionString,
        IEnumerable<AddressUnit<TUnitKey>> getAddresses, byte slaveAddress, byte masterAddress,
        Endian endian = Endian.BigEndianLsb)
        : this(id, connectionType, connectionString, getAddresses, true, slaveAddress, masterAddress, endian)
    {
    }

    // Mask Function
    public async Task<ReturnStruct<byte[]>> GetDatasByCommunicationTag(string communicationTag)
    {
        try
        {
            //Detect and connect devices
            if (!BaseUtility.IsConnected)
            {
                await BaseUtility.ConnectAsync();
            }
            //Terminate if unable to connect
            if (!BaseUtility.IsConnected)
            {
                return new ReturnStruct<byte[]>()
                {
                    Datas = new byte[0],
                    IsSuccess = false,
                    ErrorCode = -1,
                    ErrorMsg = "Connection Error"
                };
            }

            var addressUnit = GetAddresses.FirstOrDefault(addressUnit => addressUnit.CommunicationTag == communicationTag);
            if (addressUnit != null)
            {
                var returnGetObject = await BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync(addressUnit.Area + " " + addressUnit.Address, Marshal.SizeOf(addressUnit.DataType));
                // machine.Disconnect();

                return returnGetObject;
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

    public async Task<ReturnStruct<bool>> SetDatasByCommunicationTag(string communicationTag, double dataValue)
    {
        try
        {
            //Detect and connect devices
            if (!BaseUtility.IsConnected)
            {
                await BaseUtility.ConnectAsync();
            }
            //Terminate if unable to connect
            if (!BaseUtility.IsConnected)
            {
                return new ReturnStruct<bool>()
                {
                    Datas = false,
                    IsSuccess = false,
                    ErrorCode = -1,
                    ErrorMsg = "Connection Error"
                };
            }

            var addressUnit = GetAddresses.FirstOrDefault(addressUnit => addressUnit.CommunicationTag == communicationTag);
            if (addressUnit != null)
            {
                double Add1 = dataValue;
                var setDict = new Dictionary<string, double> { { communicationTag, (double)Add1 } };
                var returnSetObject = await SetDatasAsync(MachineDataType.CommunicationTag, setDict);

                return returnSetObject;
            }


            return new ReturnStruct<bool>()
            {
                Datas = false,
                IsSuccess = false,
                ErrorCode = -500,
                ErrorMsg = "No such CommunicationTag in AddressUnit List"
            };   
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return new ReturnStruct<bool>()
            {
                Datas = false,
                IsSuccess = false,
                ErrorCode = -100,
                ErrorMsg = "Unknown Exception"
            };    
        }
    }
}
