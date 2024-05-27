using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 CameraOffset;
    
    private Transform _targetLook;

    private Transform _startBorderZ, _endBorderZ, _downBorderY;
    private Vector3 basePos;

    private float _lerpSpeedTransform;
    void Start()
    {
        _lerpSpeedTransform = 5f;
        basePos = transform.position; 
    }

    public void InitCamera(Transform targetLook, Transform startBorderZ, Transform endBorderZ, Transform downBorderY)
    {
        _targetLook = targetLook;
        _startBorderZ = startBorderZ;
        _endBorderZ = endBorderZ;
        _downBorderY = downBorderY;
    }

    void Update()
    {
        if (_targetLook == null) return;
        
        Vector3 calcLook = Vector3.Lerp(transform.position, basePos + CameraOffset + _targetLook.position, _lerpSpeedTransform * Time.deltaTime);
        calcLook.z = Mathf.Clamp(calcLook.z, _startBorderZ.position.z, _endBorderZ.position.z);
        calcLook.y = Mathf.Clamp(calcLook.y, _downBorderY.position.y, Mathf.Infinity);
        transform.position = calcLook;

    }
}
