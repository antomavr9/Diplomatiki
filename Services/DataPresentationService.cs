using System.Text;

namespace ModbusExtension.Services;

public class DataPresentationService
{
    public static Int32 ByteToInt32(byte[]? byteData)
    {
        string result = ""; //result is a binary string
        var byteList = new List<string>();
        for (int i = 0; i < byteData!.Length; i += 1)
        {
            string binaryString = Convert.ToString(byteData[i], 2); // Convert byteData(int) to binary string

            // Pad zeros on the left if the binary representation is less than 8 bits
            int paddingLength = 8 - binaryString.Length;
            string paddedBinary = new string('0', paddingLength) + binaryString;
            byteList.Add(paddedBinary);
            result += byteList[i];
        }
        // Console.WriteLine(result);
        Int32 intResult = Convert.ToInt32(result, 2);
        return intResult; // Convert binary string result to Int32
    }

    public static UInt32 ByteToUInt32(byte[]? byteData)
    {
        string result = ""; //result is a binary string
        var byteList = new List<string>();
        for (int i = 0; i < byteData!.Length; i += 1)
        {
            string binaryString = Convert.ToString(byteData[i], 2); // Convert byteData(int) to binary string

            // Pad zeros on the left if the binary representation is less than 8 bits
            int paddingLength = 8 - binaryString.Length;
            string paddedBinary = new string('0', paddingLength) + binaryString;
            byteList.Add(paddedBinary);
            result += byteList[i];
        }
        // Console.WriteLine(result);
        UInt32 intResult = Convert.ToUInt32(result, 2);
        return intResult; // Convert binary string result to Int32
    }

    public static Int16 ByteToInt16(byte[]? byteData)
    {
        string result = ""; //result is a binary string
        var byteList = new List<string>();
        for (int i = 0; i < byteData!.Length; i += 1)
        {
            string binaryString = Convert.ToString(byteData[i], 2); // Convert byteData(int) to binary string

            // Pad zeros on the left if the binary representation is less than 8 bits
            int paddingLength = 8 - binaryString.Length;
            string paddedBinary = new string('0', paddingLength) + binaryString;
            byteList.Add(paddedBinary);
            result += byteList[i];
        }
        // Console.WriteLine(result);
        Int16 intResult = Convert.ToInt16(result, 2);
        return intResult; // Convert binary string result to Int32
    }

    public static UInt16 ByteToUInt16(byte[]? byteData)
    {
        string result = ""; //result is a binary string
        var byteList = new List<string>();
        for (int i = 0; i < byteData!.Length; i += 1)
        {
            string binaryString = Convert.ToString(byteData[i], 2); // Convert byteData(int) to binary string

            // Pad zeros on the left if the binary representation is less than 8 bits
            int paddingLength = 8 - binaryString.Length;
            string paddedBinary = new string('0', paddingLength) + binaryString;
            byteList.Add(paddedBinary);
            result += byteList[i];
        }
        // Console.WriteLine(result);
        UInt16 intResult = Convert.ToUInt16(result, 2);
        return intResult; // Convert binary string result to Int32
    }

    public static float ByteToFloat(byte[]? byteData)
    {
        string result = ""; //result is a binary string
        var byteList = new List<string>();
        for (int i = 0; i < byteData!.Length; i += 1)
        {
            string binaryString = Convert.ToString(byteData[i], 2); // Convert byteData(int) to binary string

            // Pad zeros on the left if the binary representation is less than 8 bits
            int paddingLength = 8 - binaryString.Length;
            string paddedBinary = new string('0', paddingLength) + binaryString;
            byteList.Add(paddedBinary);
            result += byteList[i];
        }
        // Console.WriteLine(result);
        int intResult = Convert.ToInt32(result, 2); // Convert binary string result to Int32
        return BitConverter.Int32BitsToSingle(intResult); // Convert Int32 to float
    }

