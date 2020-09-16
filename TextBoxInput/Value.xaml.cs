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

namespace TextBoxInput
{
    /// <summary>
    /// Interaction logic for Value.xaml
    /// </summary>
    public partial class Value : Window
    {
        public bool Confirmed { get; set; }

        public Value(string value)
        {
            this.InitializeComponent();
            this.txtValue.Text = value;
            this.txtValue.SelectionStart = this.txtValue.Text.Length;
            this.txtValue.Focus();

            this.PreviewKeyDown += new KeyEventHandler(WPreviewKeyDown);
        }

        private void WPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void txtValue_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Confirmed = true;
                this.Close();
            }
        }
    }
}