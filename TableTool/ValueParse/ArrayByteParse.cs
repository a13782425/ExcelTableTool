using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ArrayByteParse : IParseValue
    {
        public string Name => "array_byte";

        public bool Parse(string value, out object res)
        {
            res = new byte[0];
            try
            {
                res = JsonConvert.DeserializeObject<byte[]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    internal sealed class ArrayByte2Parse : IParseValue
    {
        public string Name => "array_byte2";

        public bool Parse(string value, out object res)
        {
            res = new byte[0, 0];
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
