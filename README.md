# scp-600v
[![Sponsor on Patreon](https://img.shields.io/badge/sponsor-patreon-orange.svg)](https://www.patreon.com/NOTIF247)
[![Download letest version](https://img.shields.io/badge/download-latest-red.svg)](https://github.com/NOTIF-API/scp-600v/releases)

# about the plugin
a new object scp-600 which at the beginning of the game has a certain chance of spawning among D-class personnel. The task of this scp object is exactly the same as that of any other scp where it helps to fulfill their goals. This object is capable of changing its appearance to that of the one killed, which makes it quite difficult to recognize it from afar.

# Questions and bugs
if you have questions about scp-600 or you find a bug, you can contact me on the `notifapi` discord or here 

[![Help](https://img.shields.io/badge/issues-aqua)]( https://github.com/NOTIF-API/scp-600v/issues)

# permission's
> s6.spawn - can an admin give himself the scp-600 role       
> s6.debug - can admin allow accses to use debug command

# Configs
```yaml
# Enable or disable plugin
is_enabled: true
# Can the player see the debug message in the server console
debug: false
# The message the player will see if they use their ability.
ability_use_message: 'You have successfully changed your appearance with your ability to %role%'
# Scp 600 role config
scp_role:
# Role special id (Don't allow similarities with other roles!)
  id: 600
  # Scp-600 max health
  max_health: 400
  # Description when a role appears, as a rule, it is displayed on the player’s screen when he appears for a certain role.
  description: 'An object performing a task as an aggressive object against humanity'
  # Information visible on the role, to hide it make the line empty
  custom_info: 'SCP-600'
  # The message that a player sees after killing another player
  kill_message: 'You killed player %player% and changed your appearance to %role%'
  # Will the player and Scp be able to attack each other (SCP VS Scp600) and SCP Ability work on 600
  is_frindle_fire_enabled: false
  # Will the player get AHP when killing a player
  is_ahp_renerate: true
  # When killed, will the player also increase the maximum amount of AHP divided by two
  is_ahp_max_increase: true
  # The amount of AHP that a player will receive when killing another player
  ahp_amount: 15
  # List of items that the object cannot take, ItemType as the base representation of the item names
  black_list_items:
  - MicroHID
  - Jailbird
  # List of items that the player will have when receiving the role (do not give what is prohibited)
  inventory:
  - 'Coin'
  - 'Adrenaline'
  # Initial appearance of the object upon respawn
  start_apperance: ClassD
  # Basic role spawn settings (location chose)
  spawn_properties:
    limit: 1
    dynamic_spawn_points: []
    static_spawn_points: []
    role_spawn_points:
    - role: ClassD
      chance: 100
    room_spawn_points: []
    locker_spawn_points: []
  # Chance for a player to appear for a given role at the start of a round
  spawn_chance: 25
  # The role that the player has on his own behalf (it is not advisable to change it, as it is a human role)
  role: Tutorial
  # Role-specific abilities that can be granted
  custom_abilities:
  - duration: 1
    cooldown: 60
    name: 'Apperance changer'
    description: 'Changes your appearance to a random one'
    # Changing this will likely break your config.
    ability_type: 'ApperanceUpdate'
  # The initial amount and type of ammo a player can have when they spawn for a given role.
  ammo:
    Nato9: 20
  keep_position_on_spawn: false
  keep_inventory_on_spawn: false
  removal_kills_player: true
  ignore_spawn_system: false
  broadcast:
  # The broadcast content
    content: ''
    # The broadcast duration
    duration: 10
    # The broadcast type
    type: Normal
    # Indicates whether the broadcast should be shown
    show: false
  display_custom_item_messages: true
  scale:
    x: 1
    y: 1
    z: 1
  gravity: 
  custom_role_f_f_multiplier: {}
  console_message: 'You have spawned as a custom role!'
  ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
```
