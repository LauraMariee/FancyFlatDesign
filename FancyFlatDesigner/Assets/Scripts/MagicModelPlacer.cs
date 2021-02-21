using UnityEngine;

public class MagicModelPlacer : MonoBehaviour
{
    private PositionHelper _positionHelper;
    
    private PlaceableModel _baseModel;

    private PlaceableModel _currentModel;
    
    private readonly float rotationDeg = -30.0f;


    private void Start()
    {
        _positionHelper = FindObjectOfType<PositionHelper>();
        
        _baseModel = FindObjectOfType<PlaceableModel>();
    }
    
    private void Update()
    {
        if (_currentModel is null)
        {
            if (Input.GetKeyDown("b"))
            {
                _currentModel = Instantiate(_baseModel,
                    Vector3.zero, Quaternion.Euler(rotationDeg, 0.0f, 0.0f)
                );
                _currentModel.StartMoving();
            }
        }
        else
        {
            _currentModel.transform.position = _positionHelper.SnapPosition;
            if (Input.GetMouseButtonDown(0) && _currentModel.IsOnValidPosition)
            {
                _currentModel.EndMoving();
                _currentModel = null;
            }
        }
    }
}
