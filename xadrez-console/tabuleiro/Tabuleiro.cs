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
        private Peca[,] Pecas;

        public Tabuleiro(int linha, int colunas)
        {
            Linhas = linha;
            Colunas = colunas;
            Pecas = new Peca[Linhas, Colunas];
        }

        public Peca peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];


        }

        public Peca peca(Posicao pos)
        { 
        
        return Pecas[pos.Linha, pos.Coluna];
        
        
        
  
        }

        public void colocarPeca(Peca p, Posicao pos)
        {
            if (existePeca(pos)) {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            Pecas[pos.Linha, pos.Coluna] = p;
            p.posicao = pos;

        }

        public bool existePeca(Posicao pos) 
        { 
            validarPosicao(pos);
            return peca(pos) != null;
        
        
        }


        public bool posicaoValida(Posicao pos)
        {

            if (pos.Linha<0 || pos.Linha >= Linhas || pos.Coluna <0 || pos.Coluna>= Colunas)
            {

                return false;


            }
            return true;

        }

        public void validarPosicao(Posicao pos)
        {

            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posicao inválida!");
            }


        }
    }
}
