using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZIpAPI.Model;

namespace ZIpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadZipController : ControllerBase
    {
        [HttpPost]
        [RequestSizeLimit(737280000)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [Consumes("multipart/form-data")]
        public ActionResult<List<UploadZip>> ExtrairZip(IFormFile arquivo)
        {
            string caminhoArquivo = @".\Arquivos\";
            string caminhoExtraido = @".\Extraídos";

            if (Directory.Exists(caminhoArquivo))
            {
                DirectoryInfo diEnviados = new DirectoryInfo(caminhoArquivo);

                foreach (FileInfo file in diEnviados.GetFiles())
                {
                    file.Delete();
                }
            }

            if (arquivo == null)
            {
                return BadRequest("Nenhum arquivo foi passado.");
            }

            var nomeArquivo = arquivo.FileName;
            string extensao = Path.GetExtension(nomeArquivo);
 
            if (!Directory.Exists(caminhoArquivo))
            {
                Directory.CreateDirectory(caminhoArquivo);
            }

            using (var stream = System.IO.File.Create(caminhoArquivo + nomeArquivo))
            {
                arquivo.CopyTo(stream);
            }

            if (!Directory.Exists(caminhoExtraido))
            {
                Directory.CreateDirectory(caminhoExtraido);
            }

            DirectoryInfo diExtraidos = new DirectoryInfo(caminhoExtraido);

            foreach (FileInfo file in diExtraidos.GetFiles())
            {
                file.Delete();
            }

            if (extensao != ".zip" && extensao != ".ZIP")
            {
                return BadRequest("A extensão " + extensao + " não é suportada.");
            }

            ZipFile.ExtractToDirectory(caminhoArquivo + nomeArquivo, caminhoExtraido);

            List<UploadZip> arquivosExtraidos = new List<UploadZip>();

            FileInfo[] files = diExtraidos.GetFiles();

            foreach (FileInfo file in files)
            {
                UploadZip uZip = new UploadZip(file.Name, file.Length);
                arquivosExtraidos.Add(uZip);
            }

            return Ok(arquivosExtraidos);
        }
    }
}
