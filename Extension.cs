// using System;
// using Modbus.Net;
// using Modbus.Net.Modbus;
// using Yolo;
// using System.Linq;


// namespace Extension
// {
//     class Extension
//     {
//         // List<AddressUnit> addressUnits = new List<AddressUnit> // each address unit has each own id
//         // {
//         //     new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)},
//         //     new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)},
//         //     new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
//         //     new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
//         // };
        
//         public async Task<ReturnStruct<byte[]>?> GetDatasByCommunicationTag(ModbusMachine<string, string> machine, string Tag, List<AddressUnit> addressUnits)
//         {
//             foreach (var addressUnit in addressUnits)
//             {
//                 if (addressUnit.CommunicationTag == Tag)
//                 {
//                     //result.Add(addressUnit);
//                     string address= addressUnit.Area + " " + addressUnit.Address;
//                     ReturnStruct<byte[]> rect = await machine.BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync(address, 2);
//                     return rect;
//                 }
//             }
//             return null;
//         }
//     }
// }





//public AddressUnit<TUnitKey> GetAddressUnitById(TUnitKey addressUnitId)
//{
//  try
//  {
//      return GetAddresses.SingleOrDefault(p => p.Id.Equals(addressUnitId));
//  }
//  catch (Exception e)
//  {
//      logger.LogError(e, $"BaseMachine -> GetAddressUnitById Id:{Id} ConnectionToken:{ConnectionToken} addressUnitId:{addressUnitId} Repeated");
//      return null;
//  }
//}



