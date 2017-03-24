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
        public bool haveChanged = false;    //флаг, показывающий менялась ли переменная текущем тике
        public int x;
        public int y;
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

    public class FFood : Button
    {
        public int x;
        public int y;

        public FFood()
        {

        }

        public FFood(int xpos, int ypos)
        {
            x = xpos;
            y = ypos;        
        }
    }


    public partial class MainWindow : Window
    {
        public FHead h1;                                        //голова
        public List<FPart> q1 = new List<FPart>();            //массив из остальных частей
        public FPart last;
        public FFood food;
        Random random = new Random();
        public DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            
            timer.Interval = TimeSpan.FromMilliseconds(100);      //задаём интервал
            timer.Tick += timer_Tick;
                                                    //включаем таймер



            StartNewGame();




        }

        void StartNewGame()
        {
            h1 = new FHead();                                     //создаём голову
            h1.dir = "up";
            grid.Children.Add(h1);
            Grid.SetColumn(h1, 10);
            Grid.SetRow(h1, 10);
            h1.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            last = h1;


            AddPart();                                           //добавляем части тела
            AddPart();
            AddPart();


            FoodEaten();
            timer.Start();
        }

        void FoodEaten()
        {
            grid.Children.Remove(food);
            AddPart();
            SpawnFood();
        }

        void SpawnFood()
        {
            food = new FFood(random.Next(0, 19), random.Next(0, 19));
            food.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            grid.Children.Add(food);
            Grid.SetColumn(food, food.y);
            Grid.SetRow(food, food.x);
        }

        void AddPart()
        {
            FBody b1 = new FBody(last);
            grid.Children.Add(b1);
            q1.Add(b1);
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
            h1.haveChanged = false;

            if(x == food.x && y == food.y)
            {
                FoodEaten();
            }


            foreach (var obj in q1)
            {

                var btn = (FBody)obj;
                int xp = Grid.GetRow(btn);
                int yp = Grid.GetColumn(btn);

                btn.Xprev = xp;
                btn.Yprev = yp;




                Grid.SetRow(btn, btn.prnt.Xprev);
                Grid.SetColumn(btn, btn.prnt.Yprev);

                if(xp == x&&yp == y)
                {
                    MessageBox.Show("You lost!");
                    grid.Children.Clear();
                    q1.Clear();
                    timer.Stop();
                    StartNewGame();
                    break; 
                }

            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {


            var btn = h1;

            if (e.Key == Key.Up && btn.haveChanged == false)
                if (btn.dir != "down")
                {
                    btn.dir = "up";
                    btn.haveChanged = true;
                }

            if (e.Key == Key.Down && btn.haveChanged == false)
                if (btn.dir != "up")
                {
                    btn.dir = "down";
                    btn.haveChanged = true;
                }

            if (e.Key == Key.Left && btn.haveChanged == false)
                if (btn.dir != "right")
                {
                    btn.dir = "left";
                    btn.haveChanged = true;
                }

            if (e.Key == Key.Right && btn.haveChanged == false)
                if (btn.dir != "left")
                {
                    btn.dir = "right";
                    btn.haveChanged = true;
                }

            if (e.Key == Key.A)
            {
                AddPart();
            }

        }
    }
}
