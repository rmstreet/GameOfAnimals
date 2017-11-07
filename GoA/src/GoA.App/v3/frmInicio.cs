
namespace GoA.App.v3
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualBasic;
    using GoA.App.v2.Domain;

    public partial class frmInicio : Form
    {

        private Jogo _jogoAnimais;

        public frmInicio()
        {
            InitializeComponent();
            _jogoAnimais = Jogo.
                            NovoJogo().
                            Configurar(
                                            (mensagem, titulo) => InformarSucesso(mensagem, titulo),
                                            (mensagem, titulo) => PerguntaInteracao(mensagem, titulo),
                                            (mensagem, titulo) => PerguntaSimOuNao(mensagem, titulo)
                                       );

        }
        
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            _jogoAnimais.Iniciar();
        }

        public static void InformarSucesso(string mensagem, string titulo)
        {
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK);
        }

        public static bool PerguntaSimOuNao(string mensagem, string titulo)
        {
            var resposta = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo);

            if (resposta == DialogResult.Yes)
                return true;
            else
                return false;
        }

        public static string PerguntaInteracao(string mensagem, string titulo)
        {
            return Interaction.InputBox(mensagem, titulo);
        }
    }    
}
