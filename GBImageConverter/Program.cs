using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBImageConverter
{
    public enum OutputFormats { BIN, RGBASM, GBDK_C, BITMAP }
    public static class Config
    {
        public static string[] args;
        public static string ImagePath = "";
        public static OutputFormats OutputFormat = OutputFormats.RGBASM;

        public static string OutputTilesFilename = "tiles";
        public static string OutputMapFilename = "map";

        public static bool PrependTilecountByte = true;

        public static bool OutputMetaTileData = false;
        public static int MetaTileWidth = 2;
        public static int MetaTileHeight = 2;

        // If meta tiles in the source image are visually aligned but need to be consecutive in data
        public static bool SortMetaTilesConsecutively = true;
        public static bool DebugStringAsmOutput = false;
        public static bool KeepDuplicateTiles = false;
    }

    class Program
    {
        static void ProcessArgs(string[] args)
        {
            Config.args = args;

            if (args.Length > 0)
            {
                Config.ImagePath = args[0];
            }

            if (args.Length > 1)
            {
                Config.OutputTilesFilename = args[1];
            }

            if (args.Length > 2)
            {
                Config.OutputMapFilename = args[2];
            }

            string result = "";

            Config.DebugStringAsmOutput = GetFlagValueFromArgs("debugasm");
            Config.KeepDuplicateTiles = GetFlagValueFromArgs("keepduplicates");

            if (GetFlagValueFromArgs("metatiles2x2"))
            {
                Config.MetaTileHeight = 2;
                Config.MetaTileWidth = 2;
                Config.SortMetaTilesConsecutively = true;
                Config.OutputMetaTileData = true;
            }

            if (GetFlagValueFromArgs("sprites1x2"))
            {
                Config.MetaTileHeight = 2;
                Config.MetaTileWidth = 1;
                Config.SortMetaTilesConsecutively = true;
                Config.OutputMetaTileData = true;
            }
        }

        static bool GetFlagValueFromArgs(string varName)
        {
            for (int i = 0; i < Config.args.Length; i++)
            {
                if(Config.args[i].ToLowerInvariant().Contains(varName.ToLowerInvariant()))
                {
                    return true;
                }
            }

            return false;
        }

        static void OutputTilesInSequence(System.Drawing.Bitmap bitmap, int[,] twobpp_values, bool includeMapSequence = false)
        {
            int tiles_x = bitmap.Width / 8;
            int tiles_y = bitmap.Height / 8;

            // 1D array (width first) of GBTiles
            //GBTile[] gb_tiles = new GBTile[tiles_x * tiles_y];
            List<GBTile> tile_list = new List<GBTile>();
            Dictionary<int, int> tile_indexes = new Dictionary<int, int>();
            int tileIdx = 0;
            int tileNum = 0;

            List<int> tile_map = new List<int>();

            for (int ty = 0; ty < tiles_y; ty++)
            {
                for (int tx = 0; tx < tiles_x; tx++)
                {
                    // grab the 8x8 chunk for this gbtile and populate it
                    int tile = (ty * tiles_x) + tx;
                    GBTile gbtile = new GBTile();
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            // todo: maintain a map of tile locations on the image, to generated tiles, and
                            // skip duplicate tiles

                            int bmp_y = ty * 8 + y;
                            int bmp_x = tx * 8 + x;

                            int color = twobpp_values[bmp_y, bmp_x];
                            gbtile.SetPixelColour((short)color, y, x);
                        }
                    }

                    if (Config.KeepDuplicateTiles)
                    {
                        // process directly without caring about mapping to duplicate tiles
                        tile_list.Add(gbtile);
                        tile_map.Add(tileIdx);
                        tileIdx++;
                        tileNum++;
                    }
                    else
                    {
                        if (!tile_list.Contains(gbtile))
                        {
                            tile_list.Add(gbtile);
                            tile_indexes.Add(gbtile.GetHashCode(), tileIdx);

                            tileIdx++;
                        }

                        tile_map.Add(tile_indexes[gbtile.GetHashCode()]);
                        tileNum++;
                    }
                }
            }

            // serialize the map data
            GBImageUtils.SerializeGBTileDataWithMap(tile_list, tile_map);
            GBImageUtils.SerializeGBTileDataBinary(tile_list, Config.OutputTilesFilename);
        }

        static void OutputMetaTileSequence(System.Drawing.Bitmap bitmap, int[,] twobpp_values)
        {
            //this assumes input tiles are laid out visually EG
            // in a grid of 4 wide, 2 high 
            // meta tile 1 would be tiles 0, 1, 4, 5
            // meta tile 2 would be tiles 2, 3, 6, 7
            // but they need to be output sequentially

            int metaTileWidth = Config.MetaTileWidth;
            int metaTileHeight = Config.MetaTileHeight;
            int tiles_x = bitmap.Width / (metaTileWidth *8);
            int tiles_y = bitmap.Height / (metaTileHeight * 8);

            // 1D array (width first) of GBTiles
            List<GBTile> tile_list = new List<GBTile>();

            int tileIdx = 0;
            int tileNum = 0;

            int metaTileIndex = 0;
            int metaTileSubIndex = 0;
            int metaTileSize = metaTileHeight * metaTileWidth;

            List<int> tile_map = new List<int>();

            for (int ty = 0; ty < tiles_y; ty++)
            {
                for (int tx = 0; tx < tiles_x; tx++)
                {
                    for(int i = 0; i < metaTileSize; i++)
                    {
                        // grab the 8x8 chunk for this sub tile and populate it
                        // todo, genericize this, for now assume 2x2 size meta tiles

                        int y_offset = 0;
                        int x_offset = 0;

                        if (metaTileSize == 4)
                        {
                            y_offset = (metaTileSubIndex == 2 || metaTileSubIndex == 3) ? 8 : 0;
                            x_offset = (metaTileSubIndex == 1 || metaTileSubIndex == 3) ? 8 : 0;
                        }
                        else // should
                        {
                            y_offset = (metaTileSubIndex == 1) ? 8 : 0;
                        }

                        GBTile gbtile = new GBTile();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                int bmp_y = (ty * (metaTileHeight*8)) + y + y_offset;
                                int bmp_x = (tx * (metaTileWidth*8)) + x + x_offset;

                                int color = twobpp_values[bmp_y, bmp_x];
                                gbtile.SetPixelColour((short)color, y, x);
                            }
                        }

                        tile_list.Add(gbtile);

                        tileIdx++;
                        metaTileSubIndex++;

                        if (metaTileSize < metaTileSubIndex)
                        {
                            metaTileSubIndex = 0;
                            metaTileIndex++;
                        }

                        tileNum++;
                    }
                }
            }

            // May need to prepend the serialized dats with the number and size of meta tiles?
            // todo

            // serialize tiles on their own
            GBImageUtils.SerializeGBMetaTileData(tile_list.ToArray(), Config.OutputTilesFilename);
            GBImageUtils.SerializeGBTileDataBinary(tile_list, Config.OutputTilesFilename);
        }
        

        static void Main(string[] args)
        {
            Console.WriteLine("GBImg Converter - Args:");
            for(int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i, args[i]);
            }

            if (args.Length > 0)
            {
                ProcessArgs(args);
                Console.WriteLine("Processing Image '{0}'", Config.ImagePath);
                System.Drawing.Bitmap bitmap = ImageLoader.LoadImage(Config.ImagePath);

                if (bitmap != null)
                {
                    Console.WriteLine("Image loaded successfully.");

                    if(bitmap.Width % 8 != 0 || bitmap.Height % 8 != 0)
                    {
                        Console.WriteLine("Image width or height not divisible by 8, rejecting");
                        return;
                    }

                    int[,] greyscaleImage = GBImageUtils.GetLuminosityValuesFromImage(ref bitmap);
                    

                    int[,] twobpp_values = GBImageUtils.LuminosityTo2BPPColour(greyscaleImage, bitmap.Width, bitmap.Height);

                    if(Config.OutputMetaTileData)
                    {
                        Console.WriteLine("Outputting MetaTile sequence");
                        OutputMetaTileSequence(bitmap, twobpp_values);
                    }
                    else
                    {
                        //output single image with map format
                        Console.WriteLine("Outputting Tiles with Tilemap sequence");
                        bool outputMapData = true;
                        OutputTilesInSequence(bitmap, twobpp_values, outputMapData);
                    }

                    

                    
                    bitmap.Dispose();
                }
                else
                {
                    Console.WriteLine("Image load error.");
                }
            }

            Console.WriteLine("Exiting.");
            return;
        }

        
    }
}
