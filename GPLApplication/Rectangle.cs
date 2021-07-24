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
    /// Class is declared as rectangle and interface is implemented
    /// </summary>
    public class Rectangle : IShapes
    {
        /// <summary>
        /// Ineteger values for x-axis, y-axis, width and height of the rectangle
        /// </summary>
        public int x, y, width, height;
       

        /// <summary>
        /// Provides width and height of the rectangle 
        /// </summary>
        public Rectangle() : base()
        {
            width = 0;
            height = 0;
        }

        /// <summary>
        /// Pass integer values of x-axis, y-axis, width and height for the rectangle
        /// </summary>
        /// <param name="x">X-axis</param>
        /// <param name="y">Y-axis</param>
        /// <param name="width">Rectangle's width</param>
        /// <param name="height">Rectangle's height</param>
        public Rectangle(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Draw rectangular shape
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Red, 2);
                //SolidBrush b = new SolidBrush(Color.Aquamarine);
                //g.FillRectangle(b, x, y, width, height);
                g.DrawRectangle(p, x - (width / 2), y - (height / 2), width * 2, height * 2);
            }
            catch (Exception ex)
            {

                // throw ex;
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Set values of x-axis, y-axis, height and width
        /// </summary>
        /// <param name="c">Color</param>
        /// <param name="list">List of parameters</param>
        public void set(Color c, params int[] list)
        {
            try
            {
                this.x = list[0];
                this.y = list[1];
                this.width = list[2];
                this.height = list[3];
            }
            catch (Exception ex)
            {

                // throw ex;
                MessageBox.Show(ex.Message);
            }
        }
    }
        
      
}
