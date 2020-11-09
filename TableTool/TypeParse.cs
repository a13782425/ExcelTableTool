using System;
using System.IO;

namespace TableTool
{
    internal class TypeParse
    {
        public static bool Parse(string value, PropertyDto propertyDto, BinaryWriter bw)
        {
            if (propertyDto.IsEnum)
            {
                return ParseEnum(value, propertyDto.PropertyType, bw);
            }
            return propertyDto.PropertyType switch
            {
                "bool" => ParseBoolean(value, bw),
                "int" => ParseInt32(value, bw),
                "uint" => ParseUInt32(value, bw),
                "byte" => ParseByte(value, bw),
                "short" => ParseInt16(value, bw),
                "long" => ParseInt64(value, bw),
                "ulong" => ParseUInt64(value, bw),
                "float" => ParseSingle(value, bw),
                "string" => ParseString(value, bw),
                "int[]" => ParseInt32Array(value, bw),
                "uint[]" => ParseUInt32Array(value, bw),
                "byte[]" => ParseByteArray(value, bw),
                "short[]" => ParseInt16Array(value, bw),
                "long[]" => ParseInt64Array(value, bw),
                "ulong[]" => ParseUInt64Array(value, bw),
                "float[]" => ParseSingleArray(value, bw),
                "string[]" => ParseStringArray(value, bw),
                _ => throw new Exception("该类型无法解析，类型为：" + propertyDto.PropertyType),
            };
        }
        public static bool Parse(string value, PropertyDto propertyDto, out object result)
        {
            if (propertyDto.IsEnum)
            {
                return ParseEnum(value, propertyDto.PropertyType, out result);
            }
            return propertyDto.PropertyType switch
            {
                "bool" => ParseBoolean(value, out result),
                "int" => ParseInt32(value, out result),
                "uint" => ParseUInt32(value, out result),
                "byte" => ParseByte(value, out result),
                "short" => ParseInt16(value, out result),
                "long" => ParseInt64(value, out result),
                "ulong" => ParseUInt64(value, out result),
                "float" => ParseSingle(value, out result),
                "string" => ParseString(value, out result),
                "int[]" => ParseInt32Array(value, out result),
                "uint[]" => ParseUInt32Array(value, out result),
                "byte[]" => ParseByteArray(value, out result),
                "short[]" => ParseInt16Array(value, out result),
                "long[]" => ParseInt64Array(value, out result),
                "ulong[]" => ParseUInt64Array(value, out result),
                "float[]" => ParseSingleArray(value, out result),
                "string[]" => ParseStringArray(value, out result),
                _ => throw new Exception("该类型无法解析，类型为：" + propertyDto.PropertyType),
            };
        }
        private static bool ParseEnum(string value, string propertyType, BinaryWriter bw)
        {
            //if (string.IsNullOrWhiteSpace(value))
            //{
            //	bw.Write(0);
            //	return true;
            //}
            //value = value.Replace("_", "");
            //if (int.TryParse(value, out var result))
            //{
            //	bw.Write(result);
            //	return true;
            //}
            //if (!Program.EnumsTypeDic.ContainsKey(propertyType))
            //{
            //	Console.WriteLine("所填写类型不存在，已使用默认值，请检查Enum是否生成");
            //	bw.Write(0);
            //	return true;
            //}
            //Type enumType = Program.EnumsTypeDic[propertyType];
            //object obj = Enum.Parse(enumType, value);
            //bw.Write((int)obj);
            bw.Write(0);
            return true;
        }
        private static bool ParseEnum(string value, string propertyType, out object obj)
        {
            obj = null;
            if (int.TryParse(value, out int num))
            {
                obj = num;
                return true;
            }
            return false;
        }

