using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Windows.Controls;
using Core;// My model LIB
using System.Windows.Input;
using Prism.Commands;
using Servises;
using DataLayer;
using System.Windows;
using Pacman.View;
using UI = Servises.UINav;
using System.Configuration;
using System.Reflection;
using System.IO;
using Core.NPC;
using Logs;
using Pacman.Views;
using System.ComponentModel;

namespace Pacman.ViewModels
{
    class GridViewModel : BindableBase
    {
        private int _redGhostCount = 1;
        private int _blueGhostCount = 1;
        private int _greenGhostCount = 2;
        private Speeds _redGhostSpeed = Speeds.PlayerSpeed;
        private Speeds _blueGhostSpeed = Speeds.Medium;
        private Speeds _greenGhostSpeed = Speeds.Low;
        private List<PointAnimationHelper> _ghostAnimation { get; set; } = new List<PointAnimationHelper>();
        private GridModel _gridModel;
        /// <summary>
        /// <param name="IsPacmanDie">Pacman state </param>
        /// </summary>
        public bool IsPacmanDie { get; private set; }
        private UserScore _player;
        /// <summary>
        /// <param name="Player">Current Player information (name , score) </param>
        /// </summary>
        public UserScore Player
        {
            get { return _player; }
            private set { _player = value; }
        }

