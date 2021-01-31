using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBImageConverter
{
    public class GBSerializationConfig
    {
        public bool IncludeTileLength;
        public bool IncludeMapDimensions;
    }

    public class GBImageSerialization
    {
        private static void SerializeGBTileMapGBDK_H(
            string filename,
            GBTileMap tile_map,
            bool prependTileCount,
            bool prependWidthHeight)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                string filename_with_ext = Path.GetFileName(filename);
                string filename_without_ext = Path.GetFileNameWithoutExtension(filename);

                file.WriteLine(string.Format("/*"));
                file.WriteLine(string.Format("    {0}", filename_with_ext));
                file.WriteLine(string.Format(""));
                file.WriteLine(@"    Generated with GBImageConverter");
                file.WriteLine(string.Format(""));
                file.WriteLine(string.Format("    Format: {0}", "tilemap as tile indeces"));
                file.WriteLine(string.Format("    Compression: {0}", "None"));
                file.WriteLine(string.Format("    Number of Tile indeces: 0 to {0}", tile_map.TileCount() - 1));
                file.WriteLine(string.Format("*/"));
                file.WriteLine(string.Format(""));

                // optional tilecount byte
                if (prependTileCount)
                {
                    file.WriteLine(@"// Tilemap Data Length");
                    file.WriteLine(string.Format(@"#define {0}Length {1}", filename_without_ext, tile_map.TileCount().ToString()));
                }

                // optional width/height
                if (prependTileCount)
                {
                    file.WriteLine(@"// Tilemap Width and Height");
                    file.WriteLine(string.Format(@"#define {0}Width {1}", filename_without_ext, tile_map.Width().ToString()));
                    file.WriteLine(string.Format(@"#define {0}Height {1}", filename_without_ext, tile_map.Height().ToString()));
                }

                file.WriteLine(@"// Tile Plane Data ");
                // declare the C array
                file.WriteLine(string.Format(@"extern const unsigned char {0}{1}[{2}];", filename_without_ext, "_tilemap", tile_map.TileCount()));

                if (tile_map.HasCollisionPlane())
                {
                    file.WriteLine(@"// Collision Plane Data ");
                    file.WriteLine(string.Format(@"extern const unsigned char {0}{1}[{2}];", filename_without_ext, "_collision", tile_map.TileCount()));
                }
                file.Write("    ");
            }
        }

        #region GBDK C
        public static void SerializeGBTileMapGBDK_C(
            GBTileMap tile_map, 
            bool prependTileCount, 
            bool prependWidthHeight,
            bool includeHeader = true
            )
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "C|*.c";
            saveFileDialog.Title = "Save Tile Map";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                // save the header file
                if (includeHeader)
                {
                    string header_filename = saveFileDialog.FileName;
                    header_filename = Path.ChangeExtension(header_filename, ".h");
                    SerializeGBTileMapGBDK_H(header_filename, tile_map, prependTileCount, prependWidthHeight);
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    string filename_with_ext = Path.GetFileName(saveFileDialog.FileName);
                    string filename_without_ext = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

                    file.WriteLine(string.Format("/*"));
                    file.WriteLine(string.Format("    {0}", filename_with_ext));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(@"    Generated with GBImageConverter");
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format("    Format: {0}", "tilemap as tile indeces"));
                    file.WriteLine(string.Format("    Compression: {0}", "None"));
                    file.WriteLine(string.Format("    Number of Tile indeces: 0 to {0}", tile_map.TileCount() - 1));
                    file.WriteLine(string.Format("*/"));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format(""));
                    
                    // optional tilecount byte
                    if (prependTileCount)
                    {
                        file.WriteLine(@"// Tilemap Data Length");
                        file.WriteLine(string.Format(@"#define {0}Length {1}", filename_without_ext, tile_map.TileCount().ToString()));
                    }

                    // optional width/height
                    if (prependTileCount)
                    {
                        file.WriteLine(@"// Tilemap Width and Height");
                        file.WriteLine(string.Format(@"#define {0}Width {1}", filename_without_ext, tile_map.Width().ToString()));
                        file.WriteLine(string.Format(@"#define {0}Height {1}", filename_without_ext, tile_map.Height().ToString()));
                    }

                    file.WriteLine(@"// TileMap Data Array");

                    // generate the c array
                    file.WriteLine(string.Format(@"const unsigned char {0}_{1}[] =", filename_without_ext, "tilemap"));
                    file.WriteLine(@"{");
                    file.Write("    ");

                    int column = 0;
                    int tileCount = tile_map.TileCount();
                    for (int i = 0; i < tileCount; i++)
                    {
                        string comma = ",";
                        if(i == tileCount - 1)
                        {
                            comma = "";
                        }

                        byte[] bytes = { (byte)tile_map.GetTile(i, GBTileMap.TilePlanes.TILE) };
                        file.Write(string.Format("0x{0}{1}", BitConverter.ToString(bytes), comma));

                        // ensure 8 per row
                        // todo: change this to match the map width?
                        column++;
                        if (column > 7)
                        {
                            file.Write("\n");

                            if (i < tileCount - 1)
                            {
                                // don't space out the last line it just looks weird
                                file.Write("    ");
                            }

                            column = 0;
                        }
                    }
                    file.WriteLine(@"};");
                    file.Write("    ");

                    if (tile_map.HasCollisionPlane())
                    {
                        file.WriteLine(string.Format(@"const unsigned char {0}_{1}[] =", filename_without_ext, "collision"));
                        file.WriteLine(@"{");
                        file.Write("    ");

                        column = 0;
                        for (int i = 0; i < tileCount; i++)
                        {
                            string comma = ",";
                            if (i == tileCount - 1)
                            {
                                comma = "";
                            }

                            byte[] bytes = { (byte)tile_map.GetTile(i, GBTileMap.TilePlanes.COLLISION) };
                            file.Write(string.Format("0x{0}{1}", BitConverter.ToString(bytes), comma));

                            // ensure 8 per row
                            // todo: change this to match the map width?
                            column++;
                            if (column > 7)
                            {
                                file.Write("\n");

                                if (i < tileCount - 1)
                                {
                                    // don't space out the last line it just looks weird
                                    file.Write("    ");
                                }

                                column = 0;
                            }
                        }
                    }
                    file.WriteLine(@"};");
                }
            }
        }

        public static void SerializeGBTileDataGBDK_H(
            string filename,
            GBTileSet gb_tiles, 
            bool prependTileCount)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                string filename_with_ext = Path.GetFileName(filename);
                string filename_without_ext = Path.GetFileNameWithoutExtension(filename);

                file.WriteLine(string.Format("/*"));
                file.WriteLine(string.Format("        {0}", filename_with_ext));
                file.WriteLine(string.Format(""));
                file.WriteLine(@"    Generated with GBImageConverter");
                file.WriteLine(string.Format(""));
                file.WriteLine(string.Format("    Format {0}", "Gameboy 2bpp interleaved"));
                file.WriteLine(string.Format("    Compression {0}", "None"));
                file.WriteLine(string.Format("    Tile Size 8x8"));
                file.WriteLine(string.Format("    Number of Tiles 0 to {0}", gb_tiles.Count() - 1));
                file.WriteLine(string.Format(""));
                file.WriteLine(string.Format("    Palette: {0}", "None"));
                file.WriteLine(string.Format("    SGB Palette: {0}", "None"));
                file.WriteLine(string.Format("    CGB Palette: {0}", "None"));
                file.WriteLine(string.Format("*/"));
                file.WriteLine(string.Format(""));
                file.WriteLine(string.Format(""));
                file.WriteLine(string.Format(""));

                // optional tilecount byte
                if (prependTileCount)
                {
                    file.WriteLine(@"// Tilemap Data Length in tiles (16 bytes each)");
                    file.WriteLine(string.Format(@"#define {0}Length {1}", filename_without_ext, gb_tiles.Count().ToString()));
                }

                file.WriteLine(@"// Tilemap Data (one 16-byte tile per line)");

                // generate the c array
                file.WriteLine(string.Format(@"extern const unsigned char {0}[{1}];", filename_without_ext, gb_tiles.Count()*16));
            }
        }

        public static void SerializeGBTileDataGBDK_C(
            GBTileSet gb_tiles, 
            bool prependTileCount,
            bool includeHeader = true
            )
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "C|*.c";
            saveFileDialog.Title = "Save Tile Data";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                // save the header file
                if (includeHeader)
                {
                    string header_filename = saveFileDialog.FileName;
                    header_filename = Path.ChangeExtension(header_filename, ".h");
                    SerializeGBTileDataGBDK_H(header_filename, gb_tiles, prependTileCount);
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    string filename_with_ext = Path.GetFileName(saveFileDialog.FileName);
                    string filename_without_ext = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

                    file.WriteLine(string.Format("/*"));
                    file.WriteLine(string.Format("        {0}", filename_with_ext));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(@"    Generated with GBImageConverter");
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format("    Format {0}", "Gameboy 2bpp interleaved"));
                    file.WriteLine(string.Format("    Compression {0}", "None"));
                    file.WriteLine(string.Format("    Tile Size 8x8"));
                    file.WriteLine(string.Format("    Number of Tiles 0 to {0}", gb_tiles.Count() - 1));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format("    Palette: {0}", "None"));
                    file.WriteLine(string.Format("    SGB Palette: {0}", "None"));
                    file.WriteLine(string.Format("    CGB Palette: {0}", "None"));
                    file.WriteLine(string.Format("*/"));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format(""));

                    // optional tilecount byte
                    if (prependTileCount)
                    {
                        file.WriteLine(@"// Tilemap Data Length in tiles (16 bytes each)");
                        file.WriteLine(string.Format(@"#define {0}Length {1}", filename_without_ext, gb_tiles.Count().ToString()));
                    }

                    file.WriteLine(@"// Tilemap Data (one 16-byte tile per line)");

                    // generate the c array
                    file.WriteLine(string.Format(@"const unsigned char {0}[] =", filename_without_ext));
                    file.WriteLine(@"{");
                    file.Write("    ");
                    file.Write("0x");

                    for (int i = 0; i < gb_tiles.Count(); i++)
                    {
                        byte[] bytes = gb_tiles.GetTile(i).GetTileBytes();
                        //Console.WriteLine("DB: " + BitConverter.ToString(bytes));
                        string hexStr = BitConverter.ToString(bytes).Replace("-", ", 0x");
                        file.Write(hexStr);

                        // ensure one 16-byte per row, and no comma on last entry
                        if(i == gb_tiles.Count()-1)
                        {
                            file.Write("\n");
                        }
                        else
                        {
                            file.Write(",\n");
                            file.Write("    0x");
                        }
                    }
                    file.WriteLine(@"};");
                }
            }
        }
        #endregion

        #region RGB ASM/ASSEMBLER
        public static void SerializeGBTileMapRGBASM(
            GBTileMap tile_map, 
            bool prependTileCount, 
            bool prependWidthHeight
            )
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "ASM|*.asm";
            saveFileDialog.Title = "Save Tile Map";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    string filename_with_ext = Path.GetFileName(saveFileDialog.FileName);
                    string filename_without_ext = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

                    file.WriteLine(string.Format("; {0}", Path.GetFileName(saveFileDialog.FileName)));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(@"; Generated with GBImageConverter");
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format("; Format: {0}", "tilemap as tile indeces"));
                    file.WriteLine(string.Format("; Compression: {0}", "None"));   
                    file.WriteLine(string.Format("; Number of Tile indices: 0 to {0}", tile_map.TileCount() - 1));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format(""));

                    // optional tilecount byte
                    if (prependTileCount)
                    {
                        file.WriteLine(@"; Tilemap Data Length in bytes");
                        file.WriteLine(string.Format(@"{0}Length", filename_without_ext));
                        file.WriteLine(@"DB " + tile_map.TileCount().ToString());
                    }

                    
                    if (prependWidthHeight)
                    {
                        file.WriteLine(string.Format(@"{0}Width", filename_without_ext));
                        file.WriteLine(@"DB " + tile_map.Width().ToString());
                        file.WriteLine(string.Format(@"{0}Height", filename_without_ext));
                        file.WriteLine(@"DB " + tile_map.Height().ToString());
                    }

                    file.WriteLine(@"; TileMap Data");
                    int tileCount = tile_map.TileCount();
                    for (int i = 0; i < tileCount; i++)
                    {
                        file.WriteLine("DB " + tile_map.GetTile(i, GBTileMap.TilePlanes.TILE));
                    }
                }
            }
        }

        public static void SerializeGBTileDataRGBASM(GBTileSet gb_tiles, bool prependTileCount)
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "ASM|*.asm";
            saveFileDialog.Title = "Save Tile Data";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    file.WriteLine(string.Format("; {0}", saveFileDialog.FileName));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(@"; Generated with GBImageConverter");
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format("; Format {0}", "Gameboy 2bpp interleaved"));
                    file.WriteLine(string.Format("; Compression {0}", "None"));
                    file.WriteLine(string.Format("; Tile Size 8x8"));
                    file.WriteLine(string.Format("; Number of Tiles 0 to {0}", gb_tiles.Count()-1));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format("; Palette: {0}", "None"));
                    file.WriteLine(string.Format("; SGB Palette: {0}", "None"));
                    file.WriteLine(string.Format("; CGB Palette: {0}", "None"));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format(""));
                    file.WriteLine(string.Format(""));

                    // optional tilecount byte
                    if (prependTileCount)
                    {
                        file.WriteLine(@"; Tilemap Data Length in tiles (16 bytes each)");
                        file.WriteLine(@"DB " + gb_tiles.Count().ToString());
                    }

                    file.WriteLine(@"; Tilemap Data (one 16-byte tile per line)");
                    for (int i = 0; i < gb_tiles.Count(); i++)
                    {
                        byte[] bytes = gb_tiles.GetTile(i).GetTileBytes();
                        Console.WriteLine("DB: " + BitConverter.ToString(bytes));

                        string hexStr = BitConverter.ToString(bytes).Replace("-", ", $");
                        string line = "DB $" + hexStr;
                        file.WriteLine(line);
                    }
                }
            }
        }
        #endregion

        #region BINARY 
        public static void SerializeGBTileMapBinary(
            GBTileMap tile_map, 
            bool prependTileCount, 
            bool prependWidthHeight)
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary|*.bin";
            saveFileDialog.Title = "Save Tile Map";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create)))
                {
                    // output single byte for tilecount
                    if (prependTileCount)
                    {
                        writer.Write((byte)tile_map.TileCount());
                    }

                    if(prependWidthHeight)
                    {
                        writer.Write((byte)tile_map.Width());
                        writer.Write((byte)tile_map.Height());
                    }

                    // output single byte for each tile location in the map
                    // (ie: 0 in the map = the first tile in the tilemap data)
                    int tileCount = tile_map.TileCount();
                    for (int i = 0; i < tileCount; i++)
                    {
                        writer.Write((byte)tile_map.GetTile(i, GBTileMap.TilePlanes.TILE));
                    }

                    writer.Close();
                }
            }
        }

        public static void SerializeGBTileDataBinary(GBTileSet tile_list, bool prependTileCount)
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary|*.bin";
            saveFileDialog.Title = "Save Tile Data";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create)))
                {
                    // output single byte for tilecount
                    if (prependTileCount)
                    {
                        writer.Write((byte)tile_list.Count());
                    }
                    // output 16 bytes per tile, 2 bytes per row
                    for (int i = 0; i < tile_list.Count(); i++)
                    {
                        byte[] bytes = tile_list.GetTile(i).GetTileBytes();
                        for (int j = 0; j < bytes.Length; j++)
                        {
                            writer.Write(bytes[j]);
                        }
                    }

                    writer.Close();
                }
            }
        }
        #endregion

        #region BITMAP
        // essentially just save the desaturated preview image
        public static void SerializeGBTileMapBitmap(Bitmap bitmap)
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bitmap|*.bmp";
            saveFileDialog.Title = "Save Tilemap Preview Image";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                bitmap.Save(saveFileDialog.FileName, ImageFormat.Bmp);
            }
        }

        // save the preview image of the tileset
        public static void SerializeGBTileDataBitmap(Bitmap tilesPreview)
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bitmap|*.bmp";
            saveFileDialog.Title = "Save Tileset Image";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                tilesPreview.Save(saveFileDialog.FileName, ImageFormat.Bmp);
            }
        }
        #endregion
    }
}
