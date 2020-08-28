using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authentication;

namespace ZIpAPI {
    public class ExtraiArquivo {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
