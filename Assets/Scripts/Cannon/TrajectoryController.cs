using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    [SerializeField] private int pointsCont;
    [SerializeField] private float spacing;
    [SerializeField] private LayerMask mask;
    private bool _draw;
    
    private CannonController _cannonController;
    private LineRenderer _lineRenderer;
    private Vector3 _startPos, _startVel;
    private List<Vector3> _points = new List<Vector3>();
    

    private void Start()
    {
        _cannonController = GetComponent<CannonController>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(GameManager.Instance.gameOver) return;
        
        if (Input.GetButtonDown("Fire1")) _draw = true;
        if (Input.GetButtonUp("Fire1")) _draw = false;
        if (!_draw)
        {
            _lineRenderer.enabled = false;
            return;
        }

        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = pointsCont;
        _startPos = _cannonController.shootPoint.position;
        _startVel = _cannonController.shootPoint.up * _cannonController.force;

        _points.Clear();
        for (float t = 0; t < pointsCont; t += spacing)
        {
            Vector3 newPoint = _startPos + t * _startVel;
            newPoint.y = _startPos.y + _startVel.y * t + Physics.gravity.y * 0.5f * t * t;
            _points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 1, mask).Length > 0)
            {
                _lineRenderer.positionCount = _points.Count;
                break;
            }

            //pointsCont++;
        }
        
        _lineRenderer.SetPositions(_points.ToArray());
    }
}
