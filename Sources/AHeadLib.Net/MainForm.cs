using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.CSharp;

namespace AHeadLib.Net
{
    public partial class MainFrom : DevExpress.XtraEditors.XtraForm
    {
        public MainFrom()
        {
            InitializeComponent();

           // buttonEdit_InputFile.Text = "C:\\Windows\\System32\\winmm.dll";
           // buttonEdit_OutputDirectory.Text = "E:\\Desktop\\New Folder";
        }

        private void buttonEdit_InputFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(xtraOpenFileDialog_OpenInputFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            buttonEdit_InputFile.Text = xtraOpenFileDialog_OpenInputFile.FileName;
        }

        private void buttonEdit_OutputDirectory_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(xtraFolderBrowserDialog_OpenOutputDirectory.ShowDialog() != DialogResult.OK) 
            {
                return;
            }

            buttonEdit_OutputDirectory.Text = xtraFolderBrowserDialog_OpenOutputDirectory.SelectedPath;
        }

        private void RefershGenerateButtonState()
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

            VSProjectGenerator generator = new VSProjectGenerator(buttonEdit_OutputDirectory.Text, Path.GetFileName(buttonEdit_InputFile.Text), exportNames);

            generator.Write();

            Log("Write Finished.");
        }

        private void buttonEdit_InputFile_EditValueChanged(object sender, EventArgs e)
        {
            RefershGenerateButtonState();
        }

        private void buttonEdit_OutputDirectory_EditValueChanged(object sender, EventArgs e)
        {
            RefershGenerateButtonState();
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
