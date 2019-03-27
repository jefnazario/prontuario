using ProntuarioAppAPI.Infra.Notificacoes;

namespace ProntuarioAppAPI.Infra.Extensions
{
    public static class ValidacaoExtensions
    {
        public static bool Obrigatorio(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}