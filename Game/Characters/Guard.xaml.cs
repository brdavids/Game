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
    public sealed partial class Guard : UserControl
    {
        private List<Point> myPatrolPath = new List<Point>();
        private double mySpeed;

        public Point Destination;
        public int PatrolIndex = 0;

        public List<Point> PatrolPath
        {
            get { return myPatrolPath; }
        }

        public double Speed { get { return mySpeed; } }
        
        public Guard(List<Point> patrolPath, double speed)
        {
            myPatrolPath = patrolPath;
            Destination = myPatrolPath[0];
            mySpeed = speed;
            this.InitializeComponent();
        }

        public void LookHere(bool left, bool up)
        {
            if (left)
            {
                Thickness newMargin1 = new Thickness(27, myPupil1.Margin.Top, myPupil1.Margin.Right, myPupil1.Margin.Bottom);
                Thickness newMargin2 = new Thickness(58, myPupil2.Margin.Top, myPupil2.Margin.Right, myPupil2.Margin.Bottom);
                myPupil1.Margin = newMargin1;
                myPupil2.Margin = newMargin2;
            }
            else
            {
                Thickness newMargin1 = new Thickness(35, myPupil1.Margin.Top, myPupil1.Margin.Right, myPupil1.Margin.Bottom);
                Thickness newMargin2 = new Thickness(66, myPupil2.Margin.Top, myPupil2.Margin.Right, myPupil2.Margin.Bottom);
                myPupil1.Margin = newMargin1;
                myPupil2.Margin = newMargin2;
            }
            if (up)
            {
                Thickness newMargin1 = new Thickness(myPupil1.Margin.Left, 34, myPupil1.Margin.Right, myPupil1.Margin.Bottom);
                Thickness newMargin2 = new Thickness(myPupil2.Margin.Left, 34, myPupil2.Margin.Right, myPupil2.Margin.Bottom);
                myPupil1.Margin = newMargin1;
                myPupil2.Margin = newMargin2;
            }
            else
            {
                Thickness newMargin1 = new Thickness(myPupil1.Margin.Left, 41, myPupil1.Margin.Right, myPupil1.Margin.Bottom);
                Thickness newMargin2 = new Thickness(myPupil2.Margin.Left, 41, myPupil2.Margin.Right, myPupil2.Margin.Bottom);
                myPupil1.Margin = newMargin1;
                myPupil2.Margin = newMargin2;
            }
        }
    }
}
