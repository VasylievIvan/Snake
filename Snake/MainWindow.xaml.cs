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

namespace Snake
{
      
   public class FHead : Button
    {
        public string dir = "up";
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += timer_Tick;
            timer.Start();

            InitializeComponent();
        }



        void timer_Tick(object sender, EventArgs e)
        {
            foreach (var obj in grid.Children)
            {
                if (obj is FHead)
                {
                    var btn = (FHead)obj;
                    int x = Grid.GetRow(btn);
                    int y = Grid.GetColumn(btn);

                    if (btn.dir == "up")
                    {
                        if (x == 0)
                            x = 19;
                        else
                            x--;
                    }

                    if (btn.dir == "down")
                    {
                        if (x == 19)
                            x = 0;
                        else
                            x++;
                    }

                    if (btn.dir == "left")
                    {
                        if (y == 0)
                            y = 19;
                        else
                            y--;
                    }

                    if (btn.dir == "right")
                    {
                        if (y == 19)
                            y = 0;
                        else
                            y++;
                    }

                    Grid.SetRow(btn, x);
                    Grid.SetColumn(btn, y);
                }
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (var obj in grid.Children)           
                if (obj is FHead)
                {
                    var btn = (FHead)obj;

                    if (e.Key == Key.Up)
                        btn.dir = "up";

                    if (e.Key == Key.Down)
                            btn.dir = "down";                    

                    if (e.Key == Key.Left)
                        btn.dir = "left";


                    if (e.Key == Key.Right)
                        btn.dir = "right";

                    
                }            
        }
    }
}
