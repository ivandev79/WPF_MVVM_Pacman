using Logs;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace Servises
{
    /// <summary>
    /// Class helper with UI work
    /// </summary>
    public class UINav
    {
        /// <summary>
        /// Looking child UI Element by his name 
        /// </summary>
        /// <typeparam name="T">UI Element</typeparam>
        /// <param name="parent">UI Parent Element</param>
        /// <param name="childName">Looking child name</param>
        /// <returns>UI Element with entering childName</returns>
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            try
            {
                // Confirm parent and childName are valid. 
                if (parent == null) return null;

                T foundChild = null;

                int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    // If the child is not of the request child type child
                    T childType = child as T;
                    if (childType == null)
                    {
                        // recursively drill down the tree
                        foundChild = FindChild<T>(child, childName);

                        // If the child is found, break so we do not overwrite the found child. 
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = (T)child;
                            break;
                        }
                    }
                    else
                    {
                        // child element found.
                        foundChild = (T)child;
                        break;
                    }
                }

                return foundChild;
            }
            catch (Exception ex)
            {
                Logger.Add(new Log("UINav", MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
            return null;
        }

        /// <summary>
        /// Get top parent element
        /// </summary>
        /// <param name="control">Any UI Element</param>
        public static DependencyObject GetTopLevelControl(DependencyObject control)
        {
            try
            {
                DependencyObject tmp = control;
                DependencyObject parent = null;
                while ((tmp = VisualTreeHelper.GetParent(tmp)) != null)
                {
                    parent = tmp;
                }
                return parent;
            }
            catch (Exception ex)
            {
                Logger.Add(new Log("UINav", MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
            return null;
        }
    }
}
