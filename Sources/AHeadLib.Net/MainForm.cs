using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Dialogs;

// ReSharper disable InvertIf

namespace AHeadLib.Net
{
    public partial class MainFrom : XtraForm
    {
        public MainFrom()
        {
            InitializeComponent();

#if DEBUG
            // for test only...
            // ReSharper disable once StringLiteralTypo
            buttonEdit_InputFile.Text = @"C:\Windows\System32\winmm.dll";
            buttonEdit_OutputDirectory.Text = @"E:\Desktop\New Folder";

            try
            {
                if (!Directory.Exists(buttonEdit_OutputDirectory.Text))
                {
                    Directory.CreateDirectory(buttonEdit_OutputDirectory.Text);
                }
            }
            catch
            {
                // ignored
            }
#endif
            var filePath = Assembly.GetExecutingAssembly().Location;

            var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);

            // ReSharper disable once VirtualMemberCallInConstructor
            Text = $@"AHeadLib.Net v{fileVersionInfo.FileVersion}";
        }

        private void buttonEdit_InputFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select your dll file";
            dlg.IsFolderPicker = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                buttonEdit_InputFile.Text = dlg.FileName;    
            }
        }

        private void buttonEdit_OutputDirectory_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select your export folder";
            dlg.IsFolderPicker = true;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                buttonEdit_OutputDirectory.Text = dlg.FileName;    
            }
        }

        private void RefreshGenerateButtonState()
        {
            simpleButton_Generate.Enabled = 
                !string.IsNullOrEmpty(buttonEdit_InputFile.Text) && File.Exists(buttonEdit_InputFile.Text) &&
                !string.IsNullOrEmpty(buttonEdit_OutputDirectory.Text) && Directory.Exists(buttonEdit_OutputDirectory.Text);
        }

        private void simpleButton_Generate_Click(object sender, EventArgs e)
        {
            if(!File.Exists(buttonEdit_InputFile.Text))
            {
                XtraMessageBox.Show("Input file is not exists.");
                return;
            }

            if(!Directory.Exists(buttonEdit_OutputDirectory.Text))
            {
                XtraMessageBox.Show("Output Directory is not exists.");
                return;
            }

            var exportNames = DllExportInfo.ReadFromFile(buttonEdit_InputFile.Text);

            if(exportNames == null)
            {
                XtraMessageBox.Show("Failed get export table.");
                return;
            }

            var names = exportNames.ToList();
            names.RemoveAll(x =>
            {
                if (!SyntaxFacts.IsValidIdentifier(x) || x.Contains("@"))
                {
                    Log($"Skip symbol:{x}");

                    return true;
                }

                return false;
            });

            exportNames = names;

            var generator = new VsProjectGenerator(buttonEdit_OutputDirectory.Text, Path.GetFileName(buttonEdit_InputFile.Text), exportNames);

            generator.Write();

            Log("Write Finished.");
        }

        private void buttonEdit_InputFile_EditValueChanged(object sender, EventArgs e)
        {
            RefreshGenerateButtonState();
        }

        private void buttonEdit_OutputDirectory_EditValueChanged(object sender, EventArgs e)
        {
            RefreshGenerateButtonState();
        }

        private void Log(string message)
        {
            memoEdit_Log.AppendText(message + Environment.NewLine);  
        }

        private void simpleButton_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void hyperlinkLabelControl_Project_HyperlinkClick(object sender, DevExpress.Utils.HyperlinkClickEventArgs e)
        {
            Process.Start("https://github.com/bodong1987/AHeadLib.Net");
        }
    }
}
