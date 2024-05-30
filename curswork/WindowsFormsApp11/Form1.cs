using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApp11;
namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        AnalysisCode ac;
        public Form1()
        {
            InitializeComponent();
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    using(StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        textBox3.Text = sr.ReadToEnd();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
             }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            richTextBox2.Clear();
            textBox5.Clear();
            ac = new AnalysisCode(textBox3.Text);
            ac.action();
            List<Token> tokens = new List<Token>();
            tokens = ac.tokens;
            LR rule = new LR(tokens);
            BetaTab betaTab = new BetaTab();
            
            try
            {
            
                richTextBox3.Text = betaTab.Info(textBox3.Text);
                rule.Programm();
                foreach (Token token in tokens)
                {

                    richTextBox2.AppendText(token.ToString());
                    richTextBox2.AppendText("\r\n");

                }
                BauerZamelzon rule1 = new BauerZamelzon(tokens);
                textBox5.Text = rule1.MatrixShow();
                MessageBox.Show("Разбор успешно завершён");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                MessageBox.Show($"Error! {ex.Message}");
            }
        }

    }
}
