namespace TableTool
{
    public class PropertyDto
    {
        /// <summary>
        /// 转化前名字
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 转化后的名字
        /// </summary>
        public string TranName { get; set; }
        public int Index { get; set; }

        public string PropertyType { get; set; }

        public string EnumType { get; set; }

        public bool IsArray { get; set; }

        public string Des { get; set; }

        public bool IsEnum { get; set; }
    }
}
