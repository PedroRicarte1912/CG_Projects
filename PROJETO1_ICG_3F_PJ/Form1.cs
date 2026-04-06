
/*Colegio Técnico Antônio Teixeira Fernandes (Univap)
 * Curso Técnico em Informática - Data de Entrega: 10 / M04 / 2026
 * Autores do Projeto: Pedro Ricarte Gisler
 * Julia Carolina Maciel
 *
 * Turma: 3F
 * Atividade Proposta em aula
 * Observação: Projeto 1 ICG
 * 
 * 
 * ******************************************************************/
/*
 O projeto usa de conceitos de trigonometria e matemática para efetivar cálculos que levam a plotagem (aparecimento)
 de um decágono regular na tela (Forms 1) 
 Calcula a posição que as coordenadas x e y podem ocupar na "área " do decágono,de acordo com as coordenadas iniciais ( no caso (400,400));
 Usa o angulo de cada uniao de lados do decagono para determinar as proximas posições dos pontos (36 graus);
 Por intermedio da constatação da diferença entre a distancia e a distancia máxima na área delimitada define se ocorrera a plotagem ou mais;
 Realiza o processo com 100000 pontos (pode ser menos ou mais);
 @github cridadores: PedroRicarte1912
 @github criadores: juliacm08
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
namespace PROJETO1_ICG_3F_PJ
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        bool estado = false;
        /*
         Primitivas
         */
        /*
         Primitiva da cor
         */
        public Color Cor(int r, int g, int b)
        {
            Color cor = new Color();
            cor = Color.FromArgb(r, g, b);
            return cor;
        }
        /*
        Primitiva da caneta
        */
        public Pen Caneta(PaintEventArgs e, Color cor)
        {
            Pen caneta = new Pen(cor);
            return caneta;
        }
        /*
        Primitiva de pintar o ponto na tela
        */
        public void PintaPonto(PaintEventArgs e, Pen caneta, Color cor, int x, int y)
        {

            e.Graphics.DrawLine(caneta, x, y, x + 1, y);
        }
        /*
         Modulo função double 
         */
        public double Modulo(double a, double b)
        {
            return (a % b + b) % b;
        }

        /*
         Variações de x e y ( delta)
         */
        public double Deltax(int x,int CentroX)
        {
            double delta_px = 0;
            delta_px = (x - CentroX);
            return delta_px;
        }
        public double Deltay(int y,int CentroY)
        {
            double delta_py = 0;
            delta_py = (y - CentroY);
            return delta_py;
        }
        /*
         Função double que calcula o Angulo 1
         */
        public double Angulo1(double dy, double dx)
        {
            double A1 = Math.Atan2(dy, dx);
            return A1;
        }
        /*
         Função double que calcula o Angulo Relativo
         */
        public double AnguloRelativo(double dy, double dx, double scc)
        {
            double A = Angulo1(dy, dx);
            double AR = Modulo(A, scc) - (scc / 2.0);
            return AR;
        }
        /*
         Função double que caucula a distancia atual
        */
        public double Distancia(double dx, double dy)
        {
            double distancia = Math.Sqrt((dx * dx) + (dy * dy));
            return distancia;
        }
        /*
         Função double para calcular a Apotema
         */
        public double Apotema(int Raio, double scc)
        {
            double Apt = Raio * Math.Cos(scc / 2.0);
            return Apt;
        }

        /*
         Começo aplicação
         */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            estado = true;
            this.Invalidate();
        }
        /*
         Procedimento decagono (aplica os demais e plota quando chamado)
         */
        public void decagono(PaintEventArgs e)
        {
            Color corFinal = Cor(0, 0, 0); // padrão preto ( caso não escolha nada)

            if (comboBox1.SelectedItem != null)
            {
                string selecionado = comboBox1.SelectedItem.ToString();

                if (selecionado == "Vermelho") corFinal = Cor(255, 0, 0);
                else if (selecionado == "Azul") corFinal = Cor(0, 0, 255);
                else if (selecionado == "Verde") corFinal = Cor(0, 255, 0);
                else if (selecionado == "Amarelo") corFinal = Cor(255, 255, 0);
                else if (selecionado == "Ciano") corFinal = Cor(0, 255, 255);
                else if (selecionado == "Rosa") corFinal = Cor(255, 0, 255);
                else if (selecionado == "Branco") corFinal = Cor(255, 255, 255);
            }

            Pen caneta_aura = Caneta(e, corFinal);
            int tentativas = 100000;
            int centroX = 400;
            int centroY = 400;
            
            for (int i = 0; i < tentativas; i++)
            {
                /*
                 Define pontos aleatórios
                 */
                int px = rnd.Next(centroX - 150, centroX + 150);
                int py = rnd.Next(centroY - 150, centroY + 150);
                /*
                 Chamada Deltax e Deltay e Distancia e Ângulo
                 */
                double D_px = Deltax(px,centroX);
                double D_py = Deltay(py,centroY);
                double distancia = Distancia(D_px, D_py);
                /*
                  ângulo, pi, seccao
                 */
                double angulo = Angulo1(D_py,D_px);
                double pi = Math.PI;
                double seccao = (2 * pi) / 10.0 ;
                /*
                 Angulo Relativo(função), Apotema(função)  e Distância Máxima
                 */
                int R = 150;
                double D_max = Apotema(R,seccao) / Math.Cos(AnguloRelativo(D_py,D_px,seccao));
                /*
                 Verificação D e Dmax
                 */
                if ( distancia< D_max && estado==true)
                {
                    PintaPonto(e, caneta_aura, corFinal, px, py);
                }
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (estado == true)
            {
                decagono(e);
                estado = false;
            }
        }
    }
}
