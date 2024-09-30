using System.Security.Cryptography.X509Certificates;
using tabuleiro;

namespace xadrez_console.xadrez
{
    internal class Rei : Peca
    {

        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

          
        }

        public override string ToString()
        {
            return "R";
        }


    }


}

