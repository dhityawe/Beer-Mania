# Beer Mania 🍻

In this game, you play as a bartender, serving beer to a series of customers with different needs and behaviors. Navigate through tables, pour beer with precision, and manage rush hours to keep up with the demand!

## 🎮 Features

### 1. **Player Controls**
- **Move Left/Right**: Use the left and right arrow keys to move the player horizontally across the bar tables.
- **Switch Tables**: Use the up and down arrow keys to move vertically between different tables.

### 2. **Beer Mechanics**
- **Pouring Beer**: Position yourself in front of the beer keg, then hold the `Z` key to fill the glass.
  - **Fill Levels**: The glass fills from 0 to 1 as long as `Z` is held.
  - **Quality Levels**:
    - **Perfect**: Between 0.65 and 0.75
    - **Good**: Between 0.55 and 0.65, or 0.75 and 0.85
    - **Bad**: Any value outside the good or perfect ranges.
- **Throwing the Glass**: Release the `Z` key to throw the filled beer glass towards a customer.
  - **Game Over**: If the glass misses a customer and reaches the spawn point.

### 3. **Customer Types and Behavior**
- **Random Spawn**: Customers spawn at any of the four tables.
- **Movement**: Each customer walks towards the end of the table.
  - **Game Over**: If a customer reaches the end without getting a drink.
  - **Receiving Beer**: A customer hit by a thrown beer glass will react based on the beer’s quality and walk back towards the spawn area.
- **Customer Types**:
  1. **Single-Serve**: Leaves after one drink.
  2. **Double-Serve**: Drinks twice, moving slightly towards the spawn after each.
  3. **Triple-Serve with Tip**: Drinks three times, leaves a tip on the table, and moves back slightly after each drink.
  - **Behavior**: Multi-serve customers throw their glass back towards the end of the table if they finish all servings without reaching the spawn point.
  - **Tips**: Tips can be collected by colliding with them.

### 4. **UI Feedback**
- **Beer Fill Indicator**: A bubble displays fill progress while the player pours.
- **Customer Feedback**: Each customer shows an expression based on the quality of their received beer.
- **Game Over Expression**: The player’s expression changes on a game over.
- **Rush Hour Effects**: During Rush Hour, additional lighting effects appear around the screen.

### 5. **Rush Hour Mode**
- **Activation**: Every 1 minute and 20 seconds, Rush Hour mode begins.
- **Pause Before Start**: Customer spawning halts before Rush Hour starts.
- **Duration**: Rush Hour lasts 10 seconds.
- **Speed Increase**: Game speed ramps up during Rush Hour.
- **Return to Normal**: Speed and customer spawning return to normal after Rush Hour.

---

Enjoy your time managing the rush and satisfying each customer’s thirst for the perfect beer! Cheers! 🍻
