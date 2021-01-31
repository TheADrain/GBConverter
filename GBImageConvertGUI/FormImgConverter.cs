using GBImageConverter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GBImageConvertGUI
{
    public partial class FormImgConverter : Form
    {
        private Image _loadedImage = null;
        private Bitmap _loadedBitmap = null;

        private Bitmap _previewBmp = null;
        private Bitmap _previewTilesBmp = null;
        public Bitmap GetPreviewTilesBmp() { return _previewTilesBmp; }

        private GBPalette _palette = new GBPalette();

        int[,] greyscaleImage = null;
        int[,] twobpp_values = null;

        int tilemapWidth = 0;
        int tilemapHeight = 0;

        int _tilemapPreviewWidthInTiles = 8;
        int _tilemapPreviewMagnification = 3;

        GBTileSet _generated_tile_list;
        public GBTileSet GetTileList() { return _generated_tile_list; }

        GBTileMap _generated_tile_map;
        public GBTileMap GetTileMap() { return _generated_tile_map; }

        GBImageConverter.OutputFormats _outputFormat = OutputFormats.BIN;

        public FormImgConverter()
        {
            InitializeComponent();

            // set binary as default output
            this.comboExportFormat.SelectedIndex = 0;
        }

        #region IMAGE LOADING
        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            //var fileContent = null;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files|*.bmp;*.png;";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    Image img = null;
                    try
                    {
                        img = Image.FromFile(filePath);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine("Error: Could not load image file {0}.", filePath);
                    }

                    if (img != null)
                    {
                        // validate the image
                        if(img.Size.Width % 8 != 0 || img.Size.Height % 8 != 0)
                        {
                            // image size must be divisible by 8 to be split into tiles
                        }
                        else
                        {
                            HandleLoadedImage(img);
                        }
                    }
                }
            }
        }
        #endregion

        #region IMAGE/MAP EXPORT
        private void btnExportMap_Click(object sender, EventArgs e)
        {
            if (_generated_tile_map == null)
            {
                return;
            }

            switch (comboExportFormat.SelectedIndex)
            {
                case (int)GBImageConverter.OutputFormats.BIN:
                    GBImageSerialization.SerializeGBTileMapBinary(
                        _generated_tile_map, 
                        checkboxPrependTileCountByte.Checked,
                        checkPrependWidthHeight.Checked
                        );
                    break;

                case (int)GBImageConverter.OutputFormats.RGBASM:
                    GBImageSerialization.SerializeGBTileMapRGBASM(
                        _generated_tile_map, 
                        checkboxPrependTileCountByte.Checked,
                        checkPrependWidthHeight.Checked
                        );
                    break;

                case (int)GBImageConverter.OutputFormats.GBDK_C:
                    GBImageSerialization.SerializeGBTileMapGBDK_C(
                        _generated_tile_map, 
                        checkboxPrependTileCountByte.Checked,
                        checkPrependWidthHeight.Checked
                        );
                    break;

                case (int)GBImageConverter.OutputFormats.BITMAP:
                    GBImageSerialization.SerializeGBTileMapBitmap(_previewBmp);
                    break;
            }
        }

        private void btnExportTiles_Click(object sender, EventArgs e)
        {
            if (_generated_tile_list == null)
            {
                return;
            }

            switch(comboExportFormat.SelectedIndex)
            {
                case (int)GBImageConverter.OutputFormats.BIN:
                    GBImageSerialization.SerializeGBTileDataBinary(_generated_tile_list, checkboxPrependTileCountByte.Checked);
                    break;

                case (int)GBImageConverter.OutputFormats.RGBASM:
                    GBImageSerialization.SerializeGBTileDataRGBASM(_generated_tile_list, checkboxPrependTileCountByte.Checked);
                    break;

                case (int)GBImageConverter.OutputFormats.GBDK_C:
                    GBImageSerialization.SerializeGBTileDataGBDK_C(_generated_tile_list, checkboxPrependTileCountByte.Checked);
                    break;

                case (int)GBImageConverter.OutputFormats.BITMAP:
                    GBImageSerialization.SerializeGBTileDataBitmap(_previewTilesBmp);
                    break;
            }
        }
        #endregion

        #region EXPORT OPTIONS
        // Remove Dupes checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RefreshAll();
        }
        #endregion

        #region IMAGE CONVERSION FUNCTIONS

        private void HandleLoadedImage(Image loadedImage)
        {
            _loadedImage = loadedImage;
            this.picOriginal.Image = _loadedImage;
            this.picOriginal.SizeMode = PictureBoxSizeMode.Zoom;

            _loadedBitmap = new Bitmap(loadedImage);

            RefreshAll();
        }

        private void RefreshAll()
        {
            if(_loadedImage == null)
            {
                return;
            }

            RefreshPreviewImage();
            RefreshGeneratedTiles();
        }

        private void RefreshPreviewImage()
        {
            // generate greyscale image from the loaded image
            greyscaleImage = GBImageUtils.GetLuminosityValuesFromImage(ref _loadedBitmap);
            twobpp_values = GBImageUtils.LuminosityTo2BPPColour(greyscaleImage, _loadedBitmap.Width, _loadedBitmap.Height);

            // duplicate the image and apply the greyscale pixels
            _previewBmp = GBImageUtils.TwoBPPColorToBitmapPreview(twobpp_values, _loadedBitmap.Width, _loadedBitmap.Height);

            // apply it to the preview picture box
            this.picPreview.Image = _previewBmp;
            this.picPreview.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void RefreshGeneratedTiles()
        {
            int duplicateTiles = 0;
            bool removeDupes = this.checkBox_RemoveDupes.Checked;

            // build the tile list and tile map
            _tilemapPreviewWidthInTiles = _previewBmp.Width / 8;

            GBImageUtils.ImageToGBTileListAndTileMap(_previewBmp, 
                out _generated_tile_list, 
                out _generated_tile_map, 
                !removeDupes, 
                out duplicateTiles);

            _previewTilesBmp = GBImageUtils.PreviewImageFromTileData(_generated_tile_list, _tilemapPreviewWidthInTiles, _palette);

            // set the preview image
            this.picTiles.Image = _previewTilesBmp;
            this.picTiles.SizeMode = PictureBoxSizeMode.Zoom;

            // resize the output picturebox to match
            this.picTiles.Size = new Size(
                _previewTilesBmp.Width* _tilemapPreviewMagnification, 
                _previewTilesBmp.Height* _tilemapPreviewMagnification);

            // populate the output data stats
            int numTiles = _generated_tile_list.Count();
            this.lblNumTiles.Text = string.Format( "Tiles: {0}", numTiles);
            this.lblDuplicateTiles.Text = string.Format("Duplicate Tiles: {0}", duplicateTiles);

            // calculte the image width and height in tiles
            tilemapWidth = _previewBmp.Width / 8;
            tilemapHeight = _previewBmp.Height / 8;
        }

        #endregion

        private void checkbox16Metatiles_CheckedChanged(object sender, EventArgs e)
        {
            RefreshAll(); 
        }
    }
}
