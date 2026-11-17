using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgendaDeAniversarios
{
    public class FormLogin : Form
    {
        TextBox caixaEmail;
        TextBox caixaPasse;
        DateTimePicker dataNascimento;

        public FormLogin()
        {
            Text = "Agenda de Aniversários";
            
            Size = new Size(360, 350);
            BackColor = Color.White;

            Label titulo = new Label();
            titulo.Text = "Agenda de Aniversários";
            titulo.Location = new Point(25, 20);
            titulo.Size = new Size(300, 30);
            titulo.Font = new Font("Arial", 15, FontStyle.Bold);

            Label textoEmail = new Label();
            textoEmail.Text = "Email:";
            textoEmail.Location = new Point(25, 65);
            textoEmail.AutoSize = true;

            caixaEmail = new TextBox();
            caixaEmail.Location = new Point(25, 85);
            caixaEmail.Size = new Size(290, 25);

            Label textoPasse = new Label();
            textoPasse.Text = "Palavra-passe:";
            textoPasse.Location = new Point(25, 120);
            textoPasse.AutoSize = true;

            caixaPasse = new TextBox();
            caixaPasse.Location = new Point(25, 140);
            caixaPasse.Size = new Size(290, 25);
            caixaPasse.UseSystemPasswordChar = true;

            Label textoData = new Label();
            textoData.Text = "Data de nascimento (para registo):";
            textoData.Location = new Point(25, 175);
            textoData.AutoSize = true;

            dataNascimento = new DateTimePicker();
            dataNascimento.Location = new Point(25, 195);
            dataNascimento.Size = new Size(290, 25);
            dataNascimento.Format = DateTimePickerFormat.Short;
            dataNascimento.MaxDate = DateTime.Today;

            Button botaoEntrar = new Button();
            botaoEntrar.Text = "Iniciar sessão";
            botaoEntrar.Location = new Point(25, 250);
            botaoEntrar.Size = new Size(130, 35);
            botaoEntrar.BackColor = Color.LightBlue;
            botaoEntrar.Click += IniciarSessao;

            Button botaoRegistar = new Button();
            botaoRegistar.Text = "Criar conta";
            botaoRegistar.Location = new Point(185, 250);
            botaoRegistar.Size = new Size(130, 35);
            botaoRegistar.BackColor = Color.LightBlue;
            botaoRegistar.Click += RegistarConta;

            Controls.Add(titulo);
            Controls.Add(textoEmail);
            Controls.Add(caixaEmail);
            Controls.Add(textoPasse);
            Controls.Add(caixaPasse);
            Controls.Add(textoData);
            Controls.Add(dataNascimento);
            Controls.Add(botaoEntrar);
            Controls.Add(botaoRegistar);
        }

        private void IniciarSessao(object sender, EventArgs e)
        {
            for (int i = 0; i < Globais.posicaoVazia; i++)
            {
                if (Globais.listaUtilizadores[i].email == caixaEmail.Text &&
                    Globais.listaUtilizadores[i].palavraPasse == caixaPasse.Text)
                {
                    FormPrincipal formulario = new FormPrincipal();
                    formulario.FormClosed += FormPrincipal_Fechado;
                    Hide();
                    formulario.Show();
                    return;
                }
            }
            MessageBox.Show("Email ou palavra-passe incorretos.");
        }

        private void FormPrincipal_Fechado(object sender, FormClosedEventArgs e)
        {
            Close();
        }


        // Método para registar uma nova conta
        private void RegistarConta(object sender, EventArgs e)
        {
            try
            {
                if (caixaEmail.Text == "" || caixaPasse.Text == "")
                {
                    throw new ValidacaoCamposVaziosException("Preencha todos os campos.");
                }

                if (!caixaEmail.Text.Contains("@") || !caixaEmail.Text.Contains("."))
                {
                    throw new ValidacaoEmailException("O email tem de conter @ e .");
                }

                for (int i = 0; i < Globais.posicaoVazia; i++)
                {
                    if (Globais.listaUtilizadores[i].email == caixaEmail.Text)
                    {
                        throw new ValidacaoEmailException("Este email já está registado.");
                    }
                }

                // Verifica se o limite de contas foi atingido
                if (Globais.posicaoVazia == 50)
                {
                    throw new Exception("O limite de contas foi atingido.");
                }

                Globais.Utilizador novoUtilizador;
                novoUtilizador.email = caixaEmail.Text;
                novoUtilizador.palavraPasse = caixaPasse.Text;
                novoUtilizador.dataNascimento = dataNascimento.Value;

                Globais.listaUtilizadores[Globais.posicaoVazia] = novoUtilizador;
                Globais.posicaoVazia++;

                MessageBox.Show("Conta criada.");
            }
            catch (ValidacaoCamposVaziosException ex)
            {
                MessageBox.Show(ex.Message, "Campos vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ValidacaoEmailException ex)
            {
                MessageBox.Show(ex.Message, "Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

