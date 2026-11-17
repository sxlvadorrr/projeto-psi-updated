using System;
using System.Collections.Generic;

namespace AgendaDeAniversarios
{
    // Variáveis usadas por mais do que um formulário.
    internal class Globais
    {
        public struct Utilizador
        {
            public string email;
            public string palavraPasse;
            public DateTime dataNascimento;
        }

        public struct Aniversario
        {
            public string nome;
            public DateTime dataNascimento;
        }

        // Vetor para guardar as contas.
        public static Utilizador[] listaUtilizadores = new Utilizador[50];
        public static int posicaoVazia = 0;

        // Lista para guardar os aniversários.
        public static List<Aniversario> listaAniversarios = new List<Aniversario>();
    }
}
