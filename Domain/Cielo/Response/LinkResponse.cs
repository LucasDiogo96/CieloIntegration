using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LinkResponse
    {
        public string Method { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
    }
}
