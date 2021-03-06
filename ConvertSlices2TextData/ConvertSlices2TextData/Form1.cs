﻿using System;
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

            FOVX_numericUpDown.Minimum = 0;
            FOVX_numericUpDown.Maximum = 1000;
            FOVY_numericUpDown.Minimum = 0;
            FOVY_numericUpDown.Maximum = 1000;
            FOVX_numericUpDown.Value = 295;
            FOVY_numericUpDown.Value = 255;
            SubROIY_comboBox.Items.AddRange(new object[] { "Full size", "10 px", "20 px" });
            SubROIY_comboBox.SelectedIndex = 2;
        }

        private void OpenImages_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd1 = new OpenFileDialog();
            opd1.Filter = "Header files (*.imgh)|*.imgh|All files (*.*)|*.*";
            //opd1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
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
                Int32 SizeX = 0;
                // Image size-Y: strings #11, 12
                Int32 SizeY = 0;

                // Height of sub-ROI-Y for calculation of emission profile in the beam edge (top Sub-ROI) and closer to the beam axis (bottom Sub-ROI)
                string SubROI_str = SubROIY_comboBox.SelectedItem.ToString();
                Int32 SubROI = 0;
                try
                {
                    if (SubROI_str.Substring(0, 4) != "Full")
                        SubROI = Convert.ToInt32(SubROI_str.Substring(0, 2));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot read Sub-ROI Y-size. Original error: " + ex.Message);
                }

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
                        SizeX = Convert.ToInt32(TextReadout.ElementAt(10)) - Convert.ToInt32(TextReadout.ElementAt(9)) + 1;
                        SizeY = Convert.ToInt32(TextReadout.ElementAt(12)) - Convert.ToInt32(TextReadout.ElementAt(11)) + 1;
                        // Read the image, the file name = the header file name with the last symbol cut (.imgh -> .img)
                        string ImageFileName = HeaderFile.Substring(0, HeaderFile.Length - 1);
                        System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.Open(ImageFileName, System.IO.FileMode.Open));
                        // Image data
                        List<UInt16> ImageData = new List<UInt16>();
                        while (br.BaseStream.Position != br.BaseStream.Length)
                            ImageData.Add(br.ReadUInt16());
                        br.Close();
                        int ReadoutLength = ImageData.Count;
                        //MessageBox.Show("Image samples read: " + (ImageData.Count()-4).ToString() + ", SizeX*SizeY: " + (SizeX*SizeY).ToString() );

                        // Remove 4 last elements
                        ImageData.RemoveRange(ReadoutLength - 5, 4);
                        double fovx = Convert.ToDouble(FOVX_numericUpDown.Value);
                        double fovy = Convert.ToDouble(FOVY_numericUpDown.Value);
                        // Create curve(s)
                        if (SubROI == 0) // Full size
                        {
                            MakeCurveFromROIdata crv = new MakeCurveFromROIdata(ImageData, SizeX, SizeY);
                            crv.FOVX = fovx;
                            crv.FOVY = fovy;
                            crv.ScaleDimAxis();
                            // Write point pairs to the text file (.txt)
                            string CurveFileName = ImageFileName.Substring(0, ImageFileName.Length - 3) + "txt";
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(CurveFileName);
                            sw.Write(crv.Coord + "\n");
                            for (int ip = 0; ip < crv.Npoints; ip++)
                            {
                                sw.Write(crv.Dimpoints.ElementAt(ip) + " " + crv.Curve.ElementAt(ip) + "\n");
                            }
                            sw.Close();
                        }
                        else
                        {
                            if (SizeX > SizeY) // X-profile
                            {
                                // Top sub-ROIY
                                MakeCurveFromROIdata crv1 = new MakeCurveFromROIdata(ImageData.GetRange(0, SizeX*SubROI), SizeX, SubROI);
                                crv1.FOVX = fovx;
                                crv1.FOVY = fovy*SubROI/SizeY;
                                crv1.ScaleDimAxis();
                                // Bottom sub-ROIY
                                MakeCurveFromROIdata crv2 = new MakeCurveFromROIdata(ImageData.GetRange(SizeX*SizeY - SizeX*SubROI, SizeX*SubROI), SizeX, SubROI);
                                crv2.FOVX = fovx;
                                crv2.FOVY = fovy*SubROI/SizeY;
                                crv2.ScaleDimAxis();
                                // Write point pairs to the text files (.txt)
                                string CurveFileName1 = ImageFileName.Substring(0, ImageFileName.Length - 4) + "-top.txt";
                                System.IO.StreamWriter sw1 = new System.IO.StreamWriter(CurveFileName1);
                                sw1.Write(crv1.Coord + "\n");
                                for (int ip = 0; ip < crv1.Npoints; ip++)
                                {
                                    sw1.Write(crv1.Dimpoints.ElementAt(ip) + " " + crv1.Curve.ElementAt(ip) + "\n");
                                }
                                sw1.Close();
                                string CurveFileName2 = ImageFileName.Substring(0, ImageFileName.Length - 4) + "-bottom.txt";
                                System.IO.StreamWriter sw2 = new System.IO.StreamWriter(CurveFileName2);
                                sw2.Write(crv2.Coord + "\n");
                                for (int ip = 0; ip < crv2.Npoints; ip++)
                                {
                                    sw2.Write(crv2.Dimpoints.ElementAt(ip) + " " + crv2.Curve.ElementAt(ip) + "\n");
                                }
                                sw2.Close();
                            }
                            else // Y-profile
                            {
                                MessageBox.Show("Y-profiles from two sub-ROIX: not implemented yet.");
                            }
                        }

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
                MessageBox.Show("Images processed: " + opd1.FileNames.Count() + ", sub-ROI mode: " + SubROI_str + " (sub-ROI size: " + SubROI.ToString() + ")" );
            }

        }

        private void FOVX_numericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }


        private void FOVY_numericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }
        private void FOVX_label_Click(object sender, EventArgs e)
        {

        }

        private void FOVY_label_Click(object sender, EventArgs e)
        {

        }

        private void SubROIY_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
