using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace FactoryInventorySoftware
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        /*PrintDocument pd = new PrintDocument();
        PrintPreviewDialog printPreviewDlg = new PrintPreviewDialog();
        printPreviewDlg.Document = pd;
        printPreviewDlg.ShowDialog();*/

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;
        private void CaptureScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            printPreviewDialog1.ShowDialog();
        }
       /* private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
           
               
           
        }
        */
        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

       
    }
}
