using Godot;
using Godot.Collections;
using System;
using System.Data;

public partial class PlayableCharacter : CharacterActor
{
	[Export]
	public Area2D zoneDetectionArea;
	
	private CharacterCommand _jumpCommand;
	private CharacterCommand _moveCommand;
	private Vector2 _lastMoveDirection;
	
	[Export]
	public CharacterTemplate[] characterTemplates;
	private int _activeTemplateIndex; 


	[Export]
	public Marker2D respawnPoint;
	[Export]
	public Area2D outOfBounds;
	[Export]
	public Area2D youWon;
	[Export]
	public Control[] youWonUI;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		zoneDetectionArea.BodyShapeEntered += OnBodyShapeEntered;
		outOfBounds.BodyEntered += OnOutOfBoundsAreaEntered;
		youWon.BodyEntered += YouWonAreaEntered;
		SwapCharacter(0);
		_lastMoveDirection = Vector2.Right;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 inputVector = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		if (Input.IsActionJustPressed("jump"))
		{
			_jumpCommand.Execute(this, new CharacterCommand.DataAtExecute(inputVector, delta, _lastMoveDirection));
		}

		_moveCommand.Execute(this, new CharacterCommand.DataAtExecute(inputVector, delta, _lastMoveDirection));

		MoveAndSlide();
		
		if (!(inputVector == Vector2.Zero)) 
		{
			_lastMoveDirection = inputVector;
		}
		
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			GlobalPosition = respawnPoint.GlobalPosition;
			Velocity = Vector2.Zero;
			
			foreach (Control node in youWonUI)
			{
				node.Hide();
			}
		}
	}
	
	private void OnOutOfBoundsAreaEntered(Node2D body)
	{
		GlobalPosition = respawnPoint.GlobalPosition;
		Velocity = Vector2.Zero;
	}
	
	private void YouWonAreaEntered(Node2D body)
	{
		if (!(body is PlayableCharacter)) return;
		
		foreach (Control node in youWonUI)
		{
			node.Show();
		}
	}
	
	private void OnBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
	{
		if (!(body is ZoneTileMap)) return;
		var tileMap = body as ZoneTileMap;
		var zone = tileMap.zone;
		
		if (zone == _activeTemplateIndex) return;
		
		SwapCharacter(zone);
	}
	
	private void SwapCharacter(int templateIndex)
	{
		var oldIndex = _activeTemplateIndex;
		var oldTemplate = characterTemplates[oldIndex];
		var newTemplate = characterTemplates[templateIndex];
		_activeTemplateIndex = templateIndex;

		GetNode<CharacterCommand>(oldTemplate.JumpCommandPath).Reset();
		GetNode<CharacterCommand>(oldTemplate.MoveCommandPath).Reset();

		GetNode<Sprite2D>(characterTemplates[oldIndex].SpritePath).Visible = false;
		GetNode<Sprite2D>(newTemplate.SpritePath).Visible = true;
		
		_jumpCommand = GetNode<CharacterCommand>(newTemplate.JumpCommandPath);
		_moveCommand = GetNode<CharacterCommand>(newTemplate.MoveCommandPath);
	}
}
