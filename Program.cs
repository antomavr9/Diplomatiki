using Modbus.Net;
using Modbus.Net.Modbus;
using System;
using System.Threading;

using ModbusExtension;
using System.Text.Json;
using ModbusExtension.Models;
using ModbusExtension.Services;

//  List<AddressUnit> addressUnits = new List<AddressUnit>
//         {
//             new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)}, //Id is mandatory
//             new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)}, // each address unit has each own id
//             new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
//             new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
//         };

class Program
{
    static async Task Main()
    {
        #region Utility
        // // Connect to server through utility api
        // ModbusUtility utility = new ModbusUtility(ModbusType.Tcp, "127.0.0.1", 1, 0);
        // bool connectionStatus = await utility.ConnectAsync(); // Here the execution starts

        // // ReturnStruct<ushort[]> result = await utility.GetMultipleRegister(0, 1, 0,new ushort[] {}); // , new ushort[] { 0 }
        // if (result.IsSuccess)
        // {
        //     Console.WriteLine("Read values:");
        //     foreach (ushort value in result.Datas)
        //     {
        //         Console.WriteLine(value);
        //     }
        // }
        // else
        // {
        //     Console.WriteLine($"Error occurred: ErrorCode: {result.ErrorCode}, ErrorMsg: {result.ErrorMsg}");
        // }

        // Console.WriteLine("Press anything to exit...");
        // Console.ReadKey();

        // // Timer
        // Console.WriteLine("IsConnected:" + utility.IsConnected);
        // int t_sec = 1000; // 1 second = 1000 milliseconds
        // Console.WriteLine("Timer Started!");
        // await Task.Delay(t_sec*10);
        // Console.WriteLine("IsConnected:" + utility.IsConnected);

        // if (!connectionStatus) // Handle timeout
        // {   
        //     Console.WriteLine("Connection Timed Out!");
        //     Console.WriteLine("Press anything to exit...");
        //     Console.ReadKey(); 
        // }
        // else
        // {
        //     // Connection completed successfully
        //     Console.WriteLine("Connection successful!");

        //     // int t_sec = 1000; // 1 second = 1000 milliseconds
        //     // Console.WriteLine("Timer Started!");
        //     // await Task.Delay(t_sec*10);
        //     // Console.WriteLine("Timer Finished!");


        //     // Set Datas
        //     ReturnStruct<bool> returnSetObject = await utility!.SetDatasAsync("4X 1", new object[] {(ushort) 1}); //(ushort) 1, (ushort) 2, (ushort) 3
        //     // false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false
        //     //bool SetDatas = returnSetObject.Datas; // SetDatas and SetSuccessStatus are the same.
        //     int SetErrorCode = returnSetObject.ErrorCode;
        //     string SetErrorMsg = returnSetObject.ErrorMsg; 
        //     bool SetSuccessStatus = returnSetObject.IsSuccess;

        //     // Check Success Status
        //     if(SetSuccessStatus)
        //     {
        //         Console.WriteLine("Set Data Status: Data Set Succesfully!");

        //     }
        //     else
        //     {
        //         Console.WriteLine("Set Data Status: Error Code: " + SetErrorCode+ ". Error Message: " + SetErrorMsg + ".");
        //     }

        //     // Get Datas
        //     ReturnStruct<byte[]> returnGetObject = await utility.GetDatasAsync("0X 1", 2);
        //     byte[]? GetDatas = returnGetObject.Datas;
        //     int GetErrorCode = returnGetObject.ErrorCode;
        //     string GetErrorMsg = returnGetObject.ErrorMsg;
        //     bool GetSuccessStatus = returnGetObject.IsSuccess;

        //     // Check Success Status
        //     if(GetSuccessStatus)
        //     {
        //         Console.WriteLine("Get Data Status: Data Received Succesfully!");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode+ ". Error Message: " + GetErrorMsg + ".");
        //     }

        //     // Print Received Datas
        //     Console.WriteLine("Data Received from Modbus Server:");
        //     for (int i = 1; i < GetDatas!.Length; i+=2)
        //     {
        //         Console.WriteLine(GetDatas[i]);
        //         //var BinaryForm = Convert.ToString(GetDatas[i], 2); // if the data is bianry
        //         //Console.WriteLine(BinaryForm);
        //     }

        //     Console.WriteLine("Press anything to exit...");
        //     Console.ReadKey();

        // }


        // //Loop
        // while (!exitKeyPressed)
        // {
        //   //Check if a key is pressed
        //   if (Console.KeyAvailable)
        //   {
        //       ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        //       exitKeyPressed = true;
        //   }
        //   await Task.Delay(2000); // Delay for 2 seconds before executing again
        // }

        #endregion

        #region Machine
        // // ---------------------------------- Machine -------------------------------------------------
        // // // TIMER
        // // int t_sec = 1000; // 1 second = 1000 milliseconds
        // // Console.WriteLine("Timer Started!");
        // // await Task.Delay(t_sec*60);
        // // Console.WriteLine("Timer Finished!");

        // List<AddressUnit> addressUnits = new()
        // {
        //     new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)}, //Id is mandatory
        //     new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)}, // each address unit has each own id
        //     new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
        //     new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
        // };

        // // Connect to server through machine api
        // var machine = new ModbusMachine<string, string>("1", ModbusType.Tcp, "127.0.0.1:502", addressUnits,false, 1, 0);
        // // Console.WriteLine(machine.KeepConnect);
        // bool connectionStatusMachine = await machine.ConnectAsync(); // we can also use machine.BaseUtility.ConnectAsync();
        // // // Here the execution starts and returns a boolean flag which we can use later to check if it connected successfully.
        // // // Using connectAsync here is not necessarily needed because machine.SetDatasAsync connects by itself.
        // // // Disconnect
        // // bool disconnectflag = machine.Disconnect();
        // // Here we use the GetAddressUnitById function to get a single address unit.
        // // AddressUnit<string> singleaddressunit = machine.GetAddressUnitById("1");
        // // Console.WriteLine("Address Unit"+ singleaddressunit);


        // // // TIMER and IsConnected check.
        // // Console.WriteLine("IsConnected:" + machine.IsConnected);
        // // int t_sec = 1000; // 1 second = 1000 milliseconds
        // // Console.WriteLine("Timer Started!");
        // // await Task.Delay(t_sec*65);
        // // Console.WriteLine("IsConnected:" + machine.IsConnected);

        // // Connection Lasts 60 seconds.
        // if (!machine.IsConnected) // or we can use the connectionStatusMachine
        // {
        //    Console.WriteLine("Connection Timed Out!");
        //    Console.WriteLine("Press anything to exit...");
        //    Console.ReadKey();    
        // }
        // else
        // {
        //     Console.WriteLine("Connection successful!");

        //     double Add1 = 11; 
        //     double Add2 = 22;
        //     double Add3 = 33;

        //     var setDic = new Dictionary<string, double> { { "Add1", (double)Add1 }, { "Add2", (double)Add2 }, { "Add3", (double)Add3 } }; // I can access each address one by one from here

        //     // ---------------------------------- Set Data -------------------------------------------------

        //     var returnSetObject = await machine.SetDatasAsync(MachineDataType.CommunicationTag, setDic);

        //     Console.WriteLine("IsConnected:" + machine.IsConnected);

        //     // bool SetDatas = returnSetObject.Datas; // this parameter is not needed because SetDatas and the following parameter SetSuccessStatus are the same.
        //     int SetErrorCode = returnSetObject.ErrorCode;
        //     string SetErrorMsg = returnSetObject.ErrorMsg; 
        //     bool SetSuccessStatus = returnSetObject.IsSuccess;

        //     // Check success status
        //     if(SetSuccessStatus)
        //     {
        //         Console.WriteLine("Set Data Status: Data Set Succesfully!");

        //     }
        //     else
        //     {
        //         Console.WriteLine("Set Data Status: Error Code: " + SetErrorCode+ ". Error Message: " + SetErrorMsg + ".");
        //     }


        //     // ------------------------------- Get Data through BaseUtility ------------------------------
        //     var returnGetObject = await machine.BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync("4X 1", 2);
        //     // in the above line, instead of var we might use ReturnStruct<byte[]>
        //     byte[]? GetDatas = returnGetObject.Datas;
        //     int GetErrorCode = returnGetObject.ErrorCode;
        //     string GetErrorMsg = returnGetObject.ErrorMsg;
        //     bool GetSuccessStatus = returnGetObject.IsSuccess;

        //     // Check Success Status
        //     if(returnGetObject.IsSuccess)
        //     {
        //         Console.WriteLine("Get Data Status: Data Received Succesfully!");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Get Data Status: Error Code: " + returnGetObject.ErrorCode + ". Error Message: " + returnGetObject.ErrorMsg + ".");
        //     }

        //     // Print Received Data
        //     Console.WriteLine("Data Received from Modbus Server:");
        //     for (int i = 1; i < GetDatas!.Length; i+=2)
        //     {
        //         Console.WriteLine(GetDatas[i]);
        //         // When the Data are binary
        //         // var BinaryForm = Convert.ToString(GetDatas[i], 2);
        //         // Console.WriteLine(BinaryForm);                
        //     }

        //     // // -------------------------------- Get Data using machine --------------------------------
        //     // var returnGetObject = await machine.GetDatasAsync(MachineDataType.CommunicationTag); 
        //     // Dictionary<string, ReturnUnit<double>> GetDatas = returnGetObject.Datas; //MapGetValuesToSetValues()  TI KANEI AYTO REEEEEE
        //     // int GetErrorCode = returnGetObject.ErrorCode;
        //     // string GetErrorMsg = returnGetObject.ErrorMsg;
        //     // bool GetSuccessStatus = returnGetObject.IsSuccess;

        //     // // Check Success Status
        //     // if(GetSuccessStatus)
        //     // {
        //     //     Console.WriteLine("Get Data Status: Data Received Succesfully!");
        //     // }
        //     // else
        //     // {
        //     //     Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode+ ". Error Message: " + GetErrorMsg + ".");
        //     // }

        //     // // Print the data
        //     // foreach (KeyValuePair<string, ReturnUnit<double>> pair in GetDatas)
        //     // {
        //     //     string key = pair.Key;
        //     //     // ReturnUnit<double> value = pair.Value;
        //     //     double? DeviceValue = pair.Value.DeviceValue;
        //     //     Console.WriteLine("Key: " + key + ", DeviceValue: " + DeviceValue);
        //     // }


        //     Console.WriteLine("Press anything to exit...");
        //     Console.ReadKey();
        // }

        #endregion

        #region Extended Machine    
        // // ---------------------------------- Extended Machine -------------------------------------------------
        // List<AddressUnit> addressUnits = new()
        // {
        //     new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)}, //Id is mandatory
        //     new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)}, // each address unit has each own id
        //     new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
        //     new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
        // };
        // var extendedMachine = new ModbusMachineExtended<string,string>("1", ModbusType.Tcp, "127.0.0.1:502", addressUnits, false, 1, 0);
        // // var machine = new ModbusMachine<TKey, TUnitKey>(base.Id, _connectionType, _connectionString, base.GetAddresses, base.SlaveAddress, base.MasterAddress);

        // // Connect to server through extendedMachine
        // bool connectionStatusMachine = await extendedMachine.ConnectAsync();
        // if(!connectionStatusMachine)
        // {
        //     Console.WriteLine("Connection Timed Out!");
        // }
        // else
        // {
        //     Console.WriteLine("Connection Successful!");

        //     // // TIMER
        //     // int t_sec = 1000; // 1 second = 1000 milliseconds
        //     // Console.WriteLine(extendedMachine.IsConnected);
        //     // Console.WriteLine("Timer Started!");
        //     // await Task.Delay(t_sec*15);
        //     // Console.WriteLine("Timer Finished!");
        //     // Console.WriteLine(extendedMachine.IsConnected);


        //     // ---------------------------------- Set Data -------------------------------------------------
        //     var returnSetObject = await extendedMachine.SetDatasByCommunicationTag("Add1", 33);
        //     // bool SetDatas = returnSetObject.Datas; // this parameter is not needed because SetDatas and the following parameter SetSuccessStatus are the same.
        //     int SetErrorCode = returnSetObject.ErrorCode;
        //     string SetErrorMsg = returnSetObject.ErrorMsg; 
        //     bool SetSuccessStatus = returnSetObject.IsSuccess;
        //     // Check Set Data Success Status
        //     if(SetSuccessStatus)
        //         Console.WriteLine("Set Data Status: Data Set Succesfully!");
        //     else
        //     {
        //         Console.WriteLine("Set Data Status: Error Code: " + SetErrorCode+ ". Error Message: " + SetErrorMsg + ".");
        //     }

        //     // ---------------------------------- Get Data ------------------------------------------------
        //     var returnGetObject = await extendedMachine.GetDatasByCommunicationTag("Add2");
        //     // in the above line, instead of var we might use ReturnStruct<byte[]>
        //     byte[]? GetDatas = returnGetObject.Datas;
        //     int GetErrorCode = returnGetObject.ErrorCode;
        //     string GetErrorMsg = returnGetObject.ErrorMsg;
        //     bool GetSuccessStatus = returnGetObject.IsSuccess;

        //     // Check Get Data Success Status
        //     if(GetSuccessStatus)
        //     {
        //         Console.WriteLine("Get Data by CommunicationTag Status: Data Received Succesfully!");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode + ". Error Message: " + GetErrorMsg + ".");
        //     }

        //     // Print Received Data
        //     Console.WriteLine("Data Received from Modbus Server:");
        //     for (int i = 1; i < GetDatas!.Length; i+=2)
        //     {
        //         Console.WriteLine(GetDatas[i]);                
        //     }
        // }
        // // Exit Program
        // Console.WriteLine("Press anything to exit...");
        // Console.ReadKey();

        #endregion

        #region Json Handler

        // string jsonFilePath = "JsonData/TEST.json"; // JSON file path
        // string jsonFilePath = "JsonData/Huawei.json";
        // string jsonFilePath = "JsonData/Sungrow.json";

        // List<Base>? jsonDataArrayBase = JsonHandler.LoadFromFileBase(jsonFilePath);
        // List<Huawei>? jsonDataArrayHuawei = JsonHandler.LoadFromFileHuawei(jsonFilePath);
        // List<Sungrow>? jsonDataArraySungrow = JsonHandler.LoadFromFileSungrow(jsonFilePath);

        // List<AddressUnit>? BaseAddressUnits = JsonHandler.AddressUnitCreator(jsonDataArrayBase);
        // List<AddressUnit>? HuaweiAddressUnits = JsonHandler.AddressUnitCreator(jsonDataArrayHuawei);
        // List<AddressUnit>? SungrowAddressUnits = JsonHandler.AddressUnitCreator(jsonDataArraySungrow);

        // Console.WriteLine(jsonDataArrayBase[0].Name);
        // Console.WriteLine(jsonDataArrayHuawei[0].Name);
        // Console.WriteLine(jsonDataArraySungrow[0].Name);

        // var extendedMachine = new ModbusMachineExtended<string,string>("1", ModbusType.Tcp, "127.0.0.1:502", BaseAddressUnits, false, 1, 0);

        #endregion

        #region ExtendedMachineJson

        List<DataLogger>? jsonServersList = JsonHandlerService.LoadFromFileDataLogger("JsonData/DataLoggers.json");

        var extendedMachine = CreateMachineService.CreateModbusMachine(jsonServersList![0].Ip!, jsonServersList![0].DataLoggerType!); // 127.0.0.1:502 : Simulation IP an Port.  192.168.1.200:502 :
        bool connectionStatusMachine = await extendedMachine.ConnectAsync();
        if (!connectionStatusMachine)
        {
            Console.WriteLine("Connection Timed Out!");
        }
        else
        {
            Console.WriteLine("Connection Successful!");

            # region WriteGetDataToCsvLoop

            DateTime endTime = DateTime.Now.AddHours(24); // Calculate the end time which is 24 hours from now
            Console.WriteLine(endTime);
            while (DateTime.Now < endTime)
            {
                var activePowerData = await extendedMachine.GetActivePower(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(activePowerData, "CsvData/activePowerData.csv");
                DataPresentationService.PrintGetData(activePowerData);

                var reactivePowerData = await extendedMachine.GetReactivePower(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(reactivePowerData, "CsvData/reactivePowerData.csv");
                DataPresentationService.PrintGetData(reactivePowerData);

                var voltage1Data = await extendedMachine.GetVoltageL1(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(voltage1Data, "CsvData/voltage1Data.csv");
                DataPresentationService.PrintGetData(voltage1Data);

                var voltage2Data = await extendedMachine.GetVoltageL2(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(voltage2Data, "CsvData/voltage2Data.csv");
                DataPresentationService.PrintGetData(voltage2Data);

                var voltage3Data = await extendedMachine.GetVoltageL3(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(voltage3Data, "CsvData/voltage3Data.csv");
                DataPresentationService.PrintGetData(voltage3Data);

                var deviceStatusData = await extendedMachine.GetDeviceStatus(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(deviceStatusData, "CsvData/deviceStatusData.csv");
                DataPresentationService.PrintGetData(deviceStatusData);

                var logStatusData = await extendedMachine.GetLogStatus(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(logStatusData, "CsvData/logStatusData.csv");
                DataPresentationService.PrintGetData(logStatusData);

                var powerSetPointLevel1Data = await extendedMachine.GetPowerSetPointLevel1(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(powerSetPointLevel1Data, "CsvData/powerSetPointLevel1Data.csv");
                DataPresentationService.PrintGetData(powerSetPointLevel1Data);

                var powerSetPointLevel2Data = await extendedMachine.GetPowerSetPointLevel2(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(powerSetPointLevel2Data, "CsvData/powerSetPointLevel2Data.csv");
                DataPresentationService.PrintGetData(powerSetPointLevel2Data);

                var powerSetPointLevelByPercentageData = await extendedMachine.GetPowerSetPointLevelByPercentage(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
                DataPresentationService.AppendToCSV(powerSetPointLevelByPercentageData, "CsvData/powerSetPointLevelByPercentageData.csv");
                DataPresentationService.PrintGetData(powerSetPointLevelByPercentageData);

                // Sleep for 60 seconds
                Thread.Sleep(5 * 1000); // Sleep for 60 seconds (60,000 milliseconds)
            }

            #endregion

            //----------------------Get----------------------

            #region Get

            // var activePowerData = await extendedMachine.GetActivePower(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(activePowerData);

            // var reactivePowerData = await extendedMachine.GetReactivePower(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(reactivePowerData);

            // var voltage1Data = await extendedMachine.GetVoltageL1(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(voltage1Data);

            // var voltage2Data = await extendedMachine.GetVoltageL2(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(voltage2Data);

            // var voltage3Data = await extendedMachine.GetVoltageL3(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(voltage3Data);

            // var deviceStatusData = await extendedMachine.GetDeviceStatus(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(deviceStatusData);

            // var logStatusData = await extendedMachine.GetLogStatus(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(logStatusData);

            // var powerSetPointLevel1Data = await extendedMachine.GetPowerSetPointLevel1(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(powerSetPointLevel1Data);

            // var powerSetPointLevel2Data = await extendedMachine.GetPowerSetPointLevel2(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(powerSetPointLevel2Data);        

            // var powerSetPointLevelByPercentageData = await extendedMachine.GetPowerSetPointLevelByPercentage(jsonServersList![0].DataLoggerType!); //"192.168.1.200:502"
            // DataPresentation.PrintGetData(powerSetPointLevelByPercentageData);

            #endregion

            //----------------------Set----------------------

            # region SetActiveAdjustment1
            // var ActiveAdjustment1Data = await extendedMachine.SetPowerSetPointLevel1(jsonServersList![0].DataLoggerType!, 4294967295); //Default: 4294967295   UInt32
            // DataPresentation.PrintSetData(ActiveAdjustment1Data);

            // var ActiveAdjustment2Data = await extendedMachine.SetPowerSetPointLevel2(jsonServersList![0].DataLoggerType!, 5000); //Default: 4294967295   UInt32
            // DataPresentation.PrintSetData(ActiveAdjustment2Data);

            // var ActiveAdjustmentByPercentageData = await extendedMachine.SetPowerSetPointLevelByPercentage(jsonServersList![0].DataLoggerType!, 90); //Default: 65535  UInt16
            // DataPresentation.PrintSetData(ActiveAdjustmentByPercentageData);

            # endregion

            //----------------------Debugging----------------------

            # region SetData

            // // ---------------------------------- Set Data -------------------------------------------------
            // var returnSetObject = await extendedMachine.SetDatasByCommunicationTag("Active Adjustment 1", 112);
            // // await extendedMachine.SetDatasByCommunicationTag("City", 222);
            // // bool SetDatas = returnSetObject.Datas; // this parameter is not needed because SetDatas and the following parameter SetSuccessStatus are the same.
            // int SetErrorCode = returnSetObject.ErrorCode;
            // string SetErrorMsg = returnSetObject.ErrorMsg; 
            // bool SetSuccessStatus = returnSetObject.IsSuccess;
            // // Check Set Data Success Status
            // if(SetSuccessStatus)
            //     Console.WriteLine("Set Data Status: Data Set Succesfully!");
            // else
            // {
            //     Console.WriteLine("Set Data Status: Error Code: " + SetErrorCode+ ". Error Message: " + SetErrorMsg + ".");
            // }

            #endregion

            # region IterateBaseAddressUnits


            // string jsonFilePath = "JsonData/Huawei.json";
            // List<Base>? jsonDataArrayBase = JsonHandler.LoadFromFileBase(jsonFilePath);
            // List<AddressUnit<string,int,int>>? BaseAddressUnits = JsonHandler.AddressUnitCreator(jsonDataArrayBase!);
            // var returnGetObjectList  = new List<ReturnStruct<byte[]>>();
            // for (var i = 0; i < BaseAddressUnits.Count; i++)
            // {
            //     Console.WriteLine(i+1);
            //     string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //     Console.WriteLine(BaseAddressUnits[i].CommunicationTag);
            //     var returnGetObject = await extendedMachine.GetDatasByCommunicationTag(BaseAddressUnits[i].CommunicationTag);

            //     byte[]? GetDatas = returnGetObject.Datas;
            //     int GetErrorCode = returnGetObject.ErrorCode;
            //     string GetErrorMsg = returnGetObject.ErrorMsg;
            //     bool GetSuccessStatus = returnGetObject.IsSuccess;

            //     if (GetSuccessStatus && GetDatas != null)
            //     {
            //         if(i==48)
            //         {
            //             Console.WriteLine(DataPresentation.ByteToStringUTF(GetDatas));
            //         }
            //         else
            //         {
            //             Console.WriteLine(DataPresentation.ByteToInt32(GetDatas));
            //         }

            //         // string getTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //         Console.WriteLine(getTimestamp);


            //         Console.WriteLine("------------------DATA RECEIVED SUCCESSFULLY!!!!!!------------------"); // Data received successfully
            //         // Console.WriteLine("Get '" + BaseAddressUnits[i].CommunicationTag + "' = " + GetDatas[^1] + ", from Server Port " + extendedMachine.ConnectionToken + ", at " + getTimestamp + ".");
            //     }
            //     else
            //     {
            //         Console.WriteLine("Failed to get data.");
            //         // Console.WriteLine("Get '" + BaseAddressUnits[i].CommunicationTag + "' from Server Port " + extendedMachine.ConnectionToken + ": Error Code: " + GetErrorCode + ". Error Message: " + GetErrorMsg + ".");
            //     }
            //     returnGetObjectList.Add(returnGetObject);
            // }

            #endregion

            # region GetData


            // // ---------------------------------- Get Data ------------------------------------------------
            // // var returnGetObject = await extendedMachine.GetDatasByCommunicationTag("Active Adjustment 1");
            // // var returnGetObject = await extendedMachine.GetDatasByCommunicationTag("Active Power");

            // var returnGetObject = await extendedMachine.GetDatasByCommunicationTag("Active Adjustment 2");
            // // var returnGetObject = await extendedMachine.GetDatasByCommunicationTag("Active Power Adjustement By Percentage");

            // // var returnGetObject = await extendedMachine.GetDatasByCommunicationTag("CO2 Reduction 2"); 
            // // var returnGetObject = await extendedMachine.BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync("4X 50002", 2); //42779

            // // in the above line, instead of var we might use ReturnStruct<byte[]>
            // byte[]? GetDatas = returnGetObject.Datas;
            // int GetErrorCode = returnGetObject.ErrorCode;
            // string GetErrorMsg = returnGetObject.ErrorMsg;
            // bool GetSuccessStatus = returnGetObject.IsSuccess;

            // // Check Get Data Success Status
            // if(GetSuccessStatus)
            // {
            //     Console.WriteLine("Get Data by CommunicationTag Status: Data Received Succesfully!");
            // }
            // else
            // {
            //     Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode + ". Error Message: " + GetErrorMsg + ".");
            // }

            // // Print Received Data
            // Console.WriteLine("Data Received from Modbus Server:");
            // if (GetSuccessStatus && GetDatas != null)
            // {
            //     Console.WriteLine(DataPresentation.ByteToUInt32(GetDatas));
            //     // Console.WriteLine(DataPresentation.ByteToUInt16(GetDatas));
            // }
            // else
            // {
            //     Console.WriteLine("No Data Received!");
            // }


            #endregion
        }
        // Exit Program
        Console.WriteLine("Press anything to exit...");
        Console.ReadKey();

        #endregion
    }
}