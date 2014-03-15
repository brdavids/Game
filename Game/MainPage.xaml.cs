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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Game2
{
    //IDEA: Levels can be composed of multiple floors chained together



    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const double FRAMERATE = 60;
        const double PLAYER_SPEED = 4;

        DispatcherTimer myPlayerTimer = new DispatcherTimer();
        DispatcherTimer myGuardsTimer = new DispatcherTimer();
        Player myPlayer = new Player();
        Guard myGuard1;
        Guard myGuard2;
        List<Guard> myGuards = new List<Guard>();

        Floor myFloor = new Floor();
        Goal myGoal = new Goal();
        List<Point> myPlayerMoveQueue = new List<Point>();

        public MainPage()
        {
            this.InitializeComponent();            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myPlayerTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000 * (1.0 / FRAMERATE)));
            myGuardsTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000 * (1.0 / FRAMERATE)));

            SetupLevel();
        }

        private void SetupLevel()
        {
            myFloor.PointerExited += myFloor_PointerExited;
            myCanvas.Children.Add(myFloor);
            double top = Canvas.GetTop(myCanvas);
            double left = Canvas.GetLeft(myCanvas);

            Canvas.SetTop(myGoal, top + myCanvas.Height - myGoal.Height);
            Canvas.SetLeft(myGoal, left + myCanvas.Width - myGoal.Width);
            myGoal.GoalEntered += myGoal_GoalEntered;
            myCanvas.Children.Add(myGoal);

            Canvas.SetTop(myPlayer, top);
            Canvas.SetLeft(myPlayer, left);
            myPlayer.PlayerSelected += myPlayer_PlayerSelected;
            myCanvas.Children.Add(myPlayer);

            List<Point> path1 = new List<Point>();
            path1.Add(new Point(left + (2 * 100), top));
            path1.Add(new Point(left + (12 * 100), top));
            myGuard1 = new Guard(path1, 2);
            Canvas.SetTop(myGuard1, top);
            Canvas.SetLeft(myGuard1, left + (12 * 100));
            myGuards.Add(myGuard1);
            myCanvas.Children.Add(myGuard1);

            List<Point> path2 = new List<Point>();
            path2.Add(new Point(left + (7 * 100), top));
            path2.Add(new Point(left + (7 * 100), top + (7 * 100)));
            myGuard2 = new Guard(path2, 3);
            Canvas.SetTop(myGuard2, top + (7 * 100));
            Canvas.SetLeft(myGuard2, left + (7 * 100));
            myGuards.Add(myGuard2);
            myCanvas.Children.Add(myGuard2);

            myGuardsTimer.Tick += myGuardsTimer_Tick;
            myGuardsTimer.Start();
            myPlayerTimer.Tick += myPlayerTimer_Tick;
        }

        void myFloor_PointerExited(object sender, EventArgs e)
        {
            Go();
        }

        void myGuardsTimer_Tick(object sender, object e)
        {
            foreach (Guard g in myGuards)
            {
                double x = Canvas.GetLeft(g);
                double y = Canvas.GetTop(g);

                if (Math.Abs(g.Destination.X - x) < g.Speed && Math.Abs(g.Destination.Y - y) < g.Speed)
                {
                    g.PatrolIndex++;
                    if (g.PatrolIndex < g.PatrolPath.Count)
                    {
                        g.Destination = g.PatrolPath[g.PatrolIndex];
                    }
                    else
                    {
                        g.PatrolIndex = 0;
                        g.Destination = g.PatrolPath[g.PatrolIndex];
                    }
                }

                if (g.Destination.X > x)
                {
                    x += g.Speed;
                }
                else if (g.Destination.X < x)
                {
                    x -= g.Speed;
                }
                if (g.Destination.Y > y)
                {
                    y += g.Speed;

                }
                else if (g.Destination.Y < y)
                {
                    y -= g.Speed;
                }

                Canvas.SetLeft(g, x);
                Canvas.SetTop(g, y);

                double dx = Canvas.GetLeft(myPlayer) - x;
                double dy = Canvas.GetTop(myPlayer) - y;
                if (Math.Abs(dx) < 50 && Math.Abs(dy) < 50)
                {
                    myTxtLoser.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    myGuardsTimer.Stop();
                    myPlayerTimer.Stop();
                }
                g.LookHere(dx < 0, dy < 0);
            }
        }

        void myGoal_GoalEntered(object sender, EventArgs e)
        {
            if (myFloor.MoveMode)
            {
                Go();
            }
        }

        void myPlayer_PlayerSelected(object sender, EventArgs e)
        {
            myFloor.MoveMode = true;
            
        }

        private void Go()
        {
            myFloor.MoveMode = false;
            List<Point> path = myFloor.Path;
            double x = Canvas.GetLeft(myPlayer);
            double y = Canvas.GetTop(myPlayer);
            int pathLength = path.Count;

            for (int i = 0; i < pathLength; i++)
            {
                double dx = path[0].X - x;
                double dy = path[0].Y - y;
                double xStep = (dx / FRAMERATE) * PLAYER_SPEED;
                double yStep = (dy / FRAMERATE) * PLAYER_SPEED;

                for (int j = 0; j < (int)(FRAMERATE / PLAYER_SPEED); j++)
                {
                    x += xStep;
                    y += yStep;
                    myPlayerMoveQueue.Add(new Point(x, y));
                }
                path.RemoveAt(0);
            }

            myPlayerTimer.Start();
            myPlayer.PlayerSelected -= myPlayer_PlayerSelected;
        }

        void myPlayerTimer_Tick(object sender, object e)
        {
            if (myPlayerMoveQueue.Count > 0)
            {
                Canvas.SetLeft(myPlayer, myPlayerMoveQueue[0].X);
                Canvas.SetTop(myPlayer, myPlayerMoveQueue[0].Y);
                myPlayerMoveQueue.RemoveAt(0);
            }
            else
            {
                myPlayerTimer.Stop();
                myPlayer.Selected = false;
                myPlayer.PlayerSelected += myPlayer_PlayerSelected;
            }

            if(Math.Abs((Canvas.GetLeft(myPlayer) - Canvas.GetLeft(myGoal))) < 50 &&
                Math.Abs(Canvas.GetTop(myPlayer) - Canvas.GetTop(myGoal)) < 50)
            {
                myTxtLoser.Text = "You Win!";
                myTxtLoser.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
    }
}
