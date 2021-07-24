using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPLApplication
{
    /// <summary>
    /// Declare class as triangle and implement the interface
    /// </summary>
    public class Triangle : IShapes
    {
        /// <summary>
        /// Inetger values for the triangle sides
        /// </summary>

        public int xcordinate1, ycordinate1, xcordinate2, ycordinate2, xcordinate3, ycordinate3, xcordinate4, ycordinate4, xcordinate5, ycordinate5, xcordinate6, ycordinate6;
       
        /// <summary>
        /// Draw the triangular shape as per the sides provided
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.RosyBrown, 2);
                g.DrawLine(p, xcordinate1, ycordinate1, xcordinate2, ycordinate2);
                g.DrawLine(p, xcordinate3, ycordinate3, xcordinate4, ycordinate4);
                g.DrawLine(p, xcordinate5, ycordinate5, xcordinate6, ycordinate6);
            }
            catch (Exception ex)
            {
                // Throw appropraite error message
                //throw ex;
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Set values for different sides of the traingle 
        /// </summary>
        /// <param name="c">Color</param>
        /// <param name="list">List of parameters</param>
        public void set(Color c, params int[] list)
        {
            this.xcordinate1 = list[0];
            this.ycordinate1 = list[1];
            this.xcordinate2 = list[2];
            this.ycordinate2 = list[3];

            this.xcordinate3 = list[4];
            this.ycordinate3 = list[5];
            this.xcordinate4 = list[6];
            this.ycordinate4 = list[7];

            this.xcordinate5 = list[8];
            this.ycordinate5 = list[9];
            this.xcordinate6 = list[10];
            this.ycordinate6 = list[11];
        }
    }
}
