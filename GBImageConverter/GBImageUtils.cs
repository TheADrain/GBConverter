using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBImageConverter
{
    #region GB PALETTE CLASS
    public class GBPalette
    {
        // primarily for preview images for the moment, does not know how to handle real gameboy palettes

        Color col0;
        Color col1;
        Color col2;
        Color col3;

        public GBPalette()
        {
            // rough approx of 0 = lightest, 3 = darkest
            col0 = Color.FromArgb(224, 224, 224);
            col1 = Color.FromArgb(140, 140, 140);
            col2 = Color.FromArgb(90, 90, 90);
            col3 = Color.FromArgb(0, 0, 0);
        }

        public GBPalette(Color c0, Color c1, Color c2, Color c3)
        {
            col0 = c0;
            col1 = c1;
            col2 = c2;
            col3 = c3;
        }

        public Color GetPaletteColorFrom2BPPValue(int twobppvalue)
        {
            switch(twobppvalue)
            {
                case 0: return col0;
                case 1: return col1;
                case 2: return col2;
                case 3: return col3;
            }

            return col0;
        }

        public Color GetColor(int index)
        {
            switch(index)
            {
                case 0: return col0;
                case 1: return col1;
                case 2: return col2;
                case 3: return col3;
            }

            return col0;
        }
    }
    #endregion

    #region GB TILEMAP
    public class GBTileMap
    {
        public enum TilePlanes { TILE, COLLISION }

        private List<int> TilePlane = new List<int>();
        private List<int> CollisionPlane = new List<int>();
        // todo: collision plane
        private int _Width;
        private int _Height;

        public GBTileMap()
        {
        }
        public bool HasCollisionPlane() { return CollisionPlane != null && CollisionPlane.Count == TilePlane.Count; }
        public int TileCount() { return _Width * _Height; }
        public int Width() { return _Width; }
        public int Height() { return _Height; }
        public int GetTile(int atIndex, TilePlanes plane)
        {
            if(atIndex < 0 || atIndex >= TileCount())
            {
                return -1;
            }

            return plane == TilePlanes.TILE ? TilePlane[atIndex] : CollisionPlane[atIndex];
        }

        public void SetDimensions(int w, int h)
        {
            _Width = w;
            _Height = h;
        }

        public void AddTile(int tile)
        {
            TilePlane.Add(tile);

            // default to no collision, build collision plane after the main tile plane
            CollisionPlane.Add(0);
        }

        public bool SetTile(int x, int y, int tile, TilePlanes plane)
        {
            if(x < 0 || x >= _Width || y < 0 || y >= _Height)
            {
                return false;
            }

            int idx = (y * _Width) + x;
            if(idx >= 0 && idx < TilePlane.Count)
            {
                if (plane == TilePlanes.TILE)
                {
                    TilePlane[idx] = tile;
                }
                else if(plane == TilePlanes.COLLISION)
                {
                    CollisionPlane[idx] = tile;
                }
            }

            return true;
        }

        public int GetTile(int x, int y, TilePlanes plane)
        {
            int idx = (y * _Width) + x;
            if (idx >= 0 && idx < TilePlane.Count)
            {
                if (plane == TilePlanes.TILE)
                {
                    return TilePlane[idx];
                }
                else if(plane == TilePlanes.COLLISION)
                {
                    return CollisionPlane[idx];
                }
            }

            return -1;
        }

        public Bitmap GeneratePreview(GBTile[] tileData)
        {
            Bitmap bitmap = new Bitmap(_Width*8, _Height*8);
            int tileDataCount = tileData.Length;

            for (int y = 0; y < _Height; y++)
            {
                for (int x = 0; x < _Width; x++)
                {                    
                    int tileIdx = GetTile(x, y, TilePlanes.TILE);
                    int collisionData = GetTile(x, y, TilePlanes.COLLISION);
                    if(tileIdx > 0 && tileIdx < tileDataCount)
                    {
                        GBTile tile = tileData[tileIdx];
                        bool hasCollision = collisionData == 1;
                        PopulatePreviewTile(ref bitmap, x, y, ref tile, hasCollision);
                    }
                }
            }

            return bitmap;
        }

        private void PopulatePreviewTile(ref Bitmap bmp, int tx, int ty, ref GBTile tile, bool hasCollision)
        {
            GBPalette defaultPalette = new GBPalette();
            

            int px = tx * 8;
            int py = ty * 8;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Color defaultCol = defaultPalette.GetColor(tile.GetPixelColour(x, y));
                    // handle collision plane preview with a red tint
                    Color collisionCol = Color.FromArgb(byte.MaxValue, defaultCol.G, defaultCol.B);

                    bmp.SetPixel(px+x, py+y, hasCollision ? collisionCol : defaultCol);
                }
            }
        }
    }
    #endregion

    #region GBTILE CLASS
    public class GBTile : IEquatable<GBTile>
    {
        // colours 0 - 3
        // 8x8 grid
        private short[,] _pixels = new short[8,8];

        public GBTile()
        {
            for(int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    _pixels[y, x] = 0;
                }
            }
        }

        public override bool Equals(object obj)
        {
            GBTile other = (GBTile)obj;
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(_pixels[i,j] != other._pixels[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Equals(GBTile other)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_pixels[i, j] != other._pixels[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return BitConverter.ToString(GetTileBytes()).GetHashCode();
        }

        public void SetPixelColour(short colourIndex, int y, int x)
        {
            if(colourIndex < 0 || colourIndex > 3)
            {
                Console.WriteLine("Attempting to set invalid colour index ({0}), at coord ({1}, {2})", colourIndex, x, y);
                return;
            }

            _pixels[y, x] = colourIndex;
        }

        public int GetPixelColour(int x, int y)
        {
            return _pixels[y, x];
        }

        public byte[] GetTileBytes()
        {
            // 1 byte = 4 pixel row
            // 2 bytes per row
            byte[] bytes = new byte[16];

            for(int y = 0; y < 8; y++)
            {
                byte[] row = GetRowBytes(y);
                bytes[y * 2] = row[0];
                bytes[(y * 2) + 1] = row[1];
            }

            return bytes;
        }

        private byte[] GetRowBytes(int rowNum)
        {
            // rownum 0-7
            byte[] row = new byte[2];
            // 2 bits per pixel, 8 bits per row = 2 bytes
            // each pixel is the two corresponding bits from both bytes 
            // eg: pixel 3 = Bit 3 of Byte 1 and Bit 3 of Byte 2
            row[0] = 0;
            row[1] = 1;

            short[] pixels = new short[8];
            for(int i = 0; i < 8; i++)
            {
                pixels[i] = _pixels[rowNum, i];
            }

            for(int x = 0; x < 8; x++)
            {
                short color = pixels[x];

                switch(color)
                {
                    case 3:
                        // 11
                        ByteUtils.SetBitInByte(7-x, true, ref row[0]);
                        ByteUtils.SetBitInByte(7-x, true, ref row[1]);
                        break;

                    case 2:
                        // 01
                        ByteUtils.SetBitInByte(7-x, false, ref row[0]);
                        ByteUtils.SetBitInByte(7-x, true, ref row[1]);
                        break;

                    case 1:
                        // 10
                        ByteUtils.SetBitInByte(7-x, true, ref row[0]);
                        ByteUtils.SetBitInByte(7-x, false, ref row[1]);
                        break;

                    case 0:
                    default:
                        // leave as 00
                        ByteUtils.SetBitInByte(7-x, false, ref row[0]);
                        ByteUtils.SetBitInByte(7-x, false, ref row[1]);
                        break;
                }
            }

            return row;
        }
    }
    #endregion

    #region UTILS
    public static class GBImageUtils
    {

        #region GUI PREVIEW FUNCS
        public static Bitmap TwoBPPColorToBitmapPreview(int[,] twobppvalues, int width, int height, GBPalette palette)
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bitmap.SetPixel(x, y, palette.GetPaletteColorFrom2BPPValue(twobppvalues[y,x]));
                }
            }
                

            return bitmap;
        }

        public static Bitmap TwoBPPColorToBitmapPreview(int[,] twobppvalues, int width, int height)
        {
            // using default palette
            GBPalette palette = new GBPalette();
            return TwoBPPColorToBitmapPreview(twobppvalues, width, height, palette);
        }

        public static void ImageToGBTileListAndTileMap(
            Bitmap bitmap, 
            out List<GBTile> tile_list, 
            out GBTileMap tile_map, 
            bool keepDuplicates, 
            out int numDuplicates)
        {
            numDuplicates = 0;
            int tiles_x = bitmap.Width / 8;
            int tiles_y = bitmap.Height / 8;

            

            int[,] greyscaleImage = GBImageUtils.GetLuminosityValuesFromImage(ref bitmap);
            int[,] twobpp_values = GBImageUtils.LuminosityTo2BPPColour(greyscaleImage, bitmap.Width, bitmap.Height);

            tile_list = new List<GBTile>();
            tile_map = new GBTileMap();
            tile_map.SetDimensions(tiles_x, tiles_y);

            Dictionary<int, int> tile_indexes = new Dictionary<int, int>();
            int tileIdx = 0;
            int tileNum = 0;

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
                            int bmp_y = ty * 8 + y;
                            int bmp_x = tx * 8 + x;

                            int color = twobpp_values[bmp_y, bmp_x];
                            gbtile.SetPixelColour((short)color, y, x);
                        }
                    }

                    // count dupes
                    bool dupe = tile_list.Contains(gbtile);
                    if(dupe)
                    {
                        numDuplicates++;
                    }

                    // add dupe only if we want to keep them
                    if (dupe && keepDuplicates)
                    {
                        // process directly without caring about mapping to duplicate tiles
                        tile_list.Add(gbtile);
                        tile_map.AddTile(tileIdx);
                        tileIdx++;
                        tileNum++;
                    }
                    else
                    {
                        if(dupe)
                        {
                            // find the matching tile and add it to the tilemap
                            tile_map.AddTile(tile_indexes[gbtile.GetHashCode()]);
                        }
                        else
                        {
                            // add the tile to the list and store its index for future matching tiles
                            tile_list.Add(gbtile);
                            tile_indexes.Add(gbtile.GetHashCode(), tileIdx);

                            tile_map.AddTile(tile_indexes[gbtile.GetHashCode()]);

                            tileIdx++;
                            tileNum++;
                        }
                    }
                }
            }
        }

        public static Bitmap PreviewImageFromTileData(GBTile[] tiles, int width_in_tiles, GBPalette palette)
        {
            int tiles_x = width_in_tiles;
            int tiles_y = (int)Math.Ceiling(((double)tiles.Length / (double)width_in_tiles));

            int width = width_in_tiles * 8;
            int height = (int)Math.Ceiling(((double)tiles.Length / (double)width_in_tiles)) * 8;

            Bitmap bmp = new Bitmap(width, height);

            // set the entire image solid color first
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    bmp.SetPixel(x, y, Color.White);
                }
            }

            for (int ty = 0; ty < tiles_y; ty++)
            {
                for (int tx = 0; tx < tiles_x; tx++)
                {
                    // grab the 8x8 chunk for this gbtile and populate it
                    int tileIdx = (ty * tiles_x) + tx;

                    GBTile tile = new GBTile();
                    // only get a tile if there is one, there may not be enough tiles
                    // to fill the last row of the preview image
                    if (tileIdx < tiles.Length)
                    {
                        tile = tiles[tileIdx];
                    }

                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            // find out pixel coordinates
                            int px = (tx*8) + x;
                            int py = (ty*8) + y;

                            // find the color palette value for this pixel
                            int paletteIndex = tile.GetPixelColour(x, y);
                            Color color = palette.GetColor(paletteIndex);

                            //Set the pixel color on the preview image
                            bmp.SetPixel(px, py, color);
                        }
                    }
                }
            }

            /*for ( int tIndex = 0; tIndex < tiles.Length; tIndex++)
            {
                // row position
                int rowOffset = (int)Math.Floor((double)(tIndex / width_in_tiles));
                // column position
                int colOffset = (int)Math.Floor((double)((tIndex % width_in_tiles)));

                GBTile tile = tiles[tIndex];

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        // find out pixel coordinates
                        int px = colOffset + x;
                        int py = rowOffset + y;

                        // find the color palette value for this pixel
                        int paletteIndex = tile.GetPixelColour(x, y);
                        Color color = palette.GetColor(paletteIndex);

                        //Set the pixel color on the preview image
                        bmp.SetPixel(px, py, color);
                    }
                }
            }*/

            return bmp;
        }
        
        #endregion

        #region IMAGE CONVERSION
        public static int[,] GetLuminosityValuesFromImage(ref System.Drawing.Bitmap bitmap)
        {
            Console.WriteLine("Getting Lumin Values (h:{0}, w:{1})", bitmap.Height, bitmap.Width);
            int[,] greyscaleImage = new int[bitmap.Height, bitmap.Width];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    System.Drawing.Color color = bitmap.GetPixel(x, y);
                    int lumin = (int)((color.R * 0.21f) + (color.G * 0.72f) + (color.B * 0.07f));
                    greyscaleImage[y, x] = lumin;
                }
            }
            
            return greyscaleImage;
        }

        public static int[,] LuminosityTo2BPPColour(int[,] luminValues, int width, int height)
        {
            Console.WriteLine("Getting 2BPP Values (h:{0}, w:{1})", height, width);
            // how do we get to 2bpp?
            //Bitmap 2bpp = new Bitmap(bmp, bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format4bppIndexed, bmp.);

            int[,] twobpp_values = new int[height, width];

            // lumin values should be 0-255

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int grey = luminValues[y, x];
                    if (grey <= 63) twobpp_values[y, x] = 3; // darkest color
                    else if (grey > 63 && grey <= 127) twobpp_values[y, x] = 2;
                    else if (grey > 127 && grey <= 191) twobpp_values[y, x] = 1;
                    else if (grey > 191) twobpp_values[y, x] = 0; // lightest color
                }
            }

            return twobpp_values;
        }
        #endregion

        #region SERIALIZATION
        public static void SerializeGBTileData(GBTile[] gb_tiles, string fname)
        {
            // for now just spit out hex tiles for GBASM, worry about more complex data/bin data later

            Console.WriteLine("Num Tiles ({0}) - Tile Bytes(Hex):", gb_tiles.Length);
            // Format:   [tab] DB $7C, $7C, $82...(16 bytes)\n

            string filename = fname + ".asm";
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename))
            {
                file.WriteLine(@"; GB Tiles");
                file.WriteLine(@"DB " + gb_tiles.Length.ToString());
                for (int i = 0; i < gb_tiles.Length; i++)
                {
                    byte[] bytes = gb_tiles[i].GetTileBytes();
                    Console.WriteLine("DB: " + BitConverter.ToString(bytes));

                    string hexStr = BitConverter.ToString(bytes).Replace("-", ", $");
                    string line = " DB $" + hexStr;
                    file.WriteLine(line);
                }
            }
        }

        public static void SerializeGBMetaTileData(GBTile[] gb_tiles, string fname)
        {
            // for now just spit out hex tiles for GBASM, worry about more complex data/bin data later

            Console.WriteLine("Num Tiles ({0}) - Tile Bytes(Hex):", gb_tiles.Length);
            // Format:   [tab] DB $7C, $7C, $82...(16 bytes)\n

            string filename = fname + ".asm";
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename))
            {
                file.WriteLine(@"; GB MetaTiles");
                file.WriteLine(@"DB " + gb_tiles.Length.ToString());
                for (int i = 0; i < gb_tiles.Length; i++)
                {
                    byte[] bytes = gb_tiles[i].GetTileBytes();
                    Console.WriteLine("DB: " + BitConverter.ToString(bytes));

                    string hexStr = BitConverter.ToString(bytes).Replace("-", ", $");
                    if (Config.DebugStringAsmOutput)
                    {
                        hexStr = BitConverter.ToString(bytes).Replace("-", " ");
                    }
                    
                    string line = " DB $" + hexStr;
                    file.WriteLine(line);
                }
            }
        }

        public static void SerializeGBTileDataWithMap(List<GBTile> tile_list, List<int> tile_map)
        {
            SerializeGBTileData(tile_list.ToArray(), "tiles");

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Config.OutputMapFilename+@".asm"))
            {
                file.WriteLine(@"; GB TileMap");

                for (int i = 0; i < tile_map.Count; i++)
                {
                    file.WriteLine("DB "+tile_map[i]);
                }
            }
        }

        public static void SerializeGBTileDataBinary(List<GBTile> tile_list, string fname)
        {
            // for now just spit out hex tiles for GBASM, worry about more complex data/bin data later

            Console.WriteLine("Num Tiles ({0}) - Tile Bytes(Hex):", tile_list.Count);
            // Format:   [tab] DB $7C, $7C, $82...(16 bytes)\n
            string filename = fname + ".bin";
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                writer.Write((byte)tile_list.Count);
                for (int i = 0; i < tile_list.Count; i++)
                {
                    byte[] bytes = tile_list[i].GetTileBytes();
                    for (int j = 0; j < bytes.Length; j++)
                    {
                        writer.Write(bytes[j]);
                    }
                }

                writer.Close();
            }
        }
        #endregion
    }
    #endregion
}
