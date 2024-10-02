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

                Console.WriteLine();
                Console.Write("Origem: ");
                Posicao origem = Tela.lerPosicaoXadrez().toPosition();


                bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                
                Console.Clear();
                Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);


                Console.Write("Destino: ");
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