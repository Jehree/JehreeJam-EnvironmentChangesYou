using Godot;


public partial class ZeroGravityMoveCommand : CharacterCommand
{
	[Export]
	public float moveSpeed = 300f;
	[Export]
	public float acceleration = 20f;
	[Export]
	public float decceleration = 50f;
	[Export]
	public bool lockedUpwardMovement = false;
	[Export]
	public bool lockedDownwardMovement = false;

	public override void Execute(CharacterActor actor, object data = null)
	{
		if (!(data is DataAtExecute)) return;
		DataAtExecute paramaters = data as DataAtExecute;
		if (lockedDownwardMovement && paramaters.inputVector.Y > 0) return; //no moving down
		if (lockedUpwardMovement && paramaters.inputVector.Y < 0) return; //no moving up

		Godot.Vector2 velocity = actor.Velocity;
		Godot.Vector2 targetVelocity = new Godot.Vector2(paramaters.inputVector.X * moveSpeed, paramaters.inputVector.Y * moveSpeed);

		if (ShouldDeccelerate(velocity, targetVelocity, paramaters.inputVector, actor))
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, decceleration);
			velocity.Y = Mathf.MoveToward(velocity.Y, 0, decceleration);
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, targetVelocity.X, acceleration);
			velocity.Y = Mathf.MoveToward(velocity.Y, targetVelocity.Y, acceleration);
		}
		
		actor.Velocity = velocity;
	}

	public override void Reset(){}
	
	private bool ShouldDeccelerate(Godot.Vector2 currentVelocity, Godot.Vector2 targetVelocity, Godot.Vector2 inputVector, CharacterActor actor)
	{
		if (
			inputVector == Godot.Vector2.Zero || //deccel if not holding an input key
			MiscUtils.WillPassThroughZero(targetVelocity.X, currentVelocity.X) //DO deccel if swapping directions
		) return true;
		else return false;
	}
}