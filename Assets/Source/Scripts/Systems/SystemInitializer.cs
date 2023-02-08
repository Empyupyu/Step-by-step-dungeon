using UnityEngine;

public class SystemInitializer : MonoBehaviour
{
    [SerializeField] private ConfigData config;

    private GameSystem[] systems;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        systems = GetComponentsInChildren<GameSystem>();

        for (int i = 0; i < systems.Length; i++)
        {
            //Quick solution but better to use Zenject

            systems[i].InitializeData(config, new GameData());
            systems[i].OnAwake();
        }
    }

    private void Start()
    {
        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].OnStart();
        }
    }

    private void Update()
    {
        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].OnUpdate();
        }
    }
}
