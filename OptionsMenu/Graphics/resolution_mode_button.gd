extends Control

@onready var option_button: OptionButton = $HBoxContainer/OptionButton

const RESOLUTION_DICT:Dictionary = {
	"1920 x 1080": Vector2i(1920, 1080),
	"1152 x 648": Vector2i(1152, 648),
	"1280 x 720": Vector2i(1280, 720),
}


func _ready() -> void:
	option_button.item_selected.connect(on_resolution_selected)
	add_resoultion_items()


func add_resoultion_items() -> void:
	for res_key in RESOLUTION_DICT:
		option_button.add_item(res_key)


func on_resolution_selected(index: int) -> void:
	DisplayServer.window_set_size(RESOLUTION_DICT.values()[index])
