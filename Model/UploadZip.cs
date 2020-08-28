using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ZIpAPI.Model
{
    public class UploadZip
    {
        public string nome { get; set; }
        public long tamanho { get; set; }

        public UploadZip(string nome, long tamanho)
        {
            this.nome = nome;
            this.tamanho = tamanho;
        }
    }
}
