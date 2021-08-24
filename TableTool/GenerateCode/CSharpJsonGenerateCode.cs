//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using static TableTool.GlobalConst;

//namespace TableTool.GenerateCode
//{
//    public class CSharpJsonGenerateCode : CSharpGenerateCode
//    {
//        public override void GenerateCode()
//        {
//            string package = "";
//            if (!string.IsNullOrWhiteSpace(Params[CONSOLE_NAMESPACE]))
//            {
//                package = Params[CONSOLE_NAMESPACE];
//            }
//            foreach (var xls in Generate.XlsDtoList)
//            {
//                foreach (var item in xls.TableDtos)
//                {
//                    string className = item.TableSheetName + "Table";

//                    StringBuilder codeSB = new StringBuilder();
//                    codeSB.AppendLine("/*");
//                    codeSB.AppendLine("\t生成代码,禁止修改");
//                    codeSB.AppendLine("*/");
//                    codeSB.AppendLine();
//                    string placeholder = "";
//                    if (!string.IsNullOrWhiteSpace(package))
//                    {
//                        codeSB.AppendLine("namespace " + package);
//                        codeSB.AppendLine("{");
//                        codeSB.AppendLine();
//                        placeholder = "    ";
//                    }

//                    codeSB.AppendLine($"{placeholder}public partial class {className}");
//                    codeSB.AppendLine($"{placeholder}{{");
//                    StringBuilder fieldSB = new StringBuilder();

//                    foreach (var propertyDto in item.PropertyDtoList)
//                    {
//                        string typeName = GetTypeName(propertyDto.PropertyType);
//                        string fieldName = Params[CONSOLE_HUMP] == null ? propertyDto.PropertyName : propertyDto.TranName;
//                        fieldSB.AppendLine($"{placeholder}    /// <summary>");
//                        fieldSB.AppendLine($"{placeholder}    /// {propertyDto.Des}");
//                        fieldSB.AppendLine($"{placeholder}    /// </summary>");
//                        fieldSB.AppendLine($"{placeholder}    public {typeName} {fieldName} {{ get; set; }}");
//                    }
//                    codeSB.Append(fieldSB.ToString());
//                    codeSB.AppendLine($"{placeholder}}}");
//                    if (!string.IsNullOrWhiteSpace(package))
//                    {
//                        codeSB.AppendLine("}");
//                    }
//                    File.WriteAllText(Path.Combine(Params[CONSOLE_CODE_PATH], className + ".gen.cs"), codeSB.ToString(), new UTF8Encoding());
//                    Console.WriteLine($"生成CSharp代码:{item.TableSheetName}成功");
//                }

//            }
//        }
//    }
//}
