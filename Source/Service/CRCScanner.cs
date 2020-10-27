using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRC.Source.Service
{
    public class CRCScanner
    {
        public List<Models.File> Files;

        public CRCScanner()
        {
            try
            {
                using (StreamReader reader = new StreamReader("crc.json"))
                {
                    var json = reader.ReadToEnd();
                    Files = JsonConvert.DeserializeObject<List<Models.File>>(json);
                }
            }
            catch
            {
                Files = new List<Models.File>();
            }
        }

        public void UpdateCRCStorage(string path)
        {
            var files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                ProcessFile(file);
            }
        }

        private void ProcessFile(string path)
        {
            byte[] data = System.IO.File.ReadAllBytes(path);

            var temp = Files.Where(x => x.Path == path).FirstOrDefault();

            if (temp != null)
            {
                Files.Where(x => x.Path == path).FirstOrDefault().CRC = CRCFactory.GetCRC32(data);
            }
            else
            {
                Files.Add(new Models.File()
                {
                    Path = path,
                    CRC = CRCFactory.GetCRC32(data),
                    Hash = MD5Hash.calculate(data)
                });
            }
        }

        public List<string> CompareFiles(string path)
        {
            var results = new List<string>();

            var files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                byte[] data = File.ReadAllBytes(file);

                var temp = Files.Where(x => x.Path == file).FirstOrDefault();

                if (temp != null)
                {
                    results.Add($"{file} is coincides with the reference: {CRCFactory.IsItReferenceCRC(temp.CRC, data)} Hash: " +
                                $"{MD5Hash.calculate(data)}");
                }
                else
                {
                    results.Add($"{file} is not exist in base");
                }
            }

            return results;
        }

        public bool IsPathExist(string path)
        {
            return Files.Where(x => x.Path.Contains(Path.GetFullPath(path))).Any();
        }


    }
}
