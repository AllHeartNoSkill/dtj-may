using Godot;
using System;

public partial class TimelineGraphButton : Button
{
	[Export] private string _timelineId;

	public override void _Ready()
	{
		GameEvents.Instance.OnTimelineChanged += OnTimelineChanged;
		GameEvents.Instance.OnActivateButtonTimeline += OnActivateButtonTimeline;
	}

	public override void _ExitTree()
	{
		GameEvents.Instance.OnTimelineChanged -= OnTimelineChanged;
		GameEvents.Instance.OnActivateButtonTimeline -= OnActivateButtonTimeline;
	}

	public override void _Pressed()
	{
		GameEvents.Instance.TimelineChanged(_timelineId);
	}

	public void OnTimelineChanged(string id)
	{
		Disabled = true;
		if (_timelineId != id)
		{
			ReleaseFocus();
			FocusMode = FocusModeEnum.None;
			return;
		}

		Disabled = true;
		FocusMode = FocusModeEnum.Click;
		GrabFocus();
	}
	
	private void OnActivateButtonTimeline(string id)
	{
		if (_timelineId == id)
		{
			Disabled = false;
		}
	}
}
