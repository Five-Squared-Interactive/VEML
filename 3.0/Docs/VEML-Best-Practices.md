# VEML Best Practices

This guide provides recommendations and best practices for creating effective, maintainable, and performant VEML virtual environments.

## File Organization and Structure

### Naming Conventions

#### File Names
- Use descriptive, lowercase names with hyphens: `office-environment.veml`
- Include version numbers for iterations: `museum-v2.veml`
- Keep names under 50 characters for compatibility

#### Entity Tags and IDs
- **Tags**: Use descriptive, hyphenated names: `conference-table`, `main-entrance`
- **IDs**: Use systematic naming: `chair-001`, `light-ceiling-01`
- Be consistent across your project
- Avoid special characters and spaces

```xml
<!-- Good -->
<entity tag="office-desk" id="desk-001">

<!-- Avoid -->
<entity tag="Desk_in_office!" id="DeskEntity1">
```

#### Resource Paths
- Use relative paths from the VEML file location
- Organize resources in logical folders:
  ```
  project/
  ├── environments/
  │   └── office.veml
  ├── models/
  │   ├── furniture/
  │   └── characters/
  ├── textures/
  ├── audio/
  └── scripts/
  ```

### Hierarchical Organization

#### Scene Graph Structure
Organize your scene hierarchically with clear groupings:

```xml
<environment>
    <background>
        <!-- Background settings -->
    </background>
    
    <!-- Static Environment -->
    <entity tag="static-environment">
        <entity tag="architecture">
            <entity tag="walls">
                <!-- Wall entities -->
            </entity>
            <entity tag="floors">
                <!-- Floor entities -->
            </entity>
            <entity tag="ceilings">
                <!-- Ceiling entities -->
            </entity>
        </entity>
        
        <entity tag="furniture">
            <entity tag="seating">
                <!-- Chairs, sofas, etc. -->
            </entity>
            <entity tag="tables">
                <!-- Tables, desks, etc. -->
            </entity>
            <entity tag="storage">
                <!-- Shelves, cabinets, etc. -->
            </entity>
        </entity>
    </entity>
    
    <!-- Dynamic Elements -->
    <entity tag="dynamic-elements">
        <entity tag="interactive-objects">
            <!-- Buttons, controls, etc. -->
        </entity>
        <entity tag="moveable-objects">
            <!-- Objects that can be moved -->
        </entity>
    </entity>
    
    <!-- Lighting -->
    <entity tag="lighting">
        <entity tag="ambient-lights">
            <!-- General lighting -->
        </entity>
        <entity tag="task-lights">
            <!-- Specific task lighting -->
        </entity>
    </entity>
    
    <!-- User Interface -->
    <entity tag="ui-elements">
        <!-- Interface elements -->
    </entity>
</environment>
```

#### Logical Grouping Principles
1. **Function-based**: Group by what objects do (lighting, furniture, etc.)
2. **Spatial**: Group by location (room1, room2, outdoor-area)
3. **Lifecycle**: Group by when objects appear (startup, level2, etc.)
4. **Ownership**: Group by who controls them (player-objects, npc-objects)

## Performance Optimization

### Entity Management

#### Minimize Entity Count
- Combine simple geometric shapes when possible
- Use single meshes instead of multiple primitive shapes for complex objects
- Consider baking multiple objects into single meshes

```xml
<!-- Less efficient: Multiple entities for a simple desk -->
<entity tag="desk-top">
    <cubemeshentity><color>#8B4513</color></cubemeshentity>
</entity>
<entity tag="desk-leg1">
    <cubemeshentity><color>#654321</color></cubemeshentity>
</entity>
<!-- ... more legs -->

<!-- More efficient: Single mesh entity -->
<entity tag="desk">
    <meshentity>
        <mesh-name>office-desk</mesh-name>
        <mesh-resource>models/desk.fbx</mesh-resource>
    </meshentity>
</entity>
```

#### Hierarchy Depth
- Keep hierarchy depth reasonable (generally < 8 levels)
- Flatten hierarchies when logical grouping isn't needed
- Balance organization with performance

### Resource Optimization

#### Texture and Model Reuse
- Reuse textures and models across multiple entities
- Create atlases for small textures
- Use texture compression when available

