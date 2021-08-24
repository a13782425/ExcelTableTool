using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace TableTool
{
    internal sealed class ArrayFloatParse : IParseValue
    {
        public string Name => "array_float";

        public bool Parse(string value, out object res)
        {
            res = new float[0];
            try
            {
                res = JsonConvert.DeserializeObject<float[]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    internal sealed class ArrayFloat2Parse : IParseValue
    {
        public string Name => "array_float2";

        public bool Parse(string value, out object res)
        {
            res = new float[0, 0];
            try
            {
                res = JsonConvert.DeserializeObject<float[,]>(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
