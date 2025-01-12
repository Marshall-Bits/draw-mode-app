using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace draw_mode_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point? _startPoint = null;
        private Line? _currentLine = null;
        private Brush? _currentBrush = Brushes.LightCoral;
        private bool _isBackgroundTransparent = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _startPoint = e.GetPosition(canvas);

                _currentLine = new Line
                {
                    Stroke = _currentBrush,
                    StrokeThickness = 5,
                    X1 = _startPoint.Value.X,
                    Y1 = _startPoint.Value.Y,
                    X2 = _startPoint.Value.X,
                    Y2 = _startPoint.Value.Y
                };

                canvas.Children.Add(_currentLine);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_startPoint.HasValue && _currentLine != null)
            {
                Point currentPosition = e.GetPosition(canvas);
                _currentLine.X2 = currentPosition.X;
                _currentLine.Y2 = currentPosition.Y;
            }
        }
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_startPoint.HasValue && _currentLine != null)
            {
                Point endPoint = e.GetPosition(canvas);
                _currentLine.X2 = endPoint.X;
                _currentLine.Y2 = endPoint.Y;
                _startPoint = null;
                _currentLine = null;
            }
        }

        private void ToggleBackground()
        {
            if (_isBackgroundTransparent)
            {
                var brush = (SolidColorBrush?)new BrushConverter().ConvertFrom("#01FFFFFF");
                if (brush != null)
                {
                    this.Background = brush;
                }
                _isBackgroundTransparent = false;
            }
            else
            {
                this.Background = Brushes.Transparent;
                _isBackgroundTransparent = true;
            }

            canvas.Children.Clear();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.T)
            {
                ToggleBackground();
                return;
            }


            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;

                case Key.D1:
                case Key.NumPad1:
                    _currentBrush = Brushes.LightCoral;
                    break;

                case Key.D2:
                case Key.NumPad2:
                    _currentBrush = Brushes.LightGreen;
                    break;

                case Key.D3:
                case Key.NumPad3:
                    _currentBrush = Brushes.LightYellow;
                    break;

                default:
                    break;
            }
        }
    }
}
