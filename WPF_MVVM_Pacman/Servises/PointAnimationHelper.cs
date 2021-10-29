using Core;
using Core.NPC;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Servises
{
    /// <summary>
    /// Animete all moving in game
    /// </summary>
    public class PointAnimationHelper : Page
    {
       
        public delegate void AnimationHandler(AbstractGhost ghost);
        /// <summary>
        /// Moving logic creating animations
        /// </summary>
        public event AnimationHandler PointAnimationHelperNotify;  
        public delegate PacmanEssence PacmanHandler(AbstractGhost ghost);
        /// <summary>
        /// Take information about pacman
        /// </summary>
        public event PacmanHandler PacmanPosition;

        /// <summary>
        /// Pacman moving ?
        /// </summary>
        public bool IsMovingNow
        {
            get;
            private set;
        } = false;

        private int _pathIndex = 1;

        public PointAnimationHelper() { }
        #region Player Pers

        /// <summary>
        /// Animate moving and forbids key press while animation isn't complite 
        /// </summary>
        /// <param name="target">Image for moving</param>
        /// <param name="angle">Image rotation angle</param>
        /// <param name="time">Time to end</param>
        public void Move(Image target, int angle, double time)
        {
            IsMovingNow = true;
            TransformGroup myTransformGroup = new TransformGroup();
            var forvardVector = GetForwardFromAngle(angle);
            #region ImgRotation 
            if (angle == 180)
            {
                myTransformGroup.Children.Add(new ScaleTransform(-1, 1, target.Width / 2, target.Width / 2));
            }
            else
            {
                myTransformGroup.Children.Add(new RotateTransform(angle, target.Width / 2, target.Width / 2));
            }
            #endregion
            TranslateTransform trans = new TranslateTransform();
            myTransformGroup.Children.Add(trans);

            if (forvardVector.VectorType == "Y")
            {
                DoubleAnimation anim1 = new DoubleAnimation(forvardVector.range, 0,
                    TimeSpan.FromSeconds(time));
                anim1.Completed += new EventHandler(AnimationCoplete);
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }

            if (forvardVector.VectorType == "X")
            {
                DoubleAnimation anim2 = new DoubleAnimation(forvardVector.range, 0,
                   TimeSpan.FromSeconds(time));
                anim2.Completed += new EventHandler(AnimationCoplete);
                trans.BeginAnimation(TranslateTransform.XProperty, anim2);
            }
            target.RenderTransform = myTransformGroup;
        }

        public static void FlickerImage(Image img, TimeSpan fadeTime)
        {
            var fadeOut = new DoubleAnimation(0.5d, fadeTime);
            fadeOut.RepeatBehavior = RepeatBehavior.Forever;
            fadeOut.Completed += (s, e) =>
            {
                img.BeginAnimation(UIElement.OpacityProperty,
                    new DoubleAnimation(1d, fadeTime));
            };
            img.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void AnimationCoplete(object sender, EventArgs e)
        {
            IsMovingNow = false;
        }


        #endregion

        #region Ghosts
        /// <summary>
        ///  Move Ghost to him next  path point with custom speed
        /// </summary>
        /// <param name="ghost">Ghost what moving</param>
        /// <param name="speed">desired speed</param>
        public void MoveGost(AbstractGhost ghost, Speeds speed)
        {
            if (_pathIndex == 0)//ExitPoint
            {
                return;
            }
            double time = ((double)speed) / 100;
            TransformGroup myTransformGroup = new TransformGroup();
            var forvardVector = GetForwardForGost(ghost);

            TranslateTransform trans = new TranslateTransform();
            myTransformGroup.Children.Add(trans);

            if (forvardVector.VectorType == "Y")
            {
                DoubleAnimation anim1 = new DoubleAnimation(forvardVector.range, 0,
                    TimeSpan.FromSeconds(time));
                anim1.Completed += (s, e) => GhostAnimationCoplete(ghost, time);

                trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            }

            if (forvardVector.VectorType == "X")
            {
                DoubleAnimation anim2 = new DoubleAnimation(forvardVector.range, 0,
                   TimeSpan.FromSeconds(time));
                anim2.Completed += (s, e) => GhostAnimationCoplete(ghost, time);
                trans.BeginAnimation(TranslateTransform.XProperty, anim2);
            }
            ghost.Model.RenderTransform = myTransformGroup;
        }

        private void GhostAnimationCoplete(AbstractGhost ghost, double time)
        {
            if (_pathIndex == 0)//ExitPoint
            {
                return;
            }
            var pacman = PacmanPosition?.Invoke(ghost);
            if (ghost.Path==null)
            {
                ghost.Dispose();
                GC.Collect();
            }
            if (_pathIndex < ghost.Path.Count)
            {
                MoveGost(ghost, ghost.Speed);
            }
            else
            {
                ghost.Behavior.Think(ghost, pacman);
                _pathIndex = 1;
                PointAnimationHelperNotify?.Invoke(ghost);
            }
        }

        private ForvardVectorMovingRange GetForwardForGost(AbstractGhost ghost)
        {
            ForvardVectorMovingRange res;
            if (ghost.Path == null)
            {
                res.VectorType = "Y";
                res.range = 0;
                ghost.Dispose();
                GC.Collect();
                return res;
            }
            if (ghost.Path.Count<2)
            {
                res.VectorType = "Y";
                res.range = 0;
                return res;
            }
            if (ghost.Path.Count <= _pathIndex)
            {
                Grid.SetRow(ghost.Model, ghost.FieldPointNow.X);
                Grid.SetColumn(ghost.Model, ghost.FieldPointNow.Y);
                _pathIndex = 1;
            }
            double range = 50 * ImageCreator._scale;
            var tmp = ghost.FieldPointNow - ghost.Path[_pathIndex];
            if (tmp.Y == -1)//right
            {
                res.VectorType = "X";
                res.range = -range;
            }
            else if (tmp.Y == 1)//left
            {
                res.VectorType = "X";
                res.range = range;
            }
            else if (tmp.X == 1)//up
            {
                res.VectorType = "Y";
                res.range = range;
            }
            else if (tmp.X == -1)//down
            {
                res.VectorType = "Y";
                res.range = -range;
            }
            else
            {
                res.VectorType = "Y";
                res.range = range;
            }
            ghost.FieldPointNow = ghost.Path[_pathIndex];
            _pathIndex++;
            Grid.SetRow(ghost.Model, ghost.FieldPointNow.X);
            Grid.SetColumn(ghost.Model, ghost.FieldPointNow.Y);
            return res;
        } 
        #endregion

        private ForvardVectorMovingRange GetForwardFromAngle(int angle)
        {
            ForvardVectorMovingRange res;
            double range = 50*ImageCreator._scale;
            if (angle == 0)//left
            {
                res.VectorType = "X";
                res.range = range;
                return res;
            }
            else if (angle == 90)//up
            {
                res.VectorType = "Y";
                res.range = range;
                return res;
            }
            else if (angle==180)//right
            {
                res.VectorType = "X";
                res.range = -range;
                return res;
            }
            else//down
            {
                res.VectorType = "Y";
                res.range = -range;
                return res;
            }
        }

        /// <summary>
        /// Reset path indexes what allow their come to exit point
        /// </summary>
        public void ResesetIndex()
        {
            _pathIndex=0;
        }

        public struct ForvardVectorMovingRange
        {
            public string VectorType;
            public double range;
        }
    }
}
