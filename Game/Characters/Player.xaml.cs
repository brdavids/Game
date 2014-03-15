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

        public Player()
        {
            this.InitializeComponent();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Selected = !Selected;
            if (Selected)
            {
                //mySelectionBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
                if (PlayerSelected != null)
                    PlayerSelected(this, null);
            }
            else
            {
                //mySelectionBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
