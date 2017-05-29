using UnityEngine;

namespace Shooter3D.Controller
{
    public class WeaponsController : BaseController
    {
        private Weapons _weapons;
        private Ammunition _ammunition;
        private int _mouseButton = (int)Main.MouseButton.LeftButton;

        public Weapons SelectedWeapons
        {
            get { return _weapons; }
        }

        public void Update()
        {
            if (!Enabled) return;

            if (Input.GetMouseButtonDown(_mouseButton))
                SelectedWeapons.Fire(_ammunition);
        }

        public virtual void On(Weapons weapons, Ammunition ammunition)
        {
            if (Enabled) return;

            base.On();
            _weapons = weapons;
            _ammunition = ammunition;
            _weapons.IsVisible = true;
        }

        public override void Off()
        {
            if (Enabled) return;
            base.Off();
            _weapons.IsVisible = false;
            _weapons = null;
            _ammunition = null;
        }
    }
}