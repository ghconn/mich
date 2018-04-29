using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace winch
{
    public partial class BatRename : DialogBase
    {
        List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

        public BatRename()
        {
            InitializeComponent();
        }

        private void chb_CheckedChanged(object sender, EventArgs e)
        {
            txtformat.Enabled = !txtformat.Enabled;
        }

        private void btnsltfold_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog(this) == DialogResult.OK)
                txtpath.Text = fbd.SelectedPath;
        }

        #region
        string identity = "";//自增变量，文件名不一样的部分
        private void btngo_Click(object sender, EventArgs e)
        {
            var temppath = "";
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                var fnames = tpc.f.FNameInPath(txtpath.Text, SearchOption.TopDirectoryOnly);
                var groups = fnames.GroupBy(s => Path.GetExtension(s));//根据文件扩展名分组


                temppath = Path.GetDirectoryName(groups.First().First()) + Path.DirectorySeparatorChar + Guid.NewGuid().ToString();
                Directory.CreateDirectory(temppath);
                var tempdict = new Dictionary<string, string>();

                foreach (var group in groups)
                {
                    identity = txtstart.Text.Trim();
                    identity = identity.Length < 1 ? "0" : identity.Length > 1 ? identity.TrimStart('0') : identity;
                    foreach (var s in group)
                    {
                        DirectoryInfo di = new DirectoryInfo(s);
                        var newfilename = "";
                        var ext = Path.GetExtension(s);
                        var fnwext = Path.GetFileNameWithoutExtension(s);
                        if (chb.Checked)
                        {
                            var shortestfilename = group.First(p => p.Length == group.Min(g => g.Length));
                            shortestfilename = Path.GetFileNameWithoutExtension(shortestfilename);
                            if (rdopre.Checked)
                                newfilename = identity + shortestfilename + ext;
                            else
                                newfilename = shortestfilename + identity + ext;
                        }
                        else
                        {
                            if (rdopre.Checked)
                                newfilename = identity + txtformat.Text.Trim() + ext;
                            else
                                newfilename = txtformat.Text.Trim() + identity + ext;
                        }

                        var newfullname = di.Parent.FullName + Path.DirectorySeparatorChar + newfilename;
                        dict.Add(s, newfullname);//记住重命名操作

                        var temprm = temppath + Path.DirectorySeparatorChar + newfilename;
                        di.MoveTo(temprm);
                        tempdict.Add(s, temprm);

                        identity = addstep(identity);
                    }
                }
                foreach (var k in tempdict.Keys)
                {
                    var di = new DirectoryInfo(tempdict[k]);
                    var newfullname = di.Parent.Parent.FullName + Path.DirectorySeparatorChar + Path.GetFileName(di.FullName);
                    di.MoveTo(newfullname);
                }

                Directory.Delete(temppath);
                list.Add(dict);//添加到历史修改记录
            }
            catch
            {

            }
            finally
            {
                if (Directory.Exists(temppath))
                {
                    Directory.Delete(temppath);
                }
            }
            MessageBox.Show("haole");
        }

        string addstep(string s)
        {
            int identity;
            if (!int.TryParse(s, out identity))
            {
                return s.Substring(0, s.Length - 1) + ((char)(s[s.Length - 1] + Convert.ToInt32(txtstep.Text.Trim()))).ToString();
            }
            return (identity + Convert.ToInt32(txtstep.Text.Trim())).ToString();
        }
        #endregion

        #region 撤销
        private void button1_Click(object sender, EventArgs e)
        {
            var temppath = "";
            try
            {
                var lastrenameoperation = list.Last();
                temppath = Path.GetDirectoryName(lastrenameoperation.First().Key) + Path.DirectorySeparatorChar + Guid.NewGuid().ToString();
                if (lastrenameoperation != null)
                {
                    Directory.CreateDirectory(temppath);
                    foreach (var k in lastrenameoperation.Keys)
                    {
                        DirectoryInfo di = new DirectoryInfo(lastrenameoperation[k]);
                        var temprm = temppath + Path.DirectorySeparatorChar + Path.GetFileName(lastrenameoperation[k]);
                        di.MoveTo(temprm);
                    }
                    foreach (var k in lastrenameoperation.Keys)
                    {
                        DirectoryInfo di = new DirectoryInfo(temppath + Path.DirectorySeparatorChar + Path.GetFileName(lastrenameoperation[k]));
                        di.MoveTo(k);
                    }
                    list.Remove(lastrenameoperation);
                }
            }
            catch
            {

            }
            finally
            {
                if (Directory.Exists(temppath))
                {
                    Directory.Delete(temppath);
                }
            }
            MessageBox.Show("haole");
        }
        #endregion
    }
}
