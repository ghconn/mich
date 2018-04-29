using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace winch
{
    public partial class webdoc : DialogBase
    {
        public webdoc()
        {
            InitializeComponent();
        }

        private void btngo_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(txturl.Text);
        }

        private void btnget_Click(object sender, EventArgs e)
        {
            if (txtSelector.Text.Trim() == "")
            {
                return;
            }
            //var elements = document.GetElementsByTagName("input");
            //for (var i = 0; i < elements.Count; i++)
            //{
            //    if (elements[i].GetAttribute("value") == "查询")
            //    {
            //        elements[i].InvokeMember("Click");
            //        break;
            //    }
            //}
            //body > div > div.module.mod-ip > form > p > input.btn
            //body > div > div.module.mod-guide > table > tbody > tr:nth-child(4) > td:nth-child(3) > a
            //body > div > div:nth-child(7) > table > tbody > tr:nth-child(2) > td > form > input.btn
            try
            {
                var document = webBrowser1.Document;
                HtmlElement element = document.Body;
                var paths = txtSelector.Text.Replace(" ", "").Split('>');
                for (var i = 1; i < paths.Length; i++)//第一个是Body，直接略过
                {
                    var tagname = Regex.Split(paths[i], ":nth-child\\(\\d+\\)")[0];
                    var tag = tagname.Split('.');
                    if (tag.Length > 1)
                    {
                        var tagelement = element.GetElementsByTagName(tag[0]);
                        var classname = string.Join(" ", tag.Skip(1));
                        for (var j = 0; j < tagelement.Count; j++)
                        {
                            var eac = tagelement[j].GetAttribute("className");
                            if (eac == classname)
                            {
                                element = tagelement[j];
                                goto outer;
                            }
                        }
                    }
                    var n = 1;
                    if (Regex.IsMatch(paths[i], "\\d+"))
                        n = int.Parse(Regex.Match(paths[i], "\\d+").Value);
                    element = element.GetElementsByTagName(tag[0])[n - 1];
                outer: ;
                }

                txtHtml.Text = element.InnerHtml;
                txtText.Text = element.InnerText;
                txtVal.Text = element.GetAttribute("value");
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
