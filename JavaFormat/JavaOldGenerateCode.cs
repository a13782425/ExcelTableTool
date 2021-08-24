using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace JavaFormat
{
    internal sealed class JavaOldGenerateCode : IGenerateCode
    {
        public string Name => "oldjava";

        public string Generate(string package, TableDto tableDto, ref string fileName)
        {
            string className = tableDto.TableSheetName;
            string packageName = "";
            if (!string.IsNullOrWhiteSpace(package))
            {
                packageName = "package " + package + ";";
            }
            StringBuilder codeSB = new StringBuilder();
            codeSB.AppendLine($"/**");
            codeSB.AppendLine($"* \t生成代码,禁止修改");
            codeSB.AppendLine($"*/");
            codeSB.AppendLine(packageName);
            codeSB.AppendLine();
            className = className.ToUpperFirst() + "Conf";
            codeSB.AppendLine($"public class {className} {{");
            StringBuilder fieldSB = new StringBuilder();
            StringBuilder funcSB = new StringBuilder();
            List<PropertyDto> tempList = tableDto.PropertyDic.Values.ToList();
            foreach (var propertyDto in tempList)
            {

                string typeName = GetTypeName(propertyDto.PropertyType);
                string fieldName = propertyDto.PropertyName;
                string funcFieldName = fieldName.ToUpperFirst();
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(fieldName);
                //for (int i = 0; i < fieldName.Length; i++)
                //{
                //    if (i == 0)
                //    {
                //        funcFieldName += fieldName[i].ToString().ToUpper();
                //    }
                //    else
                //    {
                //        funcFieldName += fieldName[i];
                //    }
                //}
                fieldSB.AppendLine($"	/**");
                fieldSB.AppendLine($"	 * {propertyDto.Des}");
                fieldSB.AppendLine($"	 */");
                fieldSB.AppendLine($"    private {typeName} {fieldName};");
                funcSB.AppendLine($"	/**");
                funcSB.AppendLine($"	 * {propertyDto.Des}");
                funcSB.AppendLine($"	 */");
                funcSB.AppendLine($"    public void set{funcFieldName}({typeName} {fieldName}) {{");
                funcSB.AppendLine($"        this.{fieldName} = {fieldName};");
                funcSB.AppendLine($"    }}");
                funcSB.AppendLine();
                funcSB.AppendLine($"	/**");
                funcSB.AppendLine($"	 * {propertyDto.Des}");
                funcSB.AppendLine($"	 */");
                if (typeName == "boolean")
                {
                    funcSB.AppendLine($"    public {typeName} is{funcFieldName}() {{");
                }
                else
                {
                    funcSB.AppendLine($"    public {typeName} get{funcFieldName}() {{");
                }
                funcSB.AppendLine($"        return this.{fieldName};");
                funcSB.AppendLine($"    }}");
                funcSB.AppendLine();
            }
            codeSB.Append(fieldSB.ToString());
            codeSB.AppendLine();
            codeSB.Append(funcSB.ToString());
            codeSB.AppendLine($"}}");
            fileName = className + ".java";
            return codeSB.ToString();
        }

        private string GetTypeName(string typeName)
        {
            string text = typeName;
            if (text.StartsWith("enum_"))
            {
                text = "enum";
            }
            switch (text)
            {
                case "bool":
                    return "boolean";
                case "int":
                case "byte":
                case "short":
                case "long":
                case "float":
                    return typeName;
                case "uint":
                    return "int";
                case "ulong":
                    return "long";
                case "string":
                    return "String";
                case "enum":
                    {
                        return "int";
                    }
                case "array_int":
                    return "int[]";
                case "array_uint":
                    return "int[]";
                case "array_byte":
                    return "byte[]";
                case "array_short":
                    return "short[]";
                case "array_long":
                    return "long[]";
                case "array_ulong":
                    return "long[]";
                case "array_float":
                    return "float";
                case "array_string":
                    return "String[]";
                default:
                    return "int";
            }
        }
    }
}
