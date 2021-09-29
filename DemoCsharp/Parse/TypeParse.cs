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

    public sealed class ArrayByteParse : IParseValue
    {
        public string Name => "array_byte";

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
                byte[] array = JsonConvert.DeserializeObject<byte[]>(value);
                if (array.Length >= byte.MaxValue)
                {
                    throw new Exception("数组长度最多不能超过255");
                }
                CSharpGenerateFormat.Binary.Write((byte)array.Length);
                foreach (byte b in array)
                {
                    CSharpGenerateFormat.Binary.Write(b);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public sealed class ArrayByte2Parse : IParseValue
    {
        public string Name => "array_byte2";

        public bool Parse(string value, out object res)
        {
            res = 0;
            try
            {
                res = JsonConvert.DeserializeObject<byte[,]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
