using tabuleiro;
using xadrez_console;
using xadrez;
using xadrez_console.xadrez;
internal class Program
{
    private static void Main(string[] args)
    {


        Tabuleiro tab = new Tabuleiro(8, 8);

        tab.colocarPeca(new Torre(tab, Cor.Preta),new Posicao(0,0));
        tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
        tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

        Tela.ImprimirTabuleiro(tab);

    }
}