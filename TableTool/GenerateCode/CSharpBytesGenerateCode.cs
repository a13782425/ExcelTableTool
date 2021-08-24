//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using static TableTool.GlobalConst;

//namespace TableTool.GenerateCode
//{
//    public sealed class CSharpBytesGenerateCode : CSharpGenerateCode
//    {
//        public override void GenerateCode()
//        {
//            CreateBase();
//            string package = "";
//            string placeholder = "";
//            if (!string.IsNullOrWhiteSpace(Params[CONSOLE_NAMESPACE]))
//            {
//                package = Params[CONSOLE_NAMESPACE];
//                placeholder = "    ";
//            }
//            foreach (var xls in Generate.XlsDtoList)
//            {
//                foreach (var item in xls.TableDtos)
//                {

//                    StringBuilder codeSB = new StringBuilder();
//                    codeSB.AppendLine("/*");
//                    codeSB.AppendLine("\t生成代码,禁止修改");
//                    codeSB.AppendLine("*/");
//                    codeSB.AppendLine();
//                    codeSB.AppendLine("using System;");
//                    codeSB.AppendLine("using System.Collections.Generic;");
//                    codeSB.AppendLine("using System.Linq;");
//                    codeSB.AppendLine("using System.IO;");
//                    codeSB.AppendLine();
//                    if (!string.IsNullOrWhiteSpace(package))
//                    {
//                        codeSB.AppendLine($"namespace {package}");
//                        codeSB.AppendLine($"{{");
//                    }
//                    string className = item.TableSheetName + "Table";
//                    codeSB.AppendLine($"{placeholder}public partial class {className} : TableBase<{className}>");
//                    codeSB.AppendLine($"{placeholder}{{");
//                    codeSB.AppendLine($"{placeholder}    public readonly Dictionary<int, {className}> data = new Dictionary<int, {className}>();");
//                    foreach (PropertyDto propertyDto in item.PropertyDtoList)
//                    {
//                        codeSB.AppendLine($"{placeholder}    /// <summary>");
//                        codeSB.AppendLine($"{placeholder}    /// {propertyDto.Des}");
//                        codeSB.AppendLine($"{placeholder}    /// </summary>");
//                        codeSB.AppendLine($"{placeholder}    public {GetTypeName(propertyDto.PropertyType)} {(Params[CONSOLE_HUMP] == null ? propertyDto.PropertyName : propertyDto.TranName)} {{ get; private set; }}");
//                    }
//                    codeSB.AppendLine();
//                    codeSB.AppendLine($"{placeholder}    internal IEnumerable<{className}> GetList(Func<{className}, bool> predicate)");
//                    codeSB.AppendLine($"{placeholder}    {{");
//                    codeSB.AppendLine($"{placeholder}        return data.Values.Where(predicate);");
//                    codeSB.AppendLine($"{placeholder}    }}");
//                    codeSB.AppendLine();
//                    codeSB.AppendLine($"{placeholder}    internal {className} Get(Func<{className}, bool> predicate)");
//                    codeSB.AppendLine($"{placeholder}    {{");
//                    codeSB.AppendLine($"{placeholder}        return data.Values.FirstOrDefault(predicate);");
//                    codeSB.AppendLine($"{placeholder}    }}");
//                    codeSB.AppendLine();
//                    codeSB.AppendLine($"{placeholder}    internal {className} Get(int id)");
//                    codeSB.AppendLine($"{placeholder}    {{");
//                    codeSB.AppendLine($"{placeholder}        if (data.ContainsKey(id))");
//                    codeSB.AppendLine($"{placeholder}            return data[id];");
//                    codeSB.AppendLine($"{placeholder}        return null;");
//                    codeSB.AppendLine($"{placeholder}    }}");
//                    codeSB.AppendLine();
//                    codeSB.AppendLine($"{placeholder}    public override int Parse(BinaryReader bReader)");
//                    codeSB.AppendLine($"{placeholder}    {{");
//                    foreach (PropertyDto propertyDto in item.PropertyDtoList)
//                    {
//                        //if (propertyDto3.IsEnum)
//                        //{
//                        //stringBuilder.AppendLine($"{placeholder}        this.{propertyDto.PropertyName } = ({propertyDto.PropertyType }){GetBinaryRead(propertyDto.PropertyType)};");
//                        //    continue;
//                        //}
//                        string proName = Params[CONSOLE_HUMP] == null ? propertyDto.PropertyName : propertyDto.TranName;
//                        codeSB.AppendLine($"{placeholder}        this.{proName} = {GetBinaryRead(propertyDto.PropertyType)};");
//                        if (propertyDto.IsArray)
//                        {
//                            string propertyType = propertyDto.PropertyType.Replace("[]", "");
//                            codeSB.AppendLine($"{placeholder}        for (int i = 0; i < this.{proName}.Length; i++)");
//                            codeSB.AppendLine($"{placeholder}        {{");
//                            codeSB.AppendLine($"{placeholder}            this.{proName}[i] = {GetBinaryRead(propertyType)};");
//                            codeSB.AppendLine($"{placeholder}        }}");
//                        }
//                    }
//                    codeSB.AppendLine($"{placeholder}        Instance.data.Add(this.id, this);");
//                    codeSB.AppendLine($"{placeholder}        return this.id;");
//                    codeSB.AppendLine($"{placeholder}    }}");
//                    codeSB.AppendLine($"{placeholder}}}");
//                    if (!string.IsNullOrWhiteSpace(package))
//                    {
//                        codeSB.AppendLine($"}}");
//                    }
//                    File.WriteAllText(Path.Combine(Params[CONSOLE_CODE_PATH], className + ".gen.cs"), codeSB.ToString(), new UTF8Encoding());
//                    Console.WriteLine($"生成CSharp代码:{item.TableSheetName}成功");
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
//            string toolFile = basePath + "/TableBase.cs";
//            File.WriteAllText(toolFile, Resource.CSharpTableBase, new UTF8Encoding());
//        }

//        private static string GetBinaryRead(string propertyType)
//        {
//            switch (propertyType)
//            {
//                case "bool":
//                    return "bReader.ReadBoolean()";
//                case "int":
//                case "enum":
//                    return "bReader.ReadInt32()";
//                case "uint":
//                    return "bReader.ReadUInt32()";
//                case "byte":
//                    return "bReader.ReadByte()";
//                case "short":
//                    return "bReader.ReadInt16()";
//                case "long":
//                    return "bReader.ReadInt64()";
//                case "ulong":
//                    return "bReader.ReadUInt64()";
//                case "float":
//                    return "bReader.ReadSingle()";
//                case "string":
//                    return "bReader.ReadString()";
//                case "int[]":
//                    return "new int[(int)bReader.ReadByte()]";
//                case "uint[]":
//                    return "new uint[(int)bReader.ReadByte()]";
//                case "byte[]":
//                    return "new byte[(int)bReader.ReadByte()]";
//                case "short[]":
//                    return "new short[(int)bReader.ReadByte()]";
//                case "long[]":
//                    return "new long[(int)bReader.ReadByte()]";
//                case "ulong[]":
//                    return "new ulong[(int)bReader.ReadByte()]";
//                case "float[]":
//                    return "new float[(int)bReader.ReadByte()]";
//                case "string[]":
//                    return "new string[(int)bReader.ReadByte()]";
//                default:
//                    return "bReader.ReadInt32()";
//            }
//        }
//    }
//}
