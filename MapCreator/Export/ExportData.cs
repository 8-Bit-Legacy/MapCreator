using MapCreatorModels.Models;
using MapCreatorModels.Models.Assets;
using MapCreatorModels.Models.Assets.AssetsFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MapCreator.Export
{
    public static class ExportData
    {
        const int TILE_BIT_AMOUNT = 4;
        const int ACTOR_BIT_AMOUNT = 4;
        const int COLOR_BIT_AMOUNT = 4;


        public static void ExportMap(Map map, string folderPath)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    // Une tile est un asset
                    Asset asset = map.getMapTile(j, i).Tile;

                    sb.Append(ByteToString(asset.Id, TILE_BIT_AMOUNT));
                }
                sb.Append("\r\n");
            }
            string allo = sb.ToString();
            System.IO.File.WriteAllText(folderPath + "\\Map.txt", allo);
        }

        public static string ExportColisionMap(Map map, string folderPath)
        {
            StringBuilder sb = new StringBuilder();
            //La map de colision sera utile pour les collisions dans le cpu
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    // Une tile est un asset
                    Tile tile = map.getMapTile(j, i).Tile;
                    sb.Append(Convert.ToString(tile.isCollision));
                }
                sb.Append("\r\n");
            }
            System.IO.File.WriteAllText(folderPath + "\\MapColision.txt", sb.ToString());
            return "Exported Colision Map";
        }

        public static void ExportActors(ActorFactory actorFactory, string folderPath)
        {
            StringBuilder sb = new StringBuilder();
            Asset[] assets = actorFactory.GetObservableCollection().ToArray();
            for (int i = 0; i < assets.Length; i++)
            {
                sb.Append(getTextureAsExport(assets[i].Texture));
                sb.Append("\r\n");
            }
            System.IO.File.WriteAllText(folderPath + "\\ActorTextures.txt", sb.ToString());
        }

        public static void ExportTiles(TileFactory tileFactory, string folderPath)
        {
            StringBuilder sb = new StringBuilder();
            Asset[] assets = tileFactory.GetObservableCollection().ToArray();
            for (int i = 0; i < assets.Length; i++)
            {
                sb.Append(getTextureAsExport(assets[i].Texture));
                sb.Append("\r\n");
            }
            System.IO.File.WriteAllText(folderPath + "\\TileTextures.txt", sb.ToString());
        }

        public static void ExportColors(string folderPath)
        {
            StringBuilder sb = new StringBuilder();
            GameColor[] colors = GameColorList.GetColors();

            for (int i = 0; i < colors.Length; i++)
            {
                sb.Append(colors[i].Id + " ");
                sb.Append(colors[i].ColorValue.ToString("x6"));
                sb.Append("\r\n");
            }

            System.IO.File.WriteAllText(folderPath + "\\colors.txt", sb.ToString());
        }
        /// <summary>
        /// Convert a byte to a string of a certain amount of bits
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitAmount"></param>
        /// <returns></returns>
        private static string ByteToString(byte value, int bitAmount)
        {
            return Convert.ToString(value, 2).PadLeft(4, '0');
        }

        private static string getTextureAsExport(Texture texture)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Texture.Height; i++)
            {
                for (int j = 0; j < Texture.Width; j++)
                {
                    // 4 bits pour chaque couleur
                    stringBuilder.Append(ByteToString(texture.GetColor(j, i).Id, 4));
                }
            }
            return stringBuilder.ToString();
        }
    }
}
