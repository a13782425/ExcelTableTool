using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCore
{
    public sealed class XlsDto
    {
        /// <summary>
        /// excel 文件路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// excel文件名
        /// </summary>
        public string FileName { get; set; }

        public List<TableDto> TableDtos { get; set; }

        public XlsDto()
        {
            TableDtos = new List<TableDto>();
        }
    }
}
