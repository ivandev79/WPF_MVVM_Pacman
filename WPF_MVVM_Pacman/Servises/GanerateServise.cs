using Core;
using Logs;
using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace Servises
{
    /// <summary>
    /// Helper class for ganeration map logic
    /// </summary>
    public class GanerateServise
    {
        private Grid _resultGrid;
        public Grid ResultGrid
        {
            get { return _resultGrid; }
            private set { _resultGrid = value; }
        }
       
        private FieldPoint _startPoint;
        /// <summary>
        /// Get pacman StartPoint.What was generated random
        /// </summary>
        public FieldPoint StartPoint
        {
            get { return _startPoint; }
            private set { _startPoint = value; }
        }

        private int[,] _field;
        public int[,] Field
        {
            get { return _field; }
        }

        private int _range;
        private int _blocks;
        private Random _r;// i know it's bad name

        /// <summary>
        /// Create Game world
        /// </summary>
        /// <param name="gameFieldRange">It's will set size of game field</param>
        /// <param name="blocks">% of territories will be blocked. 1 = 1%</param>
        public GanerateServise(int gameFieldRange, int blocks)
        {
            try
            {
                _field = new int[gameFieldRange, gameFieldRange];
                _r = new Random();
                _range = gameFieldRange;
                _blocks = CheckBloksCount(blocks);
                Randomize();
                Check();
                CreateGrid();
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Game Field successfully ganerated"));
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error",ex));
            }
        }

        /// <summary>
        /// Checks how many % of field are blocks
        /// </summary>
        /// <returns>Real max block count for this field</returns>
        private int CheckBloksCount(int blocks)
        {
            try
            {
                if (blocks > 40)
                {
                    blocks = 40;
                }
                else if (blocks < 10)
                {
                    blocks = 10;
                }

                return Convert.ToInt32(Math.Floor(_range * _range * (Convert.ToDouble(blocks) / 100.0)));
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
            return 1;
        }

        private void CreateGrid()
        {
            ResultGrid = new Grid();
            ResultGrid.Name = "MainField";
            ResultGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);
            //ResultGrid.ShowGridLines = true;
            for (int i = 0; i < _range; i++)
            {
                ResultGrid.RowDefinitions.Add(new RowDefinition());
                ResultGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            DrawWallsEat();
        }

        #region UI Fill grid objects
        private void DrawWallsEat()
        {
            ImageCreator._scale =  1/((double)_range / 15) ;
            for (int i = 0; i < _range; i++)
            {
                for (int j = 0; j < _range; j++)
                {
                    if (_field[i, j] == 1) { draw_UIWall(i, j); }
                    else draw_UIEat(i, j);
                    #region Print cells coords
                    //Label lblDynamic = new Label()
                    //{
                    //    Content = $"{i + 1} , {j + 1}"
                    //};
                    //Grid.SetRow(lblDynamic, i);
                    //Grid.SetColumn(lblDynamic, j);
                    //ResultGrid.Children.Add(lblDynamic);
                    #endregion
                }
            }
        }

        private void draw_UIWall(int x, int y)
        {
            var img = ImageCreator.CreateImage(@"Materials\GameObjects\wall.png");
            Grid.SetRow(img, x);
            Grid.SetColumn(img, y);
            ResultGrid.Children.Add(img);
        }
        private void draw_UIEat(int x, int y)
        {
            var img = ImageCreator.CreateImage($@"Materials\GameObjects\S{_r.Next(3,8)}.png");
            Grid.SetRow(img, x);
            Grid.SetColumn(img, y);
            ResultGrid.Children.Add(img);
        }
        #endregion

        private void Check()
        {
            WayToPoint wayPoint = new WayToPoint(_field,_range);
            StartPoint = wayPoint.StartPoint = SetStartOrigin();
            wayPoint.CheckField();
        }

        private FieldPoint SetStartOrigin()
        {
            FieldPoint sPoint = new FieldPoint { X=-1,Y=-1};
            FieldPoint tmp;
            do
            {
                tmp = new FieldPoint { X = _r.Next(0, _range), Y = _r.Next(0, _range) };
                if (_field[tmp.X, tmp.Y] == 0)
                {
                    sPoint.X = tmp.X;
                    sPoint.Y = tmp.Y;
                }
            } while (sPoint.X == -1);
            return sPoint;
        }

        private void Randomize()
        {
            // now 1 its wall , 0 can move

            int blocksNow = 0;
            int blocksMax = _blocks;
            for (int i = 0; i < _range; i++)
            {
                if (blocksNow > blocksMax) { break; }
                for (int j = 0; j < _range; j++)
                {
                    int r = _r.Next(0, 2);
                    blocksNow += r == 1 ? 1 : 0;
                    _field[i, j] = blocksNow <= blocksMax ? r : 0;
                    if (blocksNow <= blocksMax) { _field[i, j] = r; }
                    else break;
                }
            }
            Shuffle(new Random(), _field);
        }

        public static void Shuffle<T>(Random random, T[,] array)
        {
            int lengthRow = array.GetLength(1);

            for (int i = array.Length - 1; i > 0; i--)
            {
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;

                int j = random.Next(i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;

                T temp = array[i0, i1];
                array[i0, i1] = array[j0, j1];
                array[j0, j1] = temp;
            }
        }
    }
}
