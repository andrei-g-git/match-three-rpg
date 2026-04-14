- there are cases where matching tiles will result in V or fork shaped match groups that my code does not account for. these are rare and I should have rare but powerful effects for them

- should enemies be able to (accidentally) match, or at least not have the player receive the benefits of the enemy matching pieces? 

- there doesn't seem to be much incentive for the player to advance or do much of anything when enemies are not aggroed. Possible solutions:
    * make enemies have extremely large aggro range or ditch it altogether and just have them chase the player from the beginning
    * ditch any pretense that these levels can be carefully structured puzzles and just have a massive free for all where enemies appear and head for the player
    * add walk pieces to the random piece generation, but then AGI based walk pieces become less relevant...
    * adding random rewards to matching might work, but then the player has even more incentive to keep matching forever...
    * at least on some types of levels, collapsing pieces don't generate new random pieces, just stuff like walk tiles, but then, again, stored AGI-based walk pieces become useless...
    ----------------------
        - for now it seems like enemy aggro range should be removed, enemies should aggro at level start

    * I'm not getting a lot of useful match options, maybe matching should randomly give you buffs in adition to energy sometimes
        - maybe when my energy is full I can get buffs instead of energy. 
        - maybe the buff is temporary, limited to 2 rounds. if you keep matching each turn you can keep increasing it, useful in boss fights where buffing up your damage before you can attack is a core gimick
        - if the damage bonus is flat and is applied last, then it acts as a de-incentive as a dominant strategy where you just sit around racking up buffs (although since enemies have infinite aggro range, it's already a bit de-incentivised)
        - this makes the player a bit overpowered, but I need to try it out more, maybe it's variance.
    * It looks like there's a high chance that, with multiple enemies, the player is soon cornered and is forced to slug it out with basic attacks. maybe enemy HP should be a lot smaller...
    * ensnare should be a ranged skill. the most similar skills that should be tech would be traps. for most classes besides the sorceress, tech pieces should rearrange the board
    * the warrior is not mobile enough. he stands in place for almost the entirety of the level. I need more movement skills and abilities