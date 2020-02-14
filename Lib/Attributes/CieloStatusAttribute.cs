using System;
using System.ComponentModel;

namespace Lib.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class CieloStatusAttribute : DescriptionAttribute
    {
        public CieloStatusAttribute(string status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}
