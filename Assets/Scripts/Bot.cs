using System.Collections.Generic;
using UnityEngine;
using Shooter3D.Interface;
using UnityEngine.AI;
using System;

namespace Shooter3D
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Bot : BaseObjectScene, ISetDamage
    {
        [SerializeField] private float _hp = 100;
        private bool _isDeath;
        private float _deathTime;
        private readonly float _timeWait = 3;
        private List<Vector3> _wayPoints = new List<Vector3>();
        private int _wayCount;
        private float _curTimeout;
        private Transform _target;
        private bool _isTarget;
        private float _stoppingDistance;
        private float _distance;
        private float _activeDistance = 50;
        private float _activeAngle = 60;
        private Weapons _weapons;

        public NavMeshAgent _agent { get; private set; }
        public Transform Target
        {
            get { return _target; }
            set { _target = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            _isDeath = false;
            _agent = GetComponent<NavMeshAgent>();
            _weapons = GetTransform.Find("Gun").GetComponent<Weapons>();
        }

        public void ApplyDamage(float damage)
        {
            if (_hp > 0)
                _hp -= damage;

            if (_hp <= 0)
            {
                _hp = 0;
                _isDeath = true;

                _agent.enabled = false;
                foreach (Transform child in GetComponentInChildren<Transform>())
                {
                    child.parent = null;
                    Destroy(child.gameObject, _deathTime);
                    if (!child.gameObject.GetComponent<Rigidbody>())
                        child.gameObject.AddComponent<Rigidbody>();

                    //EnableRigidBody();
                    Destroy(InstanceObject, _deathTime);
                }
            }
        }

        private void Start()
        {
            //GameObject[] tempWayPoints = GameObject.FindGameObjectsWithTag("WayPoints"); // find position for moving
            //foreach (var tempWayPoint in tempWayPoints)
            //    _wayPoints.Add(tempWayPoint.transform.position);
        }

        private void Update()
        {
            if (_isDeath || !Target) return;
            _distance = Vector3.Distance(Position, Target.position);

            if (_distance < _activeDistance)
            {
                if (Vector3.Angle(GetTransform.forward, Target.position - Position) <= _activeAngle)
                    if (!CheckBlocked())
                        _isTarget = true;
            }

            if (_wayPoints.Count >= 2 && !_isTarget)
            {
                _agent.stoppingDistance = 0;
                _agent.SetDestination(_wayPoints[_wayCount]);
                if (!_agent.hasPath)
                {
                    _curTimeout += Time.deltaTime;

                    if (_curTimeout > _timeWait)
                    {
                        _curTimeout = 0;
                        GenerationWayPoints();
                    }
                }
            }
            else if (_wayPoints.Count == 0 || _isTarget)
            {
                _agent.stoppingDistance = _stoppingDistance;
                _agent.SetDestination(Target.position);
            }
        }

        private bool CheckBlocked()
        {
            RaycastHit hit;
            Debug.DrawLine(Position, Target.position, Color.red);
            if (Physics.Linecast(Position, Target.position, out hit))
                if (hit.transform == Target)
                    return false;

            return true;
        }

        private void GenerationWayPoints()
        {
            if (_wayCount < _wayPoints.Count - 1)
                _wayCount++;
            else _wayCount = 0;
        }
    }
}

