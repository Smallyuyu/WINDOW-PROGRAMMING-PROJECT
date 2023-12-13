using System;
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
        List<string> moneys = new List<string>(); List<string> remarks = new List<string>(); List<string> categorys = new List<string>(); List<string> dates = new List<string>();
        List<bool> cost_incomes = new List<bool>();
        private void add_btn_Click(object sender, EventArgs e)
        {
            string remark = remark_tbx.Text;string money = money_tbx.Text;string category = category_cbx.Text;string date = dateTimePicker1.Text;
            bool check_income=radioButton1.Checked; bool check_cost = radioButton2.Checked;
            //假設全部都有選
            if (string.IsNullOrWhiteSpace(remark)==false && string.IsNullOrWhiteSpace(money)==false&& string.IsNullOrWhiteSpace(date)==false&&string.IsNullOrWhiteSpace(category)==false && check_income^check_cost) 
            {
                remarks.Add(remark);categorys.Add(category);dates.Add(date);
                moneys.Add(money);cost_incomes.Add(check_income);
                MessageBox.Show("成功加入!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               // MessageBox.Show($"{remarks[0]} {categorys[0]} {moneys[0]} {dates[0]}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                remark_tbx.Clear(); money_tbx.Clear(); category_cbx.Text = "";
            } 
            else 
            {
                MessageBox.Show("少填選項喔!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
