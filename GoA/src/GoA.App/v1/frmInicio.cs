
namespace GoA.App.v1
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualBasic;
    using GoA.App.v1.Domain;

    public partial class frmInicio : Form
    {

        private Nobase _noInicial;

        public frmInicio()
        {
            InitializeComponent();
            Iniciarlizar();
        }


        private void Iniciarlizar()
        {
            var perguntaTubarao = new Pergunta("vive na água");
            var tubarao = new Animal("Tubarão");
            var macaco = new Animal("Macaco");

            perguntaTubarao.AtribuirRespostaPositiva(tubarao);
            perguntaTubarao.AtribuirRespostaNegativa(macaco);

            _noInicial = perguntaTubarao;
        }       

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            Jogar(null, _noInicial);
        }

        public void Jogar(Nobase infoLast, Nobase infoCurrent)
        {
            if (infoCurrent.GetType() == typeof(Animal))
            {
                var resposta = MessageBox.Show(String.Format("O animal que você pensou é {0}?", infoCurrent.Informacao), "Confirm", MessageBoxButtons.YesNo);
                if (resposta == DialogResult.Yes)
                {
                    MessageBox.Show("Acertei de novo!", "Confirm", MessageBoxButtons.OK);
                }
                else
                {
                    if (infoCurrent.RespostaNegativa != null)
                        Jogar(infoCurrent, infoCurrent.RespostaNegativa);
                    else
                    {
                        string nomeAnimalNovo = Interaction.InputBox("Qual animal você pensou?", "Desisto");
                        string caracteristicaAnimalNovo = Interaction.InputBox(string.Format("Um(a) {0} __________ mas um(a) {1} não.", nomeAnimalNovo, infoCurrent.Informacao), "Complete");

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
                var resposta = MessageBox.Show(String.Format("O animal que você pensou {0}?", infoCurrent.Informacao), "Confirm", MessageBoxButtons.YesNo);
                if(resposta == DialogResult.Yes)
                {
                    Jogar(infoCurrent, infoCurrent.RespostaPositiva);
                }
                else
                {
                    Jogar(infoCurrent, infoCurrent.RespostaNegativa);
                }
            }

        }
            
    }    
}
