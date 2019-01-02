using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTiewer.Unity
{
    [AttributeUsage(AttributeTargets.Property)]
    class YTParamAttribute : Attribute
    {
        public string Value { get; private set; }
        public YTParamAttribute(string value)
        {
            Value = value;
        }
    }
}
