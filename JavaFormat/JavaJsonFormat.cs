using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace JavaFormat
{
    internal sealed class JavaJsonFormat : IFormat
    {
        public string Name => "javajson";

        public string Format(TableDto tableDto, ValueParse parse, ref string fileName)
        {
            List<dynamic> list = new List<dynamic>();
            
            foreach (RowDataDto row in tableDto.Rows)
            {
                dynamic dy = new ExpandoObject();
                var obj = (IDictionary<String, Object>)dy;
                foreach (KeyValuePair<string, PropertyDto> propPair in tableDto.PropertyDic)
                {
                    PropertyDto prop = propPair.Value;
                    object res;
                    if (parse.Parse(prop.PropertyType, row[prop.PropertyName], out res))
                    {
                        obj.Add(prop.PropertyName, res);
                    }
                    else
                    {
                        throw new Exception($"{tableDto.ExcelFileName}表中{ tableDto.TableSheetName}页签,第{row.RowNum}行中，{prop.PropertyName}序列化失败，错误类型为:{prop.PropertyType}，请查看");
                    }
                }
                list.Add(dy);
            }
            fileName += ".json";
            return parse.ToJson(list);
        }
    }
}
