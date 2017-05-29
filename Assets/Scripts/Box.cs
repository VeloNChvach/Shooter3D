using UnityEngine;
using Shooter3D.Interface;

namespace Shooter3D
{
    public class Box : BaseObjectScene, ISetDamage
    {
        [SerializeField] private float _hp = 100;
        private bool _isDead = false;
        private float step = 2f;
        private float _timeToDestroy = 5f;

        public void Update()
        {
            if (_isDead)
            {
                Color color = GetMaterial.color;
                if (color.a > 0)
                {
                    color.a -= step / 100;
                    Color = color;
                }
                if (color.a < 1)
                {
                    Destroy(InstanceObject.GetComponent<Collider>());
                    Destroy(InstanceObject, _timeToDestroy);
                }
            }
        }

        public void ApplyDamage(float damage)
        {
            if (_hp > 0)
                _hp -= damage;
            
            if (_hp <= 0)
            {
                _hp = 0;
                Color = Color.red;
                _isDead = true;
            }
        }

    }
}
