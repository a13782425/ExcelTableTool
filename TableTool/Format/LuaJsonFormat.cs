//using NPOI.SS.UserModel;
//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using TableTool.GenerateCode;
//using static TableTool.GlobalConst;

//namespace TableTool.Format
//{
//    public sealed class LuaJsonFormat : BaseFormat
//    {
//        public override void GenerateData()
//        {
//            foreach (var xls in Generate.XlsDtoList)
//            {
//                foreach (var item in xls.TableDtos)
//                {
//                    int num = 4;
//                    try
//                    {
//                        List<string> keyList = new List<string>();
//                        List<dynamic> list = new List<dynamic>();
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
//                                dynamic dy = new ExpandoObject();
//                                var obj = (IDictionary<String, Object>)dy;
//                                foreach (PropertyDto propertyDto in item.PropertyDtoList)
//                                {
//                                    ICell cell = nextRow5.GetCell(propertyDto.Index);
//                                    string value = ExcelHelper.GetCellValue(cell);
//                                    object res = null;
//                                    if (!TypeParse.Parse(value, propertyDto, out res))
//                                    {
//                                        throw new Exception($"{item.ExcelFileName}表中{ item.TableSheetName}页签,第{item.Helper.Index}行中，{propertyDto.PropertyName}序列化失败，错误类型为:{propertyDto.PropertyType}，请查看");
//                                    }
//                                    obj.Add(Params[CONSOLE_HUMP] == null ? propertyDto.PropertyName : propertyDto.TranName, res);
//                                }
//                                list.Add(dy);
//                            }
//                        }

//                        string temp = "local config = '";
//                        temp += Newtonsoft.Json.JsonConvert.SerializeObject(list);
//                        temp += "'";
//                        File.WriteAllText(Path.Combine(Params[CONSOLE_OUT_PATH], "table_" + item.DataFileName), temp, new UTF8Encoding());
//                        Console.ForegroundColor = ConsoleColor.White;
//                        Console.WriteLine($"生成:{item.TableSheetName}成功");
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.ForegroundColor = ConsoleColor.Red;
//                        Console.WriteLine($"{item.ExcelFileName}表中{item.TableSheetName}页签中第{num}行附近,LuaJson生成失败,请检查表格");
//                        Console.WriteLine($"错误信息:{ex.Message}");
//                        Console.ForegroundColor = ConsoleColor.White;
//                    }
//                }
//            }
//        }
//        public override void GenerateCode()
//        {
//            if (string.IsNullOrWhiteSpace(Params[CONSOLE_CODE_TYPE]))
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine($"生成代码类型为空,请使用?或者help确定参数");
//                Console.ForegroundColor = ConsoleColor.White;
//                return;
//            }
//            IGenerateCode code;
//            switch (Params[CONSOLE_CODE_TYPE].ToLower())
//            {
//                case "java":
//                    code = new JavaJsonGenerateCode();
//                    break;
//                case "csharp":
//                    code = new CSharpJsonGenerateCode();
//                    break;
//                default:
//                    Console.ForegroundColor = ConsoleColor.Red;
//                    Console.WriteLine($"Json数据:{Params[CONSOLE_CODE_TYPE]}代码暂不支持,请使用?或者help确定参数");
//                    Console.ForegroundColor = ConsoleColor.White;
//                    return;
//            }
//            code.GenerateCode();
//        }
//    }
//}
