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

        public byte[] Format(TableDto tableDto, ValueParse parse, ref string fileName)
        {
            fileName = fileName + ".bytes";
            MemoryStream stream = new MemoryStream();
            Binary = new BinaryWriter(stream);
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
                        stream.Close();
                        stream.Dispose();
                        throw new Exception($"{tableDto.ExcelFileName}中第{row.RowNum}行中，{propPair.Key}序列化失败，错误类型为:{propPair.Value.PropertyType}，请查看");
                    }
                }
            }
            byte[] array = stream.GetBuffer();
            Binary.Close();
            Binary.Dispose();
            Binary = null;
            stream.Close();
            stream.Dispose();
            byte[] array2 = new byte[array.Length + 1];
            Random random = new Random();
            array2[0] = (byte)random.Next(1, 255);
            for (int j = 0; j < array.Length; j++)
            {
                array2[j + 1] = (byte)(array[j] ^ array2[0]);
            }
            return array2;
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
