using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPLApplication
{
    /// <summary>
    /// Declare class as ShapesFactory
    /// </summary>
    class ShapesFactory : ShapesCreator
    {
        /// <summary>
        /// Required for passing the shape of the objects
        /// </summary>
        /// <param name="ShapeType">Shape parameter</param>
        /// <returns></returns>
        public override IShapes getShape(string ShapeType)
        {
            ShapeType = ShapeType.ToLower().Trim();
            if (ShapeType.Equals("circle"))
            {
                return new Circle();
            }
            else if (ShapeType.Equals("rectangle"))
            {
                return new Rectangle();
            }

            else if (ShapeType.Equals("triangle"))
            {
                return new Triangle();
            }

            else
            {
                //throw an an exception when the shape provided does not exist
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + ShapeType + " does not exist");
                throw argEx;
            }
        }
    }
}
