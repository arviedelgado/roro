using System.Windows.Forms;

namespace Roro.Activities
{
    public class ArgumentTypeEditorUI : Form
    {
        private TextBox textbox;
        private Button okButton;
        public ArgumentTypeEditorUI()
        {
            // TextBox
            textbox = new TextBox();
            textbox.Multiline = true;
            textbox.Dock = DockStyle.Fill;
            textbox.Font = new System.Drawing.Font("Verdana", 12);
            textbox.ScrollBars = ScrollBars.Both;
            this.Controls.Add(textbox);
            // Button
            okButton = new Button();
            okButton.Text = "OK";
            okButton.Dock = DockStyle.Right;
            okButton.DialogResult = DialogResult.OK;
            this.Controls.Add(okButton);
            // Form
            this.Text = "Argument Editor";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Width = 400;
            this.Height = 200;
        }

        public string Value
        {
            get { return textbox.Text; }
            set { textbox.Text = value; }
        }
    }
}
