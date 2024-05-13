using System.ComponentModel;

namespace OnboardingApi.Domain.Enums
{
    public enum StatusEnum : ushort
    {
        [Description("Não Iniciado")]
        NaoIniciado = 0,
        [Description("Em Andamento")]
        EmAndamento = 1,
        [Description("Concluído")]
        Concluido = 2,
        [Description("Não Realizado")]
        NaoRealizado = 3
    }
}
