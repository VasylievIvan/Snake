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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using System.Collections;

namespace Snake
{



    public class FPart : Button
    {
        public int Xprev;
        public int Yprev;

    }

    public class FHead : FPart
    {
        public string dir = "up";
    }

    public class FBody : FPart
    {
        public FPart prnt;

        public FBody()
        {
            prnt = null;
        }

        public FBody(FPart a)
        {
            prnt = a;
        }
    }

    
    public partial class MainWindow : Window
    {
        public FHead h1;
        public Queue<FPart> q1 = new Queue<FPart>();
        public FPart last;

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += timer_Tick;
            timer.Start();


            h1 = new FHead();
            h1.dir = "up";
            grid.Children.Add(h1);
            Grid.SetColumn(h1, 10);
            Grid.SetRow(h1, 10);
            h1.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            last = h1;


            AddPart();
            AddPart();
            AddPart();





        }

        void AddPart()
        {
            FBody b1 = new FBody(last);
            grid.Children.Add(b1);
            q1.Enqueue(b1);
            last = b1;
        }






        void timer_Tick(object sender, EventArgs e)
        {



            int x = Grid.GetRow(h1);
            int y = Grid.GetColumn(h1);

            h1.Xprev = x;
            h1.Yprev = y;

            if (h1.dir == "up")
            {
                if (x == 0)
                    x = 19;
                else
                    x--;
            }

            if (h1.dir == "down")
            {
                if (x == 19)
                    x = 0;
                else
                    x++;
            }

            if (h1.dir == "left")
            {
                if (y == 0)
                    y = 19;
                else
                    y--;
            }

            if (h1.dir == "right")
            {
                if (y == 19)
                    y = 0;
                else
                    y++;
            }



            Grid.SetRow(h1, x);
            Grid.SetColumn(h1, y);


            foreach (var obj in q1)
            {

                var btn = (FBody)obj;
                int xp = Grid.GetRow(btn);
                int yp = Grid.GetColumn(btn);

                btn.Xprev = xp;
                btn.Yprev = yp;




                Grid.SetRow(btn, btn.prnt.Xprev);
                Grid.SetColumn(btn, btn.prnt.Yprev);

            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {


            var btn = h1;

            if (e.Key == Key.Up)
                if (btn.dir != "down")
                    btn.dir = "up";

            if (e.Key == Key.Down)
                if (btn.dir != "up")
                    btn.dir = "down";

            if (e.Key == Key.Left)
                if (btn.dir != "right")
                    btn.dir = "left";


            if (e.Key == Key.Right)
                if (btn.dir != "left")
                    btn.dir = "right";

            if (e.Key == Key.A)
            {
                AddPart();
            }

        }
    }
}
