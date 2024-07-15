namespace AHeadLib.Net
{
    partial class MainFrom
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrom));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.hyperlinkLabelControl_Project = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.simpleButton_Exit = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton_Generate = new DevExpress.XtraEditors.SimpleButton();
            this.memoEdit_Log = new DevExpress.XtraEditors.MemoEdit();
            this.buttonEdit_OutputDirectory = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEdit_InputFile = new DevExpress.XtraEditors.ButtonEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Log.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit_OutputDirectory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit_InputFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.hyperlinkLabelControl_Project);
            this.layoutControl1.Controls.Add(this.simpleButton_Exit);
            this.layoutControl1.Controls.Add(this.simpleButton_Generate);
            this.layoutControl1.Controls.Add(this.memoEdit_Log);
            this.layoutControl1.Controls.Add(this.buttonEdit_OutputDirectory);
            this.layoutControl1.Controls.Add(this.buttonEdit_InputFile);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControl1.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1266, 486);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // hyperlinkLabelControl_Project
            // 
            this.hyperlinkLabelControl_Project.Location = new System.Drawing.Point(12, 442);
            this.hyperlinkLabelControl_Project.Name = "hyperlinkLabelControl_Project";
            this.hyperlinkLabelControl_Project.Size = new System.Drawing.Size(378, 25);
            this.hyperlinkLabelControl_Project.StyleController = this.layoutControl1;
            this.hyperlinkLabelControl_Project.TabIndex = 9;
            this.hyperlinkLabelControl_Project.Text = "https://github.com/bodong1987/AHeadLib.Net";
            this.hyperlinkLabelControl_Project.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(this.hyperlinkLabelControl_Project_HyperlinkClick);
            // 
            // simpleButton_Exit
            // 
            this.simpleButton_Exit.Location = new System.Drawing.Point(1092, 442);
            this.simpleButton_Exit.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton_Exit.Name = "simpleButton_Exit";
            this.simpleButton_Exit.Size = new System.Drawing.Size(162, 32);
            this.simpleButton_Exit.StyleController = this.layoutControl1;
            this.simpleButton_Exit.TabIndex = 8;
            this.simpleButton_Exit.Text = "Exit";
            this.simpleButton_Exit.Click += new System.EventHandler(this.simpleButton_Exit_Click);
            // 
            // simpleButton_Generate
            // 
            this.simpleButton_Generate.Enabled = false;
            this.simpleButton_Generate.Location = new System.Drawing.Point(933, 442);
            this.simpleButton_Generate.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton_Generate.Name = "simpleButton_Generate";
            this.simpleButton_Generate.Size = new System.Drawing.Size(143, 32);
            this.simpleButton_Generate.StyleController = this.layoutControl1;
            this.simpleButton_Generate.TabIndex = 7;
            this.simpleButton_Generate.Text = "Generate";
            this.simpleButton_Generate.Click += new System.EventHandler(this.simpleButton_Generate_Click);
            // 
            // memoEdit_Log
            // 
            this.memoEdit_Log.Location = new System.Drawing.Point(12, 84);
            this.memoEdit_Log.Margin = new System.Windows.Forms.Padding(4);
            this.memoEdit_Log.Name = "memoEdit_Log";
            this.memoEdit_Log.Size = new System.Drawing.Size(1242, 354);
            this.memoEdit_Log.StyleController = this.layoutControl1;
            this.memoEdit_Log.TabIndex = 6;
            // 
            // buttonEdit_OutputDirectory
            // 
            this.buttonEdit_OutputDirectory.Location = new System.Drawing.Point(162, 48);
            this.buttonEdit_OutputDirectory.Name = "buttonEdit_OutputDirectory";
            this.buttonEdit_OutputDirectory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit_OutputDirectory.Size = new System.Drawing.Size(1092, 32);
            this.buttonEdit_OutputDirectory.StyleController = this.layoutControl1;
            this.buttonEdit_OutputDirectory.TabIndex = 5;
            this.buttonEdit_OutputDirectory.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit_OutputDirectory_ButtonClick);
            this.buttonEdit_OutputDirectory.EditValueChanged += new System.EventHandler(this.buttonEdit_OutputDirectory_EditValueChanged);
            // 
            // buttonEdit_InputFile
            // 
            this.buttonEdit_InputFile.Location = new System.Drawing.Point(162, 12);
            this.buttonEdit_InputFile.Name = "buttonEdit_InputFile";
            this.buttonEdit_InputFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit_InputFile.Size = new System.Drawing.Size(1092, 32);
            this.buttonEdit_InputFile.StyleController = this.layoutControl1;
            this.buttonEdit_InputFile.TabIndex = 4;
            this.buttonEdit_InputFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit_InputFile_ButtonClick);
            this.buttonEdit_InputFile.EditValueChanged += new System.EventHandler(this.buttonEdit_InputFile_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1266, 486);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.buttonEdit_InputFile;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1246, 36);
            this.layoutControlItem1.Text = "Input File:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(138, 25);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.buttonEdit_OutputDirectory;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 36);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1246, 36);
            this.layoutControlItem2.Text = "Output Directory:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(138, 25);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.memoEdit_Log;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1246, 358);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.simpleButton_Generate;
            this.layoutControlItem4.Location = new System.Drawing.Point(921, 430);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(147, 36);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.simpleButton_Exit;
            this.layoutControlItem5.Location = new System.Drawing.Point(1080, 430);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(166, 36);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(382, 430);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(539, 36);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(1068, 430);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(12, 36);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.hyperlinkLabelControl_Project;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 430);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(382, 36);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
           
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 486);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("MainFrom.IconOptions.SvgImage")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "MainFrom";
            this.Text = "AHeadLib.Net v1.0.0.0";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Log.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit_OutputDirectory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit_InputFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit_OutputDirectory;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit_InputFile;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Exit;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Generate;
        private DevExpress.XtraEditors.MemoEdit memoEdit_Log;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.HyperlinkLabelControl hyperlinkLabelControl_Project;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}

