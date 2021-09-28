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
}
