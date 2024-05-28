using Modbus.Net;
using Modbus.Net.Modbus;
using ModbusExtension.Models;
using System.Runtime.InteropServices;

namespace ModbusExtension.Services;

public class ModbusMachineExtendedService<TKey, TUnitKey, TAddressKey, TSubAddressKey> : ModbusMachine<TKey, TUnitKey> 
    where TKey : IEquatable<TKey>
    where TUnitKey : IEquatable<TUnitKey> 
    where TAddressKey : IEquatable<TAddressKey> 
    where TSubAddressKey : IEquatable<TSubAddressKey> 
{
    // Constructor 1
    public ModbusMachineExtendedService(TKey id,
                                 ModbusType connectionType,
                                 string connectionString,
                                 IEnumerable<AddressUnit<TUnitKey, int, int>> getAddresses,
                                 bool keepConnect,
                                 byte slaveAddress,
                                 byte masterAddress,
                                 Endian endian)
    : base(id, connectionType, connectionString, getAddresses, keepConnect, slaveAddress, masterAddress, endian)
    { 
    }

    // Constructor 2
    public ModbusMachineExtendedService(TKey id,
                                 ModbusType connectionType,
                                 string connectionString,
                                 IEnumerable<AddressUnit<TUnitKey, int, int>> getAddresses,
                                 byte slaveAddress,
                                 byte masterAddress,
                                 Endian endian)
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
                    ErrorMsg = "Connection Error."
                };
            }

            var addressUnit = GetAddresses.FirstOrDefault(addressUnit => addressUnit.CommunicationTag == communicationTag);
            if (addressUnit != null)
            {
                #region Huawei Error Handling

                // if(addressUnit.CommunicationTag == "Reserved")
                // {
                //     var returnGetObject =  new ReturnStruct<byte[]>()
                //     {
                //         Datas = new byte[0],
                //         IsSuccess = false,
                //         ErrorCode = -4, // ErrorCode -4: "This is a Reserved Address."
                //         ErrorMsg = "Reserved Address Access Attempt"
                //     };
                //     return returnGetObject;
                // }
                // else if(addressUnit.CommunicationTag == "ESN")
                // {
                //     var returnGetObject = await BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync(addressUnit.Area + " " +  addressUnit.Address, 20); // Marshal.SizeOf(addressUnit.DataType)
                //     return returnGetObject;
                // }
                // else if(addressUnit.CommunicationTag == "System Reset" || addressUnit.CommunicationTag == "Fast Device Access" || addressUnit.CommunicationTag == "Device Operation")
                // {
                //     // var returnGetObject = await BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync(addressUnit.Area + " " +  addressUnit.Address, Marshal.SizeOf(addressUnit.DataType));
                //     var returnGetObject =  new ReturnStruct<byte[]>()
                //     {
                //         Datas = new byte[0],
                //         IsSuccess = false,
                //         ErrorCode = -5, // ErrorCode -5: "This is a Write Only Variable"
                //         ErrorMsg = "Write Only Variable"
                //     };
                //     return returnGetObject;
                // }
                // else if(addressUnit.CommunicationTag == "Alarm Info 1")
                // {
                //     var returnGetObject = await BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync("4X 50001", 1); 
                //     return returnGetObject;
                // }
                // else if(addressUnit.CommunicationTag == "Alarm Info 2")
                // {
                //     var returnGetObject = await BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync("4X 50002", 1); 
                //     return returnGetObject;
                // }
                // else 
                // {
                //     var returnGetObject = await BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync(addressUnit.Area + " " +  addressUnit.Address, Marshal.SizeOf(addressUnit.DataType));
                //     return returnGetObject;
                // }

                #endregion
                
                var returnGetObject = await BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync(addressUnit.Area + " " +  addressUnit.Address, Marshal.SizeOf(addressUnit.DataType));
                return returnGetObject;
            }

            return new ReturnStruct<byte[]>()
            {
                Datas = new byte[0],
                IsSuccess = false,
                ErrorCode = -500,
                ErrorMsg = "No such CommunicationTag in AddressUnit List."
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
                ErrorMsg = "Unknown Exception."
            };            
        }
    }
    public async Task<ReturnStruct<bool>> SetDatasByCommunicationTag(string communicationTag, double dataValue) // Data Type of value depends on the variable we write on!!!
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
                    ErrorMsg = "Connection Error."
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
                ErrorMsg = "No such CommunicationTag in AddressUnit List."
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
                ErrorMsg = "Unknown Exception."
            };    
        }
    }

    // public async Task<ReturnStruct<bool>> SetDatasByCommunicationTag(string communicationTag, Int16 dataValue) // Data Type of value depends on the variable we write on!!!
    // {
    //     try
    //     {
    //         //Detect and connect devices
    //         if (!BaseUtility.IsConnected)
    //         {
    //             await BaseUtility.ConnectAsync();
    //         }
    //         //Terminate if unable to connect
    //         if (!BaseUtility.IsConnected)
    //         {
    //             return new ReturnStruct<bool>()
    //             {
    //                 Datas = false,
    //                 IsSuccess = false,
    //                 ErrorCode = -1,
    //                 ErrorMsg = "Connection Error."
    //             };
    //         }

    //         var addressUnit = GetAddresses.FirstOrDefault(addressUnit => addressUnit.CommunicationTag == communicationTag);
    //         if (addressUnit != null)
    //         {
    //             double Add1 = dataValue;
    //             var setDict = new Dictionary<string, double> { { communicationTag, (double)Add1 } };
    //             var returnSetObject = await SetDatasAsync(MachineDataType.CommunicationTag, setDict);

    //             return returnSetObject;
    //         }

    //         return new ReturnStruct<bool>()
    //         {
    //             Datas = false,
    //             IsSuccess = false,
    //             ErrorCode = -500,
    //             ErrorMsg = "No such CommunicationTag in AddressUnit List."
    //         };   
    //     }
    //     catch(Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return new ReturnStruct<bool>()
    //         {
    //             Datas = false,
    //             IsSuccess = false,
    //             ErrorCode = -100,
    //             ErrorMsg = "Unknown Exception."
    //         };    
    //     }
    // }
    public async Task<string> GetServerTime(string DataLoggerType)
    {
        // Server Time must be in an U32 bit register in Epoch Time UTC
        var returnGetObject = await GetDatasByCommunicationTag(DataLogger.GetServerTime(DataLoggerType));
        var localTime = returnGetObject.Datas;
        var epochSeconds = DataPresentationService.ByteToInt32(localTime);

        // Convert epoch seconds to a DateTime
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochSeconds);

        // Format the DateTime as a string in "yyyy-MM-dd HH:mm:ss" format
        string formattedTimeString = dateTimeOffset.ToString("yyyy-MM-dd HH:mm:ss");

        return formattedTimeString;
    }
    public async Task<(string, UInt32, string, string, bool, string, int)> GetActivePower(string DataLoggerType)
    {
        try
        { 
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject = await GetDatasByCommunicationTag(DataLogger.GetActivePower(DataLoggerType)); // There is exception handling incide
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt32(GetDatas);

            return ("Active Power", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);
        }
        catch (Exception ex)
        {
            return ("GetActivePower failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, Int32, string,string,bool,string,int)> GetReactivePower(string DataLoggerType)
    {
        try
        {
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject =  await GetDatasByCommunicationTag(DataLogger.GetReactivePower(DataLoggerType));
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToInt32(GetDatas);
            return ("Reactive Power", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);     
        }
        catch (Exception ex)
        {
            return ("GetReactivePower failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, UInt16, string,string,bool,string,int)> GetVoltageL1(string DataLoggerType)
    {
        try
        {
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject =  await GetDatasByCommunicationTag(DataLogger.GetVoltageL1(DataLoggerType));
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt16(GetDatas);
            return ("Uab", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);
        }
        catch (Exception ex)
        {
            return ("GetVoltageL1 failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, UInt16, string,string,bool,string,int)> GetVoltageL2(string DataLoggerType)
    {
        try
        {   
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject =  await GetDatasByCommunicationTag(DataLogger.GetVoltageL2(DataLoggerType));
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt16(GetDatas);
            return ("Ubc", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);
        }
        catch (Exception ex)
        {
            return ("GetVoltageL2 failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, UInt16, string,string,bool,string,int)> GetVoltageL3(string DataLoggerType)
    {
        try
        {
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject =  await GetDatasByCommunicationTag(DataLogger.GetVoltageL3(DataLoggerType));
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt16(GetDatas);
            return ("Uca", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);
        }
        catch (Exception ex)
        {
            return ("GetVoltageL3 failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, UInt16, string,string,bool,string,int)> GetDeviceStatus(string DataLoggerType)
    {
        try
        {  
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject =  await GetDatasByCommunicationTag(DataLogger.GetDeviceStatus(DataLoggerType));
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt16(GetDatas);
            return ("Plant Status 1", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);
        }
        catch (Exception ex)
        {
            return ("GetDeviceStatus failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, UInt16, string,string,bool,string,int)> GetLogStatus(string DataLoggerType)
    {
        try
        {
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject =  await GetDatasByCommunicationTag(DataLogger.GetLogStatus(DataLoggerType));
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt16(GetDatas);
            return ("Plant Status 2", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode); //?
        }
        catch (Exception ex)
        {
            return ("GetDeviceStatus failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
        
    }
    public async Task<(string, UInt32, string,string,bool,string,int)> GetPowerSetPointLevel1(string DataLoggerType)
    {
        try
        { 
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject =  await GetDatasByCommunicationTag(DataLogger.GetPowerSetPointLevel1(DataLoggerType));
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt32(GetDatas);
            return ("Active Adjustment 1", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);
        }
        catch (Exception ex)
        {
            return ("GetPowerSetPointLevel1 failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, UInt32, string, string, bool, string, int)> GetPowerSetPointLevel2(string DataLoggerType)
    {
        try
        { 
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject = await GetDatasByCommunicationTag(DataLogger.GetPowerSetPointLevel2(DataLoggerType)); // There is exception handling incide
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt32(GetDatas);

            return ("Active Adjustement 2", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);
        }
        catch (Exception ex)
        {
            return ("GetPowerSetPointLevel2 failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, UInt16, string, string, bool, string, int)> GetPowerSetPointLevelByPercentage(string DataLoggerType)
    {
        try
        { 
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnGetObject = await GetDatasByCommunicationTag(DataLogger.GetPowerSetPointLevelByPercentage(DataLoggerType)); // There is exception handling incide
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;
            var value = DataPresentationService.ByteToUInt16(GetDatas);

            return ("Active Power Adjustement By Percentage", value, getTimestamp, getServerTime, GetSuccessStatus, GetErrorMsg, GetErrorCode);
        }
        catch (Exception ex)
        {
            return ("GetPowerSetPointLevelByPercentage failed. ", 0, "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    // --------------------------Set--------------------------
    // Data Type of value depends on the variable we write on!!! You sould change it in SetDatasByCommunicationTag() as well!
    public async Task<(string, string, string, bool,string,int)> SetPowerSetPointLevel1(string DataLoggerType, double value)
    {
        try
        {
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnSetObject = await SetDatasByCommunicationTag(DataLogger.SetPowerSetPointLevel1(DataLoggerType), value);
            // bool SetDatas = returnSetObject.Datas;
            int SetErrorCode = returnSetObject.ErrorCode;
            string SetErrorMsg = returnSetObject.ErrorMsg;
            bool SetSuccessStatus = returnSetObject.IsSuccess;
            return ("Active Adjustment 1", getTimestamp, getServerTime, SetSuccessStatus, SetErrorMsg, SetErrorCode); 
        }
        catch (Exception ex)
        {
            return ("Active Adjustment 1 failed. ", "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, string, string, bool,string,int)> SetPowerSetPointLevel2(string DataLoggerType, double value)
    {
        try
        {
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnSetObject = await SetDatasByCommunicationTag(DataLogger.SetPowerSetPointLevel2(DataLoggerType), value);
            // bool SetDatas = returnSetObject.Datas;
            int SetErrorCode = returnSetObject.ErrorCode;
            string SetErrorMsg = returnSetObject.ErrorMsg;
            bool SetSuccessStatus = returnSetObject.IsSuccess;
            return ("Active Adjustment 2", getTimestamp, getServerTime, SetSuccessStatus, SetErrorMsg, SetErrorCode); 
        }
        catch (Exception ex)
        {
            return ("Active Adjustment 2 failed. ", "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
    public async Task<(string, string, string, bool,string,int)> SetPowerSetPointLevelByPercentage(string DataLoggerType, double value)
    { //Active Power Adjustement By Percentage
        try
        {
            string getServerTime;
            try
            {
                getServerTime = await GetServerTime(DataLoggerType);
            }
            catch (Exception ex)
            {
                getServerTime = "GetServerTime Failed. Error Message: " + ex.Message;
            }
            string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var returnSetObject = await SetDatasByCommunicationTag(DataLogger.SetPowerSetPointLevelByPercentage(DataLoggerType), value);
            // bool SetDatas = returnSetObject.Datas;
            int SetErrorCode = returnSetObject.ErrorCode;
            string SetErrorMsg = returnSetObject.ErrorMsg;
            bool SetSuccessStatus = returnSetObject.IsSuccess;
            return ("Active Power Adjustment By Percentage", getTimestamp, getServerTime, SetSuccessStatus, SetErrorMsg, SetErrorCode); 
        }
        catch (Exception ex)
        {
            return ("Active Power Adjustment By Percentage failed. ", "", "", false, $"Unknown Exception: {ex.Message}", -100);
        }
    }
}