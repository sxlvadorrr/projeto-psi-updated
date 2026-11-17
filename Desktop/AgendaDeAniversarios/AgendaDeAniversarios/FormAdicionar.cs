using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgendaDeAniversarios
{
    public class FormAdicionar : Form
    {
        TextBox caixaNome;
        DateTimePicker dataNascimento;
        int posicao;

        public FormAdicionar(int numero)
        {
            posicao = numero;
            Text = "Adicionar aniversário";
            
            Size = new Size(370, 280);
            BackColor = Color.White;

            Label textoNome = new Label();
            textoNome.Text = "Nome:";
            textoNome.Location = new Point(25, 25);
            textoNome.AutoSize = true;

            caixaNome = new TextBox();
            caixaNome.Location = new Point(25, 50);
            caixaNome.Size = new Size(300, 25);

            Label textoData = new Label();
            textoData.Text = "Data de nascimento:";
            textoData.Location = new Point(25, 90);
            textoData.AutoSize = true;

            dataNascimento = new DateTimePicker();
            dataNascimento.Location = new Point(25, 115);
            dataNascimento.Size = new Size(300, 25);
            dataNascimento.Format = DateTimePickerFormat.Short;
            dataNascimento.MaxDate = DateTime.Today;

            Button botaoGuardar = new Button();
            botaoGuardar.Text = "Guardar";
            botaoGuardar.Location = new Point(75, 175);
            botaoGuardar.Size = new Size(100, 35);
            botaoGuardar.BackColor = Color.LightBlue;
            botaoGuardar.Click += Guardar;

            Button botaoCancelar = new Button();
            botaoCancelar.Text = "Cancelar";
            botaoCancelar.Location = new Point(190, 175);
            botaoCancelar.Size = new Size(100, 35);
            botaoCancelar.Click += Cancelar;

            Controls.Add(textoNome);
            Controls.Add(caixaNome);
            Controls.Add(textoData);
            Controls.Add(dataNascimento);
            Controls.Add(botaoGuardar);
            Controls.Add(botaoCancelar);

            if (posicao >= 0)
            {
                Globais.Aniversario pessoa = Globais.listaAniversarios[posicao];
                caixaNome.Text = pessoa.nome;
                dataNascimento.Value = pessoa.dataNascimento;
                Text = "Editar aniversário";
            }
        }

        private void Guardar(object sender, EventArgs e)
        {
            if (caixaNome.Text == "")
            {
                MessageBox.Show("Escreva o nome.");
                return;
            }

            Globais.Aniversario pessoa;
            pessoa.nome = caixaNome.Text;
            pessoa.dataNascimento = dataNascimento.Value;

            if (posicao == -1)
            {
                Globais.listaAniversarios.Add(pessoa);
            }
            else
            {
                Globais.listaAniversarios[posicao] = pessoa;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancelar(object sender, EventArgs e)
        {
            Close();
        }
    }
}
