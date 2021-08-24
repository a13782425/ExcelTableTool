using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ArrayULongParse : IParseValue
    {
        public string Name => "array_ulong";

        public bool Parse(string value, out object res)
        {
            res = new ulong[0];
            try
            {
                res = JsonConvert.DeserializeObject<ulong[]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    internal sealed class ArrayULong2Parse : IParseValue
    {
        public string Name => "array_ulong2";

        public bool Parse(string value, out object res)
        {
            res = new ulong[0, 0];
            try
            {
                res = JsonConvert.DeserializeObject<ulong[,]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
