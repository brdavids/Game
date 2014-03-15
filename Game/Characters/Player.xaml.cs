using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Game
{
    public sealed partial class Player : UserControl
    {
        public bool Selected = false;

        public event EventHandler PlayerSelected;
        public event EventHandler PlayerDeselected;

        public Player()
        {
            this.InitializeComponent();
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Selected = !Selected;
            if (Selected && PlayerSelected != null) PlayerSelected(this, null);
        }

        private void Grid_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Selected = !Selected;
            if (!Selected && PlayerDeselected != null) PlayerDeselected(this, null);
        }
    }
}
