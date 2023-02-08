using Cinemachine;

public class CameraSystem : GameSystem
{
    public override void OnAwake()
    {
        game.Camera = FindObjectOfType<CinemachineVirtualCamera>();

        SetFollowTarget();
    }

    private void SetFollowTarget()
    {
        game.Camera.Follow = game.Player.transform;
    }
}