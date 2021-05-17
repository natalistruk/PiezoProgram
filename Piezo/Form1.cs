using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Piezo
{
    public partial class Form1 : Form
    {
        private Solver solver;
        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n= Convert.ToInt32(ntextBox.Text);
            double l = Convert.ToDouble(ltextBox.Text);
            double Ro= Convert.ToDouble(rotextBox.Text);
            double C= Convert.ToDouble(ctextBox.Text);
            double E = Convert.ToDouble(etextBox.Text);
            double G = Convert.ToDouble(gtextBox.Text);
            double Sigma= Convert.ToDouble(sigmatextBox.Text);
            double D= Convert.ToDouble(DtextBox.Text);

            solver = new Solver(Ro, C, E, G, Sigma, D, l, n);
            

            solver.Solve();


            pGraphControl.Invalidate();
            uGraphControl.Invalidate();
            pGraphControl.GraphPane.Title.Text = "Графік p(x)";
            pGraphControl.GraphPane.XAxis.Title.Text = "Вісь Ox";
            pGraphControl.GraphPane.YAxis.Title.Text = "Вісь Oy";


            uGraphControl.GraphPane.Title.Text = "Графік u(x)";
            uGraphControl.GraphPane.XAxis.Title.Text = "Вісь Ox";
            uGraphControl.GraphPane.YAxis.Title.Text = "Вісь Oy";

            pGraphControl.GraphPane.CurveList.Clear();
            uGraphControl.GraphPane.CurveList.Clear();
            double[] x = new double[solver.N];
            double[] u = new double[solver.N];
            double[] p = new double[solver.N];
            for (int i = 0; i < solver.N; i++)
            {
                x[i]= solver.X[i];
                u[i] = solver.U[i];
                p[i] = solver.P[i];
            }
            uGraphControl.GraphPane.AddCurve("u(x)", x, u, Color.BlueViolet, SymbolType.None);
            pGraphControl.GraphPane.AddCurve("p(x)", x, p, Color.BlueViolet, SymbolType.None);

            uGraphControl.AxisChange();
            pGraphControl.AxisChange();


            pGraphControl.Invalidate();
            uGraphControl.Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}
