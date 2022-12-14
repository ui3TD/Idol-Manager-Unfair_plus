<img src="https://github.com/ui3TD/Idol-Manager-Unfair_plus/blob/main/Mods/Unfair_plus/thumb.png" width=300></img>
# Unfair_plus

This mod is a game overhaul. It will change the behavior of the base game significantly. Remember to make backups of any files you replace. 

Main Features:
- **Traits**: In the original game, several traits had no effects implemented. This mod adds effects to all traits.
- **Rebalancing**: Several quality of life enhancements, and balance fixes allow more styles of gameplay. For example, you'll find that shows and contract jobs are quite useful. You'll also find that some of the cheesy tricks are no longer possible.
- **Unfair+**: This mode replaces the current "Unfair" mode. You'll face many challenges. Fans easily lose interest over time and will abandon your group. Girls will not tolerate low pay, and will demand a bigger cut of their profits. You'll have to put in effort to maintain friendships with your girls, and more. Try to conquer Tokyo Coliseum under these conditions!

The download includes a mod to install traits only if you want.

**![> Download Unfair_plus](https://github.com/ui3TD/Idol-Manager-Unfair_plus/releases/tag/v2.0)**

## Conflicts
Assembly-CSharp.dll<br>
\Business\business.json<br>
\Constants\constants.json<br>
\Idol\traits.json<br>
\Shows\mc.json<br>
\Shows\genre.json<br>
\Shows\medium.json<br>
\Singles\choreography.json<br>
\Singles\genres.json<br>
\Singles\lyrics.json

There have been several changes to fan appeal values of show and single production parameters. This will conflict with other similar mods like "Take It Easy On My Girls".

## Instructions
### Installation Instructions:
1. Copy contents of the Mods folder into the mod directory. On Windows, that's c:\Users\username\AppData\LocalLow\Glitch Pitch\Idol Manager\Mods\
2. Navigate to the \IM_Data\Managed\ directory of Idol Manager.
3. Rename your existing "Assembly-CSharp.dll" to "Assembly-CSharp_orig.dll" to keep it as a back-up.
4. Copy "Assembly-CSharp.dll" into the \IM_Data\Managed\ directory of Idol Manager.
5. Play the game.

### Uninstallation Instructions:
#### Quick Uninstallation Instructions:
1. Disable the mod in Idol Manager.

#### Full Uninstallation Instructions:
1. Navigate to the \IM_Data\Managed\ directory of Idol Manager.
2. Find "Assembly-CSharp.dll" and "Assembly-CSharp_orig.dll" files
3. Delete the "Assembly-CSharp.dll" file and rename the "Assembly-CSharp_orig.dll" file to replace it.
4. Delete the mod in the mod directory.

## Changes
### Quality of Life (All difficulty modes):
- Press the '4' key or click on the fast forward button twice to toggle 5x speed.
- Home row of keyboard (asdfg...) are hotkeys to open each sidebar tab.
- Show genres renamed to make more sense.
- Dating status is visible for underage members so that the player can track gossip. Player cannot flirt with them though.
- Sister group minimum member limit for creation removed to allow more freedom (it does introduce some imbalance).

### Things made more challenging (All difficulty modes):
- 20000 yen/wk is the expected starting salary for 100% satisfaction so that it's aligned with minimum wage.
- 20000 yen/wk is the default starting salary so that it's aligned with minimum wage.
- Concert revenue formula rebalanced so it approaches zero with high ticket prices. This is most impactful in Unfair+.
- Concert venues can only be unlocked if you've sold out the previous venue with a profit.
- Concert Club venue had its hype multiplier reduced. Previously, clubs had higher hype than other venues.
- Theater show sales and subscriptions start to decay after 30 days since the last single due to staleness. It's not so severe in easy/normal mode, but more severe in Unfair+.
- Theater subscription revenue decreased by 90%. Most of the revenue goes to the streaming service provider.
- Theater attendance for Manzais halved to scale with stamina cost compared to performances.
- Theater revenue added to girls' weekly earnings so that it impacts salary satisfaction.
- Show media platform appeal values reduced to encourage usage regardless of group themes.
- Staff cannot be fired using scandal points within the first month so that you can't just hire and immediately fire staff to get rid of points.
- World tours are limited to 100 stamina so you can't go to all the countries at once.

### Things made more forgiving (All difficulty modes):
- Probabilty of being lesbian reduced to 6% and probability for bisexuality reduced to 5% if the player is male so that there is less in-group dating. Probabilities unchanged for female players so that they can romance the idols easily.
- Drama business proposals get 3x the fans so they are more competitive with other methods of gaining fans.
- Drama and Ad business proposals have their max base stamina reduced to 20 at high levels and their other parameters are scaled accordingly so it's easier to micromanage stamina.
- Show MC fame points give a boost to new fans, especially so at high fame so that MCs have much more value late game.
- Single PVs have success chances increased by 33% so that they are more reliable in mid to late game but the crit success bonus has decreased to balance it.
- Fake Scandal crit success bonus increased significantly so that it's more enticing. It is the fastest single marketing method to grow fans.
- When releasing a single, the penalty for a decrease in fame and appeal now only considers past singles of the same group, so that sister groups are more independent.
- Theater attendance increased for 'everyone', 'casual' and age groups so that it is balanced with 'male' and 'female'.
- Training vocal/dance stamina reduced to 1 pt/day so that it's a bit more worth it. It takes on average 2 years to recoup on opportunity costs. It's more efficient if the girl is lower in skill or if the girl is in the front of senbatsu.
- World tours give 3.5x more fans to compensate for stamina limitation.
- Single genres/lyrics/choreo appeals overhauled so that none are clearly better, and so that negative effects are reduced to encourage trying new combinations.

### Bug Fixes (All difficulty modes):
- Fixed senbatsu stats calculation so it doesn't punish you if you don't have enough idols to fill all rows.
- Fixed Concert revenue formula so that it shows accurate estimated values.
- Fixed Theater so that it uses stamina.
- Fixed Theater so that money tooltip includes subscription earnings, excludes days off, and includes 7 days instead of 6.
- Fixed Theater so that revenue stats are not offset by one day.
- Fixed Theater so that average stats ignore days off.
- Fixed Theater so that revenue gets added to girls earnings.
- Fixed Cafe weekly revenue so that it includes 7 days instead of 6.
- Fixed so that when girls dating each other break up, their relationship status is no longer known.
- Fixed fan age pie charts to show correct proportions.
- Fixed world tour revenue text color to consider savings.

### Unfair+ Difficulty:
- In Unfair+, there is significant customer churn rate. You lose fans each day and it's very difficult to break into the big leagues. At 100k fans, you can expect to lose 5% in a month unless you take measures to grow.
- In Unfair+, Tokyo Coliseum is increased to 50,000 capacity, but has its base cost increased to 200,000,000 for more endgame challenge.
- In Unfair+, idols at 10 fame will expect at least 10% of their earnings as salary so that the endgame is a bit more challenging.
- In Unfair+, penalty for low salary satisfaction increased 10x. Each day, if salary gets under 50%, the graduation date moves closer by 10 days. If under 20% then 30 days.
- In Unfair+, audience of TV and Radio shows decrease with fatigue so you're more motivated to update them or create new shows. New fan contribution to audience decreases proportional to fatigue squared. Internet shows aren't affected.
- In Unfair+, sister group new fans are reduced 5x to be more balanced.
- In Unfair+, player relationships with girls decay each week so you have to talk to them sometimes. -2 to +1 to influence each week depending on salary satisfaction. -1 each week to romance (lose a level every ~2 years). -2 each week to friendship (lose a level every ~1 year).
- In Unfair+, Promotion lvl 3 requires 6 episodes of an internet show instead of just 1
- In Unfair+, Promotion lvl 4 requires 6 episodes of a radio show instead of just 1, and peak internet show audience of 6000 instead of 1000
- In Unfair+, Promotion lvl 6 requires 6 episodes of a TV show instead of just 1
- In Unfair+, Promotion lvl 9 requires 3 idols with 9 fame instead of 5 fame

### Trait Implementations (All difficulty modes):
- Girls with Live Fast trait have double the rate of stat decreases after their peak age, both in random decreases and on birthdays.
- Girls with Trendy trait have 1.5x the appeal to non-adults and 0.5x the appeal to adults.
- Girls with Anxiety trait have -10 to all stats when a special event is waiting.
- Girls with Clumsy trait have +30 to funny and -30 to dance.
- Girls with Complacent trait have -20 to vocal and dance if they were center in the latest single.
- Girls with Worrier trait have -20 to all stats when there are scandal points.
- Girls with Defeatist trait have -20 to all stats if the latest single does not top the charts. The modifier lasts until the next single release.
- Girls with Underdog trait have +20 to all stats if the latest single does not top the charts. The modifier lasts until the next single release.
- Stat changes are not displayed on the profile cards, but are calculated when creating Shows, Concerts, Business Proposals, and Singles.
- Girls with Lone Wolf trait have +40 to all stats when they are the only one assigned to a show.
- Girls with Photogenic trait have +100% to photoshoots before other modifiers.
- Girls with Maternal trait have positive relationships with those younger than them by default and these relationships develop naturally 4x faster.
- Girls with Precocious trait have positive relationships with those older than them by default and these relationships develop naturally 4x faster.
- Girls with Arrogant trait have all relationships naturally rapidly decrease after they center a single until the next single release.
- Girls with Forgiving trait will never dislike or hate any other girls.
- Girls with Meme Queen trait get +10 to all stats for internet shows and get +10% success rate and +5% crit success rate when participating in viral marketing campaigns (does not stack).
- Girls with Annoying trait cause other members to spend 1.2x physical stamina in shows.
- Girls with Misandry trait have a 20% chance of receiving bad opinions from Male fans when participating in a single with handshakes.
- Girls with Perfectionist trait get -20 to mental stamina when world tours end with less than 80% attendance, or when they participate in concerts with less than 100% hype.
- If there is an Indiscreet member, girls in dating relationships unknown to the player have a 2% chance of having the relationship revealed each week.
- When the relationship is indiscreetly revealed, the girls involved lose 30 mental stamina. If the group forbids dating then a scandal point is added for each girl.

# Other Mods

Check out my unlisted mods on Steam Workshop that are 100% compatible:

- National Tour: https://steamcommunity.com/sharedfiles/filedetails/?id=2872425710