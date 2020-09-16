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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextBoxInput
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.txt.Focus();
        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txt.TextChanged -= txt_TextChanged;
                Value value = new Value(this.txt.Text) { Owner = this };
                OrganizePosition(txt, value);
                this.txt.Text = this.OldText;
                value.ShowDialog();
                if (value.Confirmed)
                {
                    this.txt.Text = value.txtValue.Text;
                    this.SetValueOld();
                }
                this.txt.SelectionStart = this.txt.Text.Length;
            }
            catch
            {
            }
            finally
            {
                txt.TextChanged += txt_TextChanged;
            }
        }

        private string OldText = string.Empty;

        private void txt_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.SetValueOld();
        }

        private void SetValueOld()
        {
            OldText = this.txt.Text;
            this.lblOld.Text = OldText;
        }

        private void OrganizePosition(Control control, Value value)
        {
            PresentationSource source = PresentationSource.FromVisual(control);

            if (source != null)
            {
                double captionPointMargin = 0;

                var screen = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(Application.Current.MainWindow).Handle);
                Point location = control.PointToScreen(new Point(0, 0));

                double leftPosition = 0D;

                //leftPosition = System.Windows.Forms.Control.MousePosition.X - captionPointMargin;

                if (leftPosition < location.X)
                    leftPosition = location.X;
                else if (leftPosition > location.X + control.ActualWidth)
                    leftPosition = location.X + control.ActualWidth - (captionPointMargin * 2);

                if (((leftPosition < 0 && screen.WorkingArea.Width + leftPosition + value.Width < screen.WorkingArea.Width)) ||
                    leftPosition >= 0 && leftPosition + value.Width < screen.WorkingArea.Width)
                    value.Left = leftPosition;
                else
                    value.Left = location.X + (control.ActualWidth / 2) + captionPointMargin - value.Width;

                double height = 0;
                TextBox textbox = control as TextBox;
                if (textbox != null)
                {
                    if (control.Height > 0)
                        height = control.Height;
                    else
                        height = control.FontSize;
                    //height -= 16;
                    //height -= 5;
                    //value.txtValue.FontSize = textbox.FontSize + 10;
                    if (textbox.Width > 0)
                        value.Width = textbox.Width;
                }

                value.Top = location.Y + (control.ActualHeight / 2) - height;
            }
        }
    }
}