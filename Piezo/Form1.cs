using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            /*MathExpressionParser roParser = new MathExpressionParser();
            roParser.Init(rotextBox.Text);
            Function Ro = new Function(roParser.Evaluate);

            MathExpressionParser eParser = new MathExpressionParser();
            roParser.Init(etextBox.Text);
            Function E = new Function(eParser.Evaluate);

            MathExpressionParser cParser = new MathExpressionParser();
            roParser.Init(ctextBox.Text);
            Function C = new Function(cParser.Evaluate);

            MathExpressionParser gParser = new MathExpressionParser();
            roParser.Init(gtextBox.Text);
            Function G = new Function(gParser.Evaluate);

            MathExpressionParser sigmaParser = new MathExpressionParser();
            roParser.Init(sigmatextBox.Text);
            Function Sigma = new Function(sigmaParser.Evaluate);

            MathExpressionParser dParser = new MathExpressionParser();
            roParser.Init(DtextBox.Text);
            Function  D = new Function(roParser.Evaluate);
            */

            solver = new Solver(Ro, C, E, G, Sigma, D, l, n);
            

            solver.Solve();


        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
