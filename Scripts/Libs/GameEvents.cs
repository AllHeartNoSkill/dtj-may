using System;
using Godot;

public partial class GameEvents : Node
{
    public static GameEvents Instance;

    public override void _EnterTree()
    {
        Instance = this;
    }

    public Action OnTimelineLoaded;
    public void TimelineLoaded()
    {
        OnTimelineLoaded?.Invoke();
    }
}