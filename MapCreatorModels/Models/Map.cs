
namespace MapCreatorModels.Models
{

    // #------------ <X
    // |
    // |
    // |
    // |
    // |
    // ^
    // Y
    public class Map
    {
        public static Tile[,] Tiles { get; private set; } = new Tile[64, 64];
        public static void SetTile(int x, int y, Tile tile)
        {
            Tiles[y, x] = tile;
        }
    }
}
