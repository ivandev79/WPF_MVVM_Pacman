using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Core.NPC
{
    public abstract class AbstractGhost : IDisposable
    {
        private dynamic _behavior;

        public dynamic Behavior
        {
            get { return _behavior; }
            private set { _behavior = value; }
        }

        private List<FieldPoint> _path;

        public List<FieldPoint> Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private FieldPoint _fieldPointNow;

        public FieldPoint FieldPointNow
        {
            get { return _fieldPointNow; }
            set { _fieldPointNow = value; }
        }

        private FieldPoint _fieldPointTarget;

        public FieldPoint FieldPointTarget
        {
            get { return _fieldPointTarget; }
            set { _fieldPointTarget = value; /*CreatePath(value);*/ }
        }

        private Image _imgModel;

        public Image Model
        {
            get { return _imgModel; }
            set { _imgModel = value; }
        }
        private static int _index = 0;
        public int Index { get;private set; } = _index++;
        public Speeds Speed { get; set; }

        public AbstractGhost(dynamic behavior)
        {
            Behavior = behavior;
            _path = new List<FieldPoint>();
        }

        public static void ResetIndex()
        {
            _index = 0;
        }

        public void Dispose()
        {
            _path = null;
            _imgModel = null;
            _behavior = null;
            GC.SuppressFinalize(this);
        }

    }
}
