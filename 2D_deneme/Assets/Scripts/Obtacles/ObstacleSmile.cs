using UnityEngine;

public class ObstacleSmile : MonoBehaviour
{
    private bool IsRotated;
    private Quaternion _origRotation;
    private Obstacle _parentObstacle;

    private void Awake()
    {
        _origRotation = transform.localRotation;
        _parentObstacle = GetComponentInParent<Obstacle>();
    }

    private void Update()
    {
        if (_parentObstacle.IsGreen)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, _origRotation.eulerAngles.z + 180);
        }
        else
        {
            transform.localRotation = _origRotation;
        }
    }
}