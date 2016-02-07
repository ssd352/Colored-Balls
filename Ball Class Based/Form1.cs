using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ball_Class_Based
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics gr;
        double av;
        ball[] ba = new ball[nb];
        void init()
        {
            Random rnd = new Random();
            bool flag;
            int k,i;
            for (i = 0; i < nb; i++)
            {
                ba[i] = new ball();
                ba[i].FormHeight = this.Height;
                ba[i].FormWidth = this.Width;
                ba[i].Bkcolor = this.BackColor;
                ba[i].graph = gr;
                ba[i].r = rnd.Next(10) + 10;
                do
                {
                    flag = false;
                    ba[i].x = rnd.NextDouble() * (this.Width - 2 * ba[i].r - 15) + ba[i].r;
                    ba[i].y = rnd.NextDouble() * (this.Height - 80 - 2 * ba[i].r) + ba[i].r;
                    k = 0;
                    while (k < i)
                    {
                        if (ba[i].x == ba[k].x && ba[i].y == ba[k].y)
                            flag = true;
                        k++;
                    }
                } while (flag);
                ba[i].vx = rnd.NextDouble() * 4000 - 2000;
                ba[i].vy = rnd.NextDouble() * 4000 - 2000;
                av += Math.Sqrt(ba[i].vx * ba[i].vx + ba[i].vy * ba[i].vy);
                do
                {
                    ba[i].color = Color.FromArgb(255, rnd.Next(256), rnd.Next(256), rnd.Next(256));
                } while (ba[i].color == this.BackColor);
            }
            av /= nb;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            gr = this.CreateGraphics();
            init();
        }
        public class ball
        {
            public double x, vx;
            public double y, vy;
            public Color color;
            public int r;
            public Graphics graph
            {
                set
                {
                    g = value;
                }
            }
            public int FormWidth
            {
                set
                {
                    Width = value;
                }
            }
            public int FormHeight
            {
                set
                {
                    Height= value;
                }
            }
            public Color Bkcolor
            {
                set
                {
                    col = value;
                }
            }
            private Color col; private int Width, Height; private Graphics g;
            public void move()
            {
                double dt = Math.Sqrt(2.0) / 200;
                    Pen p = new Pen(col);
                    SolidBrush b = new SolidBrush(col);
                    g.FillEllipse(b, (int)x, (int)y, r, r);
                    //g.Clear(this.BackColor);
                    p = new Pen(color);
                    b = new SolidBrush(color);
                    if (x + vx * dt >= Width - 15 - r || x + vx * dt <= 0)
                    {
                        vx *= -1;
                    }
                    else
                    {
                        x += vx * dt;
                    }
                    if (y + vy * dt >= Height - 80 -r || y + vy * dt <= 0)
                    {
                        vy *= -1;
                        //  ba[i].x +=  ba[i].vx * dt;
                    }
                    else
                    {
                        y += vy * dt;
                    }
                    g.FillEllipse(b, (int)x, (int)y, r, r);
                }
            
        }
        static int nb = 100;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int cnt;
            for (cnt = 0; cnt < nb; cnt++)
            {
                ba[cnt].move();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int cnt;
            for (cnt = 0; cnt < nb; cnt++)
            {
                ba[cnt].move();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            double d = 1000 * Math.Sqrt(2.0);
            MessageBox.Show(av.ToString() + " " + d.ToString());
            
        }
    }
}
