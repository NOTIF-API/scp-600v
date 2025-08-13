# scp-600v
[![Sponsor on DonatAlerts](https://img.shields.io/badge/sponsor-alerts-orange.svg)](https://www.donationalerts.com/r/asmrmilo21)
[![Download letest version](https://img.shields.io/badge/download-latest-red.svg)](https://github.com/NOTIF-API/scp-600v/releases)

# about the plugin
a new object scp-600 which at the beginning of the game has a certain chance of spawning among D-class personnel. The task of this scp object is exactly the same as that of any other scp where it helps to fulfill their goals. This object is capable of changing its appearance to that of the one killed, which makes it quite difficult to recognize it from afar.

# Questions and bugs
if you have questions about scp-600 or you find a bug, you can contact me on the `notifapi` discord or here 

[![Help](https://img.shields.io/badge/issues-aqua)]( https://github.com/NOTIF-API/scp-600v/issues)

# permission's
> s6.spawn - can an admin give himself the scp-600 role       
> s6.debug - can admin allow accses to use debug command
> s6.list - can allow for your admins get active tracked players

# PlaceHolders

|  placeholder | substitutes in messages (only if there is) |
|--------------|--------------------------------------------|
|    %role%    | replaced by the role name from RoleTypeId  |
|   %second%   |        Displays the amount of time         |
|   %player%   |        replaced by the players name        |

# Configs
```yaml
# Enable or disable plugin
is_enabled: true
# Can the player see the debug message in the server console
debug: false
# List of roles from which Scp 600 will be selected
selectable_roles:
- ClassD
# The time between which the player can change his appearance
ability_cool_down: 60
# The message the player will see if they use their ability.
ability_use_message: 'You have successfully changed your appearance with your ability to %role%'
# The message the player will see when refusing to use an ability (only if he is an object)
ability_use_deny_message: 'You cannot use the ability until %second% seconds have passed.'
# Description of the top in user settings
header_description: 'Managing SCP-600 capabilities'
# Button bind parameter name
key_bind_name: 'Ability to change appearance (bind)'
# Description for the button bind parameter
key_bind_description: 'The key responsible for activating the ability to change appearance to random role'
# Button identifier for the bind, this value must be unique for its instance
key_bind_id: 601
# Scp 600 role config
scp_role:
  id: 600
  max_health: 400
  description: 'An object performing a task as an aggressive object against humanity'
  # Information visible on the role, to hide it make the line empty
  custom_info: 'SCP-600'
  # The message that a player sees after killing another player
  kill_message: 'You killed player %player% and changed your appearance to %role%'
  # Basically will 173, 106, 939 be able to apply abilities to our object
  is_scp_interact_with_player: false
  # Will the player and Scp be able to attack each other (SCP VS Scp600)
  is_ff: false
  # Will the player get AHP when killing a player
  is_ahp_renerate: true
  # When killed, will the player also increase the maximum amount of AHP divided by two
  is_ahp_max_increase: true
  # The amount of AHP that a player will receive when killing another player
  ahp_amount: 15
  # Object Spawn Probability
  spawn_chance: 25
  # Minimum player online for spawning role
  min_player_count: 8
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
  keep_inventory_on_spawn: false
  broadcast:
  # The broadcast content
    content: ''
    # The broadcast duration
    duration: 10
    # The broadcast type
    type: Normal
    # Indicates whether the broadcast should be shown
    show: true
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
