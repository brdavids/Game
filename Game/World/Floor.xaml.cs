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

namespace Game2
{
    public sealed partial class Floor : UserControl
    {
        const int TILESIZE = 100;
        private bool myMoveMode = false;
        private List<Point> myPath = new List<Point>();

        public event EventHandler PointerExited;

        public bool MoveMode
        {
            set
            {
                myMoveMode = value;
                foreach (UIElement child in myGrid.Children)
                {
                    Tile tile = child as Tile;
                    if (tile != null)
                        tile.MoveMode = myMoveMode;
                }
                if (myMoveMode)
                    myPath.Clear();
            }
            get
            {
                return myMoveMode;
            }
        }

        public List<Point> Path
        {
            get { return myPath; }
        }

        public Floor()
        {
            this.InitializeComponent();

            for (int r = 0; r < myGrid.RowDefinitions.Count; r++)
            {
                for (int c = 0; c < myGrid.ColumnDefinitions.Count; c++)
                {
                    Tile tile = new Tile();
                    Grid.SetRow(tile, r);
                    Grid.SetColumn(tile, c);
                    tile.TileEntered += tile_TileEntered;
                    myGrid.Children.Add(tile);
                }
            }
        }

        void tile_TileEntered(object sender, EventArgs e)
        {
            if (myMoveMode)
            {
                double r = (double)Grid.GetRow((Tile)sender);
                double c = (double)Grid.GetColumn((Tile)sender);
                double w = TILESIZE;
                double h = TILESIZE;

                Point tileLoc = new Point(c * w, r * h);
                myPath.Add(tileLoc);
            }
        }

        private void myGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (myMoveMode && PointerExited != null)
                PointerExited(this, null);
        }
    }
}
