extends Control

var buttons;
var lines;

# Called when the node enters the scene tree for the first time.
func _ready():
	buttons = []
	lines = []
	buttons.append(get_node("%Button"))
	for n in range(2, 18):
		buttons.append(get_node("%Button"+ str(n)))
	
	create_line_between(buttons[0], buttons[1])
	create_line_between(buttons[0], buttons[2])

	var line = Line2D.new();
	line.add_point(Vector2(57.6, 64))
	line.add_point(Vector2(98, 48))
	add_child(line)
	lines.append(line)

	print(lines)
	
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func create_line_between(button1, button2):
	var line = Line2D.new();
	var container = get_global_rect()
	var rect1: Rect2 = button1.get_global_rect()
	var rect2: Rect2 = button2.get_global_rect()
	line.add_point(Vector2(rect1.position.x + rect1.size.x - container.position.x, (rect1.position.y - container.position.y) + rect1.size.y / 2))
	line.add_point(Vector2(rect2.position.x - container.position.x, rect1.position.y - container.position.y + rect1.size.y / 2))

	add_child(line)

	lines.append(line)
