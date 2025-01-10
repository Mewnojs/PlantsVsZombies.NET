using System;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Sexy.TodLib
{
    internal class CustomContentReader
    {
        public CustomContentReader(ContentReader reader)
        {
            buffer = reader.ReadBytes((int)reader.BaseStream.Length);
        }

        public int ReadInt32()
        {
            int result = BitConverter.ToInt32(buffer, index);
            index += 4;
            return result;
        }

        public byte ReadByte()
        {
            byte result = buffer[index];
            index++;
            return result;
        }

        public float ReadSingle()
        {
            float result = BitConverter.ToSingle(buffer, index);
            index += 4;
            return result;
        }

        public bool ReadBoolean()
        {
            bool result = BitConverter.ToBoolean(buffer, index);
            index++;
            return result;
        }

        public string ReadString()
        {
            int num = BitConverter.ToInt32(buffer, index);
            index += 4;
            CustomContentReader.readStringBuilder.Remove(0, CustomContentReader.readStringBuilder.Length);
            for (int i = 0; i < num; i++)
            {
                CustomContentReader.readStringBuilder.Append(BitConverter.ToChar(buffer, index));
                index += 2;
            }
            if (num == 0)
            {
                return string.Empty;
            }
            return CustomContentReader.readStringBuilder.ToString();
        }

        private byte[] buffer;

        private int index;

        private static StringBuilder readStringBuilder = new StringBuilder(30);
    }
}
