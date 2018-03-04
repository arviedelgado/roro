using Roro.Activities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private const string FileDialogFilter = "Roro Workflow (*.xml)|*.xml";

        private void OpenLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.OpenWorkflow();
        }

        private void CreateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.CreateWorkflow();
        }

        private void OpenWorkflow()
        {
            using (var file = new OpenFileDialog())
            {
                file.Filter = FileDialogFilter;
                if (file.ShowDialog() == DialogResult.OK)
                {
                    var form = new PageForm(File.ReadAllText(file.FileName));
                    form.OnBeforeSave += (ss, ee) => File.WriteAllText(form.FileName, form.FileContent);
                    form.FileName = file.FileName;
                    form.Show();
                }
            }
        }

        private void CreateWorkflow()
        {
            var form = new PageForm(string.Empty);
            form.OnBeforeSave += (ss, ee) =>
            {
                if (form.FileName == string.Empty)
                {
                    using (var file = new SaveFileDialog())
                    {
                        file.Filter = FileDialogFilter;
                        if (file.ShowDialog() == DialogResult.OK)
                        {
                            form.FileName = file.FileName;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                File.WriteAllText(form.FileName, form.FileContent);
            };
            form.Show();
        }
    }
}
