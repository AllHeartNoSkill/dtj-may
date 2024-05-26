using Godot;

public class ScenarioData
{
    [Export] public ZoneState BakeryScenario;
    [Export] public ZoneState RestaurantScenario;
    [Export] public ZoneState WatchStoreScenario;
    [Export] public ZoneState BarScenario;

    public ScenarioData(ZoneState bakery, ZoneState restaurant, ZoneState watchStore, ZoneState bar)
    {
        BakeryScenario = bakery;
        RestaurantScenario = restaurant;
        WatchStoreScenario = watchStore;
        BarScenario = bar;
    }
}

public enum ZoneState
{
    Open,
    Closed,
    OnBreak
}