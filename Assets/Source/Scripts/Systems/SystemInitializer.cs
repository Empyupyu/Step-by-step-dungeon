using UnityEngine;

public class SystemInitializer : MonoBehaviour
{
    [SerializeField] private ConfigData config;

    private GameData gameData = new GameData();
    private PlayerData playerData = new PlayerData();
    private GameSystem[] systems;

    private const string SavePlayerData = "SavePlayerData";

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        systems = GetComponentsInChildren<GameSystem>();

        LoadPlayerData();

        for (int i = 0; i < systems.Length; i++)
        {
            //Quick solution but better to use Zenject

            systems[i].InitializeData(config, gameData, playerData);
            systems[i].OnAwake();
        }
    }

    private void LoadPlayerData()
    {
        var loadedPlayerData = SaveUtill.GetSave<PlayerData>(SavePlayerData);
        playerData = loadedPlayerData == null ? playerData : loadedPlayerData;
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

    private void OnDestroy()
    {
        SaveUtill.Save(SavePlayerData, playerData);
    }
}
