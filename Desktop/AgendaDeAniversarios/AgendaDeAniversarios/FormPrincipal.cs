using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgendaDeAniversarios
{
    public class FormPrincipal : Form
    {
        ListBox listaPessoas;
        TextBox caixaPesquisa;
        List<int> posicoesMostradas = new List<int>();

        public FormPrincipal()
        {
            Text = "Agenda de Aniversários";
            
            Size = new Size(600, 440);
            BackColor = Color.White;

            Label titulo = new Label();
            titulo.Text = "Aniversários";
            titulo.Location = new Point(25, 20);
            titulo.Size = new Size(300, 30);
            titulo.Font = new Font("Arial", 15, FontStyle.Bold);

            Label textoPesquisa = new Label();
            textoPesquisa.Text = "Pesquisar nome:";
            textoPesquisa.Location = new Point(25, 65);
            textoPesquisa.AutoSize = true;

            caixaPesquisa = new TextBox();
            caixaPesquisa.Location = new Point(25, 85);
            caixaPesquisa.Size = new Size(300, 25);
            caixaPesquisa.TextChanged += Pesquisar;

            listaPessoas = new ListBox();
            listaPessoas.Location = new Point(25, 125);
            listaPessoas.Size = new Size(530, 220);

            Button botaoAdicionar = new Button();
            botaoAdicionar.Text = "Adicionar";
            botaoAdicionar.Location = new Point(25, 360);
            botaoAdicionar.Size = new Size(100, 35);
            botaoAdicionar.BackColor = Color.LightBlue;
            botaoAdicionar.Click += Adicionar;

            Button botaoEditar = new Button();
            botaoEditar.Text = "Editar";
            botaoEditar.Location = new Point(140, 360);
            botaoEditar.Size = new Size(100, 35);
            botaoEditar.BackColor = Color.LightBlue;
            botaoEditar.Click += Editar;

            Button botaoEliminar = new Button();
            botaoEliminar.Text = "Eliminar";
            botaoEliminar.Location = new Point(255, 360);
            botaoEliminar.Size = new Size(100, 35);
            botaoEliminar.BackColor = Color.LightBlue;
            botaoEliminar.Click += Eliminar;

            Controls.Add(titulo);
            Controls.Add(textoPesquisa);
            Controls.Add(caixaPesquisa);
            Controls.Add(listaPessoas);
            Controls.Add(botaoAdicionar);
            Controls.Add(botaoEditar);
            Controls.Add(botaoEliminar);

            AtualizarLista();
        }

        private void AtualizarLista()
        {
            listaPessoas.Items.Clear();
            posicoesMostradas.Clear();

            for (int i = 0; i < Globais.listaAniversarios.Count; i++)
            {
                if (Globais.listaAniversarios[i].nome.ToLower().Contains(caixaPesquisa.Text.ToLower()))
                {
                    Globais.Aniversario pessoa = Globais.listaAniversarios[i];
                    int dias = DiasAteAniversario(pessoa.dataNascimento);
                    string textoDias;

                    if (dias == 0)
                    {
                        textoDias = "Faz anos hoje!";
                    }
                    else
                    {
                        textoDias = "Faltam " + dias + " dias";
                    }

                    string linha = pessoa.nome + " - " + pessoa.dataNascimento.ToString("dd/MM/yyyy") + " - " + textoDias;
                    listaPessoas.Items.Add(linha);
                    posicoesMostradas.Add(i);
                }
            }
        }

        private int DiasAteAniversario(DateTime data)
        {
            DateTime hoje = DateTime.Today;
            int dia = data.Day;

            if (dia > DateTime.DaysInMonth(hoje.Year, data.Month))
            {
                dia = DateTime.DaysInMonth(hoje.Year, data.Month);
            }

            DateTime proximaData = new DateTime(hoje.Year, data.Month, dia);

            if (proximaData < hoje)
            {
                int ano = hoje.Year + 1;
                dia = data.Day;
                if (dia > DateTime.DaysInMonth(ano, data.Month))
                {
                    dia = DateTime.DaysInMonth(ano, data.Month);
                }
                proximaData = new DateTime(ano, data.Month, dia);
            }

            return (proximaData - hoje).Days;
        }

        private void Pesquisar(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void Adicionar(object sender, EventArgs e)
        {
            FormAdicionar formulario = new FormAdicionar(-1);
            if (formulario.ShowDialog() == DialogResult.OK)
            {
                AtualizarLista();
            }
            formulario.Dispose();
        }

        private void Editar(object sender, EventArgs e)
        {
            if (listaPessoas.SelectedIndex == -1)
            {
                MessageBox.Show("Escolha um aniversário.");
                return;
            }

            int posicao = posicoesMostradas[listaPessoas.SelectedIndex];
            FormAdicionar formulario = new FormAdicionar(posicao);
            if (formulario.ShowDialog() == DialogResult.OK)
            {
                AtualizarLista();
            }
            formulario.Dispose();
        }

        private void Eliminar(object sender, EventArgs e)
        {
            if (listaPessoas.SelectedIndex == -1)
            {
                MessageBox.Show("Escolha um aniversário.");
                return;
            }

            int posicao = posicoesMostradas[listaPessoas.SelectedIndex];
            Globais.listaAniversarios.RemoveAt(posicao);
            AtualizarLista();
        }
    }
}
