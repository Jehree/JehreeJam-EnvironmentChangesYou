using Godot;
using System;
using System.Runtime.CompilerServices;


public partial class JumpCommand : CharacterCommand
{
	[Export]
	public AudioStreamPlayer2D jumpSoundPlayer;
	[Export]
	public float jumpVelocity = 400f;
	[Export]
	public int maxJumps = 1;
	private int allowedJumps = 0;
		
	public override void Execute(CharacterActor actor, Object data = null)
	{
		if (actor.IsOnFloor())
		{
			allowedJumps = maxJumps;
		}
		else if (allowedJumps > 1)
		{
			allowedJumps -= 1;
		}
		else 
		{
			return;
		}
		
		Vector2 velocity = actor.Velocity;
		velocity.Y = -jumpVelocity;
		actor.Velocity = velocity;
		jumpSoundPlayer.Play();
	}

	public override void Reset()
	{
		allowedJumps = maxJumps;
	}
}