using Godot;
using System;
using System.Dynamic;

[Tool]
public partial class ZoneTileMap : TileMap
{
	private Color _tileColor = Colors.White;
	
	[Export]
	public Color TileColor 
	{
		set 
		{
			Modulate = value;
			_tileColor = value;
		}
		get {return _tileColor;}
	}
	
	[Export]
	public int zone = 0;
}
