using Godot;
using System;

public partial class Zone : Node2D
{
	[Export] private string _zoneName = "Zone";
	[Export] private Area2D _doorEnter;
	[Export] private Area2D _doorExit;

	[Export] private Control _enterPrompt;
	[Export] private Control _exitPrompt;
	
	private Timeline _currentTimeline;
	private ZoneState _zoneState;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _ExitTree()
	{
		
	}

	private void BodyEnteredDoorEnter(Node2D body)
	{
		if (body is CharacterBody2D player)
		{
			PlayerControl playerControl = player.GetNode<PlayerControl>("PlayerControl");
			playerControl.ToggleInteractable(true, EnterZone);
			_enterPrompt.Visible = true;
		}
	}
	
	private void BodyExitedDoorEnter(Node2D body)
	{
		if (body is CharacterBody2D player)
		{
			PlayerControl playerControl = player.GetNode<PlayerControl>("PlayerControl");
			playerControl.ToggleInteractable(false, null);
			_enterPrompt.Visible = false;
		}
	}

	public void EnterZone()
	{
		GD.Print($"ENTERING ZONE {_zoneName}");
	}

	public void ExitZone()
	{
		
	}

	private void OnTimelineChanged(string id)
	{
		_currentTimeline = TimelineHandler.Instance.GetPlayerCurrentTimeline();
		ScenarioData scenarioData = _currentTimeline.ScenarioData;
		
		switch (_zoneName)
		{
			case "Bakery":
				_zoneState = scenarioData.BakeryScenario;
				break;
			case "Restaurant":
				_zoneState = scenarioData.RestaurantScenario;
				break;
			case "WatchStore":
				_zoneState = scenarioData.WatchStoreScenario;
				break;
			case "MusicStore":
				_zoneState = scenarioData.MusicStoreScenario;
				break;
		}

		if (_zoneState == ZoneState.Open)
		{
			_doorEnter.ProcessMode = ProcessModeEnum.Inherit;
		}
		else
		{
			_doorEnter.ProcessMode = ProcessModeEnum.Disabled;
		}
	}
}
