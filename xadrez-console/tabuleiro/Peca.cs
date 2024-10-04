using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    abstract class Peca
    {

        public Posicao posicao { get; set; }
        public Cor cor {  get; protected set; }   
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab {  get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;
            this.cor = cor;
            this.tab = tab;
            qteMovimentos = 0;
        }

        public void incrementarQteMovimentos() 
        { 
            
            qteMovimentos++; 
        

        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j<tab.Colunas; j++)
                {
                    if (mat[i,j] == true)
                    {
                        return true;

                    } 

                }

            }
            return false;
        }

        public bool PodeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.Linha, pos.Coluna];

        }

        public abstract bool[,] movimentosPossiveis();

    }
}
