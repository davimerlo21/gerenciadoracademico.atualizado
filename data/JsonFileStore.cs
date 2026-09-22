using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace GerenciadorAcademico.Data
{
    public class JsonFileStore
    {
        private readonly string _basePath;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly JsonSerializerOptions _options;

        public JsonFileStore(IHostEnvironment environment)
        {
            _basePath = Path.Combine(environment.ContentRootPath, "Data");
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<T> LerAsync<T>(string nomeArquivo) where T : new()
        {
            var caminhoCompleto = Path.Combine(_basePath, nomeArquivo);
            if (!File.Exists(caminhoCompleto)) return new T();

            string conteudo = await File.ReadAllTextAsync(caminhoCompleto);
            if (string.IsNullOrWhiteSpace(conteudo)) return new T();

            return JsonSerializer.Deserialize<T>(conteudo, _options) ?? new T();
        }

        public async Task SalvarSeguroAsync<T>(string nomeArquivo, T dados)
        {
            var caminhoCompleto = Path.Combine(_basePath, nomeArquivo);
            var caminhoTemporario = caminhoCompleto + ".tmp";

            await _semaphore.WaitAsync();
            try
            {
                string jsonString = JsonSerializer.Serialize(dados, _options);
                await File.WriteAllTextAsync(caminhoTemporario, jsonString);

                if (File.Exists(caminhoCompleto))
                {
                    File.Replace(caminhoTemporario, caminhoCompleto, null);
                }
                else
                {
                    File.Move(caminhoTemporario, caminhoCompleto);
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
