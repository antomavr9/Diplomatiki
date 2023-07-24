using System;
using System.Data.Common;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Modbus.Net;
using Modbus.Net.Modbus;
using ModbusMachineExtended;
// Pantelis Branch

class Program
{
    static async Task Main()
    {
        #region Utility

        // // Connect to server through utility api
        // ModbusUtility utility = new ModbusUtility(ModbusType.Tcp, "127.0.0.1", 1, 0);
        // bool connectionStatus = await utility.ConnectAsync(); // Here the execution starts

        // // Timer
        // Console.WriteLine("IsConnected:" + utility.IsConnected);
        // int t_sec = 1000; // 1 second = 1000 milliseconds
        // Console.WriteLine("Timer Started!");
        // await Task.Delay(t_sec*65);
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
        //     ReturnStruct<bool> returnSetObject = await utility!.SetDatasAsync("0X 1", new object[] {true});
        //     //(ushort) 1, (ushort) 2, (ushort) 3
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
        //     // Console.ReadKey();


        //     // //Loop
        //     //while (!exitKeyPressed)
        //     //{
        //     //   //Check if a key is pressed
        //     //   if (Console.KeyAvailable)
        //     //   {
        //     //       ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        //     //       exitKeyPressed = true;
        //     //   }
        //     //   await Task.Delay(2000); // Delay for 2 seconds before executing again
        //     //}

        #endregion

        #region Machine
        // ---------------------------------- Machine -------------------------------------------------
        // TIMER
        // int t_sec = 1000; // 1 second = 1000 milliseconds
        // Console.WriteLine("Timer Started!");
        // await Task.Delay(t_sec*60);
        // Console.WriteLine("Timer Finished!");

        // List<AddressUnit> addressUnits = new()
        // {
        //     new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)}, //Id is mandatory
        //     new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)}, // each address unit has each own id
        //     new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
        //     new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
        // };
        
        // // Connect to server through machine api
        // var machine = new ModbusMachine<string, string>("1", ModbusType.Tcp, "127.0.0.1:502", addressUnits, 1, 0);

        // bool connectionStatusMachine = await machine.ConnectAsync(); // we can also use machine.BaseUtility.ConnectAsync();
        // Here the execution starts and returns a boolean flag which we can use later to check if it connected successfully.
        // Using connectAsync here is not necessarily needed because machine.SetDatasAsync connects by itself.
        // // Disconnect
        // bool disconnectflag = machine.Disconnect();
        // // Here we use the GetAddressUnitById function to get a single address unit.
        //AddressUnit<string> singleaddressunit = machine.GetAddressUnitById("1");
        //Console.WriteLine("Address Unit"+ singleaddressunit);


        // // TIMER and IsConnected check.
        // Console.WriteLine("IsConnected:" + machine.IsConnected);
        // int t_sec = 1000; // 1 second = 1000 milliseconds
        // Console.WriteLine("Timer Started!");
        // await Task.Delay(t_sec*65);
        // Console.WriteLine("IsConnected:" + machine.IsConnected);

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
        // ---------------------------------- Extended Machine -------------------------------------------------
        List<AddressUnit> addressUnits = new()
        {
            new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)}, //Id is mandatory
            new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)}, // each address unit has each own id
            new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
            new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
        };
        var extendedMachine = new ModbusMachineExtended<string,string>("1", ModbusType.Tcp, "127.0.0.1:502", addressUnits, 1, 0);

        // Connect to server through extendedMachine
        bool connectionStatusMachine = await extendedMachine.ConnectAsync();
        if(!connectionStatusMachine)
        {
            Console.WriteLine("Connection Timed Out!");
        }
        else
        {
            Console.WriteLine("Connection Successful!");

            // ---------------------------------- Set Data -------------------------------------------------
            double Add1 = 11; 
            double Add2 = 22;
            double Add3 = 33;
            var setDic = new Dictionary<string, double> { {"Add1", (double) Add1}, {"Add2", (double) Add2}, {"Add5", (double) Add3}}; // I can access each address one by one from here
            var returnSetObject = await extendedMachine.SetDatasAsync(MachineDataType.CommunicationTag, setDic);
            // bool SetDatas = returnSetObject.Datas; // this parameter is not needed because SetDatas and the following parameter SetSuccessStatus are the same.
            int SetErrorCode = returnSetObject.ErrorCode;
            string SetErrorMsg = returnSetObject.ErrorMsg; 
            bool SetSuccessStatus = returnSetObject.IsSuccess;
            // Check Set Data Success Status
            if(SetSuccessStatus)
                Console.WriteLine("Set Data Status: Data Set Succesfully!");
            else
            {
                Console.WriteLine("Set Data Status: Error Code: " + SetErrorCode+ ". Error Message: " + SetErrorMsg + ".");
            }
                
            // ---------------------------------- Get Data ------------------------------------------------
            var returnGetObject = await extendedMachine.GetDatasByCommunicationTag(addressUnits,"Add2");
            // in the above line, instead of var we might use ReturnStruct<byte[]>
            byte[]? GetDatas = returnGetObject.Datas;
            int GetErrorCode = returnGetObject.ErrorCode;
            string GetErrorMsg = returnGetObject.ErrorMsg;
            bool GetSuccessStatus = returnGetObject.IsSuccess;

            // Check Get Data Success Status
            if(GetSuccessStatus)
            {
                Console.WriteLine("Get Data by CommunicationTag Status: Data Received Succesfully!");
            }
            else
            {
                Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode + ". Error Message: " + GetErrorMsg + ".");
            }
                
            // Print Received Data
            Console.WriteLine("Data Received from Modbus Server:");
            for (int i = 1; i < GetDatas!.Length; i+=2)
            {
                Console.WriteLine(GetDatas[i]);                
            }
        }
        // Exit Program
        Console.WriteLine("Press anything to exit...");
        Console.ReadKey();

        #endregion
    }
}