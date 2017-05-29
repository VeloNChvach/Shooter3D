using Shooter3D.Interface;
using UnityEngine;

namespace Shooter3D
{
    public class Bullet : Ammunition
    {
        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _damage = 20;
        [SerializeField] private float _mass = 0.01f;

        private float _currentDamage;

        protected override void Awake()
        {
            base.Awake();
            Destroy(InstanceObject, _timeToDestruct);

            _currentDamage = _damage;
            GetRigidBody.mass = _mass;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet") return;

            SetDamage(collision.gameObject.GetComponent<ISetDamage>());

            Destroy(InstanceObject, _timeToDestruct); // Delete bullet
        }

        private void SetDamage(ISetDamage obj)
        {
            if (obj != null)
                obj.ApplyDamage(_currentDamage);
        }
    }
}