        private static bool ParseBoolean(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write(value: false);
                return true;
            }
            if (int.TryParse(value, out var result))
            {
                bw.Write(result > 0);
                return true;
            }
            return false;
        }
        private static bool ParseBoolean(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = false;
                return true;
            }
            if (int.TryParse(value, out var result))
            {
                obj = result > 0;
                return true;
            }
            return false;
        }

        private static bool ParseInt32(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write(0);
                return true;
            }
            if (int.TryParse(value, out var result))
            {
                bw.Write(result);
                return true;
            }
            return false;
        }
        private static bool ParseInt32(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = 0;
                return true;
            }
            if (int.TryParse(value, out var result))
            {
                obj = result;
                return true;
            }
            return false;
        }
        private static bool ParseUInt32(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write(0u);
                return true;
            }
            if (uint.TryParse(value, out var result))
            {
                bw.Write(result);
                return true;
            }
            return false;
        }
        private static bool ParseUInt32(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = 0u;
                return true;
            }
            if (uint.TryParse(value, out var result))
            {
                obj = result;
                return true;
            }
            return false;
        }

        private static bool ParseByte(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            if (byte.TryParse(value, out var result))
            {
                bw.Write(result);
                return true;
            }
            return false;
        }
        private static bool ParseByte(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = 0;
                return true;
            }
            if (byte.TryParse(value, out var result))
            {
                obj = result;
                return true;
            }
            return false;
        }

        private static bool ParseInt16(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((short)0);
                return true;
            }
            if (short.TryParse(value, out var result))
            {
                bw.Write(result);
                return true;
            }
            return false;
        }
        private static bool ParseInt16(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = 0;
                return true;
            }
            if (short.TryParse(value, out var result))
            {
                obj = result;
                return true;
            }
            return false;
        }
        private static bool ParseInt64(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write(0L);
                return true;
            }
            if (long.TryParse(value, out var result))
            {
                bw.Write(result);
                return true;
            }
            return false;
        }
        private static bool ParseInt64(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = 0L;
                return true;
            }
            if (long.TryParse(value, out var result))
            {
                obj = result;
                return true;
            }
            return false;
        }
        private static bool ParseUInt64(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write(0uL);
                return true;
            }
            if (ulong.TryParse(value, out var result))
            {
                bw.Write(result);
                return true;
            }
            return false;
        }
        private static bool ParseUInt64(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = 0UL;
                return true;
            }
            if (long.TryParse(value, out var result))
            {
                obj = result;
                return true;
            }
            return false;
        }
        private static bool ParseSingle(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write(0f);
                return true;
            }
            if (float.TryParse(value, out var result))
            {
                bw.Write(result);
                return true;
            }
            return false;
        }
        private static bool ParseSingle(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = 0f;
                return true;
            }
            if (float.TryParse(value, out var result))
            {
                obj = result;
                return true;
            }
            return false;
        }
        private static bool ParseString(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write("");
                return true;
            }
            bw.Write(value);
            return true;
        }
        private static bool ParseString(string value, out object obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = "";
                return true;
            }
            obj = value;
            return true;
        }
        private static bool ParseInt32Array(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            bw.Write((byte)array.Length);
            string[] array2 = array;
            foreach (string s in array2)
            {
                if (int.TryParse(s, out var result))
                {
                    bw.Write(result);
                    continue;
                }
                return false;
            }
            return true;
        }

        private static bool ParseInt32Array(string value, out object obj)
        {
            obj = new Int32[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = new Int32[0];
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            int[] temp = new int[array.Length];

            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string s = array2[i];
                if (int.TryParse(s, out var result))
                {
                    temp[i] = result;
                    continue;
                }
                temp = new int[0];
                return false;
            }
            obj = temp;
            return true;
        }

        private static bool ParseUInt32Array(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            bw.Write((byte)array.Length);
            string[] array2 = array;
            foreach (string s in array2)
            {
                if (uint.TryParse(s, out var result))
                {
                    bw.Write(result);
                    continue;
                }
                return false;
            }
            return true;
        }
        private static bool ParseUInt32Array(string value, out object obj)
        {
            obj = new uint[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = new uint[0];
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            uint[] temp = new uint[array.Length];

            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string s = array2[i];
                if (uint.TryParse(s, out var result))
                {
                    temp[i] = result;
                    continue;
                }
                temp = new uint[0];
                return false;
            }
            obj = temp;
            return true;
        }

        private static bool ParseByteArray(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            bw.Write((byte)array.Length);
            string[] array2 = array;
            foreach (string s in array2)
            {
                if (byte.TryParse(s, out var result))
                {
                    bw.Write(result);
                    continue;
                }
                return false;
            }
            return true;
        }
        private static bool ParseByteArray(string value, out object obj)
        {
            obj = new byte[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = new byte[0];
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            byte[] temp = new byte[array.Length];

            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string s = array2[i];
                if (byte.TryParse(s, out var result))
                {
                    temp[i] = result;
                    continue;
                }
                temp = new byte[0];
                return false;
            }
            obj = temp;
            return true;
        }
        private static bool ParseInt16Array(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            bw.Write((byte)array.Length);
            string[] array2 = array;
            foreach (string s in array2)
            {
                if (short.TryParse(s, out var result))
                {
                    bw.Write(result);
                    continue;
                }
                return false;
            }
            return true;
        }
        private static bool ParseInt16Array(string value, out object obj)
        {
            obj = new short[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = new short[0];
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            short[] temp = new short[array.Length];

            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string s = array2[i];
                if (short.TryParse(s, out var result))
                {
                    temp[i] = result;
                    continue;
                }
                temp = new short[0];
                return false;
            }
            obj = temp;
            return true;
        }
        private static bool ParseInt64Array(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            bw.Write((byte)array.Length);
            string[] array2 = array;
            foreach (string s in array2)
            {
                if (long.TryParse(s, out var result))
                {
                    bw.Write(result);
                    continue;
                }
                return false;
            }
            return true;
        }
        private static bool ParseInt64Array(string value, out object obj)
        {
            obj = new long[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = new long[0];
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            long[] temp = new long[array.Length];

            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string s = array2[i];
                if (long.TryParse(s, out var result))
                {
                    temp[i] = result;
                    continue;
                }
                temp = new long[0];
                return false;
            }
            obj = temp;
            return true;
        }
        private static bool ParseUInt64Array(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            bw.Write((byte)array.Length);
            string[] array2 = array;
            foreach (string s in array2)
            {
                if (ulong.TryParse(s, out var result))
                {
                    bw.Write(result);
                    continue;
                }
                return false;
            }
            return true;
        }
        private static bool ParseUInt64Array(string value, out object obj)
        {
            obj = new ulong[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = new ulong[0];
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            ulong[] temp = new ulong[array.Length];

            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string s = array2[i];
                if (ulong.TryParse(s, out var result))
                {
                    temp[i] = result;
                    continue;
                }
                temp = new ulong[0];
                return false;
            }
            obj = temp;
            return true;
        }
        private static bool ParseSingleArray(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            bw.Write((byte)array.Length);
            string[] array2 = array;
            foreach (string s in array2)
            {
                if (float.TryParse(s, out var result))
                {
                    bw.Write(result);
                    continue;
                }
                return false;
            }
            return true;
        }
        private static bool ParseSingleArray(string value, out object obj)
        {
            obj = new float[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = new float[0];
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            float[] temp = new float[array.Length];

            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string s = array2[i];
                if (float.TryParse(s, out var result))
                {
                    temp[i] = result;
                    continue;
                }
                temp = new float[0];
                return false;
            }
            obj = temp;
            return true;
        }
        private static bool ParseStringArray(string value, BinaryWriter bw)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                bw.Write((byte)0);
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            bw.Write((byte)array.Length);
            string[] array2 = array;
            foreach (string value2 in array2)
            {
                bw.Write(value2);
            }
            return true;
        }
        private static bool ParseStringArray(string value, out object obj)
        {
            obj = new string[0];
            if (string.IsNullOrWhiteSpace(value))
            {
                obj = new string[0];
                return true;
            }
            string[] array = value.Split(new char[1]
            {
                '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            obj = array;
            return true;
        }
    }
}
