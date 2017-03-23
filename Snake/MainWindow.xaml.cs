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

namespace Snake
{

   public class FButton : Button
    {
        public int X;
        public int Y;
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (var obj in grid.Children)
            {
                if (obj is FButton)
                {
                    var btn = (FButton)obj;
                    int x = Grid.GetRow(btn);
                    int y = Grid.GetColumn(btn);

                    if (e.Key == Key.Up)
                    {
                        if (x == 0)
                            x = 19;
                        else
                            x--;
                    }

                    if (e.Key == Key.Down)
                    {
                        if (x == 19)
                            x = 0;
                        else
                            x++;
                    }

                    if (e.Key == Key.Left)
                    {
                        if (y == 0)
                            y = 19;
                        else
                            y--;
                    }

                    if (e.Key == Key.Right)
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
    }
}
