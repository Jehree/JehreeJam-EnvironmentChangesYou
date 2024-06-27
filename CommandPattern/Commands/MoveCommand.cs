using System;
using System.Diagnostics.Contracts;
using System.Numerics;
using Godot;

public partial class MoveCommand : CharacterCommand
{
	[Export]
	public float moveSpeed = 300f;
	[Export]
	public float acceleration = 20f;
	[Export]
	public float decceleration = 50f;
	[Export]
	float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void Execute(CharacterActor actor, object data = null)
	{
		if (!(data is DataAtExecute)) return;
		DataAtExecute paramaters = data as DataAtExecute;
		
		Godot.Vector2 velocity = actor.Velocity;
		Godot.Vector2 targetVelocity = new Godot.Vector2(paramaters.inputVector.X * moveSpeed, velocity.Y);
		
		if (ShouldDeccelerate(velocity, targetVelocity, paramaters.inputVector, actor))
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, decceleration);
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, targetVelocity.X, acceleration);
		}

		
		if (!actor.IsOnFloor())
		{
			velocity.Y += gravity * (float)paramaters.delta;
		}
		
		actor.Velocity = velocity;
	}
	
	private bool ShouldDeccelerate(Godot.Vector2 currentVelocity, Godot.Vector2 targetVelocity, Godot.Vector2 inputVector, CharacterActor actor)
	{
		if (
			actor.IsOnFloor() && //don't deccel in air
			(inputVector == Godot.Vector2.Zero || //don't deccel if holding an input key
			MiscUtils.WillPassThroughZero(targetVelocity.X, currentVelocity.X)) //DO deccel if swapping directions
		) return true;
		else return false;
	}
	public override void Reset(){}
}