//using NPOI.SS.UserModel;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using TableTool.GenerateCode;
//using static TableTool.GlobalConst;

//namespace TableTool.Format
//{
//    public sealed class BytesFormat : BaseFormat
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
//                        string path = Path.Combine(Params[CONSOLE_OUT_PATH], item.DataFileName);
//                        FileStream fileStream = new FileStream(Path.Combine(path), FileMode.Create);
//                        BinaryWriter binaryWriter = new BinaryWriter(fileStream);
//                        List<string> keyList = new List<string>();
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
//                                    Console.WriteLine($"{item.ExcelFileName}表中{ item.TableSheetName}页签中Id:{text}存在多个");
//                                    Console.ForegroundColor = ConsoleColor.White;
//                                }
//                                foreach (PropertyDto propertyDto in item.PropertyDtoList)
//                                {
//                                    ICell cell = nextRow5.GetCell(propertyDto.Index);
//                                    string value = ExcelHelper.GetCellValue(cell);
//                                    if (!TypeParse.Parse(value, propertyDto, binaryWriter))
//                                    {
//                                        throw new Exception($"{item.ExcelFileName}表中{ item.TableSheetName}页签,第{item.Helper.Index}行中，{propertyDto.PropertyName}序列化失败，错误类型为:{propertyDto.PropertyType}，请查看");
//                                    }
//                                }
//                            }
//                        }
//                        binaryWriter.Close();
//                        binaryWriter.Dispose();
//                        fileStream.Close();
//                        fileStream.Dispose();
//                        Console.ForegroundColor = ConsoleColor.White;
//                        Console.WriteLine($"生成:{item.TableSheetName}成功");
//                        //todo 加密
//                        //byte[] array = File.ReadAllBytes(Path.Combine(tableDto.DataPath, tableDto.DataFileName));
//                        //byte[] array2 = new byte[array.Length + 1];
//                        //array2[0] = (byte)random.Next(1, 255);
//                        //for (int j = 0; j < array.Length; j++)
//                        //{
//                        //	array2[j + 1] = (byte)(array[j] ^ array2[0]);
//                        //}
//                        //File.WriteAllBytes(Path.Combine(tableDto.DataPath, tableDto.DataFileName), array2);
//                        //list.Add(tableDto);
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.ForegroundColor = ConsoleColor.Red;
//                        Console.WriteLine($"{item.ExcelFileName}表中{item.TableSheetName}页签中第{num}行附近,byte生成失败,请检查表格");
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
//                case "csharp":
//                    code = new CSharpBytesGenerateCode();
//                    break;
//                case "java":
//                default:
//                    Console.ForegroundColor = ConsoleColor.Red;
//                    Console.WriteLine($"Bytes数据:{Params[CONSOLE_CODE_TYPE]}代码暂不支持,请使用?或者help确定参数");
//                    Console.ForegroundColor = ConsoleColor.White;
//                    return;
//            }
//            code.GenerateCode();
//        }
//    }
//}
