using UnityEngine;
using System.Collections;

namespace Shooter3D.Controller
{
    public class FlashlightController : BaseController
    {
        private Light _light;

        private void Awake()
        {
            _light = GameObject.Find("SpotLight").GetComponent<Light>();
        }

        public void Start()
        {
            SetActiveFlashlight(false);
        }

        public void Update()
        {
            if (!Enabled) return;
        }

        private void SetActiveFlashlight(bool value)
        {
            _light.enabled = value;
        }

        public override void On()
        {
            if (Enabled) return;
            base.On();
            SetActiveFlashlight(true);
        }

        public override void Off()
        {
            if (!Enabled) return;
            base.Off();
            SetActiveFlashlight(false);
        }
    }
}
