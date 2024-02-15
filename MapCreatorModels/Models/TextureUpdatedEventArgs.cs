using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapCreatorModels.Models.Assets;

namespace MapCreatorModels.Models
{
    public class TextureUpdatedEventArgs : EventArgs
    {
        public Texture Texture { get; set; }
    }
}
