namespace CRC.Source.Models
{
    public class File
    {
        public string Path { get; set; }
        public uint CRC { get; set; }
        public string Hash { get; set; }
    }
}