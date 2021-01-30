using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBImageConvertGUI
{
    public partial class FormMain : Form
    {
        private Button selectedButton = null;

        private Color SelectedColor = Color.FromArgb(46, 51, 73);
        private Color UnselectedColor = Color.FromArgb(23, 30, 54);

        FormImgConverter _imgConverterPanel = null;
        FormMapConverter _mapConverterPanel = null;

        Form _currentForm = null;

        private static FormMain _singleton = null;
        public static FormMain GetSingleton()
        {
            return _singleton;
        }

        public FormImgConverter GetImgConverterForm()
        {
            return _imgConverterPanel;
        }

        public FormMain()
        {
            _singleton = this;

            InitializeComponent();

            PanelNav.Height = btnImgConverter.Height;
            PanelNav.Top = btnImgConverter.Top;
            PanelNav.Left = btnImgConverter.Left;
            btnImgConverter.BackColor = SelectedColor;
            selectedButton = btnImgConverter;

            // instantiate our panels
            pnlMain.Controls.Clear();
            _imgConverterPanel = new FormImgConverter() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            _imgConverterPanel.FormBorderStyle = FormBorderStyle.None;
            pnlMain.Controls.Add(_imgConverterPanel);

            // show the current panel
            _currentForm = _imgConverterPanel;
            _currentForm.Show();
        }

        private void ClearSelectedButtonColor()
        {
            selectedButton.BackColor = UnselectedColor;
        }

        private void btnImgConverter_Click(object sender, EventArgs e)
        {
            if (_currentForm != _imgConverterPanel)
            {
                if (_currentForm != null)
                {
                    _currentForm.Hide();
                }

                ClearSelectedButtonColor();
                PanelNav.Height = btnImgConverter.Height;
                PanelNav.Top = btnImgConverter.Top;
                PanelNav.Left = btnImgConverter.Left;
                btnImgConverter.BackColor = SelectedColor;
                selectedButton = btnImgConverter;

                _currentForm = _imgConverterPanel;
                _currentForm.Show();
            }
        }

        // sprite sheets
        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentForm != null)
            {
                _currentForm.Hide();

                ClearSelectedButtonColor();
                PanelNav.Height = btnSpriteSheets.Height;
                PanelNav.Top = btnSpriteSheets.Top;
                PanelNav.Left = btnSpriteSheets.Left;
                btnSpriteSheets.BackColor = SelectedColor;
                selectedButton = btnSpriteSheets;

                _currentForm = null;
                //_currentForm.Show();
            }
        }

        // meta sprites
        private void button2_Click(object sender, EventArgs e)
        {
            if (_currentForm != null)
            {
                _currentForm.Hide();

                ClearSelectedButtonColor();
                PanelNav.Height = btnMetaSprites.Height;
                PanelNav.Top = btnMetaSprites.Top;
                PanelNav.Left = btnMetaSprites.Left;
                btnMetaSprites.BackColor = SelectedColor;
                selectedButton = btnMetaSprites;

                _currentForm = null;
                //_currentForm.Show();
            }
        }

        // bg maps
        private void button3_Click(object sender, EventArgs e)
        {
            if (_currentForm != null)
            {
                _currentForm.Hide();

                ClearSelectedButtonColor();
                PanelNav.Height = btnBGMaps.Height;
                PanelNav.Top = btnBGMaps.Top;
                PanelNav.Left = btnBGMaps.Left;
                btnBGMaps.BackColor = SelectedColor;
                selectedButton = btnBGMaps;

                _currentForm = null;
                //_currentForm.Show();
            }
        }

        // collision maps
        private void button4_Click(object sender, EventArgs e)
        {
            if (_currentForm != null)
            {
                _currentForm.Hide();

                ClearSelectedButtonColor();
                PanelNav.Height = btnCollisionMaps.Height;
                PanelNav.Top = btnCollisionMaps.Top;
                PanelNav.Left = btnCollisionMaps.Left;
                btnCollisionMaps.BackColor = SelectedColor;
                selectedButton = btnCollisionMaps;

                _currentForm = null;
                //_currentForm.Show();
            }
        }

        // settings
        private void button5_Click(object sender, EventArgs e)
        {
            if (_currentForm != null)
            {
                _currentForm.Hide();

                ClearSelectedButtonColor();
                PanelNav.Height = btnSettings.Height;
                PanelNav.Top = btnSettings.Top;
                PanelNav.Left = btnSettings.Left;
                btnSettings.BackColor = SelectedColor;
                selectedButton = btnSettings;

                _currentForm = null;
                //_currentForm.Show();
            }
        }

        private void btnMapConverter_Click(object sender, EventArgs e)
        {
            if(_mapConverterPanel == null)
            {
                _mapConverterPanel = new FormMapConverter() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                _mapConverterPanel.FormBorderStyle = FormBorderStyle.None;
                pnlMain.Controls.Add(_mapConverterPanel);
            }

            if (_currentForm != _mapConverterPanel)
            {
                if (_currentForm != null)
                {
                    _currentForm.Hide();
                }

                ClearSelectedButtonColor();
                PanelNav.Height = btnMapConverter.Height;
                PanelNav.Top = btnMapConverter.Top;
                PanelNav.Left = btnMapConverter.Left;
                btnMapConverter.BackColor = SelectedColor;
                selectedButton = btnMapConverter;

                _currentForm = _mapConverterPanel;
                _currentForm.Show();
                _mapConverterPanel.Refresh();
            }
        }
    }
}
