using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace DemoCsharp
{
    public sealed class CSharpGenerateCode : IGenerateCode
    {
        public string Name => "csharp";

        public string Generate(string package, TableDto tableDto, ref string fileName)
        {

            fileName += "Table.gen.cs";
            fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
            string className = tableDto.TableSheetName + "Table";
            className = className.Substring(0, 1).ToUpper() + className.Substring(1);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"/*");
            stringBuilder.AppendLine($"\t生成代码,禁止修改");
            stringBuilder.AppendLine($"*/");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"using System;");
            stringBuilder.AppendLine($"using System.Collections.Generic;");
            stringBuilder.AppendLine($"using System.Linq;");
            stringBuilder.AppendLine($"using System.IO;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"public partial class {className } : TableBase<{className }>");
            stringBuilder.AppendLine($"{{");
            //stringBuilder.AppendLine($"    public readonly Dictionary<int, {className}> data = new Dictionary<int, {className}>();");
            foreach (var item in tableDto.PropertyDic)
            {
                PropertyDto property = item.Value;
                stringBuilder.AppendLine($"    /// <summary>");
                stringBuilder.AppendLine($"    /// { property.Des}");
                stringBuilder.AppendLine($"    /// </summary>");
                stringBuilder.AppendLine($"    public { GetType(property.PropertyType)} {property.PropertyName } {{ get; private set; }}");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    public static IEnumerable<{className}> GetList(Func<{className}, bool> predicate)");
            stringBuilder.AppendLine($"    {{");
            stringBuilder.AppendLine($"        return Datas.Values.Where(predicate);");
            stringBuilder.AppendLine($"    }}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    public static {className} Get(Func<{className}, bool> predicate)");
            stringBuilder.AppendLine($"    {{");
            stringBuilder.AppendLine($"        return Datas.Values.FirstOrDefault(predicate);");
            stringBuilder.AppendLine($"    }}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    public static {className} Get(int id)");
            stringBuilder.AppendLine($"    {{");
            stringBuilder.AppendLine($"        if (Datas.ContainsKey(id))");
            stringBuilder.AppendLine($"            return Datas[id];");
            stringBuilder.AppendLine($"        return null;");
            stringBuilder.AppendLine($"    }}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"    public override int Parse(BinaryReader bReader)");
            stringBuilder.AppendLine($"    {{");
            foreach (var item in tableDto.PropertyDic)
            {
                PropertyDto property = item.Value;
                if (property.PropertyType.StartsWith("enum_"))
                {
                    stringBuilder.AppendLine($"        this.{ property.PropertyName } = ({ GetType(property.PropertyType) }){GetBinaryRead(property.PropertyType) };");
                    continue;
                }
                stringBuilder.AppendLine($"        this.{property.PropertyName} = {GetBinaryRead(property.PropertyType) };");
                if (property.PropertyType.StartsWith("array_"))
                {

                    string basicType = property.PropertyType.Replace("array_", "").Replace("2", "");

                    if (property.PropertyType.EndsWith("2"))
                    {
                        stringBuilder.AppendLine($"        for (int i = 0; i < this.{ property.PropertyName }.Length; i++)");
                        stringBuilder.AppendLine($"        {{");
                        stringBuilder.AppendLine($"            this.{ property.PropertyName }[i] = {GetBinaryRead("array_" + basicType)};");
                        stringBuilder.AppendLine($"            for (int j = 0; j < this.{ property.PropertyName }[i].Length; j++)");
                        stringBuilder.AppendLine($"            {{");
                        stringBuilder.AppendLine($"                this.{ property.PropertyName }[i][j] = {GetBinaryRead(basicType)};");
                        stringBuilder.AppendLine($"            }}");
                        stringBuilder.AppendLine($"        }}");
                    }
                    else
                    {
                        stringBuilder.AppendLine($"        for (int i = 0; i < this." + property.PropertyName + ".Length; i++)");
                        stringBuilder.AppendLine($"        {{");
                        stringBuilder.AppendLine($"            this." + property.PropertyName + "[i] = " + GetBinaryRead(basicType) + ";");
                        stringBuilder.AppendLine($"        }}");
                    }
                }
            }
            stringBuilder.AppendLine($"        Datas.Add(this.id, this);");
            stringBuilder.AppendLine($"        return this.id;");
            stringBuilder.AppendLine($"    }}");
            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }

        private string GetBinaryRead(string propertyType)
        {
            switch (propertyType)
            {
                case string s when (s.StartsWith("enum_")):
                    return "bReader.ReadInt32()";
                case string s when (s.StartsWith("array_")):
                    string array = s.Replace("array_", "");
                    if (array.EndsWith("2"))
                    {
                        return "new " + array.Replace("2", "[bReader.ReadByte()][]");
                    }
                    else
                    {
                        return "new " + array + "[bReader.ReadByte()]";
                    }
                case "bool":
                    return "bReader.ReadBoolean()";
                case "int":
                    return "bReader.ReadInt32()";
                case "uint":
                    return "bReader.ReadUInt32()";
                case "byte":
                    return "bReader.ReadByte()";
                case "short":
                    return "bReader.ReadInt16()";
                case "long":
                    return "bReader.ReadInt64()";
                case "ulong":
                    return "bReader.ReadUInt64()";
                case "float":
                    return "bReader.ReadSingle()";
                case "string":
                    return "bReader.ReadString()";
                default:
                    return "bReader.ReadInt32()";
            }
        }

        private string GetType(string propertyType)
        {
            switch (propertyType)
            {
                case string s when (s.StartsWith("enum_")):
                    return propertyType.Replace("enum_", "");
                case string s when (s.StartsWith("array_")):
                    string array = s.Replace("array_", "");
                    if (array.EndsWith("2"))
                    {
                        array = array.Replace("2", "[]");
                    }
                    return array + "[]";
                default:
                    return propertyType;
            }
        }
    }
}
