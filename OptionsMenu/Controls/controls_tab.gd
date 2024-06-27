extends TabBar
class_name ControlsTab

@export var hotkey_rebind_buttons:Array[HotkeyRebindButton]

func _ready() -> void:
	for hotkey_rebind_button in hotkey_rebind_buttons:
		hotkey_rebind_button.hotkey_rebind_button_toggled.connect(on_hotkey_rebind_button_toggled)
		hotkey_rebind_button.rebind_requested.connect(on_rebind_requested)
		hotkey_rebind_button.button.text = get_action_keycode(hotkey_rebind_button.action_name)


func on_hotkey_rebind_button_toggled(toggled:bool, toggled_button:HotkeyRebindButton) -> void:
	if toggled:
		toggled_button.button.text = "Press any key..."
		toggled_button.set_process_unhandled_key_input(true)
		set_all_rebind_buttons_toggle_mode(false, toggled_button.action_name)
		disable_all_unhandled_key_input(toggled_button.action_name)
	else:
		toggled_button.button.text = get_action_keycode(toggled_button.action_name)
		toggled_button.button.toggle_mode = false # button toggle mode is set to false...
		set_all_rebind_buttons_toggle_mode(true) # ...then back to true to force un-toggle it
		disable_all_unhandled_key_input()


func on_rebind_requested(action_name, event:InputEvent) -> void:
	InputMap.action_erase_events(action_name)
	InputMap.action_add_event(action_name, event)


func set_all_rebind_buttons_toggle_mode(toggled:bool, except_action_name:String = "") -> void:
	for hotkey_rebind_button in hotkey_rebind_buttons:
		if hotkey_rebind_button.action_name == except_action_name: continue
		hotkey_rebind_button.button.toggle_mode = toggled


func disable_all_unhandled_key_input(except_action_name:String = "") -> void:
	for hotkey_rebind_button in hotkey_rebind_buttons:
		if hotkey_rebind_button.action_name == except_action_name: continue
		hotkey_rebind_button.set_process_unhandled_key_input(false)


func get_action_keycode(action_name:String) -> String:
	var action_events = InputMap.action_get_events(action_name)
	var action_event = action_events[0]
	var action_keycode = OS.get_keycode_string(action_event.physical_keycode)
	return action_keycode






