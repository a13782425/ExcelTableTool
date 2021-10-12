using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace DemoCsharp
{

    public sealed class GameAttrEnumParse : IParseValue
    {
        public string Name => "enum_GameAttrEnum".ToLower();

        public bool Parse(string value, out object res)
        {
            res = "";
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write(0);
                return true;
            }
            if (int.TryParse(value, out var result))
            {
                CSharpGenerateFormat.Binary.Write(result);
                return true;
            }
            if (Enum.TryParse(typeof(GameAttrEnum), value, out res))
            {
                CSharpGenerateFormat.Binary.Write((int)res);
                return true;
            }
            CSharpGenerateFormat.Binary.Write(0);
            return true;
        }
    }
    public sealed class BoolParse : IParseValue
    {
        public string Name => "bool";

        public bool Parse(string value, out object res)
        {
            res = 0;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write(value: false);
                return true;
            }
            if (bool.TryParse(value, out var @bool))
            {
                CSharpGenerateFormat.Binary.Write(@bool);
                return true;
            }
            if (int.TryParse(value, out var @int))
            {
                CSharpGenerateFormat.Binary.Write(@int > 0);
                return true;
            }
            return false;
        }
    }

    public sealed class ByteParse : IParseValue
    {
        public string Name => "byte";

        public bool Parse(string value, out object res)
        {
            res = 0;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write((byte)0);
                return true;
            }
            if (byte.TryParse(value, out var result))
            {
                CSharpGenerateFormat.Binary.Write(result);
                return true;
            }
            return false;
        }
    }

    public sealed class ShortParse : IParseValue
    {
        public string Name => "short";

        public bool Parse(string value, out object res)
        {
            res = 0;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write((short)0);
                return true;
            }
            if (short.TryParse(value, out var result))
            {
                CSharpGenerateFormat.Binary.Write(result);
                return true;
            }
            return false;
        }
    }

    public sealed class IntParse : IParseValue
    {
        public string Name => "int";

        public bool Parse(string value, out object res)
        {
            res = 0;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write(0);
                return true;
            }
            if (int.TryParse(value, out var result))
            {
                CSharpGenerateFormat.Binary.Write(result);
                return true;
            }
            return false;
        }
    }

    public sealed class LongParse : IParseValue
    {
        public string Name => "long";

        public bool Parse(string value, out object res)
        {
            res = 0L;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write(0L);
                return true;
            }
            if (long.TryParse(value, out var result))
            {
                CSharpGenerateFormat.Binary.Write(result);
                return true;
            }
            return false;
        }
    }

    public sealed class UIntParse : IParseValue
    {
        public string Name => "uint";

        public bool Parse(string value, out object res)
        {
            res = 0U;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write(0u);
                return true;
            }
            if (uint.TryParse(value, out var result))
            {
                CSharpGenerateFormat.Binary.Write(result);
                return true;
            }
            return false;
        }
    }

    public sealed class ULongParse : IParseValue
    {
        public string Name => "ulong";

        public bool Parse(string value, out object res)
        {
            res = 0UL;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write(0UL);
                return true;
            }
            if (ulong.TryParse(value, out var result))
            {
                CSharpGenerateFormat.Binary.Write(result);
                return true;
            }
            return false;
        }
    }


    public sealed class FloatParse : IParseValue
    {
        public string Name => "float";

        public bool Parse(string value, out object res)
        {
            res = 0f;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write(0f);
                return true;
            }
            if (float.TryParse(value, out var result))
            {
                CSharpGenerateFormat.Binary.Write(result);
                return true;
            }
            return false;
        }
    }

    public sealed class StringParse : IParseValue
    {
        public string Name => "string";

        public bool Parse(string value, out object res)
        {
            res = value;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write("");
                return true;
            }
            CSharpGenerateFormat.Binary.Write(value);
            return true;
        }
    }

    public abstract class ArrayBaseParse<T> : IParseValue
    {
        public abstract string Name { get; }
        public bool Parse(string value, out object res)
        {
            res = 0;
            if (string.IsNullOrWhiteSpace(value))
            {
                CSharpGenerateFormat.Binary.Write((byte)0);
                return true;
            }
            try
            {
                T[] array = JsonConvert.DeserializeObject<T[]>(value);
                if (array.Length >= byte.MaxValue)
                {
                    throw new Exception("数组长度最多不能超过255");
                }
                CSharpGenerateFormat.Binary.Write((byte)array.Length);
                foreach (T b in array)
                {
                    Write(b);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public abstract void Write(T v);
    }

    public abstract class Array2BaseParse<T> : IParseValue
    {
        public abstract string Name { get; }

        private bool Parse(T[] array)
        {
            try
            {
                if (array.Length >= byte.MaxValue)
                {
                    throw new Exception("数组长度最多不能超过255");
                }
                CSharpGenerateFormat.Binary.Write((byte)array.Length);
                foreach (T b in array)
                {
                    Write(b);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Parse(string value, out object res)
        {
            res = 0;
            try
            {
                //byte[][] bytes = new byte[1][];
                T[][] array = JsonConvert.DeserializeObject<T[][]>(value);
                if (array.Length >= byte.MaxValue)
                {
                    throw new Exception("数组长度最多不能超过255");
                }
                CSharpGenerateFormat.Binary.Write((byte)array.Length);
                foreach (T[] item in array)
                {
                    Parse(item);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public abstract void Write(T v);
    }


    public sealed class ArrayByteParse : ArrayBaseParse<byte>
    {
        public override string Name => "array_byte";

        public override void Write(byte v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }
    public sealed class ArrayByte2Parse : Array2BaseParse<byte>
    {
        public override string Name => "array_byte2";
        public override void Write(byte v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }

    public sealed class ArrayIntParse : ArrayBaseParse<int>
    {
        public override string Name => "array_int";

        public override void Write(int v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }
    public sealed class ArrayInt2Parse : Array2BaseParse<int>
    {
        public override string Name => "array_int2";

        public override void Write(int v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }

    public sealed class ArrayUIntParse : ArrayBaseParse<uint>
    {
        public override string Name => "array_uint";

        public override void Write(uint v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }
    public sealed class ArrayUInt2Parse : Array2BaseParse<uint>
    {
        public override string Name => "array_uint2";

        public override void Write(uint v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }


    public sealed class ArrayShortParse : ArrayBaseParse<short>
    {
        public override string Name => "array_short";

        public override void Write(short v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }
    public sealed class ArrayShort2Parse : Array2BaseParse<short>
    {
        public override string Name => "array_short2";

        public override void Write(short v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }


    public sealed class ArrayLongParse : ArrayBaseParse<long>
    {
        public override string Name => "array_long";

        public override void Write(long v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }
    public sealed class ArrayLong2Parse : Array2BaseParse<long>
    {
        public override string Name => "array_long2";

        public override void Write(long v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }

    public sealed class ArrayULongParse : ArrayBaseParse<ulong>
    {
        public override string Name => "array_ulong";

        public override void Write(ulong v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }
    public sealed class ArrayULong2Parse : Array2BaseParse<ulong>
    {
        public override string Name => "array_ulong2";

        public override void Write(ulong v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }

    public sealed class ArrayFloatParse : ArrayBaseParse<float>
    {
        public override string Name => "array_float";

        public override void Write(float v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }
    public sealed class ArrayFloat2Parse : Array2BaseParse<float>
    {
        public override string Name => "array_float2";

        public override void Write(float v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }

    public sealed class ArrayStringParse : ArrayBaseParse<string>
    {
        public override string Name => "array_string";

        public override void Write(string v)
        {
            CSharpGenerateFormat.Binary.Write(v.ToString());
        }
    }
    public sealed class ArrayString2Parse : Array2BaseParse<string>
    {
        public override string Name => "array_string2";

        public override void Write(string v)
        {
            CSharpGenerateFormat.Binary.Write(v);
        }
    }
}
