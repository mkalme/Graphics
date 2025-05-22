using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsDemo {
    public partial class ImageViewerForm : Form {
        public ImageViewerForm(Image image) {
            InitializeComponent();

            PictureBox.Image = image;
        }
    }
}
