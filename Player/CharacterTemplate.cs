using Godot;


[GlobalClass]
public partial class CharacterTemplate : Resource 
{
	[Export]
	public NodePath SpritePath;
	[Export]
	public NodePath ColliderPath;
	[Export]
	public NodePath JumpCommandPath;
	[Export]
	public NodePath MoveCommandPath;
	
	public CharacterTemplate() : this("", "", "", "") {}
	
	public CharacterTemplate(NodePath spritePath, NodePath colliderPath, NodePath jumpCommandPath, NodePath moveCommandPath) 
	{
		JumpCommandPath = jumpCommandPath;
		MoveCommandPath = moveCommandPath;
		SpritePath = spritePath;
		ColliderPath = colliderPath;
	}
	
	
}