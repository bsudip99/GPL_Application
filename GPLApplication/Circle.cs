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
    /// holds commands of the class: circle
    /// </summary>
    public class Circle : IShapes
    {
        /// <summary>
        /// used to get integer values for circle 
        /// </summary>
        public int x, y, radius;

        public Circle() : base()
        {
        }

        /// <summary>
        /// used to pass values of the circle
        /// </summary>
        /// <param name="x">used to set x-cordinate value</param>
        /// <param name="y">used to set y-cordinate value</param>
        /// <param name="radius">used to set radius value for circle</param>
        public Circle(int x, int y, int radius)
        {
            this.radius = radius;
        }

        /// <summary>
        /// used to draw circle on the output panel
        /// </summary>
        /// <param name="g"></param>
        public void draw(Graphics g)
        {
            try
            {
                Pen p = new Pen(Color.Aquamarine, 2);
                g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
            }
            catch (Exception ex)
            {

                //throw ex;
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// used to define color and set values for x, y and radius
        /// </summary>
        /// <param name="c">used to define color for circle</param>
        /// <param name="list">list of parameters that will be passed inside the function</param>

        public void set(Color c, params int[] list)
        {
            try
            {
                this.x = list[0];
                this.y = list[1];
                this.radius = list[2];
            }
            catch (Exception ex)
            {

                //throw ex;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
