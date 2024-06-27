using Godot;
using System;

public class MiscUtils
{
	public static bool WillPassThroughZero(float a, float b)
	{
		// Check if a and b have the same sign or if either one is zero
		if ((a > 0 && b > 0) || (a < 0 && b < 0) || a == 0 || b == 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}
