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

    public Action<string> OnActivateButtonTimeline;
    public void ActivateButtonTimeline(string id)
    {
        OnActivateButtonTimeline?.Invoke(id);
    }
}