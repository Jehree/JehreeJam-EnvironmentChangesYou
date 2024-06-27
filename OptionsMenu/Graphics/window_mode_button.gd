extends Control

@onready var option_button: OptionButton = $HBoxContainer/OptionButton

const WINDOW_MODES:Array[String] = [
	"Full Screen",
	"Windowed",
	"Borderless Window",
	"Borderless Fullscreen",
]

enum WindowMode {
	FULL_SCREEN = 0,
	WINDOWED,
	BORDERLESS_WINDOW,
	BORDERLESS_FULLSCREEN
}


func _ready() -> void:
	add_window_mode_items()
	option_button.item_selected.connect(on_window_mode_item_selected)
	option_button.selected = WindowMode.WINDOWED


func add_window_mode_items() -> void:
	for mode in WINDOW_MODES:
		option_button.add_item(mode)


func on_window_mode_item_selected(index:int) -> void:
	match index:
		WindowMode.FULL_SCREEN:
			DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
			DisplayServer.window_set_flag(DisplayServer.WINDOW_FLAG_BORDERLESS, false)
		WindowMode.WINDOWED:
			DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
			DisplayServer.window_set_flag(DisplayServer.WINDOW_FLAG_BORDERLESS, false)
		WindowMode.BORDERLESS_WINDOW:
			DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
			DisplayServer.window_set_flag(DisplayServer.WINDOW_FLAG_BORDERLESS, true)
		WindowMode.BORDERLESS_FULLSCREEN:
			DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
			DisplayServer.window_set_flag(DisplayServer.WINDOW_FLAG_BORDERLESS, true)












