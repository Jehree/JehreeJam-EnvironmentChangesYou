using Godot;
using System;
using System.Runtime.CompilerServices;


public partial class DashCommand : CharacterCommand
{
	private Timer _timer;
	private bool _canDash = true;
	
	[Export]
	public float cooldownTime = 2;
	[Export]
	public AudioStreamPlayer2D dashSoundPlayer;
	[Export]
	public float dashStrength = 1000;
	
	private DashDirection _dashDirection = DashDirection.RIGHT;
	
	enum DashDirection 
	{
		NONE = -1,
		RIGHT,
		LEFT,
	}

	public override void _Ready()
	{
		_timer = new Timer {WaitTime = cooldownTime};
		AddChild(_timer);
		_timer.Timeout += OnCooldownTimeout;
	}

	public override void Execute(CharacterActor actor, Object data = null)
	{
		if (!(data is DataAtExecute)) return;
		if (!_canDash) return;
		var executeData = data as DataAtExecute;
		
		Vector2 velocityIncrease = new Vector2(executeData.lastMovementDirection.X * dashStrength, 0);
		actor.Velocity += velocityIncrease;
		
		dashSoundPlayer.Play();
		StartCooldown();
	}
	
	
	private void OnCooldownTimeout()
	{
		_canDash = true;
	}
	
	private void StartCooldown()
	{
		_canDash = false;
		_timer.Start();
	}

	public override void Reset()
	{
		_timer.Stop();
		_canDash = true;
	}
}