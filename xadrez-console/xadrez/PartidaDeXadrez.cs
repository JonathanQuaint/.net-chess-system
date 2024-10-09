using tabuleiro;
using System.Collections.Generic;
using System.Data;

namespace xadrez;

internal class PartidaDeXadrez
{
    public int Turno { get; private set; }

    public Cor JogadorAtual { get; private set; }

    public Tabuleiro Tab { get; private set; }

    public bool terminada { get; private set; }

    private HashSet<Peca> pecas;
    private HashSet<Peca> capturadas;

    public bool xeque { get; private set; }

    public Peca vuneralvelenPassant { get; private set; }



    public PartidaDeXadrez()
    {
        Tab = new Tabuleiro(8, 8);
        Turno = 1;
        JogadorAtual = Cor.Branca;
        terminada = false;
        vuneralvelenPassant = null;
        pecas = new HashSet<Peca>();
        capturadas = new HashSet<Peca>();
        xeque = false;
        colocarPecas();
    }

    public Peca executarMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tab.retirarPeca(origem);
        p.incrementarQteMovimentos();
        Peca pecaCapturada = Tab.retirarPeca(destino);
        Tab.colocarPeca(p, destino);
        if (pecaCapturada != null)
        {
            capturadas.Add(pecaCapturada);

        }

