using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPLApplication
{
    /// <summary>
    /// to provide shape of objects
    /// </summary>
    public interface IShapes
    {
        /// <summary>
        /// used to set color and values of the shapes
        /// </summary>
        /// <param name="c">define the color</param>
        /// <param name="list">list of parameters that will be passed inside the function</param>
        void set(Color c, params int[] list);

        /// <summary>
        /// used to draw shape of the object
        /// </summary>
        /// <param name="g"></param>
        void draw(Graphics g);
    }
}
