using MapCreatorModels.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreatorModels.Models
{
    internal class AssetRemovedEventArgs : EventArgs
    {
        public Asset Asset { get; set; }
    }
}
