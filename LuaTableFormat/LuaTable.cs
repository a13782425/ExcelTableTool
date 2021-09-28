using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableCore;

namespace LuaTableFormat
{
    public class LuaTable : IFormat
    {
        public string Name => "luatable";

        public string Format(TableDto tableDto, ValueParse parse, ref string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"-------------------------------------------- generate file -------------------------------------------------");
            sb.AppendLine("------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"---@class { "table_" + tableDto.TableSheetName}");
            foreach (KeyValuePair<string, PropertyDto> item in tableDto.PropertyDic)
            {
                PropertyDto propertyDto = item.Value;
                sb.AppendLine($"---@field public {propertyDto.PropertyName} {propertyDto.PropertyType} @{propertyDto.Des}");

            }
            sb.AppendLine("local config = {}");
            sb.AppendLine("config.Variable = {");
            var tempList = tableDto.PropertyDic.Values.ToList();
            for (int i = 0; i < tempList.Count; i++)
            {
                PropertyDto propertyDto = tempList[i];
                if (i != tempList.Count - 1)
                {
                    sb.AppendLine($"    --- {propertyDto.Des},");
                    sb.AppendLine($"    {propertyDto.PropertyName } = {propertyDto.RealIndex},");
                }
                else
                {
                    sb.AppendLine($"    --- {propertyDto.Des},");
                    sb.AppendLine($"    { propertyDto.PropertyName } = {propertyDto.RealIndex}");
                }
            }
            sb.AppendLine("}");
            StringBuilder dataSb = new StringBuilder();
            dataSb.AppendLine("config.Data = {");
            foreach (RowDataDto item in tableDto.Rows)
            {
                string str = $"    [{item.Id}] = {{";
                for (int i = 0; i < tempList.Count; i++)
                {
                    PropertyDto prop = tempList[i];
                    object res = null;
                    parse.Parse(prop.PropertyType, item[prop.PropertyName], out res);
                    string value = GetFormatValue(res, prop.PropertyType, parse);
                    if (i != tempList.Count - 1)
                    {
                        str += $"{value}, ";
                    }
                    else
                    {
                        str += $"{value}}},";
                    }
                }
                dataSb.AppendLine(str);
            }
            string temp = dataSb.ToString();
            int lastIndex = temp.LastIndexOf(",");
            temp = temp.Remove(lastIndex, 1);
            dataSb.Clear();
            dataSb.Append(temp);
            dataSb.AppendLine("}");
            sb.AppendLine($"config.Length = {tableDto.Rows.Count}");
            sb.AppendLine(dataSb.ToString());
            sb.AppendLine("return config");
            fileName = $"table_{fileName}.lua";
            return sb.ToString();
        }
        public List<IParseValue> GetCustomParse() => new List<IParseValue>() { new BoolParse() };
        private string GetFormatValue(object res, string typeName, ValueParse parse)
        {
            string text = typeName;
            switch (text)
            {
                case "bool":
                    return res.ToString().ToLower();
                case "int":
                case "byte":
                case "short":
                case "long":
                case "float":
                case "uint":
                case "ulong":
                    return res.ToString();
                case "string":
                    return $"\"{res}\"";
                case string s when (s.StartsWith("array")):
                    string str = parse.ToJson(res);
                    str = str.Replace("[", "{");
                    str = str.Replace("]", "}");
                    //string str = "{";
                    //Array array = res as Array;
                    //string ty = text.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    //foreach (var item in array)
                    //{
                    //    str += GetFormatValue(item, ty) + ", ";
                    //}
                    //if (str.Length > 2)
                    //{
                    //    str = str.Substring(0, str.Length - 2);
                    //}
                    //str += "}";
                    return str;
                default:
                    return res.ToString();
            }
        }
    }
}
