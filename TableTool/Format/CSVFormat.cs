using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TableTool.GenerateCode;
using static TableTool.GlobalConst;

namespace TableTool.Format
{
    public class CSVFormat : BaseFormat
    {
        public override void GenerateData()
        {
            foreach (var xls in Generate.XlsDtoList)
            {
                foreach (var item in xls.TableDtos)
                {
                    int num = 4;
                    try
                    {
                        List<string> keyList = new List<string>();
                        StringBuilder sb = new StringBuilder();
                        string title = "";
                        for (int i = 0; i < item.PropertyDtoList.Count; i++)
                        {
                            PropertyDto propertyDto = item.PropertyDtoList[i];
                            title += propertyDto.PropertyName;
                            if (i != item.PropertyDtoList.Count - 1)
                            {
                                title += "\t";
                            }
                        }
                        sb.AppendLine(title);
                        while (item.Helper.HaveData())
                        {
                            IRow nextRow5 = item.Helper.GetNextRow();
                            num++;
                            if (nextRow5 == null)
                            {
                                Console.WriteLine($"{item.ExcelFileName}表中{ item.TableSheetName}页签,第:{item.Helper.Index}行为空");
                            }
                            else
                            {
                                if (nextRow5.Count() < 2 || (nextRow5.GetCell(0) != null && nextRow5.GetCell(0).ToString() == "#"))
                                {
                                    continue;
                                }
                                if (nextRow5.GetCell(1) == null)
                                {
                                    break;
                                }
                                string text = nextRow5.GetCell(1).ToString();
                                if (string.IsNullOrWhiteSpace(text))
                                {
                                    break;
                                }
                                if (!keyList.Contains(text))
                                {
                                    keyList.Add(text);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(text + "存在多个");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                                string res = "";
                                for (int i = 0; i < item.PropertyDtoList.Count; i++)
                                {
                                    PropertyDto propertyDto = item.PropertyDtoList[i];
                                    string value = ((nextRow5.GetCell(propertyDto.Index) == null) ? null : nextRow5.GetCell(propertyDto.Index).ToString());
                                    res += value;
                                    if (i != item.PropertyDtoList.Count - 1)
                                    {
                                        res += "\t";
                                    }
                                }
                                sb.AppendLine(res);
                            }
                        }
                        File.WriteAllText(Path.Combine(Params[CONSOLE_OUT_PATH], item.DataFileName), sb.ToString(), new UTF8Encoding());
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"生成:{item.TableSheetName}成功");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{item.ExcelFileName}表中{item.TableSheetName}页签中第{num}行附近,CSV生成失败,请检查表格");
                        Console.WriteLine($"错误信息:{ex.Message}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        public override void GenerateCode()
        {
            if (string.IsNullOrWhiteSpace(Params[CONSOLE_CODE_TYPE]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"生成代码类型为空,请使用?或者help确定参数");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            IGenerateCode code;
            switch (Params[CONSOLE_CODE_TYPE].ToLower())
            {
                case "java":
                    code = new JavaJsonGenerateCode();
                    break;
                case "csharp":
                    code = new CSharpJsonGenerateCode();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"CSV数据:{Params[CONSOLE_CODE_TYPE]}代码暂不支持,请使用?或者help确定参数");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
            }
            code.GenerateCode();
        }
    }
}
