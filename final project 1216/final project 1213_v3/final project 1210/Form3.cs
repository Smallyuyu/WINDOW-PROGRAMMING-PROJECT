using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_project_1210
{
    public partial class Form3 : Form
    {
        public delegate void ValueChangedEventHandler(string money_limitd,string money_limitU,string searchword);
        public event ValueChangedEventHandler ValueChanged;
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string money_limitd=textBox2.Text;string money_limitU=textBox3.Text;
            string searchword = textBox1.Text;
            //獲取form3中的值
            ValueChanged?.Invoke(money_limitd,money_limitU,searchword);
            textBox1.Clear();textBox2.Clear();textBox3.Clear();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
