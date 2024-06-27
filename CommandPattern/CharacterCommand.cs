using Godot;
using System;

public abstract partial class CharacterCommand : Node
{
	internal class DataAtExecute 
	{
		public Vector2 inputVector;
		public double delta;
		public Vector2 lastMovementDirection;
		
		public DataAtExecute(Vector2 inputVector, double delta, Vector2 lastMovementDirection)
		{
			this.inputVector = inputVector;
			this.delta = delta;
			this.lastMovementDirection = lastMovementDirection;
		}
	}
	
	public abstract void Execute(CharacterActor actor, Object data = null);
	
	public abstract void Reset();
}




