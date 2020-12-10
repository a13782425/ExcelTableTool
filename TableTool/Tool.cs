using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TableTool.Format;
using TableTool.GenerateCode;
using static TableTool.GlobalConst;

namespace TableTool
{
    public static class Tool
    {
        /// <summary>
        /// 检测表格
        /// </summary>
        /// <returns></returns>
        public static bool CheckExcel()
        {
            if (string.IsNullOrWhiteSpace(Params[CONSOLE_EXCEL_PATH]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"表格路径为空,请使用?或者help确定参数");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            if (Directory.Exists(Params[CONSOLE_EXCEL_PATH]))
            {
                string[] files = Directory.GetFiles(Params[CONSOLE_EXCEL_PATH], "*.xlsx", SearchOption.AllDirectories);
                string[] array = files;
                foreach (string path in array)
                {
                    string fileName = Path.GetFileName(path);
                    if (!fileName.StartsWith("~$") && Path.GetExtension(path).ToLower() == ".xlsx")
                    {
                        XlsDto xlsDto = new XlsDto();
                        xlsDto.Path = path;
                        xlsDto.FileName = Path.GetFileNameWithoutExtension(fileName);

                        Generate.XlsDtoList.Add(xlsDto);
                    }
                }
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"表格路径没有找到:{Path.GetFullPath(Params[CONSOLE_EXCEL_PATH])},请使用?或者help确定参数");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        /// <summary>
        /// 解析excel表
        /// </summary>
        /// <returns></returns>
        public static bool ResolveExcel()
        {
            foreach (var xls in Generate.XlsDtoList)
            {
                List<ExcelHelper> excelHelperList = ExcelHelper.GetAllHelper(xls.Path);
                foreach (var item in excelHelperList)
                {
                    TableDto tableDto = new TableDto();
                    tableDto.Helper = item;
                    tableDto.ExcelFileName = xls.FileName;
                    if (string.IsNullOrWhiteSpace(item.TableName))
                    {
                        tableDto.TableSheetName = xls.FileName;
                    }
                    else
                    {
                        tableDto.TableSheetName = item.TableName;
                    }
                    tableDto.DataFileName = tableDto.TableSheetName;
                    if (!string.IsNullOrWhiteSpace(Params[CONSOLE_EXTENSION]))
                    {
                        tableDto.DataFileName += Params[CONSOLE_EXTENSION];
                    }
                    IRow nextRow = item.GetNextRow();
                    IRow nextRow2 = item.GetNextRow();
                    IRow nextRow3 = item.GetNextRow();
                    IRow nextRow4 = item.GetNextRow();
                    if (nextRow != null && nextRow2 != null && nextRow4 != null)
                    {
                        bool flag = false;
                        for (int i = 1; i < nextRow.LastCellNum && nextRow2.LastCellNum >= i && nextRow4.LastCellNum >= i; i++)
                        {
                            string a = nextRow2.GetCell(i).ToString().ToLower();
                            string des = ((nextRow3.GetCell(i) == null) ? "" : nextRow3.GetCell(i).ToString());
                            if (!(a != "both") || !(a != Params[CONSOLE_PLATFORM].ToLower()))
                            {
                                flag = true;
                                PropertyDto dto = new PropertyDto();
                                GetTypeName(nextRow.GetCell(i).ToString(), ref dto);
                                dto.Index = i;
                                dto.Des = "";
                                dto.Des = des.Replace("\n", " ");
                                dto.PropertyName = nextRow4.GetCell(i).ToString();
                                dto.TranName = TranHump(dto.PropertyName);
                                tableDto.PropertyDtoList.Add(dto);
                            }
                        }
                        if (!flag)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"表格{xls.FileName}解析失败,请查看表格");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            xls.TableDtos.Add(tableDto);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"表格{xls.FileName}描述不完整，请检查前四行");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 驼峰转化
        /// </summary>
        /// <param name="waitStr"></param>
        /// <returns></returns>
        internal static string TranHump(string waitStr)
        {
            string[] strItems = waitStr.Split('_');
            string strItemTarget = strItems[0];
            for (int j = 1; j < strItems.Length; j++)
            {
                string temp = strItems[j].ToString();
                string temp1 = temp[0].ToString().ToUpper();
                string temp2 = "";
                temp2 = temp1 + temp.Remove(0, 1);
                strItemTarget += temp2;
            }
            return strItemTarget;
        }
        /// <summary>
        /// 检测一下代码导出路径
        /// </summary>
        /// <returns></returns>
        internal static bool CheckCodeOutPath()
        {
            if (string.IsNullOrWhiteSpace(Params[CONSOLE_CODE_PATH]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"数据导出目录为空,请使用?或者help确定参数");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            if (!Directory.Exists(Params[CONSOLE_CODE_PATH]))
            {
                Directory.CreateDirectory(Params[CONSOLE_CODE_PATH]);
            }
            return true;
        }

        /// <summary>
        /// 检测一下导出路径
        /// </summary>
        /// <returns></returns>
        public static bool CheckOutPath()
        {
            if (string.IsNullOrWhiteSpace(Params[CONSOLE_OUT_PATH]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"数据导出目录为空,请使用?或者help确定参数");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            if (!Directory.Exists(Params[CONSOLE_OUT_PATH]))
            {
                Directory.CreateDirectory(Params[CONSOLE_OUT_PATH]);
            }
            return true;
        }
        /// <summary>
        /// 获得格式化类
        /// </summary>
        /// <returns></returns>
        public static IFormat GetFormatClass()
        {
            if (string.IsNullOrWhiteSpace(Params[CONSOLE_FORMAT]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"导出类型为空,请使用?或者help确定参数");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
            switch (Params[CONSOLE_FORMAT].ToLower())
            {
                case "json":
                    return new JsonFormat();
                case "csv":
                    return new CSVFormat();
                case "luatable":
                    return new LuaTableFormat();
                case "luajson":
                    return new LuaJsonFormat();
                case "byte":
                    return new BytesFormat();
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"类型:{Params[CONSOLE_FORMAT]}没找到,请使用?或者help确定参数");
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
            }
        }

        private static void GetTypeName(string name, ref PropertyDto dto)
        {
            string text = name;
            if (text.StartsWith("enum_"))
            {
                text = "enum";
            }
            dto.IsEnum = false;
            dto.IsArray = true;
            switch (text)
            {
                case "bool":
                case "int":
                case "uint":
                case "byte":
                case "short":
                case "long":
                case "ulong":
                case "float":
                case "string":
                    dto.IsArray = false;
                    dto.PropertyType = name;
                    break;
                case "enum":
                    {
                        //    dto.IsEnum = true;
                        //    dto.IsArray = false;
                        //    string text2 = name.Split(new char[1]
                        //    {
                        //'_'
                        //    }, StringSplitOptions.RemoveEmptyEntries)[1];
                        //    dto.EnumType = text2;
                        //    text2 = text2.ToLower();
                        //    foreach (KeyValuePair<string, Type> item in EnumsTypeDic)
                        //    {
                        //        if (item.Key.ToLower() == text2)
                        //        {
                        //            dto.PropertyType = item.Key;
                        //            goto End;
                        //        }
                        //    }
                        //    dto.PropertyType = "int";
                        break;
                    }
                case "array_int":
                    dto.PropertyType = "int[]";
                    break;
                case "array_uint":
                    dto.PropertyType = "uint[]";
                    break;
                case "array_byte":
                    dto.PropertyType = "byte[]";
                    break;
                case "array_short":
                    dto.PropertyType = "short[]";
                    break;
                case "array_long":
                    dto.PropertyType = "long[]";
                    break;
                case "array_ulong":
                    dto.PropertyType = "ulong[]";
                    break;
                case "array_float":
                    dto.PropertyType = "float[]";
                    break;
                case "array_string":
                    dto.PropertyType = "string[]";
                    break;
                default:
                    {
                        dto.IsArray = false;
                        dto.PropertyType = "int";
                        break;
                    }
            }
        End: int num = 0;
        }
    }
}
