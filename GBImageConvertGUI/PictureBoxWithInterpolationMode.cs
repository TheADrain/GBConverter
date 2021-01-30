using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GBImageConvertGUI
{
    public class PictureBoxWithInterpolationMode : PictureBox
    {
        public InterpolationMode Mode { get; set; }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            paintEventArgs.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            base.OnPaint(paintEventArgs);
        }
    }
}