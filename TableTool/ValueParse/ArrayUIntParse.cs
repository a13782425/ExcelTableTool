using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ArrayUIntParse : IParseValue
    {
        public string Name => "array_uint";

        public bool Parse(string value, out object res)
        {
            res = new uint[0];
            try
            {
                res = JsonConvert.DeserializeObject<uint[]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    internal sealed class ArrayUInt2Parse : IParseValue
    {
        public string Name => "array_uint2";

        public bool Parse(string value, out object res)
        {
            res = new uint[0, 0];
            try
            {
                res = JsonConvert.DeserializeObject<uint[,]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
