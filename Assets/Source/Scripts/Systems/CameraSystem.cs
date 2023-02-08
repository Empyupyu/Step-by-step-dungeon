using Cinemachine;

public class CameraSystem : GameSystem
{
    public override void OnAwake()
    {
        game.Camera = FindObjectOfType<CinemachineVirtualCamera>();

        game.Camera.Follow = game.Player.transform;
    }
}