using CRC.Source.Service;
using Newtonsoft.Json;
using System;

namespace lab_6
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            var scanner = new CRCScanner();

            while (flag)
            {
                Console.Write($"1. Update CRC-code\n2. Compare CRC-code\n3. Exit\nInput: ");
                switch (Console.ReadLine())
                {
                    case "1":
                    {
                        Console.Write($"Input path: ");
                        var path = Console.ReadLine();

                        if (scanner.IsPathExist(path))
                        {
                            Console.Write($"{path} files already exists in base.\nUpdate? Y|N ");

                            if (Console.ReadLine().ToLower() == "n")
                            {
                                break;
                            }
                        }

                        scanner.UpdateCRCStorage(path);

                        break;
                    }
                    case "2":
                    {
                        Console.Write($"Input path: ");
                        var path = Console.ReadLine();

                        foreach (string result in scanner.CompareFiles(path))
                        {
                            Console.WriteLine(result);
                        }

                        break;
                    }
                    case "3":
                    {
                        flag = false;
                        break;
                    }
                }
            }

            System.IO.File.WriteAllText("crc.json", JsonConvert.SerializeObject(scanner.Files));

        }
    }
}
