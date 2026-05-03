***these skills need to provide distinct tactical options, not slightly different uses, so far they are not doing it***

#### all
  - increase match radius

#### melee, ranged, stealth
  - jump: matches 3+ and jumps to the neares same color piece OUTSIDE of the matched group. **Adv. implementation:** has a range and gathering extra momentum increases the range, can jump to piece even if it's normally too far away

#### melee
  - `S` bullrush (HIGH DAMAGE, has pushback, **Adv. implementation:** needs strength threshold)
  - `S` whirlwind
  - `S` leap attack: like jump but ... also attacks
  - `S` delayed blow: **Adv. implementation:** powerful AOE attack that holds the player in place 1 turn and strikes the next turn. **Adv. implementation:** momentum increases range
      VVV THESE SHOUDLN"T BE INT
  - `I` enrage: buff, increase strength (or w/e) for limited turns
  - `I` startle: greatly debuff enemy agility, reducing their initiative and possibly giving the player an extra turn
  - `I` intimidate: debuff enemy strenght, agility and intelligence, debuffing their damage, primarily. Can match remotely 
  - `I` frighten: cause enemy to back away in fear 1 tile away from you; if enemies have nowhere to move due to obstacles, they get a debuff in defense and/or initiative. enemy must be adjacent to match group, will traverse the match line away from the player
  - `I` taunt: challenge enemy to march at you 1 tile and forego a bit of defense
  - `S` joust/charge: like whirlwind but only attack the first enemy adjacent to your path, stop at end. higher damage than whirlwind
  - `A` pilum throw: stand in place and throw an expendable pilum for damage proportional to your agility and str and significantly debuf target's defense
  - `A` lasso: pull an enemy to you from the end of the match line and stun it for one turn. may require strength check. **Adv. implementation:** if the check fails the enemy will not move but will be debuffed   **Adv. implementation:** may also require agility check and if that fails first the enemy is completely unaffected 
  - `S` kick: *stationary* debuff defense, if it passes strength check, pushes enemy back OR object that can then break, cause damage to whatever it hits and even explode if applicable
  - `S` sweep: *stationary* attacks all targets on the opposite side of the match line, player needs to be roughly adjacent to he middle of the opposite side of the effect. does not benefit from momentum

  idea: agility based melee weapons and skills have far more stationary skills (because dex weapons are more for duelling) and fewer tile matching skills

  - `A` throw equipped weapon: **Adv. implementation:** desperate attack that temporarily removes your melee weapon from you. once the enemy dies, preferably in that attack, it appears as an item on the ground 
  - `S` [r] push object: **Adv. implementation:** if stationary with no matches, pushes a piece one tile ahead, if it has momentum, pushes the piece 2 tiles (or maybe more). if an enemy is in the way, takes damage and debuff -- maybe I should have these replentish
  - `C` fortify: *stationary* buff defense, can match any anjacent defensive group, lasts at least 2 turns **Adv. implementation:**: the match group must be 'ahead' of the player, but which way is ahead is difficult to determine. maybe an area between the player and the closest aggroed enemy?
  - `C` a skill that keeps you from being pushed back and grants defense, can only match groups directly behind the player like Kick
  - `C` shield focus **Adv. implementation:** pick an enemy to debuff damage indefinitely. if you cast the skill on another enemy, that one bets the debuff and the former loses the debuff
  - `S` search opening **Adv. implementation:** pick an enemy to debuff defense indefinitely. ----//----
  - `S` stare-down **Adv. implementation:** pick an enemy to debuff initiative indefinitely. ----//----

  - `C` strong-side **Adv. implementation:** pick between 6 directions by matching adjacent pieces in one of those directions: physical attacks coming from that direction will be softened

#### ranged
  - `A` distract: throw a pebble to make enemy stop and check around for a turn. **Adv. implementation:** should pass intelligence check
  - `A` piercing shot: fire an arrow when matching a group behind an enemy (when you're on the other side and on the same board line/direction                            )

#### stealth
  - `A` runthrough?: attacks in straighht line but the thief stops way past the match group, 1 tile away from and behind the target
  - `I` remote grab: grab an item at a distance
  - `A` charge at the target in front, strike, then dash back 1 tile
  - `A` strike the adjacent enemy and vault/back. must have adjacent match group and adjacent end of line, will traverse away from the enemy. Low damage

#### sorceress
  - freeze ground: **Adv. implementation:** passing enemies trip and loose 1 turn (*requires a third grid of effects, superimposes those effects over the bg*)
  - firewall: same
  - `C` force shield: reduce damage against the next attack the next turn 
  - `C` force barrier: reduce damage against all attacks the next turn 
  -A rare skill that does damage on a large AOE, but would work best with skills that draw enemies together 

  - `I` reroll the piece in front of the match line
  - `I` reroll the piece behind of the match line
  - `I` rearrange pieces clockwise around you
  - `I` rearrange pieces clockwise around the end of the match line
  - `I` rearrange pieces clockwise around the start of the match line

#### placeable pieces
  -walk piece: uses depend on agility
  -melee piece: more with strength
  -defensive: constitution
  -tech: intelligence
