using System;

namespace AgendaDeAniversarios
{
    internal class ValidacaoCamposVaziosException : Exception
    {
        public ValidacaoCamposVaziosException(string mensagem) : base(mensagem) { }
    }
}
