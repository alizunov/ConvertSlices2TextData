using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertSlices2TextData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenImages_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd1 = new OpenFileDialog();
            opd1.Filter = "Header files (*.imgh)|*.imgh|All files (*.*)|*.*";
            opd1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            opd1.Multiselect = true;
            opd1.Title = "Browse multiple Image/Header";

            if (opd1.ShowDialog() == DialogResult.OK)
            {
                // Directory of the data files
                string DataDir = "";
                // String List containing header text readout from the selected file.
                List<string> TextReadout = new List<string>();

                // Double list containing image frame delays in ms:
                // delay =  Internal_delay + GDT_delay, where
                // Internal_delay = string #16
                // GDT_delay = string #17
                List<double> Delays = new List<double>();

                // Image size-X: strings #9, 10
                double SizeX = 0;
                // Image size-Y: strings #11, 12
                double SizeY = 0;

                foreach(string HeaderFile in opd1.FileNames)
                {
                    try
                    {
                        System.IO.StreamReader sr = System.IO.File.OpenText(HeaderFile);
                        DataDir = System.IO.Path.GetDirectoryName(HeaderFile);
                        TextReadout.Clear();
                        // Read the image header
                        string str = "";
                        while ((str = sr.ReadLine()) != null)
                            TextReadout.Add(str);
                        sr.Close();
                        Delays.Add(Convert.ToDouble(TextReadout.ElementAt(16)) + Convert.ToDouble(TextReadout.ElementAt(17)));
                        SizeX = Convert.ToDouble(TextReadout.ElementAt(10)) - Convert.ToDouble(TextReadout.ElementAt(9)) + 1;
                        SizeY = Convert.ToDouble(TextReadout.ElementAt(12)) - Convert.ToDouble(TextReadout.ElementAt(11)) + 1;
                        // Read the image, the file name = the header file name with the last symbol cut (.imgh -> .img)
                        string ImageFileName = HeaderFile.Substring(0, HeaderFile.Length - 1);
                        System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.Open(ImageFileName, System.IO.FileMode.Open));
                        // Image data
                        List<UInt16> ImageData = new List<UInt16>();
                        while (br.BaseStream.Position != br.BaseStream.Length)
                            ImageData.Add(br.ReadUInt16());
                        br.Close();
                        MessageBox.Show("Image samples read: " + (ImageData.Count()-4).ToString() + ", SizeX*SizeY: " + (SizeX*SizeY).ToString() );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cannot open header/image file. Original error: " + ex.Message);
                    }
                }
                // Write the timing file
                try
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(DataDir + "\\timing.txt");
                    foreach (double del in Delays)
                        sw.Write(del + "\n");
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot create the file for timing entries. Original error: " + ex.Message);
                }

            }

        }
    }
}
