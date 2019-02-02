using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Server
{

    public class ByteBuffer : IDisposable
    {
        private readonly List<byte> buff; // buffer 
        private byte[] readbuffer;
        private int readPos;
        private bool buffUpdater = false; // indica se ho updatato il buffer

        public ByteBuffer() // instazia la classe
        {
            buff = new List<byte>();
            readPos = 0;
        }

        public int getReadposition()
        {
            return readPos;
        }
        public byte[] ToArray()
        {
            return buff.ToArray();
        }
        public int Count()
        {
            return buff.Count;
        }
        public int Length()
        {
            return Count() - readPos;
        }
        public void Clear() // svouta buffer
        {
            buff.Clear();
            readPos = 0;
        }

        // metodi per scrivere cose nel buffer
        public void WriteByte(byte input)
        {
            buff.Add(input);
            buffUpdater = true;
        }
        public void writeBytes(byte[] input)
        {
            buff.AddRange(input);
            buffUpdater = true;
        }
        public void Writeshort(short input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdater = true;
        }
        public void WriteInteger(int input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdater = true;
        }
        public void WriteLong(long input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdater = true;
        }
        public void WriteFloat(float input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdater = true;
        }
        public void WriteBool(bool input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdater = true;
        }
        public void WriteString(string input)
        {
            buff.AddRange(BitConverter.GetBytes(input.Length));
            buff.AddRange(Encoding.ASCII.GetBytes(input));
            buffUpdater = true;
        }

        // leggere dal buffer
        public byte ReadByte(bool peek = true)
        {
            if (buff.Count > readPos)
            {
                if (buffUpdater)
                {
                    readbuffer = buff.ToArray();
                    buffUpdater = false;
                }
                byte value = readbuffer[readPos];
                if (peek && buff.Count > readPos)
                {
                    readPos += 1;
                }
                return value;
            }
            else
                throw new Exception("non stai leggendo un byte");
        }
        public byte[] ReadBytes(int length, bool peek = true)
        {
            if (buff.Count > readPos)
            {
                if (buffUpdater)
                {
                    readbuffer = buff.ToArray();
                    buffUpdater = false;
                }
                byte[] value = buff.GetRange(readPos, length).ToArray();
                if (peek && buff.Count > readPos)
                {
                    readPos += length;
                }
                return value;
            }
            else
                throw new Exception("non stai leggendo un byte[]");
        }
        public short ReadShort(bool peek = true)
        {
            if (buff.Count > readPos)
            {
                if (buffUpdater)
                {
                    readbuffer = buff.ToArray();
                    buffUpdater = false;
                }
                short value = BitConverter.ToInt16(readbuffer, readPos);
                if (peek && buff.Count > readPos)
                {
                    readPos += 2; //perchè short = 2 byte
                }
                return value;
            }
            else
                throw new Exception("non stai leggendo un short");
        }
        public int ReadInteger(bool peek = true)
        {
            if (buff.Count > readPos)
            {
                if (buffUpdater)
                {
                    readbuffer = buff.ToArray();
                    buffUpdater = false;
                }
                int value = BitConverter.ToInt32(readbuffer, readPos);
                if (peek && buff.Count > readPos)
                {
                    readPos += 4; //perchè short = 2 byte
                }
                return value;
            }
            else
                throw new Exception("non stai leggendo un int");
        }
        public long ReadLong(bool peek = true)
        {
            if (buff.Count > readPos)
            {
                if (buffUpdater)
                {
                    readbuffer = buff.ToArray();
                    buffUpdater = false;
                }
                long value = BitConverter.ToInt64(readbuffer, readPos);
                if (peek && buff.Count > readPos)
                {
                    readPos += 8; //perchè short = 2 byte
                }
                return value;
            }
            else
                throw new Exception("non stai leggendo un long");
        }
        public float ReadFloat(bool peek = true)
        {
            if (buff.Count > readPos)
            {
                if (buffUpdater)
                {
                    readbuffer = buff.ToArray();
                    buffUpdater = false;
                }
                float value = BitConverter.ToSingle(readbuffer, readPos);
                if (peek && buff.Count > readPos)
                {
                    readPos += 4; //perchè short = 2 byte
                }
                return value;
            }
            else
                throw new Exception("non stai leggendo un float");
        }
        public bool ReadBool(bool peek = true)
        {
            if (buff.Count > readPos)
            {
                if (buffUpdater)
                {
                    readbuffer = buff.ToArray();
                    buffUpdater = false;
                }
                bool value = BitConverter.ToBoolean(readbuffer, readPos);
                if (peek && buff.Count > readPos)
                {
                    readPos += 1; //perchè short = 2 byte
                }
                return value;
            }
            else
                throw new Exception("non stai leggendo un bool");
        }
        public string ReadString(bool peek = true)
        {
            try
            {
                int length = ReadInteger(true);

                if (buffUpdater)
                {
                    readbuffer = buff.ToArray();
                    buffUpdater = false;
                }
                string value = Encoding.ASCII.GetString(readbuffer, readPos, length);
                if (peek && buff.Count > readPos && value.Length > 0)
                {
                    readPos += length;
                }
                return value;
            }
            catch (Exception)
            {
                throw new Exception("non stai leggendo una stringa");
            }


        }

        private bool DisposeValue = false; 
        protected virtual void Dispose(bool disposing) // aggiunge uno spazio
        {
            if (!DisposeValue)
            {
                if (disposing)
                {
                    buff.Clear();
                    readPos = 0;
                }
                DisposeValue = true;
            }
           
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // distrugge la classe dopo che è stata usata

        }
    }
}
