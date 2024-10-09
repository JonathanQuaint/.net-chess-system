using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    internal class Peao : Peca
    {
        private PartidaDeXadrez partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;

        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                pos.definirValores(posicao.Linha - 1, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;

                }

                pos.definirValores(posicao.Linha - 2, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {

                    mat[pos.Linha, pos.Coluna] = true;

                }


                pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (tab.posicaoValida(pos) && ExisteInimigo(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;

                }


                pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (tab.posicaoValida(pos) && ExisteInimigo(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;

                }

                // #jogadaespecial en passant

                if (posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna -  1);
                    if (tab.posicaoValida(esquerda) && ExisteInimigo(esquerda) && tab.peca(esquerda) == partida.vuneralvelenPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tab.posicaoValida(direita) && ExisteInimigo(direita) && tab.peca(direita) == partida.vuneralvelenPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }

                }

            }

            else
            {
                pos.definirValores(posicao.Linha + 1, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;

                }

                pos.definirValores(posicao.Linha + 2, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {

                    mat[pos.Linha, pos.Coluna] = true;

                }


                pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (tab.posicaoValida(pos) && ExisteInimigo(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;

                }


                pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (tab.posicaoValida(pos) && ExisteInimigo(pos))
                {

                    mat[pos.Linha, pos.Coluna] = true;



                }


                // #jogadaespecial en passant

                if (posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tab.posicaoValida(esquerda) && ExisteInimigo(esquerda) && tab.peca(esquerda) == partida.vuneralvelenPassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tab.posicaoValida(direita) && ExisteInimigo(direita) && tab.peca(direita) == partida.vuneralvelenPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }

                }

            }

            return mat;
        }
    }
}

