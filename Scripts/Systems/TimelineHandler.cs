using Godot;
using System;
using System.Collections.Generic;

public partial class TimelineHandler : Node3D
{
	public static TimelineHandler Instance;
	
	[Export] private PackedScene _timelinePrefab;

	[Export] private int _playerStartingDepth = 3;
	[Export] private int _playerMoveDepth = 2;
	
	private Timeline _playerCurrentTimeline;
	private Timeline _currentTargetTimeline;

	private bool _playerPlaced;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().Root.Ready += Start;
	}
	
	private void Start()
	{
		Instance = this;
		
		PopulateTimelineTree();
		
		GD.Print($"Player Timeline = {_playerCurrentTimeline.Time}");
		
		GameEvents.Instance.TimelineLoaded();
		GameEvents.Instance.TimelineChanged(_playerCurrentTimeline.Id);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsKeyPressed(Key.O))
		{
			OpenTimelineGraph();
		}
	}

	public void ChangePlayerTimeline(Timeline timeline)
	{
		_playerCurrentTimeline = timeline;
	}

	public void OpenTimelineGraph()
	{
		TraverseTree(_playerCurrentTimeline, 0);
		GameEvents.Instance.TraverseTimelineDone();
	}

	private void TraverseTree(Timeline node, int depth)
	{
		if (depth > _playerMoveDepth)
		{
			return;
		}

		node.Traversed = true;
		if (node != _playerCurrentTimeline)
		{
			GameEvents.Instance.ActivateButtonTimeline(node.Id);
		}
		
		foreach (var child in node.Child)
		{
			TraverseTree(child, depth + 1);
		}

		foreach (var parent in node.Parent)
		{
			TraverseTree(parent, depth + 1);
		}
	}

	public void PopulateTimelineTree()
	{
		Timeline rootTimeline = new Timeline("1a", 7, new ScenarioData(ZoneState.Open, ZoneState.Closed, ZoneState.Closed, ZoneState.Closed));

		Timeline timeline2a = new Timeline("2a", 8, new ScenarioData(ZoneState.Open, ZoneState.Open, ZoneState.Closed, ZoneState.Closed));
		Timeline timeline2b = new Timeline("2b", 8, new ScenarioData(ZoneState.Open, ZoneState.Closed, ZoneState.Closed, ZoneState.Closed));
		
		Timeline timeline3a = new Timeline("3a", 9, new ScenarioData(ZoneState.Open, ZoneState.Open, ZoneState.Closed, ZoneState.Closed));
		Timeline timeline3b = new Timeline("3b", 9, new ScenarioData(ZoneState.Open, ZoneState.Open, ZoneState.Open, ZoneState.Closed));
		Timeline timeline3c = new Timeline("3c", 9, new ScenarioData(ZoneState.Open, ZoneState.Closed, ZoneState.Closed, ZoneState.Closed));

		Timeline timeline4a = new Timeline("4a", 10, new ScenarioData(ZoneState.Open, ZoneState.Open, ZoneState.Open, ZoneState.Open));
		Timeline timeline4b = new Timeline("4b", 10, new ScenarioData(ZoneState.Open, ZoneState.Open, ZoneState.Open, ZoneState.Closed));

		Timeline timeline5a = new Timeline("5a", 11, new ScenarioData(ZoneState.Closed, ZoneState.Open, ZoneState.OnBreak, ZoneState.Open));
		Timeline timeline5b = new Timeline("5b", 11, new ScenarioData(ZoneState.Closed, ZoneState.Open, ZoneState.Open, ZoneState.Open));
		Timeline timeline5c = new Timeline("5c", 11, new ScenarioData(ZoneState.Open, ZoneState.Open, ZoneState.OnBreak, ZoneState.Open));
		Timeline timeline5d = new Timeline("5d", 11, new ScenarioData(ZoneState.Open, ZoneState.Open, ZoneState.Open, ZoneState.Open));

		Timeline timeline6a = new Timeline("6a", 12, new ScenarioData(ZoneState.Closed, ZoneState.Open, ZoneState.OnBreak, ZoneState.Open));
		Timeline timeline6b = new Timeline("6b", 12, new ScenarioData(ZoneState.Closed, ZoneState.Open, ZoneState.Open, ZoneState.Open));

		Timeline timeline7a = new Timeline("7a", 13, new ScenarioData(ZoneState.Closed, ZoneState.Open, ZoneState.Open, ZoneState.Closed));
		Timeline timeline7b = new Timeline("7b", 13, new ScenarioData(ZoneState.Closed, ZoneState.Open, ZoneState.Open, ZoneState.Open));
		Timeline timeline7c = new Timeline("7c", 13, new ScenarioData(ZoneState.Closed, ZoneState.Closed, ZoneState.Open, ZoneState.Open));
		
		rootTimeline.RegisterChild(timeline2a);
		rootTimeline.RegisterChild(timeline2b);
		
		timeline2a.RegisterChild(timeline3a);
		timeline2a.RegisterChild(timeline3b);
		
		timeline2b.RegisterChild(timeline3b);
		timeline2b.RegisterChild(timeline3c);
		
		timeline3a.RegisterChild(timeline4a);
		timeline3a.RegisterChild(timeline4b);
		
		timeline3b.RegisterChild(timeline4a);
		timeline3b.RegisterChild(timeline4b);
		
		timeline3c.RegisterChild(timeline4a);
		timeline3c.RegisterChild(timeline4b);

		timeline4a.RegisterChild(timeline5a);
		timeline4a.RegisterChild(timeline5b);
		timeline4a.RegisterChild(timeline5c);
		
		timeline4b.RegisterChild(timeline5d);

		timeline5a.RegisterChild(timeline6a);
		
		timeline5b.RegisterChild(timeline6a);
		
		timeline5c.RegisterChild(timeline6b);
		
		timeline5d.RegisterChild(timeline6a);
		
		timeline6a.RegisterChild(timeline7a);
		timeline6a.RegisterChild(timeline7b);
		timeline6a.RegisterChild(timeline7c);
		
		timeline6b.RegisterChild(timeline7a);
		timeline6b.RegisterChild(timeline7b);
		timeline6b.RegisterChild(timeline7c);
		
		PlacePlayerAndTargetInTree(rootTimeline, 0);
	}

	private void PlacePlayerAndTargetInTree(Timeline currentTimeline, int depth)
	{
		for (int i = 0; i < currentTimeline.Child.Count; i++)
		{
			var child = currentTimeline.Child[i];
			if (!_playerPlaced && i >= Mathf.FloorToInt(currentTimeline.Child.Count / 2f) - 1 && depth >= _playerStartingDepth)
			{
				_playerPlaced = true;
				_playerCurrentTimeline = currentTimeline;
			}
			PlacePlayerAndTargetInTree(child, depth + 1);
		}
	}

	public Timeline GetPlayerCurrentTimeline()
	{
		return _playerCurrentTimeline;
	}
}