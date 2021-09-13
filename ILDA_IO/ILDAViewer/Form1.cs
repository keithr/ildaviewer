using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILDAViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            idlaViewerControl1.CurrentFrameChangeEvent += OnCurrentFrameChange;
            idlaViewerControl1.TotalFrameChangeEvent += OnTotalFrameChange;
            CurrentFrameLabel.Text = "0";
            TotalFramesLabel.Text = "0";
        }

        private void OnCurrentFrameChange(int value)
        {
            if (idlaViewerControl1.TotalFrames == 0) return;
            int current = value % idlaViewerControl1.TotalFrames;
            CurrentFrameLabel.Text = current.ToString();
        }

        private void OnTotalFrameChange(int value)
        {
            TotalFramesLabel.Text = value.ToString();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "ILDA files (*.ild)|*.ild|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                idlaViewerControl1.Filename = openFileDialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            idlaViewerControl1.CurrentFrame -= 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            idlaViewerControl1.CurrentFrame += 1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            idlaViewerControl1.Animate = checkBox1.Checked;
        }
    }

}
