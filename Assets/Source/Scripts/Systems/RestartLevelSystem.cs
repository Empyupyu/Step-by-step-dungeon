using Supyrb;
using UnityEngine.SceneManagement;

public class RestartLevelSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<RestartLevelSignal>().AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}