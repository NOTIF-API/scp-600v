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
  # Can SCPs beat our
  is_scp_can_damage_me: false
  # Max healt
  maxhealt: 400
  # Percentage to spawn scp at the beginning of the game
  percent_to_spawn: 25
  # determines if SCP-600 will take damage over its lifetime
  can_bleading: true
  # Spawn message for SCP-600V
  spawn_message: You appeared behind SCP-600V
  # Badge color in player list
  badge_color: red
  # message when player changed class
  complete_transform: Done
  # message when admin gets list of scp players
  list_getted: 'Players: {name} | now playing as SCP-600V'
  # message when admin doesn't have command rights
  permission_denied: Problems with your's permissions
  # player search error message
  player_n_f: Player not found
  # message when admin changed max hp
  mhp_chenged: 'your max healt set: {amount} and healed'
  # error in determining the player's health
  mhp_heal_arg_er: i can't find out how much health you need to give
  # message when admin cannot become scp-600
  spawn_command_er: you cannot become scp-600 because you are an observer
  # message to the player when he transforms into another player
  message_scp_transform: you transformed into {player}
  # message if player dead by scp-600
  message_death_player_by_scp600: your killed by <color="red">SCP-600V</color>
  # whether badge will be assigned to the object from the beginning of the game
  is_visible_badge: true
  # if the server has disabled friendly fire set true
  is_f_f_enabled: true
  # due to the mechanics of the game, the game often reduces the damage dealt
  multiple_damage: 3
  on_last_player: >-
    <align="left"><color=#00FF00>Your</color> last player

    <align="center"><b><color="red">All human dead!</color></b>
  # the number of rounds given out at the beginning
  start_ammo:
    Nato9: 30
    Ammo44Cal: 0
    Nato556: 0
    Nato762: 0
    Ammo12Gauge: 0
```
