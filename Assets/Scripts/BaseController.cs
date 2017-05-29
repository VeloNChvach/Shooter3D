using UnityEngine;
using System.Collections;

namespace Shooter3D.Controller
{
    public abstract class BaseController : MonoBehaviour
    {
        private bool _enabled = false;

        public bool Enabled
        {
            get { return _enabled; }
            private set { _enabled = value; }
        }

        public virtual void On()
        {
            _enabled = true;
        }

        public virtual void Off()
        {
            _enabled = false;
        }
    }
}
