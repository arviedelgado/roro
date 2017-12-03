using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class PropertyEditor : Form
    {
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node6");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Text", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Number", new System.Windows.Forms.TreeNode[] {
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Flag", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode26,
            treeNode27});
            this.outputTabControl = new System.Windows.Forms.TabControl();
            this.outputTabPage = new System.Windows.Forms.TabPage();
            this.outputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.outputMoveDownButton = new System.Windows.Forms.Button();
            this.outputMoveUpButton = new System.Windows.Forms.Button();
            this.outputRemoveButton = new System.Windows.Forms.Button();
            this.outputAddButton = new System.Windows.Forms.Button();
            this.leftRightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.inOutSplitContainer = new System.Windows.Forms.SplitContainer();
            this.inputTabControl = new System.Windows.Forms.TabControl();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.inputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.inputMoveDownButton = new System.Windows.Forms.Button();
            this.inputMoveUpButton = new System.Windows.Forms.Button();
            this.inputRemoveButton = new System.Windows.Forms.Button();
            this.inputAddButton = new System.Windows.Forms.Button();
            this.variableTabControl = new System.Windows.Forms.TabControl();
            this.variableTabPage = new System.Windows.Forms.TabPage();
            this.variableTreeView = new System.Windows.Forms.TreeView();
            this.variableGlobalTabPage = new System.Windows.Forms.TabPage();
            this.outputTabControl.SuspendLayout();
            this.outputTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputSplitContainer)).BeginInit();
            this.outputSplitContainer.Panel2.SuspendLayout();
            this.outputSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftRightSplitContainer)).BeginInit();
            this.leftRightSplitContainer.Panel1.SuspendLayout();
            this.leftRightSplitContainer.Panel2.SuspendLayout();
            this.leftRightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inOutSplitContainer)).BeginInit();
            this.inOutSplitContainer.Panel1.SuspendLayout();
            this.inOutSplitContainer.Panel2.SuspendLayout();
            this.inOutSplitContainer.SuspendLayout();
            this.inputTabControl.SuspendLayout();
            this.inputTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).BeginInit();
            this.inputSplitContainer.Panel2.SuspendLayout();
            this.inputSplitContainer.SuspendLayout();
            this.variableTabControl.SuspendLayout();
            this.variableTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputTabControl
            // 
            this.outputTabControl.Controls.Add(this.outputTabPage);
            this.outputTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTabControl.Location = new System.Drawing.Point(0, 0);
            this.outputTabControl.Name = "outputTabControl";
            this.outputTabControl.SelectedIndex = 0;
            this.outputTabControl.Size = new System.Drawing.Size(537, 217);
            this.outputTabControl.TabIndex = 0;
            this.outputTabControl.TabStop = false;
            // 
            // outputTabPage
            // 
            this.outputTabPage.Controls.Add(this.outputSplitContainer);
            this.outputTabPage.Location = new System.Drawing.Point(4, 24);
            this.outputTabPage.Name = "outputTabPage";
            this.outputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outputTabPage.Size = new System.Drawing.Size(529, 189);
            this.outputTabPage.TabIndex = 0;
            this.outputTabPage.Text = "Outputs";
            this.outputTabPage.UseVisualStyleBackColor = true;
            // 
            // outputSplitContainer
            // 
            this.outputSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.outputSplitContainer.IsSplitterFixed = true;
            this.outputSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.outputSplitContainer.Name = "outputSplitContainer";
            this.outputSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // outputSplitContainer.Panel1
            // 
            this.outputSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // outputSplitContainer.Panel2
            // 
            this.outputSplitContainer.Panel2.Controls.Add(this.outputMoveDownButton);
            this.outputSplitContainer.Panel2.Controls.Add(this.outputMoveUpButton);
            this.outputSplitContainer.Panel2.Controls.Add(this.outputRemoveButton);
            this.outputSplitContainer.Panel2.Controls.Add(this.outputAddButton);
            this.outputSplitContainer.Size = new System.Drawing.Size(523, 183);
            this.outputSplitContainer.SplitterDistance = 149;
            this.outputSplitContainer.TabIndex = 0;
            this.outputSplitContainer.TabStop = false;
            // 
            // outputMoveDownButton
            // 
            this.outputMoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.outputMoveDownButton.Location = new System.Drawing.Point(440, 3);
            this.outputMoveDownButton.Name = "outputMoveDownButton";
            this.outputMoveDownButton.Size = new System.Drawing.Size(80, 23);
            this.outputMoveDownButton.TabIndex = 2;
            this.outputMoveDownButton.TabStop = false;
            this.outputMoveDownButton.Text = "Move Down";
            this.outputMoveDownButton.UseVisualStyleBackColor = true;
            // 
            // outputMoveUpButton
            // 
            this.outputMoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.outputMoveUpButton.Location = new System.Drawing.Point(354, 3);
            this.outputMoveUpButton.Name = "outputMoveUpButton";
            this.outputMoveUpButton.Size = new System.Drawing.Size(80, 23);
            this.outputMoveUpButton.TabIndex = 3;
            this.outputMoveUpButton.TabStop = false;
            this.outputMoveUpButton.Text = "Move Up";
            this.outputMoveUpButton.UseVisualStyleBackColor = true;
            // 
            // outputRemoveButton
            // 
            this.outputRemoveButton.Location = new System.Drawing.Point(89, 3);
            this.outputRemoveButton.Name = "outputRemoveButton";
            this.outputRemoveButton.Size = new System.Drawing.Size(80, 23);
            this.outputRemoveButton.TabIndex = 1;
            this.outputRemoveButton.TabStop = false;
            this.outputRemoveButton.Text = "Remove";
            this.outputRemoveButton.UseVisualStyleBackColor = true;
            // 
            // outputAddButton
            // 
            this.outputAddButton.Location = new System.Drawing.Point(3, 3);
            this.outputAddButton.Name = "outputAddButton";
            this.outputAddButton.Size = new System.Drawing.Size(80, 23);
            this.outputAddButton.TabIndex = 0;
            this.outputAddButton.TabStop = false;
            this.outputAddButton.Text = "Add";
            this.outputAddButton.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.leftRightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftRightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftRightSplitContainer.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.leftRightSplitContainer.Panel1.Controls.Add(this.inOutSplitContainer);
            this.leftRightSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer2.Panel2
            // 
            this.leftRightSplitContainer.Panel2.Controls.Add(this.variableTabControl);
            this.leftRightSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.leftRightSplitContainer.Size = new System.Drawing.Size(784, 461);
            this.leftRightSplitContainer.SplitterDistance = 557;
            this.leftRightSplitContainer.TabIndex = 1;
            this.leftRightSplitContainer.TabStop = false;
            // 
            // inOutSplitContainer
            // 
            this.inOutSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inOutSplitContainer.Location = new System.Drawing.Point(10, 10);
            this.inOutSplitContainer.Name = "inOutSplitContainer";
            this.inOutSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // inOutSplitContainer.Panel1
            // 
            this.inOutSplitContainer.Panel1.Controls.Add(this.inputTabControl);
            // 
            // inOutSplitContainer.Panel2
            // 
            this.inOutSplitContainer.Panel2.Controls.Add(this.outputTabControl);
            this.inOutSplitContainer.Size = new System.Drawing.Size(537, 441);
            this.inOutSplitContainer.SplitterDistance = 220;
            this.inOutSplitContainer.TabIndex = 1;
            this.inOutSplitContainer.TabStop = false;
            // 
            // inputTabControl
            // 
            this.inputTabControl.Controls.Add(this.inputTabPage);
            this.inputTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTabControl.Location = new System.Drawing.Point(0, 0);
            this.inputTabControl.Name = "inputTabControl";
            this.inputTabControl.SelectedIndex = 0;
            this.inputTabControl.Size = new System.Drawing.Size(537, 220);
            this.inputTabControl.TabIndex = 1;
            this.inputTabControl.TabStop = false;
            // 
            // inputTabPage
            // 
            this.inputTabPage.Controls.Add(this.inputSplitContainer);
            this.inputTabPage.Location = new System.Drawing.Point(4, 24);
            this.inputTabPage.Name = "inputTabPage";
            this.inputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputTabPage.Size = new System.Drawing.Size(529, 192);
            this.inputTabPage.TabIndex = 0;
            this.inputTabPage.Text = "Inputs";
            this.inputTabPage.UseVisualStyleBackColor = true;
            // 
            // inputSplitContainer
            // 
            this.inputSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.inputSplitContainer.IsSplitterFixed = true;
            this.inputSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.inputSplitContainer.Name = "inputSplitContainer";
            this.inputSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // inputSplitContainer.Panel1
            // 
            this.inputSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // inputSplitContainer.Panel2
            // 
            this.inputSplitContainer.Panel2.Controls.Add(this.inputMoveDownButton);
            this.inputSplitContainer.Panel2.Controls.Add(this.inputMoveUpButton);
            this.inputSplitContainer.Panel2.Controls.Add(this.inputRemoveButton);
            this.inputSplitContainer.Panel2.Controls.Add(this.inputAddButton);
            this.inputSplitContainer.Size = new System.Drawing.Size(523, 186);
            this.inputSplitContainer.SplitterDistance = 152;
            this.inputSplitContainer.TabIndex = 0;
            this.inputSplitContainer.TabStop = false;
            // 
            // inputMoveDownButton
            // 
            this.inputMoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputMoveDownButton.Location = new System.Drawing.Point(440, 3);
            this.inputMoveDownButton.Name = "inputMoveDownButton";
            this.inputMoveDownButton.Size = new System.Drawing.Size(80, 23);
            this.inputMoveDownButton.TabIndex = 2;
            this.inputMoveDownButton.TabStop = false;
            this.inputMoveDownButton.Text = "Move Down";
            this.inputMoveDownButton.UseVisualStyleBackColor = true;
            // 
            // inputMoveUpButton
            // 
            this.inputMoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputMoveUpButton.Location = new System.Drawing.Point(354, 3);
            this.inputMoveUpButton.Name = "inputMoveUpButton";
            this.inputMoveUpButton.Size = new System.Drawing.Size(80, 23);
            this.inputMoveUpButton.TabIndex = 3;
            this.inputMoveUpButton.TabStop = false;
            this.inputMoveUpButton.Text = "Move Up";
            this.inputMoveUpButton.UseVisualStyleBackColor = true;
            // 
            // inputRemoveButton
            // 
            this.inputRemoveButton.Location = new System.Drawing.Point(89, 3);
            this.inputRemoveButton.Name = "inputRemoveButton";
            this.inputRemoveButton.Size = new System.Drawing.Size(80, 23);
            this.inputRemoveButton.TabIndex = 1;
            this.inputRemoveButton.TabStop = false;
            this.inputRemoveButton.Text = "Remove";
            this.inputRemoveButton.UseVisualStyleBackColor = true;
            // 
            // inputAddButton
            // 
            this.inputAddButton.Location = new System.Drawing.Point(3, 3);
            this.inputAddButton.Name = "inputAddButton";
            this.inputAddButton.Size = new System.Drawing.Size(80, 23);
            this.inputAddButton.TabIndex = 0;
            this.inputAddButton.TabStop = false;
            this.inputAddButton.Text = "Add";
            this.inputAddButton.UseVisualStyleBackColor = true;
            // 
            // variableTabControl
            // 
            this.variableTabControl.Controls.Add(this.variableTabPage);
            this.variableTabControl.Controls.Add(this.variableGlobalTabPage);
            this.variableTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variableTabControl.Location = new System.Drawing.Point(10, 10);
            this.variableTabControl.Name = "variableTabControl";
            this.variableTabControl.SelectedIndex = 0;
            this.variableTabControl.Size = new System.Drawing.Size(203, 441);
            this.variableTabControl.TabIndex = 0;
            this.variableTabControl.TabStop = false;
            // 
            // variableTabPage
            // 
            this.variableTabPage.Controls.Add(this.variableTreeView);
            this.variableTabPage.Location = new System.Drawing.Point(4, 24);
            this.variableTabPage.Name = "variableTabPage";
            this.variableTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.variableTabPage.Size = new System.Drawing.Size(195, 413);
            this.variableTabPage.TabIndex = 0;
            this.variableTabPage.Text = "Variables";
            this.variableTabPage.UseVisualStyleBackColor = true;
            // 
            // variableTreeView
            // 
            this.variableTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.variableTreeView.Location = new System.Drawing.Point(3, 3);
            this.variableTreeView.Name = "variableTreeView";
            treeNode15.Name = "Node3";
            treeNode15.Text = "Node3";
            treeNode16.Name = "Node4";
            treeNode16.Text = "Node4";
            treeNode17.Name = "Node5";
            treeNode17.Text = "Node5";
            treeNode18.Name = "Node6";
            treeNode18.Text = "Node6";
            treeNode19.Name = "Node0";
            treeNode19.Text = "Text";
            treeNode20.Name = "Node7";
            treeNode20.Text = "Node7";
            treeNode21.Name = "Node8";
            treeNode21.Text = "Node8";
            treeNode22.Name = "Node9";
            treeNode22.Text = "Node9";
            treeNode23.Name = "Node10";
            treeNode23.Text = "Node10";
            treeNode24.Name = "Node1";
            treeNode24.Text = "Number";
            treeNode25.Name = "Node11";
            treeNode25.Text = "Node11";
            treeNode26.Name = "Node12";
            treeNode26.Text = "Node12";
            treeNode27.Name = "Node13";
            treeNode27.Text = "Node13";
            treeNode28.Name = "Node2";
            treeNode28.Text = "Flag";
            this.variableTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode24,
            treeNode28});
            this.variableTreeView.Size = new System.Drawing.Size(189, 407);
            this.variableTreeView.TabIndex = 0;
            this.variableTreeView.TabStop = false;
            // 
            // variableGlobalTabPage
            // 
            this.variableGlobalTabPage.Location = new System.Drawing.Point(4, 22);
            this.variableGlobalTabPage.Name = "variableGlobalTabPage";
            this.variableGlobalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.variableGlobalTabPage.Size = new System.Drawing.Size(195, 415);
            this.variableGlobalTabPage.TabIndex = 1;
            this.variableGlobalTabPage.Text = "Environment Variables";
            this.variableGlobalTabPage.UseVisualStyleBackColor = true;
            // 
            // PropertyEditor
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.leftRightSplitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "PropertyEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.PropertyEditor_Load);
            this.outputTabControl.ResumeLayout(false);
            this.outputTabPage.ResumeLayout(false);
            this.outputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputSplitContainer)).EndInit();
            this.outputSplitContainer.ResumeLayout(false);
            this.leftRightSplitContainer.Panel1.ResumeLayout(false);
            this.leftRightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftRightSplitContainer)).EndInit();
            this.leftRightSplitContainer.ResumeLayout(false);
            this.inOutSplitContainer.Panel1.ResumeLayout(false);
            this.inOutSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inOutSplitContainer)).EndInit();
            this.inOutSplitContainer.ResumeLayout(false);
            this.inputTabControl.ResumeLayout(false);
            this.inputTabPage.ResumeLayout(false);
            this.inputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).EndInit();
            this.inputSplitContainer.ResumeLayout(false);
            this.variableTabControl.ResumeLayout(false);
            this.variableTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private TabControl outputTabControl;
        private TabPage outputTabPage;
        private SplitContainer outputSplitContainer;
        private Button outputMoveUpButton;
        private Button outputMoveDownButton;
        private Button outputRemoveButton;
        private Button outputAddButton;
        private SplitContainer leftRightSplitContainer;
        private TabControl variableTabControl;
        private TabPage variableTabPage;
        private TabPage variableGlobalTabPage;
        private TreeView variableTreeView;
        private SplitContainer inOutSplitContainer;
        private TabControl inputTabControl;
        private TabPage inputTabPage;
        private SplitContainer inputSplitContainer;
        private Button inputMoveDownButton;
        private Button inputMoveUpButton;
        private Button inputRemoveButton;
        private Button inputAddButton;
        private ActivityGrid inputGrid;
        private ActivityGrid outputGrid;

        public PropertyEditor(Activity act)
        {
            this.InitializeComponent();
            // inputs
            this.inputGrid = new ActivityGrid(act.Inputs);
            this.inputGrid.Parent = this.inputSplitContainer.Panel1;
            if (act is StartNodeActivity)
            {
                this.inputAddButton.Click += this.inputGrid.AddRow;
                this.inputRemoveButton.Click += this.inputGrid.RemoveRow;
                this.inputMoveUpButton.Click += this.inputGrid.MoveUpRow;
                this.inputMoveDownButton.Click += this.inputGrid.MoveDownRow;
                this.inOutSplitContainer.Panel2Collapsed = true;
            }
            else
            {
                this.inputGrid.Columns[0].ReadOnly = true;
                this.inputGrid.Columns[1].ReadOnly = true;
                this.inputSplitContainer.Panel2Collapsed = true;
            }
            // outputs
            this.outputGrid = new ActivityGrid(act.Outputs);
            this.outputGrid.Parent = this.outputSplitContainer.Panel1;
            if (act is EndNodeActivity)
            {
                this.outputAddButton.Click += this.outputGrid.AddRow;
                this.outputRemoveButton.Click += this.outputGrid.RemoveRow;
                this.outputMoveUpButton.Click += this.outputGrid.MoveUpRow;
                this.outputMoveDownButton.Click += this.outputGrid.MoveDownRow;
                this.inOutSplitContainer.Panel1Collapsed = true;
            }
            else
            {
                this.outputGrid.Columns[0].ReadOnly = true;
                this.outputGrid.Columns[1].ReadOnly = true;
                this.outputSplitContainer.Panel2Collapsed = true;
            }
        }

        private void PropertyEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
