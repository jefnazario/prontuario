using System.Collections.Generic;
using FluentValidator;

namespace ProntuarioAppAPI.Infra.Notificacoes
{
    public class Notificavel : Notifiable
    {
        public IReadOnlyCollection<Notification> Notificacoes => Notifications;

        public void AdicionarNotificacao(string mensagem)
        {
            AddNotification("", mensagem);
        }

        public void AdicionarNotificacoes(IReadOnlyCollection<Notification> notificacoes)
        {
            AddNotifications(notificacoes);
        }

        public bool PodeContinuar()
        {
            return IsValid();
        }
    }
}
