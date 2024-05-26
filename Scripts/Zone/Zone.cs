using Godot;
using System;

public partial class Zone : Node2D
{
	[Export] private string _zoneName = "Zone";
	[Export] private Area2D _doorEnter;
	[Export] private Area2D _doorExit;

	[Export] private Control _enterPrompt;
	[Export] private Control _exitPrompt;

	[Export] private Node2D _interior;
	[Export] private Node2D _enterPoint;

	[Export] private Node2D _exterior;
	
	private Timeline _currentTimeline;
	private ZoneState _zoneState;

	private PlayerControl _playerControl;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameEvents.Instance.OnEnterZone += OnEnterZone;
		GameEvents.Instance.OnExitZone += OnExitZone;
	}

	public override void _ExitTree()
	{
		GameEvents.Instance.OnEnterZone -= OnEnterZone;	
		GameEvents.Instance.OnExitZone -= OnExitZone;
	}
	
	private void OnEnterZone(string zoneName)
	{
		_doorEnter.Visible = false;
		_doorEnter.ProcessMode = ProcessModeEnum.Disabled;
	}
	
	private void OnExitZone(string zoneName)
	{
		_doorEnter.Visible = true;
		_doorEnter.ProcessMode = ProcessModeEnum.Inherit;
	}

	private void BodyEnteredDoorEnter(Node2D body)
	{
		if (body is CharacterBody2D player)
		{
			_playerControl = player.GetNode<PlayerControl>("PlayerControl");
			_playerControl.ToggleInteractable(true, EnterZone);
			_enterPrompt.Visible = true;
		}
	}
	
	private void BodyExitedDoorEnter(Node2D body)
	{
		if (body is CharacterBody2D player)
		{
			_playerControl = player.GetNode<PlayerControl>("PlayerControl");
			_playerControl.ToggleInteractable(false, null);
			_enterPrompt.Visible = false;
		}
	}
	
	private void BodyEnteredDoorExit(Node2D body)
	{
		if (body is CharacterBody2D player)
		{
			_playerControl = player.GetNode<PlayerControl>("PlayerControl");
			_playerControl.ToggleInteractable(true, ExitZone);
			_exitPrompt.Visible = true;
		}
	}
	
	private void BodyExitedDoorExit(Node2D body)
	{
		if (body is CharacterBody2D player)
		{
			_playerControl = player.GetNode<PlayerControl>("PlayerControl");
			_playerControl.ToggleInteractable(false, null);
			_exitPrompt.Visible = false;
		}
	}

	public void EnterZone()
	{
		GD.Print($"ENTERING ZONE {_zoneName} enter point {_enterPoint.GlobalPosition}");
		_interior.Visible = true;
		_interior.ProcessMode = ProcessModeEnum.Inherit;
		_exterior.Visible = false;
		_exterior.ProcessMode = ProcessModeEnum.Disabled;

		(_playerControl.GetParent() as Node2D).GlobalPosition = _enterPoint.GlobalPosition;
		GameEvents.Instance.EnterZone(_zoneName);
	}

	public void ExitZone()
	{
		GD.Print($"EXITING ZONE {_zoneName}");
		_interior.Visible = false;
		_interior.ProcessMode = ProcessModeEnum.Disabled;
		_exterior.Visible = true;
		_exterior.ProcessMode = ProcessModeEnum.Inherit;

		(_playerControl.GetParent() as Node2D).GlobalPosition = _doorEnter.GlobalPosition;
		GameEvents.Instance.ExitZone(_zoneName);
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
