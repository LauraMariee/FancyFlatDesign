using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionHelper : MonoBehaviour
{
    private Camera _flatCamera;
    private List<Vector3> _availablePositions;

    public Vector3 SnapPosition { get; private set; }
    
    private void Start()
    {
        _flatCamera = GameObject.Find("Flat Camera Helper").GetComponent<Camera>();

        _availablePositions = transform.GetComponentsInChildren<Component>()
            .ToList()
            .ConvertAll(child => child.transform.position);
    }
    
    private void Update()
    {
        var mouseDisplayPos = Input.mousePosition;
        var modifiedDisplayMousePos = new Vector3(mouseDisplayPos.x, mouseDisplayPos.y + 100.0f, 0.0f);
        var displayRay = _flatCamera.ScreenPointToRay(modifiedDisplayMousePos);

        if (Physics.Raycast(displayRay, out var hit))
        {
            var mousePos = hit.point;

            float shortestDistance = int.MaxValue;
            var closestPosition = _availablePositions[0];
            
            foreach (var position in _availablePositions)
            {
                var distance = Mathf.Abs((position - mousePos).magnitude);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestPosition = position;
                }
            }

            SnapPosition = closestPosition;
        }
    }
}
