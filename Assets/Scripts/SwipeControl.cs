using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    public TrajectoryRenderer trajectoryRenderer;
    public BallController ballController;

    private Vector2 _startMousePosition, _endMousePosition;

    void Update()
    {
        if (ballController.IsMoving()) return;

        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            trajectoryRenderer.ShowTrajectory(true);
        }

        if (Input.GetMouseButton(0))
        {
            _endMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            trajectoryRenderer.DrawTrajectory(_startMousePosition, _endMousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _endMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ballController.Swipe(_startMousePosition, _endMousePosition);
            trajectoryRenderer.ShowTrajectory(false);
        }
    }
}
