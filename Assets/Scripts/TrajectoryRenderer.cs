using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int trajectoryPointsCount = 30;
    public float timeStep = 0.1f;
    public BallController ballController;

    void Start()
    {
        lineRenderer.positionCount = trajectoryPointsCount;
        lineRenderer.enabled = false;
    }

    public void ShowTrajectory(bool show)
    {
        lineRenderer.enabled = show;
    }

    public void DrawTrajectory(Vector2 start, Vector2 end)
    {
        Vector2 direction = (end - start).normalized;
        float swipeForce = Mathf.Clamp((end - start).magnitude * ballController.SensitivityMultiplier, 0, ballController.MaxSwipeForce);

        Vector2 velocity = -direction * swipeForce;

        for (int i = 0; i < trajectoryPointsCount; i++)
        {
            float time = i * timeStep;
            Vector2 point = CalculateTrajectoryPoint(ballController.transform.position, velocity, time);
            lineRenderer.SetPosition(i, point);
        }
    }

    Vector2 CalculateTrajectoryPoint(Vector2 startPosition, Vector2 initialVelocity, float time)
    {
        // s = ut + 1/2 * a * t^2, where a is gravity in the 2D physics world.
        Vector2 gravity = Physics2D.gravity;
        Vector2 displacement = initialVelocity * time + 0.5f * gravity * time * time;
        return startPosition + displacement;
    }
}
