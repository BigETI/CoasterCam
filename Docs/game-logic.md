# Game logic

## States
- Waiting for input
- Game running
- Game paused
- Game finished

## Actions

### Detect input
- Detect input that is used in the game

### Start game
- Shows text
- Starts a timer (3s)

### Pause game
- Set time scale to 0
- Show pause dialog

### Resume game
- Remove pause dialog
- Set time scale to 1

### Restart game
- Reload scene

### Exit game
- Exit application

### Activate wagon spawner
- Update passenger indicators
- Start a timer (3-5s) for wagon spawner

### Spawn wagon
- Shake screen
- Enable indicators
- Let quality meter follow curve
- Spawn wagon enity
- Move wagon on tracks

### Shoot photo
- Render image to texture
- Create polaroid UI control
- Assign rendered texture to polaroid
- Move polaroid down through the print-out

### Finish wagon ride
- Despawn wagon

### Deactivate wagon spawner
- Calculate score
- Show polaroids
- Apply score
- Increase difficulty

### Show game over screen
- Show all polaroids
- Show score
- Show dialog to save score

### Submit dialog
- Restart game