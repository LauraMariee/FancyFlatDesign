using System;
using UnityEngine;

public class PlaceableModel : MonoBehaviour
{
    public Boolean IsOnValidPosition { private set; get; }

    private Rigidbody _rigidBody;
    private Boolean _isMoving;

    void Start()
    {
        IsOnValidPosition = true;
        DeeplyInitColor(transform);
    }

    private void Update()
    {
        
    }
    
    // Public State Setters

    public void StartMoving()
    {
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        _rigidBody = gameObject.AddComponent<Rigidbody>();
        _rigidBody.isKinematic = true;

        _isMoving = true;
    }

    public void EndMoving() {
        Destroy(_rigidBody);

        _isMoving = false;
    }
    
    // Collision

    private void OnTriggerEnter(Collider other)
    {
        if (!_isMoving)
        {
            return;
        }
        
        IsOnValidPosition = false;
        DeeplySetColorToRed(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isMoving)
        {
            return;
        }
        
        IsOnValidPosition = true;
        DeeplyResetColor(transform);
    }

    // Color Manipulation
    
    private void DeeplyInitColor(Transform component)
    {
        foreach (Transform child in component.transform)
        {
            foreach (var childRenderer in child.GetComponents<Renderer>())
            {
                var color = childRenderer.material.color;
                childRenderer.material.SetColor("__originalColor", color);
            }
            DeeplyInitColor(child);
        }
    }

    private void DeeplySetColorToRed(Transform component)
    {
        foreach (Transform child in component.transform)
        {
            foreach (var childRenderer in child.GetComponents<Renderer>())
            {
                childRenderer.material.SetColor("_Color", new Color(1.0f, 0.1f, 0.1f, 0.3f));
            }
            DeeplySetColorToRed(child);
        }
    }
    
    private void DeeplyResetColor(Transform component)
    {
        foreach (Transform child in component.transform)
        {
            foreach (var childRenderer in child.GetComponents<Renderer>())
            {
                if (childRenderer.material.HasProperty("__originalColor"))
                {
                    var color = childRenderer.material.GetColor("__originalColor");
                    childRenderer.material.SetColor("_Color", color);
                }
            }
            DeeplyResetColor(child);
        }
    }
}
