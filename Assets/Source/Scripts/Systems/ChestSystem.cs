using Supyrb;
using Utills;

public class ChestSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<OpenChestSignal>().AddListener(OpenChest);

        InitializeChests();
    }

    private void InitializeChests()
    {
        for (int i = 0; i < game.Level.Chests.Count; i++)
        {
            game.Level.Chests[i].SetData(congfig.ChestDatas.GetRandom());
        }
    }

    private void OpenChest(Chest chest)
    {
        Signals.Get<InfoSignal>().Dispatch(nameof(chest) + " " + chest.ChestData.InfoTextAfterInteract + " " + chest.ChestData.Reward!);

        Destroy(chest.gameObject, 2.5f);
    }
}