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
    public partial class alarm : Form
    {
        public alarm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Hour = (int)numericUpDown1.Value;
            int Minute = (int)numericUpDown2.Value;

            MessageBox.Show($"鬧鐘時間已設定為：{Hour:D2}:{Minute:D2}");
        }
    }
}
