# Notes
Collection of learnings from doing this jam

- Z axis is your steering wheel axis
- When using `FixedUpdate` make sure you use `fixedDeltaTime`


### Player controller

- Generally good to do input in Update and then physics in FixedUpdate


## 2D Collisions

- Collision Detection:
    - For 2D Platforming, should always be setting collision detection to `continuous`. It's less performant but more accurate
    - Should also be setting Interpolate to `Interpolate` - I _think_ this means the physics will estimate future interactions between frames so that a collision doesn't get skipped. TODO: learn more about interpolation