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

namespace Game.World
{
    /// <summary>
    /// Base class for tiles added to the floor class
    /// </summary>
    public sealed partial class Tile : UserControl
    {
        private bool myMoveMode = false;
        private bool myFilled = false;
        private SolidColorBrush myDefaultColor = new SolidColorBrush(Colors.Black);
        private Type myType = Type.floor;

        public event EventHandler TileEntered;
        public event EventHandler CancelMoveMode;

        public enum Type
        {
            floor,
            wall
        }

        public Type TileType
        {
            get { return myType; }
            set
            {
                myType = value;
                if (myType == Type.wall) myDefaultColor = new SolidColorBrush(Colors.DarkRed);
                else if (myType == Type.floor) myDefaultColor = new SolidColorBrush(Colors.Black);
                myTile.Fill = myDefaultColor;
            }
        }

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
                    myTile.Fill = myDefaultColor;
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
                if (myType == Type.wall)
                {
                    if (CancelMoveMode != null) CancelMoveMode(this, null);
                }
                else if (myType == Type.floor)
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
        }

        private void myTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (MoveMode)
            {
                if (CancelMoveMode != null) CancelMoveMode(this, null);
            }
        }
    }
}
