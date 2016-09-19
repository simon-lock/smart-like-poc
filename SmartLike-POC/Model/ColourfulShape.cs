using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLike_POC.Model
{
    public class ColourfulShape
    {
        public ColourfulShape(Colour colour, Shape shape)
        {
            Colour = colour;
            Shape = shape;
        }

        public Colour Colour { get; private set; }
        public Shape Shape { get; private set; }

    }
}
