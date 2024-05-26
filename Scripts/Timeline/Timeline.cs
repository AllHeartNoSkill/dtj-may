using Godot;
using System;
using System.Collections.Generic;

public partial class Timeline : Node3D
{
	private ScenarioData _scenarioData;

	private Dictionary<Actor, ActorAction> _allActorActions = new Dictionary<Actor, ActorAction>();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetScenarioData(ScenarioData scenarioData)
	{
		_scenarioData = scenarioData;
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

