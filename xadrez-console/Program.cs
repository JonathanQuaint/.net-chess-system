using tabuleiro;
using xadrez_console;
using xadrez;

internal class Program
{
    private static void Main(string[] args)
    {

        try
        {
           PartidaDeXadrez partida = new PartidaDeXadrez();

            while (!partida.terminada)
            {

                Console.Clear();
                Tela.ImprimirTabuleiro(partida.tab);

                Console.WriteLine("Origem: ");
                Posicao origem = Tela.lerPosicaoXadrez().toPosition();
                Console.WriteLine("Destino: ");
                Posicao destino = Tela.lerPosicaoXadrez().toPosition();

                partida.executarMovimento(origem, destino);
            }

           
        }

        catch (TabuleiroException e) 
        {
            Console.WriteLine(e.Message);
        }
    }
}