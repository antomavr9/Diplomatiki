using System;
using System.Data.Common;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Modbus.Net;
using Modbus.Net.Modbus;
// Pantelis Branch
class Program
{
    static async Task Main()
    {
        #region Utility

        // // Connect to server through utility api using modbus type tcp at ip 127.0.0.1 at port 502
        // IUtility? utility = new ModbusUtility(ModbusType.Tcp, "127.0.0.1", 1, 0);
        // // utility.AddressTranslator = new AddressTranslatorModbus();
        // bool connectionStatus = await utility.ConnectAsync(); // Here the execution starts
       

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
            
        //     // Set Data
        //     ReturnStruct<bool> returnSetObject = await utility!.SetDatasAsync("0X 0", new object[] {true}); //(ushort) 1, (ushort) 2, (ushort) 3 //false, true, true, true, true, true, true, true, true, true, true, true, true, true, true,false
        //     // ReturnStruct<bool> returnSetObject = await utility!.SetDatasAsync("0X 1", new object[] { (ushort) 1});
        //     bool SetDatas = returnSetObject.Datas; // SetDatas and SetSuccessStatus are the same.
        //     int SetErrorCode = returnSetObject.ErrorCode;
        //     string SetErrorMsg = returnSetObject.ErrorMsg; 
        //     bool SetSuccessStatus = returnSetObject.IsSuccess;

        //     Console.WriteLine("Set Datas: "+SetDatas);
        //     Console.WriteLine("SetSuccessStatus: "+SetSuccessStatus);

        //     if(SetSuccessStatus)
        //     {
        //         Console.WriteLine("Set Data Status: Data Set Succesfully!");

                
        //     }
        //     else
        //     {
        //         Console.WriteLine("Set Data Status: Error Code: " + SetErrorCode+ ". Error Message: " + SetErrorMsg + ".");
        //     }
        
        //     bool exitKeyPressed = false;

        // // Start the loop
        // while (!exitKeyPressed)
        // {
        //     // Return object at first and then get datas
        //     ReturnStruct<byte[]> returnGetObject = await utility.GetDatasAsync("0X 1", 2);
        //     byte[]? GetDatas = returnGetObject.Datas;
        //     int GetErrorCode = returnGetObject.ErrorCode;
        //     string GetErrorMsg = returnGetObject.ErrorMsg;
        //     bool GetSuccessStatus = returnGetObject.IsSuccess;

        //     // Check Success Status
        //     if (GetSuccessStatus)
        //     {
        //         Console.WriteLine("Get Data Status: Data Received Successfully!");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode + ". Error Message: " + GetErrorMsg + ".");
        //     }

        //     // Print Received Datas
        //     Console.WriteLine("Data Received from Modbus Server:");
        //     for (int i = 1; i < GetDatas!.Length; i += 2)
        //     {
        //         Console.WriteLine(GetDatas[i]);
        //     }

        //     // Check if a key is pressed
        //     if (Console.KeyAvailable)
        //     {
        //         ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        //         exitKeyPressed = true;
        //     }

        //     await Task.Delay(2000); // Delay for 50 seconds before executing again
        // }
        // }

        #endregion

        #region Machine

        List<AddressUnit> addressUnits = new List<AddressUnit>
        {
            new AddressUnit() {Id = "1", Area = "4X", Address = 1, CommunicationTag = "Add1", DataType = typeof (ushort)}, //Id is mandatory but no idea what it does
            new AddressUnit() {Id = "2", Area = "4X", Address = 2, CommunicationTag = "Add2", DataType = typeof (ushort)},
            new AddressUnit() {Id = "3", Area = "4X", Address = 3, CommunicationTag = "Add3", DataType = typeof (ushort)},
            new AddressUnit() {Id = "4", Area = "4X", Address = 4, CommunicationTag = "Add4", DataType = typeof (ushort)}
        };

        // Connect to server through machine api using modbus type tcp at ip 127.0.0.1 at port 502
        BaseMachine<string, string>? machine = new ModbusMachine<string, string>("1", ModbusType.Tcp, "127.0.0.1:502", addressUnits, 1, 0);
        bool connectionStatusMachine = await machine.BaseUtility.ConnectAsync(); //Machine creates a utility object and connects with it.
        // await machine.ConnectAsync();

        Console.WriteLine("IsConnected:" + machine.IsConnected);
        int t_sec = 1000; // 1 second = 1000 milliseconds
        Console.WriteLine("Timer Started!");
        await Task.Delay(t_sec*65);
        Console.WriteLine("IsConnected:" + machine.IsConnected);

        // ------------------------------------------------ Set Datas ------------------------------------------------ \\
        double Add1 = 11; 
        double Add2 = 340;
        double Add3 = 30;
        var setDic = new Dictionary<string, double> { { "Add1", (double)Add1 }, { "Add2", (double)Add2 }, { "Add3", (double)Add3 } }; // I can access each address one by one
        
        var returnSetObject = await machine.SetDatasAsync(MachineDataType.CommunicationTag, setDic);
        bool SetDatas = returnSetObject.Datas; // SetDatas and SetSuccessStatus are the same.
        int SetErrorCode = returnSetObject.ErrorCode;
        string SetErrorMsg = returnSetObject.ErrorMsg; 
        bool SetSuccessStatus = returnSetObject.IsSuccess;

        // Print SetDatas Status
        if(SetSuccessStatus)
        {
            Console.WriteLine("Set Data Status: Data Set Succesfully!");
            
        }

        Console.WriteLine("IsConnected:" + machine.IsConnected);
        Console.WriteLine("Press anything to exit...");
        Console.ReadKey();

        // Return object at first and then get datas
        ReturnStruct<byte[]> returnGetObject = await machine.BaseUtility.GetUtilityMethods<IUtilityMethodDatas>().GetDatasAsync("4X 1", 2);
        byte[]? GetDatas = returnGetObject.Datas;
        int GetErrorCode = returnGetObject.ErrorCode;
        string GetErrorMsg = returnGetObject.ErrorMsg;
        bool GetSuccessStatus = returnGetObject.IsSuccess;

        // Check Success Status
        if(GetSuccessStatus)
        {
            Console.WriteLine("Get Data Status: Data Received Succesfully!");
        }
        else
        {
            Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode+ ". Error Message: " + GetErrorMsg + ".");
        }


