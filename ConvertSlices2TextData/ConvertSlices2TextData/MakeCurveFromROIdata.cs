using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertSlices2TextData
{
    /// <summary>
    /// Converts 1d list of UInt16 data to the profile (curve). Takes Size-X and Size-Y parameters of the data ROI. 
    /// Data size must be Size-X * Size-Y. The bigger of Size-X, Size-Y is considered as a profile resolved dimension, 
    /// the other dimension is averaged.
    /// </summary>
    public class MakeCurveFromROIdata
    {
        // Number of points in the resolved dimension
        private Int32 NresDimPoints;
        // Number of points to average in the transverse ROI dimension
        private Int32 NavDimPoints;
        // Scale X
        private double fovx;
        // Scale Y
        private double fovy;
        // Curve data
        private List<double> curve;
        // Resolved dimension points
        private List<double> dimpoints;

        private string coord;

        // Properties
        public int Npoints
        {
            get
            {
                return NresDimPoints;
            }
        }

        public double FOVX
        {
            get
            {
                return fovx;
            }
            set
            {
                fovx = value;
            }
        }

        public double FOVY
        {
            get
            {
                return fovy;
            }
            set
            {
                fovy = value;
            }
        }

        public List<double> Curve
        {
            get
            {
                return curve;
            }
        }

        public List<double> Dimpoints
        {
            get
            {
                return dimpoints;
            }
        }

        public string Coord
        {
            get
            {
                return coord;
            }
        }

        // ctor
        public MakeCurveFromROIdata(List<UInt16> data, Int32 SizeX, Int32 SizeY)
        {
            curve = new List<double>();
            dimpoints = new List<double>();

            if (SizeX * SizeY == data.Count)
            {
                try
                {
                    if (SizeX >= SizeY)
                    {
                        NresDimPoints = SizeX;
                        NavDimPoints = SizeY;
                        coord = "X-profile";
                        for (int ix = 0; ix < NresDimPoints; ix++)
                        {
                            double v = 0;
                            for (int iy = 0; iy < NavDimPoints; iy++)
                                v += data.ElementAt(ix + iy * NresDimPoints);
                            curve.Add(v / NavDimPoints);
                            dimpoints.Add(ix);
                        }
                    }
                    else
                    {
                        NresDimPoints = SizeY;
                        NavDimPoints = SizeX;
                        coord = "Y-profile";
                        for (int iy = 0; iy < NresDimPoints; iy++)
                        {
                            double v = 0;
                            for (int ix = 0; ix < NavDimPoints; ix++)
                                v += data.ElementAt(iy * NavDimPoints + ix);
                            curve.Add(v / NavDimPoints);
                            dimpoints.Add(iy);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating curve. Original error: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Data size != SizeX * SizeY");
            }

        } // ctor

        public void ScaleDimAxis()
        {
            if (coord == "X-profile" && fovx != 0)
            {
                dimpoints.Clear();
                for (int ix = 0; ix < NresDimPoints; ix++)
                    dimpoints.Add(-0.5 * fovx + ix * fovx / NresDimPoints);
            }
            else if (coord =="Y-profile" && fovy != 0)
            {
                dimpoints.Clear();
                for (int iy = 0; iy < NresDimPoints; iy++)
                    dimpoints.Add(-0.5 * fovy + iy * fovy / NresDimPoints);
            }
        }

    }
}
