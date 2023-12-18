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
 
    public partial class Form4 : Form
    {
        public int recievefeed = 0;
        public Form4(int feed)
        {
            InitializeComponent();
            recievefeed = feed;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            bg.BackColor = Color.Transparent;
            groupBox1.Parent = bg;
            animal.Parent = bg;
            label1.Text = "我的飼料:" + recievefeed.ToString();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (recievefeed == 0)
            {
                MessageBox.Show("沒有飼料了喔!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                recievefeed--;
                label1.Text = "我的飼料:"+recievefeed.ToString(); 
            }
        }
        
        int move1, move2;
        // Create an array of image paths or Image objects
        private Image[] images = { Properties.Resources.圖片10, Properties.Resources.圖片11,Properties.Resources.圖片12, Properties.Resources.圖片13, Properties.Resources.圖片14 };
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rd1 = new Random();
            move1=rd1.Next(-100, 100);
            Random rd2 = new Random();
            move2=rd2.Next(-15, 15);
            Random random = new Random();
            int randomIndex = random.Next(images.Length);
            if (animal.Location.X <= 615 && animal.Location.X >= -3 && animal.Location.Y >= 102 && animal.Location.Y <= 269)
            {
                animal.Left = move1 + animal.Location.X;
                animal.Top = move2 + animal.Location.Y;
                animal.Image = images[randomIndex];
            }
        }
        
    }
}
