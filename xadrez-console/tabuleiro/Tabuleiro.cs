using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    internal class Tabuleiro
    {

        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public Peca[,] Pecas;

        public Tabuleiro(int linha, int colunas)
        {
            Linhas = linha;
            Colunas = colunas;
            Pecas = new Peca[Linhas, Colunas];
        }
    }
}
