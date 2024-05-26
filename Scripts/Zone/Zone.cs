using Godot;
using System;

public partial class Zone : Node2D
{
	[Export] private string _zoneName = "Zone";
	[Export] private bool _isZoneOpen = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
