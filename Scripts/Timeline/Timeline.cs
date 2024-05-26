using Godot;
using System;
using System.Collections.Generic;

public class Timeline
{
	public string Id;
	public int Time;
	public ScenarioData ScenarioData;

	public bool Traversed = false;

	public List<Timeline> Child = new List<Timeline>();
	public List<Timeline> Parent = new List<Timeline>();
	
	private Dictionary<Actor, ActorAction> _allActorActions = new Dictionary<Actor, ActorAction>();
	
	public Timeline(string id, int time, ScenarioData scenarioData)
	{
		Id = id;
		Time = time;
		ScenarioData = scenarioData;

		GameEvents.Instance.OnTimelineChanged += OnTimelineChanged;
		GameEvents.Instance.OnTraverseTimelineDone += OnTraverseTimelineDone;
	}

	public void DeleteTimeline()
	{
		GameEvents.Instance.OnTimelineChanged -= OnTimelineChanged;
		GameEvents.Instance.OnTraverseTimelineDone -= OnTraverseTimelineDone;
	}

	public void OnTimelineChanged(string id)
	{
		if (Id != id)
		{
			return;
		}
		
		TimelineHandler.Instance.ChangePlayerTimeline(this);
	}

	public void OnTraverseTimelineDone()
	{
		Traversed = false;
	}

	public void RegisterChild(Timeline child)
	{
		Child.Add(child);
		child.Parent.Add(this);
	}

	public void SetScenarioData(ScenarioData scenarioData)
	{
		ScenarioData = scenarioData;
	}

	public ActorAction GetActorAction(Actor actor)
	{
		if (_allActorActions.TryGetValue(actor, out var action))
		{
			return action;
		}

		return null;
	}

	public void SetActorAction(Actor actor, ActorAction action)
	{
		_allActorActions.Add(actor, action);
	}
}

