using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using ProntuarioAppAPI.Infra.Extensions;
using ProntuarioAppAPI.Infra.Notificacoes;
using Microsoft.AspNetCore.Http;

namespace ProntuarioAppAPI.Infra.Services
{
    public class UploadService : Notificavel
    {
        public string SalvarImagemWeb(IFormFile imagem, string caminhoFisicoDiretorioArquivos, string diretorio, string formato)
        {
            if (imagem != null && imagem.Length > 0)
            {
                using (new StreamReader(imagem.OpenReadStream()))
                {
                    byte[] fileBytes;
                    string extensao;
                    using (var fileStream = imagem.OpenReadStream())
                    using (var ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        if (!fileBytes.IsValidImage())
                        {
                            AdicionarNotificacao("Formato da imagem não é permitido.");
                            return null;
                        }

                        ContentDispositionHeaderValue.Parse(imagem.ContentDisposition);
                        extensao = Path.GetExtension(imagem.FileName);
                    }

                    return SalvarImagem(fileBytes, caminhoFisicoDiretorioArquivos, diretorio, extensao, formato);
                }
            }
            return null;
        }

        public string SalvarImagem(byte[] imagemUpload, string caminhoFisicoDiretorioArquivos, string diretorio, string extensao, string formato)
        {
            try
            {
                extensao = string.IsNullOrEmpty(extensao) ? ".png" : extensao;

                var uploadFolder = $@"{caminhoFisicoDiretorioArquivos}\{diretorio}";
                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                var guid = Guid.NewGuid();

                var nomeDoArquivo = $"{guid}{extensao}";
                var fullFileName = Path.Combine(uploadFolder, nomeDoArquivo);
                    
                using (var contexto = new Imageflow.Bindings.JobContext())
                {
                    contexto.AddInputBytesPinned(0, imagemUpload);
                    contexto.AddOutputBuffer(1);
                    var response = contexto.ExecuteImageResizer4CommandString(0, 1, formato);

                    var data = response.DeserializeDynamic();
                    var outputStream = contexto.GetOutputBuffer(1);

                    byte[] buffer = new byte[16 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = outputStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }

                        File.WriteAllBytes(fullFileName, ms.ToArray());
                    }
                }

                return $@"{diretorio}/{nomeDoArquivo}";
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
