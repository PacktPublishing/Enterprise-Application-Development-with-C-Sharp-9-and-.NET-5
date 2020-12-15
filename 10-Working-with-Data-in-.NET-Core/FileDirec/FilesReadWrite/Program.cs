using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FilesReadWrite
{
    class Program
    {
        static async Task Main(string[] args)
        //  static void Main(string[] args)
        {
            // await UsingStreamReaderWriter();
            // await FileWriteUsingFileStream();
            //SerializeUsingJSONNET();
            await ReadStreamAsync();
            // UsingBinaryReaderWriter();
            Console.ReadLine();
        }

        static async Task UsingStreamReaderWriter()
        {
            // Create a string array with the lines of text
            string[] lines = { "This is the First line", "This is the Second line", "This is the Third line" };

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter("WriteLinesUsingSW.txt"))
            {
                foreach (string line in lines)
                {
                    await outputFile.WriteLineAsync(line);
                }
            }
            Console.WriteLine("File write completed using StreamWriter\n");

            StringBuilder sb = new StringBuilder();
            StringWriter srw = new StringWriter();

            using (StreamReader sr = new StreamReader("WriteLinesUsingSW.txt"))
            {
                Console.WriteLine(sr.BaseStream.Length); // Using base stream to retrieve length of file contents.
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    srw.Write($"{line}\n"); // Appending . at the end of line for easy seperate later
                }
            }

            Console.WriteLine("Reading from StringWriter");
            Console.WriteLine(srw.ToString());
            using (StringReader str = new StringReader(srw.ToString()))
            {
                while (str.Peek() > -1) // Peeking to check end of string
                {
                    char currentCharacter = Convert.ToChar(str.Read());

                    if (currentCharacter != '\n') //Checking current character and inserting at the beggining of string builder
                        sb.Insert(0, currentCharacter);
                    else
                        sb.Insert(0, '.');
                }
            }

            if (srw != null)
                srw.Dispose();
            Console.WriteLine("Reading from Stringbuilder aftere reversal");
            Console.WriteLine(sb.ToString());
        }

        static void SerializeUsingJSONNET()
        {
            Employee employee = new Employee
            {
                Name = "John",
                Id = 1
            };

            // // serialize JSON to a string and then write string to a file
            File.WriteAllText("employee.json", JsonConvert.SerializeObject(employee));
            JsonSerializer serializer = new JsonSerializer();
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText("employee.json"))
            {
                serializer.Serialize(file, employee);
            }

            var output = JsonConvert.DeserializeObject<Employee>(File.ReadAllText("employee.json"));

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText("employee.json"))
            {
                Employee employee2 = (Employee)serializer.Deserialize(file, typeof(Employee));
            }
        }

        static async Task ReadStreamAsync()
        {
            byte[] writeData = new byte[5] { 80, 65, 67, 75, 84 };
            UTF8Encoding temp = new UTF8Encoding(true);
            string data = "PACKT";
            Encoding.ASCII.GetBytes(data);
            using (Stream fs = File.Create("WriteDataUsingFileStream.txt"))
            {
                await fs.WriteAsync(writeData); //String PACKT in ASCII   
                byte[] readData = new byte[5];
                fs.Position = 0; // Setting the stram position to 0
                int chunkRed = 0, dataRed = 0;
                while ((chunkRed = fs.Read(readData, dataRed, readData.Length - chunkRed)) > 0)
                {
                    dataRed += chunkRed;
                }

                for (int i = 0; i < readData.Length; i++)
                    Console.Write(readData[i]);

                Console.Write($"{Encoding.ASCII.GetString(readData)}");
            }

        }
    }
}
