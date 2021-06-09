using APIBanco.Domain.Entities;

namespace APIBanco.Tests.Domain.Factories
{
    public static class ContaCorrenteFactory
    {
        public static ContaCorrente GetContaOrigemValida()
        {
            return new ContaCorrente("Finn", "31984551540", 1234, 5, 98765, "finn@email.com", "1234-5678");
        }

        public static ContaCorrente ObterContaDestinoValida()
        {
            return new ContaCorrente("Arthur", "23322148360", 5678, 9, 43210, "arthur@email.com", "8765-4321");
        }
    }
}