```xml
<!-- Reuse the same chair model -->
<entity tag="chair-1">
    <transform><position x="1" y="0" z="0" /></transform>
    <entity>
        <meshentity>
            <mesh-name>office-chair</mesh-name>
            <mesh-resource>models/chair.fbx</mesh-resource>
        </meshentity>
    </entity>
</entity>

<entity tag="chair-2">
    <transform><position x="3" y="0" z="0" /></transform>
    <entity>
        <meshentity>
            <mesh-name>office-chair</mesh-name>
            <mesh-resource>models/chair.fbx</mesh-resource>
        </meshentity>
    </entity>
</entity>
```

#### File Format Selection
- **Models**: Use FBX for complex models, OBJ for simple geometry
- **Textures**: Use JPEG for photos, PNG for graphics with transparency
- **Audio**: Use OGG for music, WAV for short sound effects

## Coordinate System and Positioning

### Consistent Units
- Establish a unit scale (e.g., 1 unit = 1 meter)
- Document your scale choice
- Be consistent throughout your project

### Reference Points
- Use world origin (0,0,0) as a meaningful reference point
- Consider placing the "ground level" at Y=0
- Position major elements at round numbers when possible

```xml
<!-- Good: Clear, round coordinates -->
<entity tag="main-table">
    <transform>
        <position x="0" y="0.75" z="0" />  <!-- Center of room, table height -->
    </transform>
</entity>

<!-- Avoid: Arbitrary precision -->
<entity tag="side-table">
    <transform>
        <position x="2.3847" y="0.7491" z="-1.8832" />
    </transform>
</entity>
```

### Default Values
- Use default transform values when appropriate:
  - Position: (0, 0, 0)
  - Rotation: (0, 0, 0, 1)
  - Scale: (1, 1, 1)

```xml
<!-- Explicit (verbose) -->
<transform>
    <position x="0" y="0" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <scale x="1" y="1" z="1" />
</transform>

<!-- Using defaults (cleaner) -->
<transform>
    <position x="0" y="0" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <scale x="1" y="1" z="1" />
</transform>
```

## Color and Visual Design

### Color Specification
- Use consistent color formats within a project
- Prefer hex colors for exact color matching
- Use named colors for common colors in prototypes

```xml
<!-- Good: Consistent hex colors -->
<cubemeshentity><color>#3498DB</color></cubemeshentity>
<spheremeshentity><color>#E74C3C</color></spheremeshentity>

<!-- Good: Named colors for prototyping -->
<cubemeshentity><color>blue</color></cubemeshentity>
<spheremeshentity><color>red</color></spheremeshentity>
```

### Visual Hierarchy
- Use size, color, and position to create visual hierarchy
- Make important interactive elements prominent
- Use consistent styling for similar objects

## Interaction Design

### Event Naming
- Use descriptive event names: `openDoor`, `playMusic`, `showHelp`
- Follow a consistent naming pattern (camelCase or snake_case)
- Group related events with prefixes: `ui_showMenu`, `ui_hideMenu`

```xml
<buttonentity on-click-event="toggleMainLight">
<buttonentity on-click-event="showHelpDialog">
<inputentity on-change-event="updatePlayerName">
```

### User Experience
- Provide visual feedback for interactive elements
- Use consistent interaction patterns
- Consider accessibility (font sizes, color contrast, etc.)

### VR Considerations
- Design for comfortable VR viewing distances (1-10 meters)
- Avoid small UI elements that are hard to interact with
- Consider both seated and standing VR experiences

```xml
<!-- VR-friendly text size -->
<textentity text="Welcome!" font-size="0.2">

<!-- VR-friendly button size -->
<entity tag="vr-button">
    <transform>
        <scale x="0.5" y="0.2" z="0.1" />  <!-- Large enough to target -->
    </transform>
</entity>
```

### Enhanced UI Styling and Layout
The latest VEML version includes enhanced styling and alignment options for UI elements:

#### Text Entity Enhancements
```xml
<!-- Rich text with styling -->
<textentity text="Styled Header" font-size="0.15" fonts="Arial" 
           bold="true" text-align-horizontal="center" 
           align-horizontal="center" align-vertical="top">
    <color>#2E86C1</color>
</textentity>

<!-- Wrapped text with custom alignment -->
<textentity text="This is a longer text that will wrap nicely" 
           font-size="0.08" italic="true" text-wrap="wrap"
           text-align-horizontal="left" stretch-to-parent="true">
    <color>darkgreen</color>
</textentity>
```

