using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCore
{

    public sealed class ValueParse
    {

        private Dictionary<string, IParseValue> _parseDic = new Dictionary<string, IParseValue>();

        public IParseValue this[string key]
        {
            get
            {
                string str = key.ToLower();
                if (_parseDic.ContainsKey(str))
                {
                    return _parseDic[str];
                }
                return null;
            }
        }

        public bool Parse(string key, string value, out object res)
        {
            IParseValue parse = this[key];
            if (parse != null)
            {
                return parse.Parse(value, out res);
            }
            Console.WriteLine($"数据解析失败,类型:{key},已返回原值");
            res = value;
            return true;
        }

        public string ToJson(object res)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(res);
        }

        private void setParseValues(List<IParseValue> list)
        {
            foreach (var item in list)
            {
                if (_parseDic.ContainsKey(item.Name))
                {
                    _parseDic[item.Name] = item;
                }
                else
                {
                    _parseDic.Add(item.Name, item);
                }
            }
        }

    }
}
