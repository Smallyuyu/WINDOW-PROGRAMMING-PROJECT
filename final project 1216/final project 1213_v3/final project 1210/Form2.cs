using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static final_project_1210.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace final_project_1210
{
    public partial class Form2 : Form
    {
        public List<data_type> receivedValue;public string cnfromform1;public Dictionary<bool, string> cost_incomes_int_to_stringfromform1;
        // 當改動form2的值
        //public delegate void ValueChangedEventHandler(string[] newValuelines,int newLength);
        // 定義一个事件
        //public event ValueChangedEventHandler ValueChanged;

        public Form2(List<data_type> datasfromform1,string category_name, Dictionary<bool, string> cost_incomes_int_to_string)
        {
            InitializeComponent(); 
            receivedValue = datasfromform1; cnfromform1 = category_name; cost_incomes_int_to_stringfromform1 = cost_incomes_int_to_string;    
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 關閉Form2
            this.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
           
            label1.Text = cnfromform1;
            double totalmoney=0;
            foreach (var item in receivedValue)
            {
                if (item.category == cnfromform1)
                {
                    Text = $"{cost_incomes_int_to_stringfromform1[item.cost_incomes]} {item.y}/{item.m}/{item.d} {item.remarks} {item.moneys}\n";
                    textBox1.AppendText(Text+ Environment.NewLine);
                    totalmoney += item.moneys;
                }
            }
            label2.Text = "總金額:" + totalmoney;
            this.Text = "分類管理";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.ValueChanged += Form3_ValueChanged;
            frm3.Show(); //顯示Form3
        }
        private void Form3_ValueChanged(string money_limitd, string money_limitU, string searchword)
        {
            //處理從form3傳來的值
            if (string.IsNullOrWhiteSpace(money_limitd) == true) { money_limitd = "0"; }
            if (string.IsNullOrWhiteSpace(money_limitU) == true) { money_limitU = "10000000000000000000000000000000000"; }
            double totalmoney = 0;int count = 0;
            if (string.IsNullOrWhiteSpace(searchword) == false) //有值
            {
                textBox1.Clear();
                foreach (var item in receivedValue)
                {
                    if (item.remarks == searchword)
                    {
                        if (item.moneys >= double.Parse(money_limitd) && item.moneys <= double.Parse(money_limitU))
                        {
                            Text = $"{cost_incomes_int_to_stringfromform1[item.cost_incomes]} {item.y}/{item.m}/{item.d} {item.remarks} {item.moneys}\n";
                            textBox1.AppendText(Text + Environment.NewLine);
                            totalmoney += item.moneys;
                        }
                        count += 1;
                    }
                }
                if (count==0) 
                { 
                    MessageBox.Show("未找到相對應紀錄","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                textBox1.Clear();
                foreach (var item in receivedValue)
                {
                    if (item.category == cnfromform1)
                    {
                        if (item.moneys >= double.Parse(money_limitd) && item.moneys <= double.Parse(money_limitU))
                        {
                            Text = $"{cost_incomes_int_to_stringfromform1[item.cost_incomes]} {item.y}/{item.m}/{item.d} {item.remarks} {item.moneys}\n";
                            textBox1.AppendText(Text + Environment.NewLine);
                            totalmoney += item.moneys;
                        }
                    }
                }
            }
            label2.Text = "總金額:" + totalmoney;
            this.Text = "分類管理";
        }    
    }
}
