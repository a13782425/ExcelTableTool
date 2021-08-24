using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ArrayIntParse : IParseValue
    {
        public string Name => "array_int";

        public bool Parse(string value, out object res)
        {
            res = new int[0];
            try
            {
                res = JsonConvert.DeserializeObject<int[]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    internal sealed class ArrayInt2Parse : IParseValue
    {
        public string Name => "array_int2";

        public bool Parse(string value, out object res)
        {
            res = new int[0, 0];
            try
            {
                res = JsonConvert.DeserializeObject<int[,]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
