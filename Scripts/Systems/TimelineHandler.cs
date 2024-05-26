using Godot;
using System;
using System.Collections.Generic;

public partial class TimelineHandler : Node3D
{
	public static TimelineHandler Instance;
	
	[Export] private PackedScene _timelinePrefab;

	[Export] private int _playerStartingDepth = 2;
	
	private TimelineTree _timelineTree;
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
		
		CreateTimelineTree();
		PopulateTimelineTree();
		
		GameEvents.Instance.TimelineLoaded();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CreateTimelineTree()
	{
		_timelineTree = new TimelineTree("Timeline 1");

		var timeline4A = new TimelineTree("Timeline 4A");
		var timeline4B = new TimelineTree("Timeline 4B");
		var timeline4C = new TimelineTree("Timeline 4C");
		var timeline4D = new TimelineTree("Timeline 4D");
		var timeline4E = new TimelineTree("Timeline 4E");
		var timeline4F = new TimelineTree("Timeline 4F");
		
		var timeline3A = new TimelineTree("Timeline 3A");
		var timeline3B = new TimelineTree("Timeline 3B");
		var timeline3C = new TimelineTree("Timeline 3C");
		var timeline3D = new TimelineTree("Timeline 3D");
		
		var timeline2A = new TimelineTree("Timeline 2A");
		var timeline2B = new TimelineTree("Timeline 2B");
		
		_timelineTree.RegisterChild(timeline2A);
		_timelineTree.RegisterChild(timeline2B);
		
		timeline2A.RegisterChild(timeline3A);
		
		timeline2B.RegisterChild(timeline3B);
		timeline2B.RegisterChild(timeline3C);
		timeline2B.RegisterChild(timeline3D);
		
		timeline3A.RegisterChild(timeline4A);
		timeline3A.RegisterChild(timeline4B);
		timeline3A.RegisterChild(timeline4C);
		
		timeline3C.RegisterChild(timeline4D);
		
		timeline3D.RegisterChild(timeline4E);
		timeline3D.RegisterChild(timeline4F);
	}

	public void PopulateTimelineTree()
	{
		Timeline rootTimeline = _timelinePrefab.Instantiate() as Timeline;
		rootTimeline.Name = _timelineTree.TimelineName;
		AddChild(rootTimeline);

		TraverseTree(_timelineTree, rootTimeline, 0);
	}

	private void TraverseTree(TimelineTree node, Timeline currentTimeline, int depth)
	{
		for (int i = 0; i < node.Children.Count; i++)
		{
			var child = node.Children[i];
			
			Timeline timeline = _timelinePrefab.Instantiate() as Timeline;
			timeline.SetScenarioData(child.ScenarioData);
			timeline.Name = child.TimelineName;
			currentTimeline.AddChild(timeline);

			if (!_playerPlaced && i >= Mathf.FloorToInt(node.Children.Count / 2f) && depth >= _playerStartingDepth)
			{
				_playerPlaced = true;
				_playerCurrentTimeline = timeline;
			}
			
			TraverseTree(child, timeline, depth + 1);
		}
	}

	public Timeline GetPlayerCurrentTimeline()
	{
		return _playerCurrentTimeline;
	}
}


public class TimelineTree
{
	public string TimelineName;
	public ScenarioData ScenarioData;
	public TimelineTree Parent;
	public List<TimelineTree> Children;

	public TimelineTree()
	{
		Children = new List<TimelineTree>();
	}
	
	public TimelineTree(string name)
	{
		TimelineName = name;
		Children = new List<TimelineTree>();
	}

	public TimelineTree(ScenarioData scenarioData, List<TimelineTree> children)
	{
		ScenarioData = scenarioData;
		Children = children;
	}

	public void RegisterChild(TimelineTree child)
	{
		Children.Add(child);
	}

	public void SetChildren(List<TimelineTree> children)
	{
		Children = children;
	}
}