        // Print Received Datas
        Console.WriteLine("Data Received from Modbus Server:");
        for (int i = 1; i < GetDatas!.Length; i+=2)
        {
            Console.WriteLine(GetDatas[i]);
            // When the Data are binary
            // var BinaryForm = Convert.ToString(GetDatas[i], 2);
            // Console.WriteLine(BinaryForm);
        }





        // // Connection Lasts 60 seconds.
        // if (!machine.IsConnected)
        // {
        //     Console.WriteLine("Connection Timed Out!");
        //     Console.WriteLine("Press anything to exit...");
        //     Console.ReadKey();    
        // }
        // else
        // {
        //     Console.WriteLine("Connection successful!");
        //     double Add1 = 11; 
        //     double Add2 = 340;
        //     double Add3 = 30;

        //     var setDic = new Dictionary<string, double> { { "Add1", (double)Add1 }, { "Add2", (double)Add2 }, { "Add3", (double)Add3 } }; // I can access each address one by one
        //     // Set Datas 
        //     var returnSetObject = await machine.SetDatasAsync(MachineDataType.CommunicationTag, setDic);
        //     bool SetDatas = returnSetObject.Datas; // SetDatas and SetSuccessStatus are the same.
        //     int SetErrorCode = returnSetObject.ErrorCode;
        //     string SetErrorMsg = returnSetObject.ErrorMsg; 
        //     bool SetSuccessStatus = returnSetObject.IsSuccess;
        //     if(SetSuccessStatus)
        //     {
        //         Console.WriteLine("Set Data Status: Data Set Succesfully!");
                
        //     }

        //     Console.WriteLine("IsConnected:" + machine.IsConnected);

            // // Return object at first and then get datas
            // ReturnStruct<byte[]> returnGetObject = await utility.GetDatasAsync("0X 1", 2);
            // byte[]? GetDatas = returnGetObject.Datas;
            // int GetErrorCode = returnGetObject.ErrorCode;
            // string GetErrorMsg = returnGetObject.ErrorMsg;
            // bool GetSuccessStatus = returnGetObject.IsSuccess;
            // // Check Success Status
            // if(GetSuccessStatus)
            // {
            //     Console.WriteLine("Get Data Status: Data Received Succesfully!");
            // }
            // else
            // {
            //     Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode+ ". Error Message: " + GetErrorMsg + ".");
            // }
            // // Print Received Datas
            // Console.WriteLine("Data Received from Modbus Server:");
            // for (int i = 1; i < GetDatas!.Length; i+=2)
            // {
            //     Console.WriteLine(GetDatas[i]);
            //     // When the Data are binary
            //     // var BinaryForm = Convert.ToString(GetDatas[i], 2);
            //     // Console.WriteLine(BinaryForm);
            // }



            // // var returnGetObject = await machine.GetDatasAsync(MachineDataType.CommunicationTag);
            // Dictionary<string, ReturnUnit<double>> GetDatas = returnGetObject.Datas;
            // int GetErrorCode = returnGetObject.ErrorCode;
            // string GetErrorMsg = returnGetObject.ErrorMsg;
            // bool GetSuccessStatus = returnGetObject.IsSuccess;

            // // Check Success Status
            // if(GetSuccessStatus)
            // {
            //     Console.WriteLine("Get Data Status: Data Received Succesfully!");
            // }
            // else
            // {
            //     Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode+ ". Error Message: " + GetErrorMsg + ".");
            // }

            // foreach (KeyValuePair<string, ReturnUnit<double>> pair in GetDatas)
            // {
            //     string key = pair.Key;
            //     // ReturnUnit<double> value = pair.Value;
            //     double? DeviceValue = pair.Value.DeviceValue;
            //     Console.WriteLine("Key: " + key + ", DeviceValue: " + DeviceValue);
            // }
            // Console.WriteLine("Press anything to exit...");
            // Console.ReadKey();
        // }
 

        #endregion


        // else
        // {
        //     Console.WriteLine("Set Data Status: Error Code: " + SetErrorCode+ ". Error Message: " + SetErrorMsg + ".");
        // }

        // // MachineDataType machineObject = new MachineDataType( 1, "Add1",  Add1,"1");
        // var returnGetObject = await machine.GetDatasAsync(MachineDataType.CommunicationTag); 

        //     // var returnGetObject = await machine.GetDatasAsync(MachineDataType.CommunicationTag);



    }
}