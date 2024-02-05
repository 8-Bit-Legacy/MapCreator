﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MapCreatorModels.Models.Assets
{
    public abstract class Asset
    {
        [JsonInclude]
        public byte Id { get; internal set; }

        [JsonInclude]
        public string Name { get; set; }
        
        [JsonInclude]
        public Texture Texture { get; protected set; }

        [JsonConstructor]
        protected Asset() { }

        protected Asset(byte id, string name)
        {
            Id = id;
            Name = name;
            Texture = new Texture();
        }

        protected Asset(byte id, string name, Texture texture)
        {
            Id = id;
            Name = name;
            Texture = texture;
        }
    }
}
