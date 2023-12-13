using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace final_project_1210
{
    public partial class Form1 : Form
    {
        public struct data_type
        {
            public string category;
            public int y, m, d;
            public string date;
            public bool cost_incomes;
            public string remarks;
            public double moneys;
        }
        Dictionary<bool, string> cost_incomes_int_to_string = new Dictionary<bool, string>(){[true] = "收入", [false] = "支出"};
        List<data_type> datas = new List<data_type>();
        //Dictionary<string, data_type> datas = new Dictionary<string, data_type>(); //用Dictionary分類data的category
        List<string> winning_number =   new List<string>();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            web_crawler();
            
        }
        private async Task web_crawler()
        {
            var url = "https://invoice.etax.nat.gov.tw/";
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);
            var htmldocument = new HtmlAgilityPack.HtmlDocument();
            htmldocument.LoadHtml(html);
            var divs = htmldocument.DocumentNode.Descendants("p").Where(node => node.GetAttributeValue("class", "").Equals("etw-tbiggest"));
            string str;
            int cnt = 0;
            foreach ( var div in divs )
            {
                winning_number.Add(div.Descendants("span").FirstOrDefault().InnerText);
                cnt++;
                if (cnt == 2) break;
            }
            cnt = 0;
            divs = htmldocument.DocumentNode.Descendants("p").Where(node => node.GetAttributeValue("class", "").Equals("etw-tbiggest mb-md-4"));
            foreach (var div in divs)
            {
                str = div.Descendants("span").FirstOrDefault().InnerText;
                str = str + div.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("font-weight-bold etw-color-red")).FirstOrDefault().InnerText;
                winning_number.Add(str);
                cnt++;
                if (cnt == 3) break;
            }
        }
        //建立各個選項的list
        List<double> moneys = new List<double>(); List<string> remarks = new List<string>(); List<string> categorys = new List<string>(); List<string> dates = new List<string>();
        List<bool> cost_incomes = new List<bool>();
        double expenses = 0; //總支出
        double remains = 0; //總收入
        private void add_btn_Click(object sender, EventArgs e)
        {
            string remark = remark_tbx.Text;double money = double.Parse(money_tbx.Text);string category = category_cbx.Text;string date = dateTimePicker1.Text;
            bool check_income=radioButton1.Checked; bool check_cost = radioButton2.Checked;
            //假設全部都有選
            if (string.IsNullOrWhiteSpace(remark)==false && money_tbx.Text != string.Empty && string.IsNullOrWhiteSpace(date)==false&&string.IsNullOrWhiteSpace(category)==false && check_income^check_cost) 
            {
                //分類資料
                data_type data;
                data.y = dateTimePicker1.Value.Year; data.m = dateTimePicker1.Value.Month; data.d = dateTimePicker1.Value.Day;
                data.category = category; data.moneys = money; data.date = date; data.remarks = remark; data.cost_incomes = check_income;
                datas.Add(data);

                remarks.Add(remark);categorys.Add(category);dates.Add(date);
                moneys.Add(money);cost_incomes.Add(check_income);
                MessageBox.Show("成功加入!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               // MessageBox.Show($"{remarks[0]} {categorys[0]} {moneys[0]} {dates[0]}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                remark_tbx.Clear(); money_tbx.Clear(); category_cbx.Text = "";

                //更新支出、結餘
                if (check_cost) expenses += money;
                else remains += money;
                label5.Text = "合計支出:" + expenses.ToString();
                label6.Text = "結餘:" + (remains - expenses).ToString();
                update_textbox();
            } 
            else 
            {
                MessageBox.Show("少填選項喔!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //限制只能輸入數字
        private void money_tbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //是否輸入數字
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //小數點最多只能有一個
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            update_textbox();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            update_textbox();
        }
        //更新顯示板
        private void update_textbox()
        {
            textBox2.Text = string.Empty;
            //時間範圍上下界
            int left_y = dateTimePicker2.Value.Year; int right_y = dateTimePicker3.Value.Year;
            int left_m = dateTimePicker2.Value.Month; int right_m = dateTimePicker3.Value.Month;
            int left_d = dateTimePicker2.Value.Day; int right_d = dateTimePicker3.Value.Day;
            int left = left_y * 365 + left_m * 31 + left_d;
            int right = right_y * 365 + right_m * 31 + right_d;
            //左界是否小於等於右界
            if (left <= right) {
                foreach(var item in datas)
                {
                    int sum = item.y * 365 + item.m * 31 + item.d;
                    if(left <= sum && sum <= right)
                    {
                        textBox2.Text += cost_incomes_int_to_string[item.cost_incomes] + " " + item.category + " " + item.moneys + " " +item.remarks + Environment.NewLine;
                    }
                }
            }
        }
    }
}
