using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ArrayShortParse : IParseValue
    {
        public string Name => "array_short";

        public bool Parse(string value, out object res)
        {
            res = new short[0];
            try
            {
                res = JsonConvert.DeserializeObject<short[]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    internal sealed class ArrayShort2Parse : IParseValue
    {
        public string Name => "array_short2";

        public bool Parse(string value, out object res)
        {
            res = new short[0, 0];
            try
            {
                res = JsonConvert.DeserializeObject<short[,]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
