using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    public TrajectoryRenderer trajectoryRenderer;
    public BallController ballController;

    private Camera _mainCamera;
    private Vector2 _startMousePosition, _endMousePosition;

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (ballController.IsMoving()) return;

        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            trajectoryRenderer.ShowTrajectory(true);
        }

        if (Input.GetMouseButton(0))
        {
            _endMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            trajectoryRenderer.DrawTrajectory(_startMousePosition, _endMousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _endMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ballController.Swipe(_startMousePosition, _endMousePosition);
            trajectoryRenderer.ShowTrajectory(false);
        }
    }
}
