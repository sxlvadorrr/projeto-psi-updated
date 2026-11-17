using System;

namespace AgendaDeAniversarios
{
    internal class ValidacaoEmailException : Exception
    {
        public ValidacaoEmailException(string mensagem) : base(mensagem) { }
    }
}
