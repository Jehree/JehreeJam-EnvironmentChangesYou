@tool
extends Node

@export var control_tab: ControlsTab
@export var update:bool:
	get: 
		return false
	set(_val):
		control_tab.hotkey_rebind_buttons = get_all_rebind_button_nodes(control_tab)
		update = false


func get_all_rebind_button_nodes(target:Node) -> Array[HotkeyRebindButton]:
	if not Engine.is_editor_hint(): return []
	
	var all_buttons:Array[HotkeyRebindButton] = []
	
	for node in target.get_children():
		all_buttons += get_all_rebind_button_nodes(node)
		
		if node is HotkeyRebindButton:
			print(node.name + " added to rebind key list!")
			all_buttons.push_back(node)
	
	return all_buttons
