namespace TableTool
{
    public class PropertyDto
    {
        /// <summary>
        /// ת��ǰ����
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// ת���������
        /// </summary>
        public string TranName { get; set; }
        public int Index { get; set; }
        /// <summary>
        /// Lua����
        /// </summary>
        public int LuaIndex { get; set; }
        public string PropertyType { get; set; }

        public string EnumType { get; set; }

        public bool IsArray { get; set; }

        public string Des { get; set; }

        public bool IsEnum { get; set; }
    }
}
