
namespace GoA.App.v3.Domain
{
    using GoA.App.v3.Resources;
    using System;

    public class Game
    {
        private BaseNode _noInicial;
        private Config _config;
        private Game() { Init(); }

        private void Init()
        {
            var firstAnimalQuestion = new Question(MessageInfo.questionAnimal1);
            var firstAnimal = new Animal(MessageInfo.animal1);
            var secondAnimal = new Animal(MessageInfo.animal2);

            firstAnimalQuestion.WithPositiveQuestion(firstAnimal);
            firstAnimalQuestion.WithNegativeQuestion(secondAnimal);

            _noInicial = firstAnimalQuestion;
        }

        public Game Configure(Action<string, string> finding,
                               Func<string, string, string> interactiveQuestion,
                               Func<string, string, bool> yesOrNoQuestion)
        {
            _config = Config.Configure(finding, interactiveQuestion, yesOrNoQuestion);
            return this;
        }

        private void Play(BaseNode infoLast, BaseNode infoCurrent)
        {
            infoCurrent.
                When<Animal>(() => { ActionOfAnimal(infoLast, infoCurrent); }).
                When<Question>(() => { ActionOfQuestion(infoLast, infoCurrent); });
        }

        private void ActionOfAnimal(BaseNode infoLast, BaseNode infoCurrent)
        {
            bool answer = _config.WhenYesOrNoQuestion(String.Format(MessageInfo.questionYesOrNoAnimal, infoCurrent.Information), MessageInfo.titleConfirm);
            if (answer)
            {
                _config.WhenFinding(MessageInfo.messageWinner, MessageInfo.titleConfirm);
            }
            else
            {
                if (infoCurrent.NegativeQuestion != null)
                    Play(infoCurrent, infoCurrent.NegativeQuestion);
                else
                {
                    string nameNewAnimal = _config.WhenInteractiveQuestion(MessageInfo.questionAnimalName, MessageInfo.titleDesist);
                    string featureNewAnimal = _config.WhenInteractiveQuestion(string.Format(MessageInfo.completeAnimal, nameNewAnimal, infoCurrent.Information), MessageInfo.titleComplete);

                    if (!string.IsNullOrEmpty(nameNewAnimal) && !string.IsNullOrEmpty(featureNewAnimal))
                    {
                        var question = new Question(featureNewAnimal);
                        var animal = new Animal(nameNewAnimal);
                        question.WithPositiveQuestion(animal);

                        question.WithNegativeQuestion(infoCurrent);
                        if (infoCurrent.Equals(infoLast.PositiveQuestion))
                            infoLast.WithPositiveQuestion(question);
                        else if (infoCurrent.Equals(infoLast.NegativeQuestion))
                            infoLast.WithNegativeQuestion(question);
                    }
                }
            }
        }
        private void ActionOfQuestion(BaseNode infoLast, BaseNode infoCurrent)
        {
            bool answer = _config.WhenYesOrNoQuestion(String.Format(MessageInfo.questionYesOrNoAnimal, infoCurrent.Information), MessageInfo.titleConfirm);
            if (answer)
            {
                Play(infoCurrent, infoCurrent.PositiveQuestion);
            }
            else
            {
                Play(infoCurrent, infoCurrent.NegativeQuestion);
            }
        }
        public void Start()
        {
            if (_config == null) throw new Exception("Game without configuration.");

            Play(null, _noInicial);
        }

        public void Reset()
        {
            Init();
        }

        #region Factory
        public static Game NewGame()
        {
            return new Game();
        }
        #endregion

        #region Configuração do jogo
        private class Config
        {
            private Action<string, string> _findind;
            private Func<string, string, string> _interactiveQuestion;
            private Func<string, string, bool> _yesOrNoQuestion;

            internal void WhenFinding(string msg, string title)
            {
                _findind(msg, title);
            }

            internal string WhenInteractiveQuestion(string msg, string title)
            {
                return _interactiveQuestion(msg, title);
            }

            internal bool WhenYesOrNoQuestion(string msg, string title)
            {
                return _yesOrNoQuestion(msg, title);
            }

            #region Factory
            public static Config Configure(Action<string, string> finding,
                                           Func<string, string, string> interactiveQuestion,
                                           Func<string, string, bool> yesOrNoQuestion)
            {
                return new Config() { _findind = finding, _interactiveQuestion = interactiveQuestion, _yesOrNoQuestion = yesOrNoQuestion };
            }
            #endregion

        }
        #endregion       

    }
}
