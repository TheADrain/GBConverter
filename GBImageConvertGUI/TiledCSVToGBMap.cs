using GBImageConverter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GBImageConverter.GBTileMap;

namespace GBImageConvertGUI
{
    public static class TiledCSVToGBMap
    {
        public enum CSVResultCode { INCONSISTENT_WIDTH, INVALID_VALUE_DETECTED, SUCCESS, FAIL_UNKNOWN_ERROR }
        

        public static CSVResultCode LoadCSV(out GBTileMap result, MapOrientation orientation = MapOrientation.COLUMNS_FIRST)
        {
            result = new GBTileMap();
            var filePath = string.Empty;

            List<int> tileBuff = new List<int>();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files|*.csv;*.txt;";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    using (var reader = new StreamReader(filePath))
                    {
                        int width = 0;
                        int height = 0;

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            for(int i = 0; i < values.Length; i++)
                            {
                                int value = -1;

                                if (int.TryParse(values[i], out value))
                                {
                                    tileBuff.Add(value);                                    
                                }
                                else
                                {
                                    return CSVResultCode.INVALID_VALUE_DETECTED;
                                }
                            }

                            height++;
                            if(width == 0)
                            {
                                width = values.Length;
                            }
                            else
                            {
                                if(width != values.Length)
                                {
                                    return CSVResultCode.INCONSISTENT_WIDTH;
                                }
                            }
                        }

                        result.SetDimensions(width, height);

                        if (orientation == MapOrientation.ROWS_FIRST)
                        {
                            result.SetOrientation(MapOrientation.ROWS_FIRST);
                            // default ordering 
                            for (int i = 0; i < tileBuff.Count; i++)
                            {
                                result.AddTile(tileBuff[i]);
                            }
                        }
                        else if(orientation == MapOrientation.COLUMNS_FIRST)
                        {
                            result.SetOrientation(MapOrientation.COLUMNS_FIRST);
                            // re-order the level to be layed out in sequential columns
                            for (int x = 0; x < width; x++)
                            {
                                for(int y = 0; y < height; y++)
                                {
                                    int idx = (y * width) + x;
                                    result.AddTile(tileBuff[idx]);
                                }
                            }
                        }
                    }

