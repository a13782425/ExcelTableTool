using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ArrayStringParse : IParseValue
    {
        public string Name => "array_string";

        public bool Parse(string value, out object res)
        {
            res = new string[0];
            try
            {
                res = JsonConvert.DeserializeObject<string[]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    internal sealed class ArrayString2Parse : IParseValue
    {
        public string Name => "array_string2";

        public bool Parse(string value, out object res)
        {
            res = new string[0, 0];
            try
            {
                res = JsonConvert.DeserializeObject<string[,]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
