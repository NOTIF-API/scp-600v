# scp-600v
a new object scp-600 which at the beginning of the game with some chance will spawn among class d, its task is absolutely the same as that of any scp, when a person is killed, it changes its role to the killed one (only the role), when there is no one left but him and other scp round ends
# permission's
> s6.SelfSpawn - can an admin give himself the scp-600 role    
> s6.ChengeMHP - can admin change max health while playing as scp-600     
> s6.debug - can admin get player's session variables     
> s6.GetPlayers - can the admin find out the list of players playing for scp-600     

# Configs
```yaml
s_c_p600_v:
# Enable or disable plugin
  is_enabled: true
  # Can the player see the debug message in the server console
  debug: false
  # probability of occurrence at the beginning of the game in percentages
  percent_to_spawn: 25
  # determines if SCP-600 will take damage over its lifetime
  can_bleading: true
  # Badge color in player list only lowers chars
  badge_color: 'red'
  # message when admin gets list of scp players
  list_getted: 'Players: {name} | now playing as SCP-600V'
  # SCP-600V Role Configurations
  scp600_config_role:
    custom_info: 'SCP-600V'
    name: 'SCP-600V'
    max_health: 400
    # whether it will be shown next to the player's name that he is SCP-600V
    visible_scp_name: true
    role: Tutorial
    # the initial appearance of the object during behavior (preferably left as it will follow among class D)
    start_appearance: ClassD
    description: '<color="purple">Help other scp complete task</color>'
    id: 96
    inventory:
    - 'KeycardScientist'
    console_message: '<color="green">Your are spawned as</color> <color="red">SCP-600V</color>'
    custom_role_f_f_multiplier:
      Scp049: 0
      Scp173: 0
      Scp096: 0
      Scp939: 0
      Scp0492: 0
      Scp106: 0
    ammo:
      Nato9: 30
      Ammo44Cal: 0
      Nato556: 0
      Nato762: 0
      Ammo12Gauge: 0
    dont_user_items:
    - MicroHID
    custom_abilities: []
    spawn_properties:
      limit: 0
      dynamic_spawn_points: []
      static_spawn_points: []
      role_spawn_points: []
    keep_position_on_spawn: false
    keep_inventory_on_spawn: false
    removal_kills_player: true
    keep_role_on_death: false
    spawn_chance: 0
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
    ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
```
