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
    
    public Action OnTraverseTimelineDone;
    public void TraverseTimelineDone()
    {
        OnTraverseTimelineDone?.Invoke();
    }

    public Action<string> OnTimelineChanged;
    public void TimelineChanged(string id)
    {
        OnTimelineChanged?.Invoke(id);
    }

    public Action OnTimelineDoneChanging;
    public void TimelineDoneChanging()
    {
        OnTimelineDoneChanging?.Invoke();
    }

    public Action<string> OnActivateButtonTimeline;
    public void ActivateButtonTimeline(string id)
    {
        OnActivateButtonTimeline?.Invoke(id);
    }

    public Action<string> OnEnterZone;
    public void EnterZone(string zoneName)
    {
        OnEnterZone?.Invoke(zoneName);
    }
    
    public Action<string> OnExitZone;
    public void ExitZone(string zoneName)
    {
        OnExitZone?.Invoke(zoneName);
    }
}