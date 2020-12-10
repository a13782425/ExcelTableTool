using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static TableTool.GlobalConst;

namespace TableTool.GenerateCode
{
    public sealed class JavaJsonGenerateCode : JavaGenerateCode
    {
        public override void GenerateCode()
        {
            string package = "";
            if (!string.IsNullOrWhiteSpace(Params[CONSOLE_NAMESPACE]))
            {
                package = Params[CONSOLE_NAMESPACE];
            }
            foreach (var xls in Generate.XlsDtoList)
            {
                foreach (var item in xls.TableDtos)
                {
                    string className = item.TableSheetName;
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

                    className = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(className) + "Conf";
                    codeSB.AppendLine($"public class {className} {{");
                    StringBuilder fieldSB = new StringBuilder();
                    StringBuilder funcSB = new StringBuilder();

                    foreach (var propertyDto in item.PropertyDtoList)
                    {
                        string typeName = GetTypeName(propertyDto.PropertyType);
                        string fieldName = Params[CONSOLE_HUMP] == null ? propertyDto.PropertyName : propertyDto.TranName;
                        string funcFieldName = fieldName.ToUpperFirst();// System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(fieldName);
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

                    File.WriteAllText(Path.Combine(Params[CONSOLE_CODE_PATH], className + ".java"), codeSB.ToString(), new UTF8Encoding());
                    Console.WriteLine($"生成Java代码:{item.TableSheetName}成功");
                }
            }

        }


    }
}
