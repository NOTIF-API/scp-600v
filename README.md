# scp-600v
[![Sponsor on Patreon](https://img.shields.io/badge/sponsor-patreon-orange.svg)](https://www.patreon.com/NOTIF247)
[![Download letest version](https://img.shields.io/badge/download-latest-red.svg)](https://github.com/NOTIF-API/scp-600v/releases)

# about the plugin
a new object scp-600 which at the beginning of the game has a certain chance of spawning among D-class personnel. The task of this scp object is exactly the same as that of any other scp where it helps to fulfill their goals. This object is capable of changing its appearance to that of the one killed, which makes it quite difficult to recognize it from afar.

# Questions and bugs
if you have questions about scp-600 or you find a bug, you can contact me on the `notifapi` discord or here 

[![Help](https://img.shields.io/badge/issues-aqua)]( https://github.com/NOTIF-API/scp-600v/issues)

# permission's
> s6.Spawn - can an admin give himself the scp-600 role    
> s6.ChengeMHP - can admin change max health while playing as scp-600     
> s6.debug - can admin allow accses to use debug command
> s6.GetPlayers - can the admin find out the list of players playing for scp-600     

# Configs
```yaml
scp600v:
# Enable or disable plugin
  is_enabled: true
  # Can the player see the debug message in the server console
  debug: false
  # Configuration for the Scp600 role
  scp_config:
    id: 600
    max_health: 400
    name: 'SCP-600V'
    description: 'Angry scp 600, help other scp complete a task'
    # If need hide set a epty string
    custom_info: 'SCP-600V'
    # Role a player self visible (do not change to scp's)
    role: Tutorial
    # Can scp600 get damage
    can_bleading: true
    spawn_chance: 15
    transformation_message: 'you kelled %player% and changed your apperance to %role%'
    spawn_properties:
      limit: 0
      dynamic_spawn_points: []
      static_spawn_points: []
      role_spawn_points:
      - role: ClassD
        chance: 100
    black_list_items:
    - MicroHID
    custom_abilities: []
    inventory: []
    ammo: {}
    keep_position_on_spawn: false
    keep_inventory_on_spawn: false
    removal_kills_player: true
    keep_role_on_death: false
    ignore_spawn_system: false
    keep_role_on_changing_role: false
    broadcast:
    # The broadcast content
      content: ''
      # The broadcast duration
      duration: 10
      # The broadcast type
      type: Normal
      # Indicates whether the broadcast should be shown or not
      show: true
    display_custom_item_messages: true
    scale:
      x: 1
      y: 1
      z: 1
    custom_role_f_f_multiplier: {}
    console_message: 'You have spawned as a custom role!'
    ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
```
