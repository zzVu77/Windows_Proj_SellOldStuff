using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UTEMerchant
{
    /// <summary>
    /// Interaction logic for WinImageZoom.xaml
    /// </summary>
    public partial class WinImageZoom : Window
    {
        private readonly string _imagePath;
        private readonly Image _image;

        Point? _lastCenterPositionOnTarget;
        Point? _lastMousePositionOnTarget;
        Point? _lastDragPoint;

        public WinImageZoom()
        {
            InitializeComponent();
        }

        public WinImageZoom(string imagePath) : this()
        {
            _imagePath = imagePath;
        }

        public WinImageZoom(Image image) : this()
        {
            _image = image;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_imagePath != null)
            {
                img.Source = new BitmapImage(new Uri(_imagePath));
                return;
            }

            if (_image != null)
            {
                img.Source = _image.Source;
            }
        }

        private void svImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(svImage);

                double dX = posNow.X - _lastDragPoint.Value.X;
                double dY = posNow.Y - _lastDragPoint.Value.Y;
                _lastDragPoint = posNow;

                svImage.ScrollToHorizontalOffset(svImage.HorizontalOffset - dX);
                svImage.ScrollToVerticalOffset(svImage.VerticalOffset - dY);
            }
        }

        private void svImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(svImage);
            if (mousePos.X <= svImage.ViewportWidth && mousePos.Y <
                svImage.ViewportHeight) //make sure we still can use the scrollbars
            {
                svImage.Cursor = Cursors.SizeAll;
                _lastDragPoint = mousePos;
                Mouse.Capture(svImage);
            }
        }

        private void svImage_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _lastMousePositionOnTarget = Mouse.GetPosition(grdImage);

            if (e.Delta > 0)
            {
                sliImage.Value += 1;
            }
            if (e.Delta < 0)
            {
                sliImage.Value -= 1;
            }

            e.Handled = true;
        }

        private void svImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            svImage.Cursor = Cursors.Arrow;
            svImage.ReleaseMouseCapture();
            _lastDragPoint = null;
        }

        private void svImage_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            svImage.Cursor = Cursors.Arrow;
            svImage.ReleaseMouseCapture();
            _lastDragPoint = null;
        }

        private void svImage_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!_lastMousePositionOnTarget.HasValue)
                {
                    if (_lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(svImage.ViewportWidth / 2,
                            svImage.ViewportHeight / 2);
                        Point centerOfTargetNow =
                            svImage.TranslatePoint(centerOfViewport, grdImage);

                        targetBefore = _lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = _lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(grdImage);

                    _lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double factorX = e.ExtentWidth / grdImage.Width;
                    double refactorY = e.ExtentHeight / grdImage.Height;

                    double newOffsetX = svImage.HorizontalOffset -
                                        dXInTargetPixels * factorX;
                    double newOffsetY = svImage.VerticalOffset -
                                        dYInTargetPixels * refactorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    svImage.ScrollToHorizontalOffset(newOffsetX);
                    svImage.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        private void sliImage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScaleTransform != null)
            {
                ScaleTransform.ScaleX = e.NewValue;
                ScaleTransform.ScaleY = e.NewValue;

                var centerOfViewport = new Point(svImage.ViewportWidth / 2,
                    svImage.ViewportHeight / 2);
                _lastCenterPositionOnTarget = svImage.TranslatePoint(centerOfViewport, grdImage);
            }
        }
    }
}
