using System;

public class EconomyManager : Singleton<EconomyManager>
{
    public Action<int> OnGoldUpdated;

    private int currentGold = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    public void UpdateCurrentGold()
    {
        currentGold += 1;

        OnGoldUpdated?.Invoke(currentGold);
    }
}