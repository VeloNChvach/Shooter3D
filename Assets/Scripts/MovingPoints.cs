using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Characters.ThirdPerson;

public class MovingPoints : MonoBehaviour
{
    //[SerializeField] private AICharacterControl _agent;
    [SerializeField] private GameObject _point;

    private Queue<GameObject> _points = new Queue<GameObject>();

    private Vector3 _position;
    private readonly Color _c1 = Color.red;
    private readonly Color _c = Color.red;
    private readonly int lengthOfLineRenderer = 2;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.SetWidth(0.5f, 0.5f);
        _lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        _lineRenderer.SetColors(_c, _c1);
        _lineRenderer.SetVertexCount(lengthOfLineRenderer);
        //_lineRenderer.SetPosition(0, _agent.transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                DrawPoint(hit.point);

            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
                _position = hitInfo.point;

            _lineRenderer.SetPosition(1, _position);
        }
    }

    private void DrawPoint(Vector3 position)
    {
        GameObject tempPoint = Instantiate(_point, position, Quaternion.identity) as GameObject;
        _points.Enqueue(tempPoint);
        _lineRenderer.SetPosition(0, tempPoint.transform.position);
    }

    private void GotoNextPoint()
    {
        //_agent.SetTarget(_points.Dequeue().transform);
    }
}
