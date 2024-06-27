extends Node
class_name MiscUtils

static func string_to_spaced_pascal(string:String) -> String:
	var space_convert = string.replace("_", " ")
	var prettier:String = ""
	
	var capitalize := false
	var first_iteration := true
	for character in space_convert:
		var processed_char = character
		
		if character == " ":
			capitalize = true
			prettier += character
			continue
		
		if capitalize or first_iteration:
			first_iteration = false
			processed_char = character.to_upper()
			capitalize = false
		
		prettier += processed_char
	
	return prettier
