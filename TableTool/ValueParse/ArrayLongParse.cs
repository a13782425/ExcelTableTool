using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ArrayLongParse : IParseValue
    {
        public string Name => "array_long";

        public bool Parse(string value, out object res)
        {
            res = new long[0];
            try
            {
                res = JsonConvert.DeserializeObject<long[]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    internal sealed class ArrayLong2Parse : IParseValue
    {
        public string Name => "array_long2";

        public bool Parse(string value, out object res)
        {
            res = new long[0, 0];
            try
            {
                res = JsonConvert.DeserializeObject<long[,]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
