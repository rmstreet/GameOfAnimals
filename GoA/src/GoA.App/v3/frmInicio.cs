
namespace GoA.App.v3
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualBasic;
    using GoA.App.v3.Domain;
    using System.Globalization;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public partial class frmInicio : Form
    {

        private Game _jogoAnimais;

        private Dictionary<string, string> dictionaryLanguage = new Dictionary<string, string>();

        public frmInicio()
        {
            InitializeComponent();
            dictionaryLanguage.Add("English EUA (Default)", "en-US");
            dictionaryLanguage.Add("Portuguese Brazil", "pt-BR");

            cboLanguage.DisplayMember = "Key";
            cboLanguage.ValueMember = "Value";
            cboLanguage.DataSource = (from l in dictionaryLanguage select new { Key = l.Key, Value = l.Value }).ToList();
            cboLanguage.SelectedIndex = 0;

            _jogoAnimais = Game.
                            NewGame().
                            Configure(
                                            (mensagem, titulo) => InformarSucesso(mensagem, titulo),
                                            (mensagem, titulo) => PerguntaInteracao(mensagem, titulo),
                                            (mensagem, titulo) => PerguntaSimOuNao(mensagem, titulo)
                                       );
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            _jogoAnimais.Start();
        }

       
        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = (ComboBox)sender;
            ChangeLanguage(combo.SelectedValue.ToString());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(combo.SelectedValue.ToString());
            if(_jogoAnimais != null)
                _jogoAnimais.Reset();
        }

        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(frmInicio));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));                  
            }
        }

        private static void InformarSucesso(string mensagem, string titulo)
        {
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK);
        }

        private static bool PerguntaSimOuNao(string mensagem, string titulo)
        {
            var resposta = MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo);

            if (resposta == DialogResult.Yes)
                return true;
            else
                return false;
        }

        private static string PerguntaInteracao(string mensagem, string titulo)
        {
            return Interaction.InputBox(mensagem, titulo);
        }

        
    }
}
