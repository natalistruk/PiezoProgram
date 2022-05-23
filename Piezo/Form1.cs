using System;
using System.Drawing;
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
        private void FillPNormDataGridView()
        {
            dataGridView1.Rows.Add();
            int RowIndex = dataGridView1.RowCount - 1;
            DataGridViewRow R = dataGridView1.Rows[RowIndex];
            R.Cells["N"].Value = solver.N;
            
            R.Cells["L2_Norm"].Value = solver.P_L2_Norm();


        }
        private void FillUNormDataGridView()
        {
            dataGridView2.Rows.Add();
            int RowIndex = dataGridView2.RowCount - 1;
            DataGridViewRow R = dataGridView2.Rows[RowIndex];
            R.Cells["N"].Value = solver.N;

            R.Cells["L2_Norm"].Value = solver.U_L2_Norm();


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
            FillPNormDataGridView();
            FillUNormDataGridView();

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
            dataGridView1.Columns.Add("N", "N");
            dataGridView1.Columns.Add("L2_Norm", "L2_Norm");

            dataGridView2.Columns.Add("N", "N");
            dataGridView2.Columns.Add("L2_Norm", "L2_Norm");


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 3)
            {
                double p_h_l2 = (double)dataGridView1.Rows[0].Cells["L2_Norm"].Value;
                double p_h2_l2 = (double)dataGridView1.Rows[1].Cells["L2_Norm"].Value;
                double p_h4_l2 = (double)dataGridView1.Rows[2].Cells["L2_Norm"].Value;

                double p_l2 = Math.Log(Math.Abs((p_h_l2 - p_h2_l2) / (p_h2_l2 - p_h4_l2)), 2.0);

                textBox1.Text = p_l2.ToString();
                
                
            }

            if (dataGridView2.Rows.Count == 3)
            {
                double u_h_l2 = (double)dataGridView2.Rows[0].Cells["L2_Norm"].Value;
                double u_h2_l2 = (double)dataGridView2.Rows[1].Cells["L2_Norm"].Value;
                double u_h4_l2 = (double)dataGridView2.Rows[2].Cells["L2_Norm"].Value;

                double u_l2 = Math.Log(Math.Abs((u_h_l2 - u_h2_l2) / (u_h2_l2 - u_h4_l2)), 2.0);

                textBox2.Text = u_l2.ToString();


            }

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
