using Godot;
using System;
using Godot.Collections;
using Range = System.Range;

public partial class Actor : CharacterBody2D
{
	[Export] private Array<ActorAction> _possibleActions = new Array<ActorAction>();
	
	private Timeline _currentTimeline;
	private ActorAction _currentAction;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameEvents.Instance.OnTimelineLoaded += OnTimelineLoaded;
	}

	private void Start()
	{
		
	}

	public override void _ExitTree()
	{
		GameEvents.Instance.OnTimelineLoaded -= OnTimelineLoaded;
	}

	private void OnTimelineLoaded()
	{
		GetActorDataFromTimeline();
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	public void RegisterAction(ActorAction action)
	{
		_possibleActions.Add(action);
	}

	private void GetActorDataFromTimeline()
	{
		_currentTimeline = TimelineHandler.Instance.GetPlayerCurrentTimeline();
		_currentAction = _currentTimeline.GetActorAction(this);

		if (_currentAction == null)
		{
			ChooseActionForCurrentTimeline();
		}
		
		InitiateAction();
	}

	private void ChooseActionForCurrentTimeline()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();
		_currentAction = _possibleActions[rng.RandiRange(0, _possibleActions.Count - 1)];
		_currentTimeline.SetActorAction(this, _currentAction);
	}

	private void InitiateAction()
	{
		_currentAction.InitiateAction();
	}
}