        public GridModel MyModel
        {
            get { return _gridModel; }
            set { SetProperty(ref _gridModel, value); }
        }
        private PacmanEssence pacman;
        private PointAnimationHelper AnimationHelper;
        private int[,] _field;
        private int _gameWinScore;
        private int _score;
        public ICommand StartCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand MoveCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public TextBlock _totalScore_Viewer { get; private set; }
        public TextBlock _playerName_Viewer { get; private set; }
        /// <summary>
        /// Create instance of game
        /// </summary>
        /// <param name="DynamicControls">The panel in which the playing field will be added</param>
        public GridViewModel(DockPanel DynamicControls)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"ViewModel Context Created"));
            _gridModel = new GridModel();
            MyModel.GridParent = DynamicControls;
            StartCommand = new DelegateCommand(StartMethod);
            ExitCommand = new DelegateCommand<bool?>(ExitMethod);
            PauseCommand = new DelegateCommand(PauseMethod);
            MoveCommand = new DelegateCommand<string>(MoveMethod);
            _totalScore_Viewer = UI.FindChild<TextBlock>(MyModel.GridParent, "totalScore");
            _playerName_Viewer = UI.FindChild<TextBlock>(MyModel.GridParent, "pName");
            AnimationHelper = new PointAnimationHelper();
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"ViewModel base objects successfully init"));

        }

        /// <summary>
        /// Create instance of game
        /// </summary>
        /// <param name="DynamicControls">The panel in which the playing field will be added</param>
        /// <param name="playerName">Current Player name</param>
        public GridViewModel(DockPanel DynamicControls, string playerName) : this(DynamicControls)
        {
            Player = new UserScore();
            Player.Name = playerName;
            _playerName_Viewer.Text = playerName;
        }

        private void MoveMethod(string s)
        {
            if (AnimationHelper.IsMovingNow)
            {
                return;
            }
            bool CanMove = false;
            int range = _field.GetUpperBound(0);
            switch (s)
            {
                case "Up":
                    if (pacman.Point.X > 0 && _field[pacman.Point.X - 1, pacman.Point.Y] != 1)
                    {
                        pacman.NextPoint = new FieldPoint(pacman.Point.X - 1, pacman.Point.Y);
                        pacman.ImgAngle = 90;
                        CanMove = true;
                        Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player moving {s} to [{pacman.NextPoint.X}:{pacman.NextPoint.Y}]"));
                    }
                    break;
                case "Down":
                    if (pacman.Point.X < range && _field[pacman.Point.X + 1, pacman.Point.Y] != 1)
                    {
                        pacman.NextPoint = new FieldPoint(pacman.Point.X + 1, pacman.Point.Y);
                        pacman.ImgAngle = 270;
                        CanMove = true;
                        Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player moving {s} to [{pacman.NextPoint.X}:{pacman.NextPoint.Y}]"));
                    }
                    break;
                case "Right":
                    if (pacman.Point.Y < range && _field[pacman.Point.X, pacman.Point.Y + 1] != 1)
                    {
                        pacman.NextPoint = new FieldPoint(pacman.Point.X, pacman.Point.Y + 1);
                        pacman.ImgAngle = 180;
                        CanMove = true;
                        Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player moving {s} to [{pacman.NextPoint.X}:{pacman.NextPoint.Y}]"));
                    }
                    break;
                case "Left":
                    if (pacman.Point.Y > 0 && _field[pacman.Point.X, pacman.Point.Y - 1] != 1)
                    {
                        pacman.NextPoint = new FieldPoint(pacman.Point.X, pacman.Point.Y - 1);
                        pacman.ImgAngle = 0;
                        CanMove = true;
                        Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player moving {s} to [{pacman.NextPoint.X}:{pacman.NextPoint.Y}]"));
                    }
                    break;
            }

            if (CanMove)
            {
                RenderMoving();
            }

        }

        private void RenderMoving()
        {
            try
            {
                Draw_pacman(MyModel.Field, pacman);
                MealDestroit(MyModel.Field, pacman.Point);
                pacman.Point = pacman.NextPoint;
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Render Error", ex));
            }
        }

        private void PauseMethod()
        {
            throw new NotImplementedException();
        }

        private void ExitMethod(bool? closing = false)
        {
            try
            {
                IsPacmanDie = true;
                if (closing != true)
                {
                    Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player exit game"));
                    (UI.GetTopLevelControl(MyModel.GridParent) as Window).Close();
                }
                else if (closing == true)
                {
                    Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player close window"));
                    MenuView menu = new MenuView();
                    menu.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }

        private void StartMethod()
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"New game started"));

            #region Map Size/Blocks

            int size;
            int blocks;
            if (Int32.TryParse(ConfigurationManager.AppSettings["Map_Blocks"], out blocks))
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Map_Blocks loaded from AppConfig - {blocks}"));
            }
            else
            {
                blocks = 20;
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Map_Blocks set as default - {blocks}"));
            }
            if (Int32.TryParse(ConfigurationManager.AppSettings["Map_Size"], out size))
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Map_Size loaded from AppConfig - {size}"));

            }
            else
            {
                size = 20;
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Map_Size set as default - {size}"));
            }
            #endregion
            var tmp = new GanerateServise(size, blocks);
            // make it look like a square
            tmp.ResultGrid.Width = MyModel.GridParent.ActualHeight;
            MyModel.Field = tmp.ResultGrid;
            _field = new int[size, size];
            _field = tmp.Field;
            _gameWinScore = SetGameWinScore(tmp.Field);
            if (IsPacmanDie)
            {
                IsPacmanDie = !IsPacmanDie;
                var s = UI.FindChild<Grid>(MyModel.GridParent, "MainField");
                MyModel.GridParent.Children.Remove(s);
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Old game field removed"));
                _score = 0;
                _ghostAnimation = new List<PointAnimationHelper>();
                AnimationHelper = new PointAnimationHelper();
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Reset all start params"));
            }
            MyModel.GridParent.Children.Add(tmp.ResultGrid);
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Game field Added at main View Model window"));
            pacman = new PacmanEssence(tmp.StartPoint, ImageCreator.CreateImage($@"Materials\Pacman\state1.png"));
            pacman.NextPoint = pacman.Point;
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Pacman assign to game"));
            Draw_pacman(tmp.ResultGrid, pacman, true);
            PointAnimationHelper.FlickerImage(pacman.MainImage, TimeSpan.FromSeconds(0.3));
            //pacman._animationStateImage.Add(ImageCreator.CreateImage($@"Materials\Pacman\state1.png"));
            //pacman._animationStateImage.Add(ImageCreator.CreateImage($@"Materials\Pacman\state2.png"));
            //StartMouthAnimation();
            AbstractGhost.ResetIndex();
            InitGhosts();
            CreateGhosts(_greenGhostCount, _blueGhostCount, _redGhostCount);
        }

        private void InitGhosts()
        {
            try
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Init Ghosts start"));
                if (bool.Parse((ConfigurationManager.AppSettings["IsAdvancedGhostOptionsEnable"])))
                {
                    Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Init advanced Ghosts options"));
                    _redGhostCount = int.Parse(ConfigurationManager.AppSettings["RedGhostCount"]);
                    _blueGhostCount = int.Parse(ConfigurationManager.AppSettings["BlueGhostCount"]);
                    _greenGhostCount = int.Parse(ConfigurationManager.AppSettings["GreenGhostCount"]);
                    _redGhostSpeed = (Speeds)Enum.Parse(typeof(Speeds), ConfigurationManager.AppSettings["RedGhostSpeed"]);
                    _blueGhostSpeed = (Speeds)Enum.Parse(typeof(Speeds), ConfigurationManager.AppSettings["BlueGhostSpeed"]);
                    _greenGhostSpeed = (Speeds)Enum.Parse(typeof(Speeds), ConfigurationManager.AppSettings["GreenGhostSpeed"]);
                    Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"{_redGhostCount} Red Ghost with {_redGhostSpeed} speed"));
                    Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"{_blueGhostCount} Blue Ghost with {_blueGhostSpeed} speed"));
                    Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"{_greenGhostCount} Green Ghost with {_greenGhostSpeed} speed"));
                }
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Succesful Ghosts Init "));
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }

        private int SetGameWinScore(int[,] field)
        {
            int winScore = 0;
            for (int i = 0; i <= field.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= field.GetUpperBound(0); j++)
                {
                    if (field[i, j] == 0)
                    {
                        winScore += 1;
                    }
                }
            }
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Game win Score was set on {winScore}"));
            return winScore;
        }

        private void GhostMoving(AbstractGhost ghost)
        {
            if (IsPacmanDie)
            {
                _ghostAnimation[ghost.Index].PacmanPosition -= PacmanCatchUp;
                _ghostAnimation[ghost.Index].PointAnimationHelperNotify -= GhostMoving;
            }
            else
            {
                _ghostAnimation[ghost.Index].MoveGost(ghost, ghost.Speed);
            }
        }

        private PacmanEssence PacmanCatchUp(AbstractGhost ghost)
        {
            try
            {
                if (ghost.FieldPointNow == pacman.Point)
                {
                    if (!IsPacmanDie)
                    {
                        Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Pacman die at {pacman.Point}"));
                        IsPacmanDie = true;
                        PacmanDie();
                    }
                }
                return pacman;
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
                return pacman;
            }
        }

        private void PacmanDie(bool Winner = false)
        {
            DieRestartWindow view;
            if (Winner)
            {
                view = new DieRestartWindow(_score, "Winner!");
            }
            else
            {
                view = new DieRestartWindow(_score);
            }
            MyModel.Field.Visibility = Visibility.Hidden;
            foreach (var it in _ghostAnimation)
            {
                it.ResesetIndex();
            }
            SaveIntoDB();
            if (bool.Parse(view.ShowDialog().ToString()))//restart
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player restart the game"));
                StartMethod();
            }
            else//to mainMenu
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player Exit to Main Menu"));
                ExitMethod();
            }
        }

        private void CreateGhosts(int Green, int Blue, int Red)
        {
            try
            {
                var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Plugins\\";
                if (Directory.Exists(dir))
                {
                    foreach (string fileName in Directory.GetFiles(dir))
                    {
                        if (fileName.Contains(".dll"))
                        {
                            if (fileName.Contains("Core") || fileName.Contains("Prism") ||  fileName.Contains("Logs")  || fileName.Contains("ValueTuple")) { continue; }
                            Type[] file = Assembly.LoadFrom(fileName).GetTypes();
                            var AIfromConfiguration = ConfigurationManager.AppSettings["AI_behavior"];
                            int AI_index = 0;
                            Int32.TryParse(AIfromConfiguration.Substring(0, 1), out AI_index);
                            dynamic sAI = Activator.CreateInstance(file[AI_index], _field);
                            for (int i = 0; i < Blue; i++)
                            {
                                var tmpGhost = new BlueGhost(sAI, ImageCreator.CreateImage($@"Materials\Ghosts\BlueGhost.png"));
                                tmpGhost.FieldPointNow = sAI.RandomPoint();
                                tmpGhost.FieldPointTarget = sAI.RandomPoint();
                                tmpGhost.Path = new PathCreator(_field).GetWay(tmpGhost.FieldPointNow, tmpGhost.FieldPointTarget);
                                tmpGhost.Speed = _blueGhostSpeed;

                                DrawGhost(MyModel.Field, tmpGhost);
                                _ghostAnimation.Add(new PointAnimationHelper());
                                _ghostAnimation[tmpGhost.Index].PointAnimationHelperNotify += GhostMoving;
                                _ghostAnimation[tmpGhost.Index].PacmanPosition += PacmanCatchUp;
                                _ghostAnimation[tmpGhost.Index].MoveGost(tmpGhost, tmpGhost.Speed);
                            }
                            for (int i = 0; i < Red; i++)
                            {
                                var tmpGhost = new RedGhost(sAI, ImageCreator.CreateImage($@"Materials\Ghosts\RedGhost.png"));
                                tmpGhost.FieldPointNow = sAI.RandomPoint();
                                tmpGhost.FieldPointTarget = sAI.RandomPoint();
                                tmpGhost.Path = new PathCreator(_field).GetWay(tmpGhost.FieldPointNow, tmpGhost.FieldPointTarget);
                                tmpGhost.Speed = _redGhostSpeed;

                                DrawGhost(MyModel.Field, tmpGhost);
                                _ghostAnimation.Add(new PointAnimationHelper());
                                _ghostAnimation[tmpGhost.Index].PointAnimationHelperNotify += GhostMoving;
                                _ghostAnimation[tmpGhost.Index].PacmanPosition += PacmanCatchUp;
                                _ghostAnimation[tmpGhost.Index].MoveGost(tmpGhost, tmpGhost.Speed);
                            }
                            for (int i = 0; i < Green; i++)
                            {
                                var tmpGhost = new GreenGhost(sAI, ImageCreator.CreateImage($@"Materials\Ghosts\GreenGhost.png"));
                                tmpGhost.FieldPointNow = sAI.RandomPoint();
                                tmpGhost.FieldPointTarget = sAI.RandomPoint();
                                tmpGhost.Path = new PathCreator(_field).GetWay(tmpGhost.FieldPointNow, tmpGhost.FieldPointTarget);
                                tmpGhost.Speed = _greenGhostSpeed;

                                DrawGhost(MyModel.Field, tmpGhost);
                                _ghostAnimation.Add(new PointAnimationHelper());
                                _ghostAnimation[tmpGhost.Index].PointAnimationHelperNotify += GhostMoving;
                                _ghostAnimation[tmpGhost.Index].PacmanPosition += PacmanCatchUp;
                                _ghostAnimation[tmpGhost.Index].MoveGost(tmpGhost, tmpGhost.Speed);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Plugins folder not found", "Worning", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }

        private void SaveIntoDB()
        {
            try
            {
                Player.Score = _score;
                ScoreData.SaveResult(Player.Name, Player.Score);
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Result of {Player.Name} was saved into DB"));
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"DB Error", ex));
            }
        }

        private void DrawGhost(Grid grid, AbstractGhost ghost)
        {
            try
            {
                Grid.SetRow(ghost.Model, ghost.FieldPointNow.X);
                Grid.SetColumn(ghost.Model, ghost.FieldPointNow.Y);
                grid.Children.Add(ghost.Model);
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Ghost was drawing to grid"));
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }


        #region Grid object drawing

        private void Draw_pacman(Grid grid, PacmanEssence pers, bool IsFirstTime = false)
        {
            try
            {
                AnimationHelper.Move(pacman.MainImage, pacman.ImgAngle, 0.25);

                MealDestroit(grid, pers.NextPoint);
                Grid.SetRow(pacman.MainImage, pers.NextPoint.X);
                Grid.SetColumn(pacman.MainImage, pers.NextPoint.Y);
                if (IsFirstTime)
                {
                    grid.Children.Add(pacman.MainImage);
                    Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Pacman model was added into field"));
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }

        public void MealDestroit(Grid grid, FieldPoint p)
        {
            try
            {
                foreach (UIElement control in grid.Children)
                {
                    if (Grid.GetRow(control) == p.X && Grid.GetColumn(control) == p.Y)
                    {
                        var matches = System.Text.RegularExpressions.Regex.Matches((control as Image).Source.ToString(), @"\/S.", System.Text.RegularExpressions.RegexOptions.None);
                        if (matches.Count > 0)
                        {
                            //imgNumber
                            //var x = matches[0].ToString().Substring(matches[0].ToString().Length - 1);
                            _score += 1;
                            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Score increased to {_score}"));
                            _totalScore_Viewer.Text = _score.ToString();
                            grid.Children.Remove(control);
                        }
                        //var s = (control as Image).Source.ToString().Contains("Ghost.png");

                        Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Meal at [{p.X}:{p.Y}] will  destroit "));
                        if (_score == _gameWinScore)
                        {
                            PacmanDie(true);
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }

        #endregion

       

        internal void OnWindowClosing(object sender, CancelEventArgs e)
        {
            try
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "[VIEW MODEL WINDOW CLOSING]"));
                SaveIntoDB();
                ExitMethod(true);
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "[VIEW MODEL WINDOW CLOSED]"));

            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Error", ex));
            }
        }

        private void KILL()
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Application was closed"));
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
