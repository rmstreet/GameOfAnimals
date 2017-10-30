
namespace GoA.App.v2.Domain
{
    using System;

    public class Jogo
    {
        private Nobase _noInicial;
        private Config _config;
        private Jogo() { Init(); }

        private void Init()
        {
            var perguntaTubarao = new Pergunta("vive na água");
            var tubarao = new Animal("Tubarão");
            var macaco = new Animal("Macaco");

            perguntaTubarao.AtribuirRespostaPositiva(tubarao);
            perguntaTubarao.AtribuirRespostaNegativa(macaco);

            _noInicial = perguntaTubarao;
        }

        public Jogo Configurar(Action<string, string> descobri,
                               Func<string, string, string> perguntaInterativa,
                               Func<string, string, bool> perguntaSimOuNao)
        {
            _config = Config.Configure(descobri, perguntaInterativa, perguntaSimOuNao);
            return this;
        }

        private void Jogar(Nobase infoLast, Nobase infoCurrent)
        {
            if (infoCurrent.GetType() == typeof(Animal))
            {
                bool resposta = _config.PerguntaSimOuNao(String.Format("O animal que você pensou é {0}?", infoCurrent.Informacao), "Confirm");
                if (resposta)
                {
                    _config.Descobri("Acertei de novo!", "Confirm");
                }
                else
                {
                    if (infoCurrent.RespostaNegativa != null)
                        Jogar(infoCurrent, infoCurrent.RespostaNegativa);
                    else
                    {
                        string nomeAnimalNovo = _config.PerguntaInterativa("Qual animal você pensou?", "Desisto");
                        string caracteristicaAnimalNovo = _config.PerguntaInterativa(string.Format("Um(a) {0} __________ mas um(a) {1} não.", nomeAnimalNovo, infoCurrent.Informacao), "Complete");

                        if (!string.IsNullOrEmpty(nomeAnimalNovo) && !string.IsNullOrEmpty(caracteristicaAnimalNovo))
                        {
                            var pergunta = new Pergunta(caracteristicaAnimalNovo);
                            var animal = new Animal(nomeAnimalNovo);
                            pergunta.AtribuirRespostaPositiva(animal);

                            pergunta.AtribuirRespostaNegativa(infoCurrent);
                            if (infoCurrent.Equals(infoLast.RespostaPositiva))
                                infoLast.AtribuirRespostaPositiva(pergunta);
                            else if (infoCurrent.Equals(infoLast.RespostaNegativa))
                                infoLast.AtribuirRespostaNegativa(pergunta);
                        }
                    }
                }
            }
            else if (infoCurrent.GetType() == typeof(Pergunta))
            {
                bool resposta = _config.PerguntaSimOuNao(String.Format("O animal que você pensou {0}?", infoCurrent.Informacao), "Confirm");
                if (resposta)
                {
                    Jogar(infoCurrent, infoCurrent.RespostaPositiva);
                }
                else
                {
                    Jogar(infoCurrent, infoCurrent.RespostaNegativa);
                }
            }

        }

        public void Iniciar()
        {
            Jogar(null, _noInicial);
        }

        #region Factory
        public static Jogo NovoJogo()
        {
            return new Jogo();
        }
        #endregion

        #region Configuração do jogo
        private class Config
        {
            private Action<string, string> _descobri;
            private Func<string, string, string> _perguntaInterativa;
            private Func<string, string, bool> _perguntaSimOuNao;

            internal void Descobri(string msg, string titulo)
            {
                _descobri(msg, titulo);
            }

            internal string PerguntaInterativa(string msg, string titulo)
            {
                return _perguntaInterativa(msg, titulo);
            }

            internal bool PerguntaSimOuNao(string msg, string titulo)
            {
                return _perguntaSimOuNao(msg, titulo);
            }

            #region Factory
            public static Config Configure(Action<string, string> descobri,
                                           Func<string, string, string> perguntaInterativa,
                                           Func<string, string, bool> perguntaSimOuNao)
            {
                return new Config() { _descobri = descobri, _perguntaInterativa = perguntaInterativa, _perguntaSimOuNao = perguntaSimOuNao };
            }
            #endregion

        }
        #endregion       

    }
}
