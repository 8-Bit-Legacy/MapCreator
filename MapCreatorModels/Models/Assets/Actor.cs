
using System.Text.Json.Serialization;

namespace MapCreatorModels.Models.Assets
{
    /// <summary>
    /// Les acteurs sont des element non utilisable pour la construction de la map
    /// Il sont des elements qui peuvent bouger et interagir avec le joueur dans le jeux.
    /// Ils peuvent aussi être des lettres utilisé pour générer un overlay dans le futur.
    /// </summary>
    public class Actor : Asset
    {
        [JsonConstructor]
        private Actor() : base() { }
        internal Actor(byte id, string name) : base(id, name) { }
        internal Actor(byte id, string name, Texture texture) : base(id, name, texture) { }
    }
}
