using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPLApplication
{
    /// <summary>
    /// Provide shape of the object
    /// </summary>
    abstract class Shapes : IShapes
    {
        protected Color colour; 
        protected int x, y;
        public Shapes()
        {
            colour = Color.Red;
            x = y = 100;
        }

        /// <summary>
        /// Set color, x-axis and y-axis values
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Shapes(Color colour, int x, int y)
        {
          

            this.colour = colour;
            this.x = x;
            this.y = y;

        }
        /// <summary>
        /// Used as drawing shape of any object
        /// </summary>
        /// <param name="g"></param>
        public abstract void draw(Graphics g);

        /// <summary>
        /// Used to set values for different shapes
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="list"></param>
        public virtual void set(Color colour, params int[] list)
        {
            this.colour = colour;
            this.x = list[0];
            this.y = list[1];
        }

        /// <summary>
        /// Used to overwrite the values
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }
    }
}
