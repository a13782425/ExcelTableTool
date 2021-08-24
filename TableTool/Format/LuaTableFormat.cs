//using NPOI.SS.UserModel;
//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using static TableTool.GlobalConst;

//namespace TableTool.Format
//{
//    public class LuaTableFormat : BaseFormat
//    {

//        public override void GenerateData()
//        {
//            CreateBase();
//            foreach (var xls in Generate.XlsDtoList)
//            {
//                foreach (var item in xls.TableDtos)
//                {
//                    int num = 4;
//                    try
//                    {
//                        List<string> keyList = new List<string>();
//                        StringBuilder sb = new StringBuilder();
//                        sb.AppendLine("------------------------------------------------------------------------------------------------------------");
//                        sb.AppendLine($"-------------------------------------------- generate file -------------------------------------------------");
//                        sb.AppendLine("------------------------------------------------------------------------------------------------------------");
//                        sb.AppendLine($"---@class { "table_" + item.TableSheetName}");
//                        for (int i = 0; i < item.PropertyDtoList.Count; i++)
//                        {
//                            PropertyDto propertyDto = item.PropertyDtoList[i];
//                            sb.AppendLine($"---@field public {(Params[CONSOLE_HUMP] == null ? propertyDto.PropertyName : propertyDto.TranName)} {propertyDto.PropertyType} @{propertyDto.Des}");
//                        }
//                        sb.AppendLine("local config = {}");
//                        sb.AppendLine("config.Variable = {");
//                        for (int i = 0; i < item.PropertyDtoList.Count; i++)
//                        {
//                            PropertyDto propertyDto = item.PropertyDtoList[i];
//                            if (i != item.PropertyDtoList.Count - 1)
//                            {
//                                sb.AppendLine($"    --- {propertyDto.Des},");
//                                sb.AppendLine($"    {(Params[CONSOLE_HUMP] == null ? propertyDto.PropertyName : propertyDto.TranName)} = {propertyDto.LuaIndex},");
//                            }
//                            else
//                            {
//                                sb.AppendLine($"    --- {propertyDto.Des},");
//                                sb.AppendLine($"    {(Params[CONSOLE_HUMP] == null ? propertyDto.PropertyName : propertyDto.TranName)} = {propertyDto.LuaIndex}");
//                            }
//                        }
//                        sb.AppendLine("}");
//                        StringBuilder dataSb = new StringBuilder();
//                        dataSb.AppendLine("config.Data = {");
//                        int length = 0;
//                        while (item.Helper.HaveData())
//                        {
//                            IRow nextRow5 = item.Helper.GetNextRow();
//                            num++;
//                            if (nextRow5 == null)
//                            {
//                                Console.WriteLine($"{item.ExcelFileName}表中{ item.TableSheetName}页签,第:{item.Helper.Index}行为空");
//                            }
//                            else
//                            {
//                                if (nextRow5.Count() < 2 || (nextRow5.GetCell(0) != null && nextRow5.GetCell(0).ToString() == "#"))
//                                {
//                                    continue;
//                                }
//                                if (nextRow5.GetCell(1) == null)
//                                {
//                                    break;
//                                }
//                                string text = nextRow5.GetCell(1).ToString();
//                                if (string.IsNullOrWhiteSpace(text))
//                                {
//                                    break;
//                                }
//                                if (!keyList.Contains(text))
//                                {
//                                    keyList.Add(text);
//                                }
//                                else
//                                {
//                                    Console.ForegroundColor = ConsoleColor.Red;
//                                    Console.WriteLine(text + "存在多个");
//                                    Console.ForegroundColor = ConsoleColor.White;
//                                }
//                                length++;
//                                string str = $"    [{text}] = {{";
//                                for (int i = 0; i < item.PropertyDtoList.Count; i++)
//                                {
//                                    PropertyDto propertyDto = item.PropertyDtoList[i];
//                                    ICell cell = nextRow5.GetCell(propertyDto.Index);
//                                    string value = ExcelHelper.GetCellValue(cell);
//                                    object res = null;
//                                    if (!TypeParse.Parse(value, propertyDto, out res))
//                                    {
//                                        throw new Exception($"{item.ExcelFileName}表中{ item.TableSheetName}页签,第{item.Helper.Index}行中，{propertyDto.PropertyName}序列化失败，错误类型为:{propertyDto.PropertyType}，请查看");
//                                    }
//                                    value = GetFormatValue(res, propertyDto.PropertyType);
//                                    if (i != item.PropertyDtoList.Count - 1)
//                                    {
//                                        str += $"{value}, ";
//                                    }
//                                    else
//                                    {
//                                        str += $"{value}}},";
//                                    }
//                                }
//                                dataSb.AppendLine(str);
//                            }
//                        }
//                        string temp = dataSb.ToString();
//                        int lastIndex = temp.LastIndexOf(",");
//                        temp = temp.Remove(lastIndex, 1);
//                        dataSb.Clear();
//                        dataSb.Append(temp);
//                        dataSb.AppendLine("}");
//                        sb.AppendLine($"config.Length = {length}");
//                        sb.AppendLine(dataSb.ToString());
//                        sb.AppendLine("return config");
//                        File.WriteAllText(Path.Combine(Params[CONSOLE_OUT_PATH], "table_" + item.DataFileName), sb.ToString(), new UTF8Encoding());
//                        Console.ForegroundColor = ConsoleColor.White;
//                        Console.WriteLine($"生成:{item.TableSheetName}成功");
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.ForegroundColor = ConsoleColor.Red;
//                        Console.WriteLine($"{item.ExcelFileName}表中{item.TableSheetName}页签中第{num}行附近,LuaTable生成失败,请检查表格");
//                        Console.WriteLine($"错误信息:{ex.Message}");
//                        Console.ForegroundColor = ConsoleColor.White;
//                    }
//                }
//            }
//        }

//        private void CreateBase()
//        {
//            string basePath = Path.Combine(Params[CONSOLE_OUT_PATH], "Base");
//            if (!Directory.Exists(basePath))
//            {
//                Directory.CreateDirectory(basePath);
//            }
//            string toolFile = basePath + "/tableTool.lua";
//            File.WriteAllText(toolFile, Resource.LuaTableTool, new UTF8Encoding());
//        }

//        private string GetFormatValue(object res, string typeName)
//        {
//            string text = typeName;
//            if (text.StartsWith("enum_"))
//            {
//                text = "enum";
//            }
//            switch (text)
//            {
//                case "bool":
//                    return res.ToString().ToLower();
//                case "int":
//                case "byte":
//                case "short":
//                case "long":
//                case "float":
//                case "uint":
//                case "ulong":
//                    return res.ToString();
//                case "string":
//                    return $"\"{res}\"";
//                case "enum":
//                    {
//                        return res.ToString();
//                    }
//                case "int[]":
//                case "uint[]":
//                case "byte[]":
//                case "short[]":
//                case "long[]":
//                case "ulong[]":
//                case "float[]":
//                case "string[]":
//                    string str = "{";
//                    Array array = res as Array;
//                    string ty = text.Replace("[]", "");
//                    foreach (var item in array)
//                    {
//                        str += GetFormatValue(item, ty) + ", ";
//                    }
//                    if (str.Length > 2)
//                    {
//                        str = str.Substring(0, str.Length - 2);
//                    }
//                    str += "}";
//                    return str;
//                default:
//                    return res.ToString();
//            }
//        }
//    }
//}
