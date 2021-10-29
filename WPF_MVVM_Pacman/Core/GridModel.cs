using System.Windows.Controls;
using Prism.Mvvm;

namespace Core
{
    public class GridModel : BindableBase
    {

        private DockPanel _gridParent;
        public DockPanel GridParent
        {
            get { return _gridParent; }
            set { SetProperty(ref _gridParent, value); }
        }
      
        private Grid _grid;
        public Grid Field
        {
            get { return _grid; }
            set { SetProperty(ref _grid, value); }
        }
    }
}
