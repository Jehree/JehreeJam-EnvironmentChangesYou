extends Control


@export var label:Label
@export var button:Button
@export var action_name:String = ""




func _ready() -> void:
	set_process_unhandled_key_input(false)
	set_action_name()
	set_button_text()
	
	button.toggled.connect(on_button_toggled)


func _unhandled_key_input(event: InputEvent) -> void:
	rebind_action_key(event)
	button.button_pressed = false


func rebind_action_key(event:InputEvent) -> void:
	InputMap.action_erase_events(action_name)
	InputMap.action_add_event(action_name, event)
	
	set_process_unhandled_key_input(false)
	set_button_text()
	set_action_name()


func on_button_toggled(button_toggled:bool) -> void:
	if button_toggled:
		button.text = "Press any key..."
		set_process_unhandled_key_input(button_toggled)
		
		for node:Node in get_tree().get_nodes_in_group("hotkey_button"):
			if not (node is HotkeyRebindButton): return
			var button = node as HotkeyRebindButton
			
			if button.action_name != self.action_name:
				button.button.toggle_mode = false
				button.set_process_unhandled_key_input(false)
	else:
		for node:Node in get_tree().get_nodes_in_group("hotkey_button"):
			if not (node is HotkeyRebindButton): return
			var button = node as HotkeyRebindButton
			
			if button.action_name != self.action_name:
				button.button.toggle_mode = true
				button.set_process_unhandled_key_input(false)
		
		set_button_text()


func set_action_name() -> void:
	label.text = "Unassigned"
	
	if action_name:
		label.text = MiscUtils.string_to_spaced_pascal(action_name)


func set_button_text() -> void:
	var action_events = InputMap.action_get_events(action_name)
	var action_event = action_events[0]
	var action_keycode = OS.get_keycode_string(action_event.physical_keycode)
	
	button.text = action_keycode

























