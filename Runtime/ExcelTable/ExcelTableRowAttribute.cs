using System;

namespace Pangoo
{
    public class ExcelTableColAttribute : Attribute
    {
        public string Name {get;}

        public string Head { get; }
        public string ColType {get;}
        
        public string NameCn {get;}

        public int Index {get;}
        
        public ExcelTableColAttribute(string name, string head, string col_type,string name_cn, int index)
        {
            Name =name;
            Head = head;
            ColType = col_type;
            NameCn = name_cn;
            Index = index;
        }
    }
}