                    return CSVResultCode.SUCCESS;
                }
            }

            return CSVResultCode.FAIL_UNKNOWN_ERROR;
        }

        public static CSVResultCode LoadCSVAsCollisionPlane(ref GBTileMap result)
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files|*.csv;*.txt;";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    using (var reader = new StreamReader(filePath))
                    {
                        int width = 0;
                        int height = 0;

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            for (int i = 0; i < values.Length; i++)
                            {
                                int value = -1;

                                if (int.TryParse(values[i], out value))
                                {
                                    result.AddTile(value, GBTileMap.TilePlanes.COLLISION);
                                }
                                else
                                {
                                    return CSVResultCode.INVALID_VALUE_DETECTED;
                                }
                            }

                            height++;
                            if (width == 0)
                            {
                                width = values.Length;
                            }
                            else
                            {
                                if (width != values.Length)
                                {
                                    return CSVResultCode.INCONSISTENT_WIDTH;
                                }
                            }
                        }
                    }

                    return CSVResultCode.SUCCESS;
                }
            }

            return CSVResultCode.FAIL_UNKNOWN_ERROR;
        }
    }

    public static class JSONToGBMap
    {
        public enum JSONResultCode {
            JSON_PARSE_ERROR,
            DIMENSIONS_DONT_MATCH_TILEARRAY_LENGTH,
            INVALID_VALUE_DETECTED,
            SUCCESS,
            FAIL_INVALID_BACKGROUND_TILEDATA,
            FAIL_INVALID_COLLISION_TILEDATA,
            FAIL_UNKNOWN_ERROR }

        public static JSONResultCode LoadJSON(out GBTileMap result)
        {
            GBTileMap map = new GBTileMap();
            JObject data = null;
            result = map;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files|*.json;*.txt;";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader file = File.OpenText(openFileDialog.FileName))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            data = (JObject)JToken.ReadFrom(reader);
                        }
                    }
                }
            }

            /* READ TILESETS (needed for the GID offset) */
            JArray tilesets = (JArray)data["tilesets"];
            int backgroundTilesOffset = 0;
            int gameplayTilesOffset = 0;
            Dictionary<string, int> TilesetGIDOffsets = new Dictionary<string, int>();
            foreach (var tileset in tilesets)
            {
                string tilesetSource = (string)tileset["source"];
                // just gonna have to try and pull the tileset name from the filename and 
                //   that should be ok just make sure the tile project has documentation
                //   saying do not change the name of 'gameplay tiles'
                //   and see if there's a way to ensure it's the second tileset loaded in a map
                //   and not the first...

                // ensure background tiles are the first tileset
                // ensure gameplay is the second
                // ensure there are no other tilesets

                if(tilesetSource.ToLowerInvariant().Contains("gameplay"))
                {
                    gameplayTilesOffset = (int)tileset["firstgid"];
                }
                else
                {
                    backgroundTilesOffset = (int)tileset["firstgid"];
                }
            }

            /* READ PROPERTIES */

            JArray properties = (JArray)data["properties"];
            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    string propName = (string)prop["name"];
                    propName = propName.ToLowerInvariant();

                    if (propName.CompareTo("scrolldirection") == 0)
                    {
                        string direction = (string)prop["value"];
                        if (direction.ToLowerInvariant().CompareTo("vertical") == 0)
                        {
                            map.SetOrientation(MapOrientation.ROWS_FIRST);
                        }
                        else
                        {
                            map.SetOrientation(MapOrientation.COLUMNS_FIRST);
                        }
                    }
                }
            }
            else
            {
                map.SetOrientation(MapOrientation.ROWS_FIRST);
            }

            int height = (int)data["height"];
            int width = (int)data["width"];

            if(height == 0 || width == 0)
            {
                return JSONResultCode.JSON_PARSE_ERROR;
            }

            map.SetDimensions(width, height);

            /* READ LAYERS AND BUILD TILEPLANES */
            JArray layers = (JArray)data["layers"];

            JObject bkgLayer = null;
            JObject collisionLayer = null;

            for (int i = 0; i < layers.Count; i++)
            {
                JObject obj = (JObject)layers[i];
                string layerName = ((string)obj["name"]).ToLowerInvariant();
                if (layerName.CompareTo("background") == 0)
                {
                    JArray tilemap = (JArray)obj["data"];
                    int[] tileNums = JArrayToIntArray(tilemap);

                    if (tileNums.Length != height*width)
                    {
                        return JSONResultCode.DIMENSIONS_DONT_MATCH_TILEARRAY_LENGTH;
                    }

                    if (!TilemapFromIntArray(ref map, tileNums, TilePlanes.TILE, backgroundTilesOffset))
                    {
                        return JSONResultCode.FAIL_INVALID_BACKGROUND_TILEDATA;
                    }
                }
                else if (layerName.CompareTo("collision") == 0)
                {
                    JArray tilemap = (JArray)obj["data"];
                    int[] tileNums = JArrayToIntArray(tilemap);

                    if (tileNums.Length != height * width)
                    {
                        return JSONResultCode.DIMENSIONS_DONT_MATCH_TILEARRAY_LENGTH;
                    }

                    if(!TilemapFromIntArray(ref map, tileNums, TilePlanes.COLLISION, gameplayTilesOffset, true))
                    {
                        return JSONResultCode.FAIL_INVALID_COLLISION_TILEDATA;
                    }
                }
            }

            return JSONResultCode.SUCCESS;
        }

        private static int[] JArrayToIntArray(JArray jarr)
        {
            int[] intarr = new int[jarr.Count];

            for(int i = 0; i < jarr.Count; i++)
            {
                int num = (int)jarr[i];
                intarr[i] = num;
            }

            return intarr;
        }

        private static bool TilemapFromIntArray(
            ref GBTileMap map,
            int[] tileData,
            TilePlanes tilePlane,
            int gidOffset = 0,
            bool forceRowsFirst = false)
        {
            if (map.GetOrientation() == MapOrientation.ROWS_FIRST || forceRowsFirst)
            {
                map.SetOrientation(MapOrientation.ROWS_FIRST);
                // default ordering 
                for (int i = 0; i < tileData.Count(); i++)
                {
                    int tile = tileData[i] - gidOffset;

                    if (tile < 0)
                    {
                        tile = 0;
                        // tiles are off by 1 because Tiled considers 0 an empty cell
                    }

                    map.AddTile(tile, tilePlane);
                }
            }
            else if (map.GetOrientation() == MapOrientation.COLUMNS_FIRST)
            {
                map.SetOrientation(MapOrientation.COLUMNS_FIRST);
                // re-order the level to be layed out in sequential columns
                for (int x = 0; x < map.Width(); x++)
                {
                    for (int y = 0; y < map.Height(); y++)
                    {
                        int idx = (y * map.Width()) + x;
                        int tile = tileData[idx]-gidOffset;

                        if (tile < 0)
                        {
                            tile = 0;
                            // tiles are off by 1 because Tiled considers 0 an empty cell
                        }
                       
                        map.AddTile(tile, tilePlane);
                    }
                }
            }

            return true;
        }
    }
}
