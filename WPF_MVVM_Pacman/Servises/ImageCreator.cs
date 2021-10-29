using Logs;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace Servises
{
    /// <summary>
    /// Create image what use like model in game.Also have image scale coefficient
    /// </summary>
    public class ImageCreator
    {
        /// <summary>
        /// Image scale coefficient
        /// </summary>
        public static double _scale { get; set; } = 1;
        public ImageCreator() { }
        /// <summary>
        /// Create and scale  Image by path 
        /// </summary>
        /// <param name="path">Path of image</param>
        /// <returns>Image after scale</returns>
        public static Image CreateImage(string path)
        {
            try
            {
                Image Mole = new Image();
                Mole.Width = 45 * _scale;
                Mole.Height = 45 * _scale;
                Mole.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(path, UriKind.Relative));
                return Mole;
            }
            catch (Exception ex)
            {
                Logger.Add(new Log("CreateImage", MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
                return null;
        }
    }
}
