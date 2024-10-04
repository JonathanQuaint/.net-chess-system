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

                try
                {

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab);

                    Console.WriteLine();
                    Console.WriteLine("Turno: " + partida.Turno);
                    Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosition();
                    partida.ValidarPosicaoDeOrigem(origem);


                    bool[,] posicoesPossiveis = partida.Tab.peca(origem).movimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);


                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosition();
                    partida.validarPosicaoDeDestino(origem, destino);

                    partida.realizadaJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {

                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

           
        }

        catch (TabuleiroException e) 
        {
            Console.WriteLine(e.Message);
        }
    }
}