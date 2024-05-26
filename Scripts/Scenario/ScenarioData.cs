using Godot;

public class ScenarioData
{
    [Export] public ZoneState BakeryScenario;
    [Export] public ZoneState RestaurantScenario;
    [Export] public ZoneState WatchStoreScenario;
    [Export] public ZoneState MusicStoreScenario;

    public ScenarioData(ZoneState bakery, ZoneState restaurant, ZoneState watchStore, ZoneState musicStore)
    {
        BakeryScenario = bakery;
        RestaurantScenario = restaurant;
        WatchStoreScenario = watchStore;
        MusicStoreScenario = musicStore;
    }
}

public enum ZoneState
{
    Open,
    Closed,
    OnBreak
}