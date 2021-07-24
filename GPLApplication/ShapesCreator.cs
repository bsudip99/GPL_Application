using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPLApplication
{
    /// <summary>
    /// Declare class name as ShapesCreator
    /// </summary>
    abstract class ShapesCreator
    {
        /// <summary>
        /// Used to pass shape of any objects
        /// </summary>
        /// <param name="ShapeType">Shape parameter</param>
        /// <returns></returns>
      public abstract IShapes getShape(string ShapeType);
    }
}
