using UnityEngine;

namespace Shooter3D
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        protected GameObject _instanceObject;
        protected Transform _transform;
        protected Rigidbody _rigidbody;
        protected Material _material;
        protected Color _color;
        protected Vector3 _position;
        protected Quaternion _rotation;
        protected Vector3 _scale;
        protected string _name;
        protected int _layer;
        protected bool _isVisible;

        protected virtual void Awake()
        {
            _instanceObject = gameObject;
            _name = _instanceObject.name;
            _rigidbody = _instanceObject.GetComponent<Rigidbody>();
            _transform = _instanceObject.transform;
            if (GetComponent<Renderer>())
                _material = GetComponent<Renderer>().material;
        }

        private void AskLayer(Transform obj, int lvl)
        {
            obj.gameObject.layer = lvl;
            if (obj.childCount > 0)
                foreach (Transform d in obj)
                    AskLayer(d, lvl);
        }

        private void AskColor(Transform obj, Color color)
        {
            if (obj.gameObject.GetComponent<Material>())
                obj.gameObject.GetComponent<Material>().color = color;
            if (obj.childCount > 0)
                foreach (Transform d in obj)
                    AskColor(d, color);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _instanceObject.name = _name;
            }
        }

        public int Layers
        {
            get { return _layer; }
            set
            {
                _layer = value;
                if (_instanceObject != null)
                {
                    _instanceObject.layer = _layer;
                    AskLayer(GetTransform, value);
                }
                    
            }
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (_material != null)
                {
                    _material.color = _color;
                    AskColor(GetTransform, value);
                }
            }
        }

        public Material GetMaterial
        {
            get { return _material; }
        }

        public Transform GetTransform
        {
            get { return _transform; }
        }

        public Vector3 Position
        {
            get
            {
                if (_instanceObject != null)
                    _position = GetTransform.position;
                return _position;
            }
            set
            {
                _position = value;
                if (_instanceObject != null)
                    GetTransform.position = _position;
            }
        }

        public Vector3 Scale
        {
            get
            {
                if (_instanceObject != null)
                    _scale = GetTransform.localScale;
                return _scale;
            }
            set
            {
                _scale = value;
                if (_instanceObject != null)
                    GetTransform.localScale = _scale;
            }
        }

        public Quaternion Roration
        {
            get
            {
                if (_instanceObject != null)
                    _rotation = GetTransform.rotation;
                return _rotation;
            }
            set
            {
                _rotation = value;
                if (_instanceObject != null)
                    GetTransform.rotation = _rotation;
            }
        }

        public Rigidbody GetRigidBody
        {
            get { return _rigidbody; }
        }

        public GameObject InstanceObject
        {
            get { return _instanceObject; }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                if (_instanceObject.GetComponent<MeshRenderer>())
                    _instanceObject.GetComponent<MeshRenderer>().enabled = _isVisible;
                if (_instanceObject.GetComponent<SkinnedMeshRenderer>())
                    _instanceObject.GetComponent<SkinnedMeshRenderer>().enabled = _isVisible;
            }
        }
    }
}