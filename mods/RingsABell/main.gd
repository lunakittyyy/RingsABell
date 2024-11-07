extends Node

const _streamplayer = preload("res://mods/RingsABell/bell_ring.tscn")
const _streams = [
	preload("res://mods/RingsABell/jingle1.mp3"),
	preload("res://mods/RingsABell/jingle2.mp3"),
	preload("res://mods/RingsABell/jingle3.mp3")
]

var audioPlayerNode

func _ready():
	print("*yaaawn* morning.......")

func _setup_plr(player: Node):
	print("Here we go")
	if player.name != "player":
		print("Player doesn't seem to be you - skipping")
		return
	if player.get_node_or_null("sound_manager") == null:
		print("We have no sound manager on this Player for some reason")
		return
	if player.get_node_or_null("sound_manager/bell_ring") == null:
		print("Making a ringer")
		audioPlayerNode = _streamplayer.instance()
		player.get_node("sound_manager").add_child(audioPlayerNode)
	else: print("Ringer already present")

func _ring_bell():
	print("Ringing bell")
	if PlayerData.cosmetics_equipped["accessory"].has("accessory_collar_bell"):
		audioPlayerNode.stream = _streams[randi() % _streams.size()]
		audioPlayerNode.pitch_scale = rand_range(0.9, 1.1)
		audioPlayerNode.play()
		print("Ring!")
	else: print("No bell collar!")
