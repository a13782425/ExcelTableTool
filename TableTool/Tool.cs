using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TableCore;
using static TableTool.GlobalConst;

namespace TableTool
{
    public static class Tool
    {
        //public static unsafe string ToUpperFirst(this string str)
        //{
        //    if (str == null) return null;
        //    string temp = new string(str);
        //    fixed (char* ptr = temp)
        //        *ptr = char.ToUpper(*ptr);
        //    return temp;
        //}

        internal static void Init()
        {
            LoadValueParse();
            LoadPlugin();
        }

        /// <summary>
        /// 检测表格
        /// </summary>
        /// <returns></returns>
        public static bool CheckExcel(GenerateDto generate)
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

                        generate.XlsDtoList.Add(xlsDto);
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
        public static bool ResolveExcel(GenerateDto generate)
        {
            foreach (var xls in generate.XlsDtoList)
            {
                List<ExcelHelper> excelHelperList = ExcelHelper.GetAllHelper(xls.Path);
                foreach (var item in excelHelperList)
                {
                    TableDto tableDto = new TableDto();
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
                    IRow nextRow = item.GetNextRow();
                    IRow nextRow2 = item.GetNextRow();
                    IRow nextRow3 = item.GetNextRow();
                    IRow nextRow4 = item.GetNextRow();
                    if (nextRow != null && nextRow2 != null && nextRow4 != null)
                    {
                        bool flag = false;
                        int num = 1;
                        for (int i = 1; i < nextRow.LastCellNum && nextRow2.LastCellNum >= i && nextRow4.LastCellNum >= i; i++)
                        {
                            try
                            {
                                string platform = nextRow2.GetCell(i).ToString().ToLower();
                                string des = ((nextRow3.GetCell(i) == null) ? "" : nextRow3.GetCell(i).ToString());
                                if (!(platform != "both") || !(platform != Params[CONSOLE_PLATFORM].ToLower()))
                                {
                                    flag = true;
                                    PropertyDto dto = new PropertyDto();
                                    dto.PropertyType = nextRow.GetCell(i).ToString();
                                    dto.Index = i;
                                    dto.RealIndex = num;
                                    dto.Des = des.Replace("\n", " ");
                                    dto.PropertyName = nextRow4.GetCell(i).ToString();
                                    if (string.IsNullOrWhiteSpace(dto.PropertyName))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"表格{xls.FileName}中的{item.TableName}第{i}列附近字段名为空");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        return false;
                                    }
                                    if (!string.IsNullOrWhiteSpace(Params[CONSOLE_HUMP]))
                                    {
                                        //如果需要驼峰
                                        dto.PropertyName = TranHump(dto.PropertyName);
                                    }
                                    if (tableDto.PropertyDic.ContainsKey(dto.PropertyName))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"表格{xls.FileName}中的{item.TableName}第{i}列附近字段名重复");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        return false;
                                    }
                                    tableDto.PropertyDic.Add(dto.PropertyName, dto);
                                    num++;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"表格{xls.FileName}中的{item.TableName}前四行第{i}列附近数据有异常");
                                Console.ForegroundColor = ConsoleColor.White;
                                return false;
                            }
                        }
                        if (!flag)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"表格{xls.FileName}中的{item.TableName}没有当前平台所需要的数据");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            xls.TableDtos.Add(tableDto);
                            if (!ResolveExcelData(item, tableDto))
                                return false;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"表格{xls.FileName}中的{item.TableName}描述不完整，请检查前四行");
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }


                }
            }
            return true;
        }

        private static bool ResolveExcelData(ExcelHelper excel, TableDto tableDto)
        {
            int num = 4;
            try
            {
                List<string> keyList = new List<string>();
                while (excel.HaveData())
                {
                    IRow row = excel.GetNextRow();
                    num++;
                    if (row == null)
                    {
                        Console.WriteLine($"{tableDto.ExcelFileName}表中{ tableDto.TableSheetName}页签,第{excel.Index}行为空");
                    }
                    else
                    {
                        if (row.Count() < 2 || (row.GetCell(0) != null && row.GetCell(0).ToString() == "#"))
                        {
                            continue;
                        }
                        if (row.GetCell(1) == null)
                        {
                            break;
                            //throw new Exception($"{tableDto.ExcelFileName}表中{ tableDto.TableSheetName}页签,第{excel.Index}行Id为空");
                        }
                        string text = row.GetCell(1).ToString();
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            break;
                            //throw new Exception($"{tableDto.ExcelFileName}表中{ tableDto.TableSheetName}页签,第:{excel.Index}行Id为空");
                        }
                        if (!keyList.Contains(text))
                        {
                            keyList.Add(text);
                        }
                        else
                        {
                            throw new Exception($"{tableDto.ExcelFileName}表中{ tableDto.TableSheetName}页签中Id:{text}存在多个");
                        }
                        RowDataDto rowData = new RowDataDto(excel.Index);
                        rowData.Id = text;
                        foreach (KeyValuePair<string, PropertyDto> item in tableDto.PropertyDic)
                        {
                            PropertyDto propertyDto = item.Value;
                            ICell cell = row.GetCell(propertyDto.Index);
                            rowData[item.Key] = ExcelHelper.GetCellValue(cell);

                        }
                        tableDto.Rows.Add(rowData);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
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

        internal static bool GenerateCode(GenerateDto generate)
        {
            string codeName = Params[CONSOLE_CODE_TYPE].ToLower();
            if (!GenerateDic.ContainsKey(codeName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"代码生成类:{codeName}没找到,请检查插件中是否存在");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            IGenerateCode generateCode = GenerateDic[codeName];
            string packageName = "";
            if (!string.IsNullOrWhiteSpace(Params[CONSOLE_NAMESPACE]))
            {
                packageName = Params[CONSOLE_NAMESPACE];
            }
            foreach (var xlsDto in generate.XlsDtoList)
            {
                foreach (var tableDto in xlsDto.TableDtos)
                {
                    try
                    {
                        string fileName = tableDto.TableSheetName;
                        string str = generateCode.Generate(packageName, tableDto, ref fileName);
                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            File.WriteAllText(Path.Combine(Params[CONSOLE_CODE_PATH], fileName), str, new UTF8Encoding());
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"生成代码:{tableDto.TableSheetName}成功");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }
                }
            }
            return true;
        }
        internal static bool GenerateData(GenerateDto generate)
        {
            string formatName = Params[CONSOLE_FORMAT].ToLower();
            if (!FormatDic.ContainsKey(formatName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"数据生成类:{formatName}没找到,请检查插件中是否存在");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            IFormat format = FormatDic[formatName];
            ValueParse parse = new ValueParse();
            MethodInfo methodInfo = parse.GetType().GetMethod("setParseValues", BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo.Invoke(parse, new[] { ParseValueDic.Values.ToList() });
            methodInfo.Invoke(parse, new[] { format.GetCustomParse() });
            foreach (var xlsDto in generate.XlsDtoList)
            {
                foreach (var tableDto in xlsDto.TableDtos)
                {
                    try
                    {
                        string fileName = tableDto.DataFileName;
                        byte[] bytes = format.Format(tableDto, parse, ref fileName);
                        File.WriteAllBytes(Path.Combine(Params[CONSOLE_OUT_PATH], fileName), bytes);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"生成:{tableDto.TableSheetName}成功");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// 加载默认的数据转换
        /// </summary>
        private static void LoadValueParse()
        {
            Type parseType = typeof(IParseValue);
            Type[] types = typeof(Program).Assembly.GetTypes();
            foreach (var item in types)
            {
                if (!item.IsAbstract && !item.IsInterface)
                {
                    if (item.IsAssignableTo(parseType))
                    {
                        IParseValue parseValue = Activator.CreateInstance(item) as IParseValue;
                        string str = parseValue.Name.ToLower();
                        if (ParseValueDic.ContainsKey(str))
                        {
                            ParseValueDic[str] = parseValue;
                        }
                        else
                        {
                            ParseValueDic.Add(str, parseValue);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        private static void LoadPlugin()
        {
            ResolveAssembly(typeof(Program).Assembly);
            if (Directory.Exists(PLUGIN_PATH))
            {
                string[] paths = Directory.GetFiles(PLUGIN_PATH, "*.dll");
                foreach (var item in paths)
                {
                    string fileName = Path.GetFileNameWithoutExtension(item);
                    Assembly assembly = Assembly.LoadFile(Path.GetFullPath(item));
                    ResolveAssembly(assembly);
                    Console.WriteLine($"插件:{Path.GetFileNameWithoutExtension(item)} 加载完毕");
                }
            }
        }

        /// <summary>
        /// 解析程序集
        /// </summary>
        /// <param name="assembly"></param>
        private static void ResolveAssembly(Assembly assembly)
        {
            string assemblyName = assembly.GetName().Name;
            if (PluginDic.ContainsKey(assemblyName))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"插件:{assemblyName}重复加载");
                Console.ForegroundColor = ConsoleColor.White;
                PluginDic[assemblyName] = assembly;
            }
            else
            {
                PluginDic.Add(assemblyName, assembly);
            }
            Type[] types = assembly.GetTypes();

            foreach (var item in types)
            {
                if (!item.IsAbstract && !item.IsInterface)
                {
                    if (item.IsAssignableTo(FormatType))
                    {
                        IFormat format = Activator.CreateInstance(item) as IFormat;
                        string str = format.Name.ToLower();
                        if (FormatDic.ContainsKey(str))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"格式化:{format.Name} 已存在,即将被覆盖");
                            Console.ForegroundColor = ConsoleColor.White;
                            FormatDic[str] = format;
                        }
                        else
                        {
                            FormatDic.Add(str, format);
                        }
                    }
                    else if (item.IsAssignableTo(GenerateType))
                    {
                        IGenerateCode generateCode = Activator.CreateInstance(item) as IGenerateCode;
                        string str = generateCode.Name.ToLower();
                        if (GenerateDic.ContainsKey(str))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"代码生成:{generateCode.Name} 已存在,即将被覆盖");
                            Console.ForegroundColor = ConsoleColor.White;
                            GenerateDic[str] = generateCode;
                        }
                        else
                        {
                            GenerateDic.Add(str, generateCode);
                        }
                    }
                }
            }

        }
    }
}
