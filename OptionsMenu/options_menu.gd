extends Control
class_name OptionsMenu


@export var back_button:Button

signal exited_options_menu


func _ready() -> void:
	back_button.button_down.connect(on_exit_button_down)
	set_process(false)


func on_exit_button_down() -> void:
	exited_options_menu.emit()
	set_process(false)
