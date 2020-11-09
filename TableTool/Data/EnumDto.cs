using System;
using System.Collections.Generic;
using System.Text;

namespace TableTool
{

    public class EnumDto
    {
        public EnumDto()
        {
            EnumInfoDic = new Dictionary<string, EnumInfoDto>();
        }
        public string ClassName { get; set; }
        public Dictionary<string, EnumInfoDto> EnumInfoDic { get; set; }
    }
    public class EnumInfoDto
    {
        public string Name { get; set; }
        public string Des { get; set; }
        public int Value { get; set; }
    }

}
