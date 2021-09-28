using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace DemoCsharp
{
    public sealed class CSharpGenerateFormat : IFormat
    {
        public string Name => "csharp-byte";

        public static BinaryWriter Binary { get; private set; }

        public string Format(TableDto tableDto, ValueParse parse, ref string fileName)
        {
            string filePath = Path.Combine(ToolParams.Params["outpath"], fileName);
            fileName = "";
            filePath += ".bytes";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            Binary = new BinaryWriter(fileStream);
            foreach (var row in tableDto.Rows)
            {
                foreach (KeyValuePair<string, PropertyDto> propPair in tableDto.PropertyDic)
                {
                    string value = row[propPair.Key];

                    if (!parse.Parse(propPair.Value.PropertyType, value, out object o))
                    {
                        Binary.Close();
                        Binary.Dispose();
                        Binary = null;
                        fileStream.Close();
                        fileStream.Dispose();
                        throw new Exception($"{tableDto.ExcelFileName}中第{row.RowNum}行中，{propPair.Key}序列化失败，错误类型为:{propPair.Value.PropertyType}，请查看");
                    }
                }
            }
            Binary.Close();
            Binary.Dispose();
            fileStream.Close();
            fileStream.Dispose();
            byte[] array = File.ReadAllBytes(filePath);
            byte[] array2 = new byte[array.Length + 1];
            Random random = new Random();
            array2[0] = (byte)random.Next(1, 255);
            for (int j = 0; j < array.Length; j++)
            {
                array2[j + 1] = (byte)(array[j] ^ array2[0]);
            }
            File.WriteAllBytes(filePath, array2);
            return "";
        }


        public List<IParseValue> GetCustomParse()
        {
            return new List<IParseValue>()
            {
                new IntParse(),
                new StringParse(),
                new GameAttrEnumParse(),
            };
        }
    }
}
