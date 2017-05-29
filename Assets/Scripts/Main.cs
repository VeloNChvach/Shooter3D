using UnityEngine;
using System.Collections.Generic;
using Shooter3D.Controller;
using Shooter3D.Helper;

namespace Shooter3D
{
    public sealed class Main : MonoBehaviour
    {
        private GameObject _controllersGameObject;
        private InputController _inputController;
        private FlashlightController _flashlightController;
        private WeaponsController _weaponsController;
        private ObjectManager _objectManager;

        public static Main Instance { get; private set; }

        public enum MouseButton
        {
            LeftButton,
            RightButton,
            CenterButton
        }

        private void Start()
        {
            Instance = this;
            _controllersGameObject = new GameObject { name = "Controllers" };
            _inputController = _controllersGameObject.AddComponent<InputController>();
            _flashlightController = _controllersGameObject.AddComponent<FlashlightController>();
            _weaponsController = _controllersGameObject.AddComponent<WeaponsController>();
            _objectManager = GetComponent<ObjectManager>();
        }

        public FlashlightController GetFlashlightController
        {
            get { return _flashlightController; }
        }

        public InputController GetInputController
        {
            get { return _inputController; }
        }

        public WeaponsController GetWeaponsController
        {
            get { return _weaponsController; }
        }

        public ObjectManager GetObjectManager
        {
            get { return _objectManager; }
        }
    }
}
