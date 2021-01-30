using GBImageConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBImageConvertGUI
{
    public static class TiledCSVToGBMap
    {
        public enum CSVResultCode { INCONSISTENT_WIDTH, INVALID_VALUE_DETECTED, SUCCESS, FAIL_UNKNOWN_ERROR }

        public static CSVResultCode LoadCSV(out GBTileMap result)
        {
            result = new GBTileMap();
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

                            for(int i = 0; i < values.Length; i++)
                            {
                                int value = -1;

                                if (int.TryParse(values[i], out value))
                                {
                                    result.AddTile(value);
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
                                    if(!result.SetTile(i, height, value, GBTileMap.TilePlanes.COLLISION))
                                    {
                                        return CSVResultCode.INCONSISTENT_WIDTH;
                                    }
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
}
