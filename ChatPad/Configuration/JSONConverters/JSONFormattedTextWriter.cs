using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPad.Configuration.JSONConverters
{
    internal class JSONFormattedTextWriter : JsonTextWriter
    {
        public JSONFormattedTextWriter(TextWriter writer) : base(writer)
        {
        }

        protected override void WriteIndent()
        {
            if (WriteState != WriteState.Array)
            {
                base.WriteIndent();
            }
            else
            {
                WriteIndentSpace();
            }
        }
    }
}
