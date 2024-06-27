extends Control
class_name MainMenu

@export var main:MarginContainer
@export var start_button:Button
@export var exit_button:Button
@export var options_button:Button
@export var options_menu:OptionsMenu
@onready var start_level = preload("res://world.tscn") as PackedScene


func _ready() -> void:
	handle_connecting_signals()


func on_start_button_down() -> void:
	get_tree().change_scene_to_packed(start_level)


func on_options_button_down() -> void:
	main.hide()
	options_menu.show()
	options_menu.set_process(true)



func on_exit_button_down() -> void:
	get_tree().quit()


func on_options_menu_exited() -> void:
	main.show()
	options_menu.hide()


func handle_connecting_signals() -> void:
	start_button.button_down.connect(on_start_button_down)
	options_button.button_down.connect(on_options_button_down)
	exit_button.button_down.connect(on_exit_button_down)
	options_menu.exited_options_menu.connect(on_options_menu_exited)
