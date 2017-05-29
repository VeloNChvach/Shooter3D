using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathTest : MonoBehaviour
{
    [SerializeField] private GameObject _point;
    [SerializeField] private GameObject _agent;
    private Queue<Transform> _points = new Queue<Transform>();

    private Camera _mainCamera;
    private Transform _rootPoint;

    private Color c1 = Color.red;
    private Color c2 = Color.red;
    private int lengthOfLineRenderer = 2;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        GameObject tempLineRenderer = new GameObject("LineRenderer");
        _lineRenderer = tempLineRenderer.AddComponent<LineRenderer>();
        _lineRenderer.SetWidth(0.5f, 0.5f);
        _lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        _lineRenderer.SetColors(c1, c2);
        _lineRenderer.SetVertexCount(lengthOfLineRenderer);

        _mainCamera = Camera.main;
        _rootPoint = new GameObject("rootPoint").transform;
        _lineRenderer.SetPosition(0, _agent.transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
                CreatePoint(hit.point);
        }

        RaycastHit raycastHit;
        if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out raycastHit))
            _lineRenderer.SetPosition(1, raycastHit.point);
    }

    private void CreatePoint(Vector3 point)
    {
        GameObject tempPoint = Instantiate(_point, point, Quaternion.identity, _rootPoint);
        _points.Enqueue(tempPoint.transform);
    }
}
