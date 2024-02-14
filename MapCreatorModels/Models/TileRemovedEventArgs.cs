using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreatorModels.Models
{
    public class TileRemovedEventArgs : EventArgs
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
