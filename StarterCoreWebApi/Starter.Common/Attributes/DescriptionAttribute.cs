using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Common.Attributes
{
    public class DescriptionAttribute : Attribute
    {
        public string DescriptName { get; set; }
        public DescriptionAttribute(string descriptName)
        {
            this.DescriptName = descriptName;
        }
    }
}