        //jogadaespecial roque pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, destino.Coluna + 1);
            Peca T = Tab.retirarPeca(origemT);
            T.incrementarQteMovimentos();
            Tab.colocarPeca(T, destinoT);
        }

        //jogadaespecial roque grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoT = new Posicao(origem.Linha, destino.Coluna - 1);
            Peca T = Tab.retirarPeca(origemT);
            T.incrementarQteMovimentos();
            Tab.colocarPeca(T, destinoT);
        }

        //jogadaespecial en passant
        if (p is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posP;
                if (p.cor == Cor.Branca)
                {

                    posP = new Posicao(destino.Linha + 1, destino.Coluna);


                }

                else
                {
                    posP = new Posicao(destino.Linha, destino.Coluna);

                }
                pecaCapturada = Tab.retirarPeca(posP);
                capturadas.Add(pecaCapturada);

            }

        }


        return pecaCapturada;
    }

    public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca p = Tab.retirarPeca(destino);
        p.decrementarQteMovimentos();
        if (pecaCapturada != null)
        {
            Tab.colocarPeca(pecaCapturada, destino);
            capturadas.Remove(pecaCapturada);

        }
        Tab.colocarPeca(p, origem);




        //jogadaespecial roque pequeno
        if (p is Rei && destino.Coluna == origem.Coluna + 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoT = new Posicao(origem.Linha, destino.Coluna + 1);
            Peca T = Tab.retirarPeca(destinoT);
            T.decrementarQteMovimentos();
            Tab.colocarPeca(T, origemT);
        }

        //jogadaespecial roque grande
        if (p is Rei && destino.Coluna == origem.Coluna - 2)
        {
            Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 4);
            Posicao destinoT = new Posicao(origem.Linha, destino.Coluna - 1);
            Peca T = Tab.retirarPeca(destinoT);
            T.decrementarQteMovimentos();
            Tab.colocarPeca(T, origemT);
        }

        //jogadaespecial en passant
        if (p is Peao)
        {
            if(origem.Coluna != destino.Coluna && pecaCapturada == vuneralvelenPassant)
            {
                Peca peao = Tab.retirarPeca(destino);
                Posicao posP;
                if (p.cor == Cor.Branca)
                {

                    posP = new Posicao(3, destino.Coluna);


                }

                else
                {
                    posP = new Posicao(4, destino.Coluna);

                }

                Tab.colocarPeca(peao, posP);
            }


        }

    }

    public void realizadaJogada(Posicao origem, Posicao destino)
    {
        Peca pecaCapturada = executarMovimento(origem, destino);

        if (estaEmXeque(JogadorAtual))
        {
            desfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }

        Peca p = Tab.peca(destino);

        //jogadaespecial promocao 

        if (p is Peao) 
        { 
        
            if ((p.cor == Cor.Branca && destino.Linha == 0) || (p.cor == Cor.Preta && destino.Linha == 7))
            {
                p = Tab.retirarPeca(destino);
                pecas.Remove(p);
                Peca dama = new Dama(Tab, p.cor);
                Tab.colocarPeca(dama, destino);
                pecas.Add(dama);
            }      
        
        
      
        
        }

        if (estaEmXeque(adversaria(JogadorAtual)))
        {
            xeque = true;
        }
        else
        {
            xeque = false;

        }

        if (testeXequeMate(adversaria(JogadorAtual)))
        {
            terminada = true;

        }

        else
        {
            Turno++;
            mudaJogador();
        }

       

        // #jogadaespecial en passant 

        if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
        {
            vuneralvelenPassant = p;

        }
        else
        {
            vuneralvelenPassant = null;
        }
    }

    public void ValidarPosicaoDeOrigem(Posicao pos)
    {
        if (Tab.peca(pos) == null)
        {
            throw new TabuleiroException("Não existe peça na posição de origem escolhida");
        }
        if (JogadorAtual != Tab.peca(pos).cor)
        {
            throw new TabuleiroException("A peça de origem escolhida não é sua!");

        }
        if (!Tab.peca(pos).existeMovimentosPossiveis())
        {

            throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");

        }

    }

    public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (!Tab.peca(origem).movimentoPossivel(destino))
        {
            throw new TabuleiroException("Posição de destino inválida!");
        }



    }

    private void mudaJogador()
    {
        if (JogadorAtual == Cor.Branca)
        {
            JogadorAtual = Cor.Preta;

        }
        else
        {
            JogadorAtual = Cor.Branca;


        }


    }


    public HashSet<Peca> pecasCapturadas(Cor cor)
    {

        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca x in capturadas)
        {

            if (x.cor == cor)
            {

                aux.Add(x);

            }


        }
        return aux;



    }


    private Cor adversaria(Cor cor)
    {
        if (cor == Cor.Branca)
        {
            return Cor.Preta;

        }

        else
        {
            return Cor.Branca;

        }
    }

    private Peca rei(Cor cor)
    {
        foreach (Peca x in pecasEmJogo(cor))
        {
            if (x is Rei)
            {
                return x;
            }
        }

        return null;
    }

    public bool estaEmXeque(Cor cor)
    {
        Peca R = rei(cor);
        if (R == null)
        {
            throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");

        }

        foreach (Peca x in pecasEmJogo(adversaria(cor)))
        {
            bool[,] mat = x.movimentosPossiveis();
            if (mat[R.posicao.Linha, R.posicao.Coluna])
            {

                return true;

            }


        }
        return false;
    }

    public bool testeXequeMate(Cor cor)
    {
        if (!estaEmXeque(cor))
        {

            return false;
        }
        foreach (Peca x in pecasEmJogo(cor))
        {
            bool[,] mat = x.movimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        Posicao origem = x.posicao;
                        Posicao destino = new Posicao(i, j);
                        Peca pecaCapturada = executarMovimento(origem, new Posicao(i, j));
                        bool testeXeque = estaEmXeque(cor);
                        desfazMovimento(origem, destino, pecaCapturada);
                        if (!testeXeque)
                        {
                            return false;

                        }
                    }


                }


            }

        }
        return true;

    }

    public HashSet<Peca> pecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca x in pecas)
        {
            if (x.cor == cor)
            {

                aux.Add(x);

            }


        }

        aux.ExceptWith(pecasCapturadas(cor));
        return aux;
    }

    public void colocarNovaPeca(char coluna, int linha, Peca peca)
    {
        Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosition());
        pecas.Add(peca);

    }

    private void colocarPecas()
    {
        colocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
        colocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
        colocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
        colocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
        colocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
        colocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
        colocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
        colocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
        colocarNovaPeca('a', 2, new Peao(Tab, Cor.Branca, this));
        colocarNovaPeca('b', 2, new Peao(Tab, Cor.Branca, this));
        colocarNovaPeca('c', 2, new Peao(Tab, Cor.Branca, this));
        colocarNovaPeca('d', 2, new Peao(Tab, Cor.Branca, this));
        colocarNovaPeca('e', 2, new Peao(Tab, Cor.Branca, this));
        colocarNovaPeca('f', 2, new Peao(Tab, Cor.Branca, this));
        colocarNovaPeca('g', 2, new Peao(Tab, Cor.Branca, this));
        colocarNovaPeca('h', 2, new Peao(Tab, Cor.Branca, this));
        colocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
        colocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
        colocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
        colocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
        colocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
        colocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
        colocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
        colocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
        colocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta, this));
        colocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta, this));
        colocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta, this));
        colocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta, this));
        colocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta, this));
        colocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta, this));
        colocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta, this));
        colocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta, this));
    }
}
