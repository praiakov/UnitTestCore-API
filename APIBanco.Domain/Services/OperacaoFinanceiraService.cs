using APIBanco.Domain.Entities;

namespace APIBanco.Domain.Services
{
    public class OperacaoFinanceiraService
    {
        public bool Transferencia(ContaCorrente contaOrigem, ContaCorrente contaDestino, decimal valor)
        {
            if (contaOrigem.PodeTransferir(valor))
            {
                contaOrigem.Debitar(valor);
                contaDestino.Creditar(valor);

                return true;
            }

            return false;
        }
    }
}
