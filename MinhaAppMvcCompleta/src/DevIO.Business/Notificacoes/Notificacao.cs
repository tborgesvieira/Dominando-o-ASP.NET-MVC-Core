using DevIO.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIO.Business.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; }
    }

    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacacoes;

        public Notificador()
        {
            _notificacacoes = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacacoes.Any();
        }
    }
}
