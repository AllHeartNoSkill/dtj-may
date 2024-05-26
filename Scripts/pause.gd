extends Control


func _ready():
	$AnimationPlayer.play("RESET")


func _resume():
	get_tree().paused = false
	$AnimationPlayer.play_backwards("blur")

func _pause():
	get_tree().paused = true
	$AnimationPlayer.play("blur")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if Input.is_action_pressed("pause") and !get_tree().paused:
		_pause()
	
	elif Input.is_action_pressed("pause") and get_tree().paused:
		_resume()


func _on_resume_pressed():
	_resume()


func _on_restart_pressed():
	_resume()
	get_tree().reload_current_scene()


func _on_main_menu_pressed():
	get_tree().change_scene_to_file("res://Nodes/main_menu.tscn")

