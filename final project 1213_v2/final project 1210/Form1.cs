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

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
