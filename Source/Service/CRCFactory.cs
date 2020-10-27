namespace CRC.Source.Service
{
    public static class CRCFactory
    {

        public static uint GetCRC32(byte[] buffer)
        {
            uint crc = 0xFFFFFFFF;
            uint[] crcTable = new uint[256];

            for (uint i = 0; i < 256; i++)
            {
                crc = i;
                for (uint j = 0; j < 8; j++)
                    crc = (crc & 1) != 0 ? (crc >> 1) ^ 0xEDB88320 : crc >> 1;

                crcTable[i] = crc;
            };


            crc = 0xFFFFFFFF;

            foreach (byte s in buffer)
            {
                crc = crcTable[(crc ^ s) & 0xFF] ^ (crc >> 8);
            }

            crc ^= 0xFFFFFFFF;

            return crc;
        }

        public static bool IsItReferenceCRC(uint reference, byte[] buffer)
        {
            var crc = GetCRC32(buffer);

            return crc == reference;
        }
    }
}