#### Button Entity Enhancements
```xml
<!-- Colored button with center alignment -->
<buttonentity on-click-event="handleClick" align-horizontal="center">
    <color>#E74C3C</color>
    <entity>
        <textentity text="Click Me!" font-size="0.1" bold="true">
            <color>white</color>
        </textentity>
    </entity>
</buttonentity>
```

#### Best Practices for Enhanced UI
- Use `align-horizontal` and `align-vertical` for consistent layouts
- Set `stretch-to-parent="true"` for responsive design
- Combine text styling flags (`bold`, `italic`, `underline`) judiciously
- Use `text-wrap="wrap"` for longer text content
- Choose appropriate `fonts` for your target platform

## Scripting Integration

### Script Organization
- Keep scripts focused and modular
- Use descriptive function names that match VEML event names
- Comment your script files thoroughly

```javascript
// main.js - Main environment controller
function toggleMainLight() {
    // Toggle the main room light
}

function openDoor() {
    // Handle door opening animation
}

function showHelpDialog() {
    // Display help information
}
```

### Event Handling
- Create a consistent event handling pattern
- Handle error cases gracefully
- Provide user feedback for actions

## Multi-User Environments

### Synchronization Strategy
- Only synchronize entities that need to be shared
- Use appropriate synchronizer names
- Consider network bandwidth in your design

```xml
<!-- Synchronize player avatars -->
<entity tag="player-avatar" synchronizer="player-sync">

<!-- Don't synchronize static environment -->
<entity tag="static-wall">
    <!-- No synchronizer needed -->
</entity>
```

### User Identification
- Use meaningful session and ID values
- Plan for user authentication and permissions
- Consider privacy implications

## Documentation and Maintenance

### Commenting
Use XML comments to document complex sections:

```xml
<!-- Main conference room area with seating for 12 people -->
<entity tag="conference-area">
    <!-- 
        Conference table dimensions: 6m x 3m
        Designed for VR users with comfortable reach distances
    -->
    <entity tag="conference-table">
        <!-- Table implementation -->
    </entity>
</entity>
```

### Version Control
- Use meaningful commit messages
- Tag stable versions
- Document breaking changes
- Consider binary file management for assets

### Testing Checklist
Before releasing your VEML environment:

1. **Validation**
   - [ ] VEML validates against XSD schema
   - [ ] All referenced files exist
   - [ ] No broken links or missing resources

2. **Visual Quality**
   - [ ] Objects appear in correct positions
   - [ ] Colors and materials look appropriate
   - [ ] Text is readable at intended viewing distances
   - [ ] No visual glitches or clipping

3. **Interaction**
   - [ ] All buttons and interactive elements work
   - [ ] Events trigger appropriate responses
   - [ ] User feedback is clear and immediate

4. **Performance**
   - [ ] Environment loads in reasonable time
   - [ ] Frame rate is acceptable on target hardware
   - [ ] Memory usage is within limits

5. **Multi-User (if applicable)**
   - [ ] Multiple users can join successfully
   - [ ] Synchronization works correctly
   - [ ] Network performance is acceptable

## Common Pitfalls to Avoid

### Structural Issues
- **Deep hierarchies**: Don't nest entities more than necessary
- **Missing transforms**: Every entity needs a transform
- **Inconsistent naming**: Stick to your naming conventions

### Performance Problems
- **Too many entities**: Combine simple shapes into complex meshes
- **Large textures**: Use appropriate resolution for viewing distance
- **Unnecessary synchronization**: Don't sync static objects

### User Experience Issues
- **Tiny UI elements**: Make buttons and text appropriately sized
- **No feedback**: Provide visual/audio feedback for interactions
- **Inconsistent interactions**: Use the same patterns throughout

### Technical Problems
- **Invalid XML**: Always validate against the schema
- **Missing resources**: Check all file paths and references
- **Hardcoded paths**: Use relative paths for portability

Following these best practices will help you create VEML environments that are performant, maintainable, and provide excellent user experiences across different platforms and use cases.