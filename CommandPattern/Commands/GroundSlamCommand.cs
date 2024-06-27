using Godot;


public partial class GroundSlamCommand : CharacterCommand
{
	[Export]
	public AudioStreamPlayer2D slamSoundPlayer;
	[Export]
	public float maxSlamSpeed = 300f;
	[Export]
	public float acceleration = 20f;

	private CharacterActor _actor;
	private bool _slamActive = false;

	public override void _PhysicsProcess(double delta)
	{
		if (_actor == null) return;
		if (_actor.IsOnFloor())
		{
			_slamActive = false;
			return;
		}
		if (!_slamActive) return;
		
		Godot.Vector2 velocity = _actor.Velocity;
		velocity.Y = Mathf.MoveToward(velocity.Y, maxSlamSpeed, acceleration);
		_actor.Velocity = velocity;
	}

	public override void Execute(CharacterActor actor, object data = null)
	{
		if (_actor == null) 
		{
			_actor = actor;
		}
		if (_slamActive) return;
		if (_actor.IsOnFloor()) return;
		
		_slamActive = true;
		
		slamSoundPlayer.Play();
	}

	public override void Reset()
	{
		_slamActive = false;
	}
}