    public static string ByteToStringUTF(byte[]? byteData)
    {
        string result = ""; //result is a binary string
        var byteList = new List<string>();
        for (int i = 0; i < byteData!.Length; i += 1)
        {
            string binaryString = Convert.ToString(byteData[i], 2); // Convert byteData(int) to binary string

            // Pad zeros on the left if the binary representation is less than 8 bits
            int paddingLength = 8 - binaryString.Length;
            string paddedBinary = new string('0', paddingLength) + binaryString;
            byteList.Add(paddedBinary);
            result += byteList[i];
        }

        byte[] byteArray = new byte[result.Length / 8];

        for (int l = 0; l < byteArray.Length; l++)
        {
            byteArray[l] = Convert.ToByte(result.Substring(l * 8, 8), 2);
        }

        // Decode byte array to UTF-8 string
        string utf8String = Encoding.UTF8.GetString(byteArray);

        return utf8String;
    }

    public static void PrintGetData<T>((string, T, string, string, bool, string, int) getData)
    {
        var name = getData.Item1;
        var value = getData.Item2;
        var getTimestamp = getData.Item3;
        var getServerTime = getData.Item4;
        var GetSuccessStatus = getData.Item5;
        var GetErrorMsg = getData.Item6;
        var GetErrorCode = getData.Item7;

        // Check Get Data Success Status
        if (GetSuccessStatus)
        {
            Console.WriteLine(name + ": " + value + ". Local Time: " + getTimestamp + ". Server Time: " + getServerTime + ".");
        }
        else
        {
            Console.WriteLine("Get Data Status: Error Code: " + GetErrorCode + ". Error Message: " + GetErrorMsg + ".");
        }
    }

    public static void PrintSetData((string, string, string, bool, string, int) setData)
    {
        var name = setData.Item1;
        var getTimestamp = setData.Item2;
        var getServerTime = setData.Item3;
        var SetSuccessStatus = setData.Item4;
        var SetErrorMsg = setData.Item5;
        var SetErrorCode = setData.Item6;
        if (SetSuccessStatus)
            Console.WriteLine(name + " was Set Succesfully! Local Time: " + getTimestamp + ". Server Time: " + getServerTime + ".");
        else
        {
            Console.WriteLine("Set Data Status: Error Code: " + SetErrorCode + ". Error Message: " + SetErrorMsg + ".");
        }
    }
    public static void AppendToCSV<T>((string, T, string, string, bool, string, int) data, string filePath)
    {
        // Create CSV file if it doesn't exist
        if (!File.Exists(filePath))
        {
            using StreamWriter writer = new StreamWriter(filePath);
            writer.WriteLine("Name,Value,Timestamp,ServerTime,SuccessStatus,ErrorMessage,ErrorCode");
        }

        // Append data to the CSV file
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"{data.Item1},{data.Item2},{data.Item3},{data.Item4},{data.Item5},{data.Item6},{data.Item7}");
        }

        Console.WriteLine("Data appended to CSV file successfully.");
    }

    public static float CalculateAverageAngle(List<float> anglesInDegrees)
    {
        float sumSin = 0.0f;
        float sumCos = 0.0f;

        foreach (var angle in anglesInDegrees)
        {
            float angleInRadians = angle * (float)Math.PI / 180.0f;
            sumSin += (float)Math.Sin(angleInRadians);
            sumCos += (float)Math.Cos(angleInRadians);
        }

        float averageAngleInRadians = (float)Math.Atan2(sumSin / anglesInDegrees.Count, sumCos / anglesInDegrees.Count);
        float averageAngleInDegrees = averageAngleInRadians * 180.0f / (float)Math.PI;

        // Normalize the angle to be within [0, 360)
        averageAngleInDegrees = (averageAngleInDegrees + 360) % 360;

        return averageAngleInDegrees;
    }

    public static bool CalculateLastValue(List<bool> values)
    {
        return values.Last();
    }

    public static float CalculateAverage(List<float> values)
    {
        return values.Average();
    }

    public static string CheckIfFolderExists(string folderName)
    {
        // Check if the directory exists and create it if it doesn't
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        return directoryPath;
    }
}
