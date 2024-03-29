﻿using GBImageConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GBImageConvertGUI.JSONToGBMap;
using static GBImageConvertGUI.TiledCSVToGBMap;

namespace GBImageConvertGUI
{
    public partial class FormMapConverter : Form
    {
        // We need a ref to the img converter panel to get the loaded tile data
        FormImgConverter _imgConverterForm = null;

        private Bitmap _previewTilesBmp = null;
        //List<GBTile> _generated_tile_list = null;
        GBTileSet _generated_tile_list = null;
        GBTileMap _tileMap = null;

        public FormMapConverter()
        {
            InitializeComponent();

            // get the main form as we need its reference to the image converter
            _imgConverterForm = FormMain.GetSingleton().GetImgConverterForm();
            lblError.Text = "";
        }

        public void Refresh()
        {
            RefreshAll();
        }

        private void RefreshAll()
        {
            if(_imgConverterForm == null)
            {
                return;
            }

            bool cullDupes = checkDuplicateReplacement.Checked;

            // grab the tileset from the img converter panel
            _generated_tile_list = _imgConverterForm.GetTileList();

            _previewTilesBmp = _imgConverterForm.GetPreviewTilesBmp();
            this.picTilesetPreview.Image = _previewTilesBmp;
            this.picTilesetPreview.SizeMode = PictureBoxSizeMode.Zoom;

            // load the preview of the map file
            if(_tileMap != null && _generated_tile_list != null)
            {
                picMapPreview.Image = _tileMap.GeneratePreview(_generated_tile_list, cullDupes);
                this.picMapPreview.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void btn_importJSON_Click(object sender, EventArgs e)
        {
            // import both map and collision and special properties as JSON
            JSONResultCode result = JSONToGBMap.LoadJSON(out _tileMap);

            if (_generated_tile_list != null && checkDuplicateReplacement.Checked)
            {
                // apply the replacements from the loaded tilemap
                _tileMap.ApplyTileSetReplacements(_generated_tile_list);
            }

            if (result == JSONResultCode.SUCCESS)
            {
                RefreshAll();
            }
            else
            {
                switch (result)
                {
                    case JSONResultCode.DIMENSIONS_DONT_MATCH_TILEARRAY_LENGTH:
                        lblError.Text = @"ERROR: Inconsistent dimensions detected in map.";
                        break;
                    case JSONResultCode.INVALID_VALUE_DETECTED:
                        lblError.Text = @"ERROR: An invalid value (non-integer, or integer below 0 was detected in the source map!";
                        break;
                    case JSONResultCode.FAIL_INVALID_BACKGROUND_TILEDATA:
                        lblError.Text = @"ERROR: An invalid value (non-integer, or integer below 0 was detected in background map!";
                        break;
                    case JSONResultCode.FAIL_INVALID_COLLISION_TILEDATA:
                        lblError.Text = @"ERROR: An invalid value (non-integer, or integer below 0 was detected in collision map!";
                        break;
                    default:
                        lblError.Text = @"ERROR: Unknown Error occured while loading JSON map file.";
                        break;
                }
            }
        }

        private void btnImportTiledCSV_Click(object sender, EventArgs e)
        {
            CSVResultCode result = TiledCSVToGBMap.LoadCSV(out _tileMap);

            if (_generated_tile_list != null && checkDuplicateReplacement.Checked)
            {
                // apply the replacements from the loaded tilemap
                _tileMap.ApplyTileSetReplacements(_generated_tile_list);
            }

            if (result == CSVResultCode.SUCCESS)
            {
                RefreshAll();
            }
            else
            {
                switch(result)
                {
                    case CSVResultCode.INCONSISTENT_WIDTH:
                        lblError.Text = @"ERROR: Inconsistent width detected in map csv.";
                        break;
                    case CSVResultCode.INVALID_VALUE_DETECTED:
                        lblError.Text = @"ERROR: An invalid value (non-integer, or integer below 0 was detected in the source CSV file!";
                        break;
                    default:
                        lblError.Text = @"ERROR: Unknown Error occured while loading CSV map file.";
                        break;
                }
            }
        }

        private void btnImportCollision_Click(object sender, EventArgs e)
        {
            CSVResultCode result = TiledCSVToGBMap.LoadCSVAsCollisionPlane(ref _tileMap);

            if (result == CSVResultCode.SUCCESS)
            {
                RefreshAll();
            }
            else
            {
                switch (result)
                {
                    case CSVResultCode.INCONSISTENT_WIDTH:
                        lblError.Text = @"ERROR: Inconsistent width detected in map csv.";
                        break;
                    case CSVResultCode.INVALID_VALUE_DETECTED:
                        lblError.Text = @"ERROR: An invalid value (non-integer, or integer below 0 was detected in the source CSV file!";
                        break;
                    default:
                        lblError.Text = @"ERROR: Unknown Error occured while loading CSV map file.";
                        break;
                }
            }
        }

        private void btnExportMap_Click(object sender, EventArgs e)
        {
            if (_tileMap == null)
            {
                return;
            }

            switch (comboExportFormat.SelectedIndex)
            {
                case (int)GBImageConverter.OutputFormats.BIN:
                    GBImageSerialization.SerializeGBTileMapBinary(
                        _tileMap,
                        checkboxPrependTileCountByte.Checked,
                        checkPrependWidthHeight.Checked
                        );
                    break;

                case (int)GBImageConverter.OutputFormats.RGBASM:
                    GBImageSerialization.SerializeGBTileMapRGBASM(
                        _tileMap,
                        checkboxPrependTileCountByte.Checked,
                        checkPrependWidthHeight.Checked
                        );
                    break;

                case (int)GBImageConverter.OutputFormats.GBDK_C:
                    GBImageSerialization.SerializeGBTileMapGBDK_C(
                        _tileMap,
                        checkboxPrependTileCountByte.Checked,
                        checkPrependWidthHeight.Checked
                        );
                    break;
            }
        }

        private void checkDuplicateReplacement_CheckedChanged(object sender, EventArgs e)
        {
            RefreshAll();
        }
    }
}
