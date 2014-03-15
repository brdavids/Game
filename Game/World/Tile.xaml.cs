using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class Tile : UserControl
    {
        private bool myMoveMode = false;

        private bool myFilled = false;

        public event EventHandler TileEntered;
        public event EventHandler TileTapped;

        public bool MoveMode
        {
            get
            {
                return myMoveMode;
            }
            set
            {
                myMoveMode = value;
                if (myMoveMode)
                {
                    myFilled = false;
                    myTile.Fill = new SolidColorBrush(Colors.Black);
                }
            }
        } 
            

        public Tile()
        {
            this.InitializeComponent();
        }

        private void myTile_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (MoveMode)
            {
                if (myFilled)
                    myTile.Fill = new SolidColorBrush(Colors.LightGray);
                else
                {
                    myFilled = true;
                    myTile.Fill = new SolidColorBrush(Colors.Gray);
                }

                if (TileEntered != null) TileEntered(this, null);
            }
        }

        private void myTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (MoveMode)
            {
                if (TileTapped != null) TileTapped(this, null);
            }
        }
    }
}
