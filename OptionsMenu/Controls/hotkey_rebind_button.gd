extends Control
class_name HotkeyRebindButton

@onready var label:Label = $HBoxContainer/Label
@onready var button:Button = $HBoxContainer/Button
@export var action_name:String = ""

signal hotkey_rebind_button_toggled(toggled:bool, toggled_button:HotkeyRebindButton)
signal rebind_requested(action_name:String, event:InputEvent)

func _ready() -> void:
	set_label_name()
	button.toggled.connect(on_button_toggled)
	set_process_unhandled_key_input(false)


func _unhandled_key_input(event: InputEvent) -> void:
	rebind_requested.emit(action_name, event)
	hotkey_rebind_button_toggled.emit(false, self)


func on_button_toggled(toggled:bool) -> void:
	hotkey_rebind_button_toggled.emit(toggled, self)


func set_label_name() -> void:
	label.text = "No Action Assigned"
	
	if action_name:
		label.text = MiscUtils.string_to_spaced_pascal(action_name)
