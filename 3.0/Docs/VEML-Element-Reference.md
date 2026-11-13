# VEML Element Reference

This document provides a comprehensive reference for all VEML elements, organized alphabetically. Each entry includes the element's purpose, required and optional child elements, attributes, and usage examples.

## Root Elements

### `<veml>`
The root element of every VEML document.

**Namespace:** `http://www.fivesqd.com/schemas/veml/2.3`

**Required Child Elements:**
- `<metadata>` - Document metadata and configuration
- `<environment>` - Scene content and objects

**Example:**
```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>My Virtual Environment</title>
    </metadata>
    <environment>
        <!-- Scene content -->
    </environment>
</veml>
```

## Metadata Elements

### `<metadata>`
Contains document information and global settings.

**Required Child Elements:**
- `<title>` - Environment title

**Optional Child Elements:**
- `<capability>` (0..∞) - Feature capabilities
- `<script>` (0..∞) - Script file references
- `<inputevent>` (0..∞) - Input event mappings
- `<controlflags>` (0..1) - Control settings
- `<synchronizationservice>` (0..∞) - Network synchronization

### `<title>`
A human-readable title for the virtual environment.

**Content:** String text

**Example:**
```xml
<title>My First VEML Scene</title>
```

### `<capability>`
Declares a feature capability required by the environment.

**Content:** String capability name

**Common Values:**
- `physics` - Physics simulation
- `vr` - Virtual reality support
- `audio` - Audio support
- `networking` - Multi-user networking
- `terrain` - Terrain rendering
- `water` - Water effects

**Example:**
```xml
<capability>physics</capability>
<capability>vr</capability>
```

### `<script>`
References an external JavaScript file.

**Content:** String path to script file

**Example:**
```xml
<script>js/main.js</script>
<script>js/physics.js</script>
```

### `<inputevent>`
Maps input to event names for script handling.

**Attributes:**
- `input` (optional) - Input specification (e.g., "key:space", "controller:trigger")
- `event` (optional) - Event name to trigger

**Example:**
```xml
<inputevent input="key:space" event="jump" />
<inputevent input="controller:trigger" event="grab" />
```

### `<controlflags>`
Configures input controls and interaction methods.

**Optional Child Elements:**
- `<left-vr-pointer>` - Left VR controller pointer type
- `<right-vr-pointer>` - Right VR controller pointer type
- `<left-vr-poker>` - Enable left controller poking
- `<right-vr-poker>` - Enable right controller poking
- `<left-hand-interaction>` - Enable left hand interaction
- `<right-hand-interaction>` - Enable right hand interaction
- `<turn-locomotion>` - Turn locomotion mode
- `<joystick-motion>` - Enable joystick motion
- `<left-grab-move>` - Enable left grab movement
- `<right-grab-move>` - Enable right grab movement
- `<two-handed-grab-move>` - Enable two-handed grab movement

**Example:**
```xml
<controlflags>
    <left-vr-pointer>ray</left-vr-pointer>
    <right-vr-pointer>ray</right-vr-pointer>
    <joystick-motion>true</joystick-motion>
    <turn-locomotion>snap</turn-locomotion>
</controlflags>
```

### `<synchronizationservice>`
Configures multi-user synchronization.

**Attributes:**
- `type` (optional) - Service type (e.g., "websocket")
- `address` (optional) - Service address/URL
- `id` (optional) - Service identifier
- `session` (optional) - Session identifier

**Example:**
```xml
<synchronizationservice type="websocket" address="wss://server.com" id="room1" session="meeting" />
```

## Environment Elements

### `<environment>`
Contains all scene content.

**Required Child Elements:**
- `<background>` - Background/sky settings

**Optional Child Elements:**
- `<effects>` (0..1) - Visual effects
- `<entity>` (0..∞) - Scene entities

### `<background>`
Defines the environment background.

**Choice of Child Elements (exactly one):**
- `<panorama>` - Panoramic image background
- `<color>` - Solid color background
- `<lite-procedural-sky>` - Procedural sky

**Example:**
```xml
<background>
    <color>#87CEEB</color>
</background>
```

### `<panorama>`
360-degree panoramic image background.

**Content:** String path to panoramic image

**Example:**
```xml
<background>
    <panorama>textures/sky_panorama.jpg</panorama>
</background>
```

### `<color>`
Solid color background.

**Content:** Color string (hex, named color, or color() format)

**Example:**
```xml
<background>
    <color>#87CEEB</color>
</background>
```

### `<lite-procedural-sky>`
Advanced procedural sky with atmospheric effects.

**Attributes (all optional with defaults):**
- `sun-entity-tag` - Entity tag to use for sun position
- `day-night-cycle-enabled` (default: true) - Enable day/night cycle
- `ground-enabled` (default: true) - Show procedural ground
- `ground-color` (default: "#07070700") - Ground color
- `ground-height` (default: -0.02) - Ground height
- `sun-enabled` (default: true) - Show sun
- `sun-diameter` (default: 2) - Sun size
- `clouds-enabled` (default: true) - Show clouds
- `cloudiness` (default: 0.279) - Cloud density
- `stars-enabled` (default: true) - Show stars
- `moon-enabled` (default: true) - Show moon
- And many more atmospheric parameters...

**Example:**
```xml
<background>
    <lite-procedural-sky 
        sun-enabled="true" 
        clouds-enabled="true" 
        cloudiness="0.5"
        day-sky-color="#87CEEB" 
    />
</background>
```

### `<effects>`
Visual effects and atmospheric settings.

**Optional Child Elements:**
- `<lite-fog>` - Fog effects

### `<lite-fog>`
Fog atmospheric effect.

**Attributes:**
- `enabled` (default: false) - Enable fog
- `color` (default: "gray") - Fog color
- `density` (default: 0.5) - Fog density

**Example:**
```xml
<effects>
    <lite-fog enabled="true" color="white" density="0.1" />
</effects>
```

## Entity Elements

### `<entity>`
A container for objects in the scene. Entities can contain other entities, creating hierarchies.

**Required Child Elements:**
- `<transform>` - Position, rotation, and scale

**Optional Child Elements:**
- `<entity>` (0..∞) - Child entities
- `<synchronizer>` (0..1) - Network synchronization
- `<placement-socket>` (0..∞) - Attachment points

**Entity Type Child Elements (0..1):**
Any one of the entity type elements can be a child

**Attributes:**
- `tag` (optional) - Friendly name for referencing
- `id` (optional) - Unique identifier
- `on-load-event` (optional) - Script event to fire when loaded

**Example:**
```xml
<entity tag="my-cube" id="cube-001">
    <transform>
        <position x="0" y="1" z="0" />
        <rotation x="0" y="0" z="0" w="1" />
        <scale x="1" y="1" z="1" />
    </transform>
    <entity>
        <cubemeshentity>
            <color>red</color>
        </cubemeshentity>
    </entity>
</entity>
```

## Transform Elements

### `<transform>`
Defines entity position, rotation, and scale. The transform type is determined by its child elements.

**Scale Transform (most common):**
```xml
<transform>
    <position x="0" y="1" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <scale x="1" y="1" z="1" />
</transform>
```

**Size Transform:**
```xml
<transform>
    <position x="0" y="1" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <size x="2" y="1" z="1" />
</transform>
```

**Canvas Transform (for UI):**
```xml
<transform>
    <position-percent x="0.5" y="0.5" />
    <size-percent x="0.2" y="0.1" />
</transform>
```

### `<position>`
3D position coordinates.

**Attributes:**
- `x` (default: 0) - X coordinate (left/right)
- `y` (default: 0) - Y coordinate (down/up)
- `z` (default: 0) - Z coordinate (back/forward)

### `<rotation>`
3D rotation as quaternion.

**Attributes:**
- `x` (default: 0) - X component
- `y` (default: 0) - Y component
- `z` (default: 0) - Z component
- `w` (default: 1) - W component

### `<scale>`
3D scale factors.

**Attributes:**
- `x` (default: 1) - X scale
- `y` (default: 1) - Y scale
- `z` (default: 1) - Z scale

### `<size>`
3D size dimensions (alternative to scale).

**Attributes:**
- `x` (default: 1) - X size
- `y` (default: 1) - Y size
- `z` (default: 1) - Z size

### `<position-percent>`
2D position as percentage of parent (for UI elements).

**Attributes:**
- `x` (default: 0) - X percentage (0-1)
- `y` (default: 0) - Y percentage (0-1)

### `<size-percent>`
2D size as percentage of parent (for UI elements).

**Attributes:**
- `x` (default: 1) - X percentage (0-1)
- `y` (default: 1) - Y percentage (0-1)

## Common Entity Attributes

All entities support the following optional attributes for enhanced layout and alignment:

**Layout Attributes:**
- `align-horizontal` - Horizontal alignment within parent container
  - Values: `center`, `left`, `right`
- `align-vertical` - Vertical alignment within parent container  
  - Values: `center`, `top`, `bottom`
- `stretch-to-parent` - Whether to stretch to fit parent container
  - Values: `true`, `false`

**Standard Attributes:**
- `tag` - Unique identifier for scripting reference
- `id` - UUID identifier (auto-generated if not provided)
- `on-load-event` - Script event to invoke when entity loads

**Example:**
```xml
<entity tag="my-button" align-horizontal="center" align-vertical="top" stretch-to-parent="false">
    <transform>
        <position x="0" y="0" z="0" />
        <scale x="1" y="1" z="1" />
    </transform>
</entity>
```

## Mesh Entity Elements

### `<containerentity>`
A generic container entity with no visual representation.

**No child elements or attributes**

### `<cubemeshentity>`
A cube/box shape.

**Required Child Elements:**
- `<color>` - Cube color

**Example:**
```xml
<cubemeshentity>
    <color>red</color>
</cubemeshentity>
```

### `<spheremeshentity>`
A sphere shape.

**Required Child Elements:**
- `<color>` - Sphere color

### `<cylindermeshentity>`
A cylinder shape.

**Required Child Elements:**
- `<color>` - Cylinder color

### `<capsulemeshentity>`
A capsule/pill shape.

**Required Child Elements:**
- `<color>` - Capsule color

### `<planemeshentity>`
A flat plane shape.

**Required Child Elements:**
- `<color>` - Plane color

### `<conemeshentity>`
A cone shape.

**Required Child Elements:**
- `<color>` - Cone color

### `<torusmeshentity>`
A torus/donut shape.

**Required Child Elements:**
- `<color>` - Torus color

### `<tetrahedronmeshentity>`
A tetrahedron shape.

**Required Child Elements:**
- `<color>` - Tetrahedron color

### `<rectangularpyramidmeshentity>`
A rectangular pyramid shape.

**Required Child Elements:**
- `<color>` - Pyramid color

### `<prismmeshentity>`
A prism shape.

**Required Child Elements:**
- `<color>` - Prism color

### `<archmeshentity>`
An arch shape.

**Required Child Elements:**
- `<color>` - Arch color

### `<meshentity>`
A custom 3D mesh loaded from a file.

**Required Child Elements:**
- `<mesh-name>` - Mesh identifier
- `<mesh-resource>` (1..∞) - Paths to mesh files

**Example:**
```xml
<meshentity>
    <mesh-name>my-model</mesh-name>
    <mesh-resource>models/mymodel.fbx</mesh-resource>
    <mesh-resource>textures/mymodel_diffuse.jpg</mesh-resource>
</meshentity>
```

## Media Entity Elements

### `<audioentity>`
An audio source.

**Required Child Elements:**
- `<audio-file>` - Path to audio file
- `<auto-play>` - Auto-play on load (true/false)
- `<loop>` - Loop playback (true/false)
- `<priority>` - Playback priority (integer)
- `<volume>` - Volume level (0.0-1.0)
- `<pitch>` - Pitch multiplier (float)
- `<stereo-pan>` - Stereo panning (-1.0 to 1.0)

**Example:**
```xml
<audioentity>
    <audio-file>sounds/music.ogg</audio-file>
    <auto-play>true</auto-play>
    <loop>true</loop>
    <priority>1</priority>
    <volume>0.8</volume>
    <pitch>1.0</pitch>
    <stereo-pan>0.0</stereo-pan>
</audioentity>
```

### `<imageentity>`
An image display.

**Required Child Elements:**
- `<image-file>` - Path to image file

**Example:**
```xml
<imageentity>
    <image-file>images/picture.jpg</image-file>
</imageentity>
```

### `<textentity>`
Text display with enhanced styling and alignment options.

**Elements:**
- `<color>` (optional) - Text color

**Attributes:**
- `text` (required) - Text content to display
- `font-size` (required) - Font size
- `fonts` (optional) - Font family name
- `bold` (optional) - Whether text should be bold (true/false)
- `italic` (optional) - Whether text should be italic (true/false)
- `underline` (optional) - Whether text should be underlined (true/false)
- `strikethrough` (optional) - Whether text should have strikethrough (true/false)
- `text-align-horizontal` (optional) - Horizontal text alignment (center, left, right)
- `text-align-vertical` (optional) - Vertical text alignment (center, top, bottom)
- `text-wrap` (optional) - Text wrapping behavior (no-wrap, wrap)
- `align-horizontal` (optional) - Entity horizontal alignment (center, left, right)
- `align-vertical` (optional) - Entity vertical alignment (center, top, bottom)
- `stretch-to-parent` (optional) - Whether to stretch to fit parent container (true/false)

**Example:**
```xml
<textentity text="Hello World!" font-size="0.1" fonts="Arial" bold="true" 
           text-align-horizontal="center" align-horizontal="center">
    <color>red</color>
</textentity>
```

### `<htmlentity>`
HTML content display.

**Attributes:**
- `url` (optional) - URL to load
- `on-message` (optional) - Script event for messages

**Example:**
```xml
<htmlentity url="https://example.com" on-message="handleMessage">
</htmlentity>
```

## Interactive Entity Elements

### `<buttonentity>`
An interactive button with enhanced styling and alignment options.

**Elements:**
- `<color>` (optional) - Button color

**Attributes:**
- `on-click-event` (optional) - Script event when clicked
- `align-horizontal` (optional) - Entity horizontal alignment (center, left, right)
- `align-vertical` (optional) - Entity vertical alignment (center, top, bottom)
- `stretch-to-parent` (optional) - Whether to stretch to fit parent container (true/false)

**Example:**
```xml
<buttonentity on-click-event="buttonClicked" align-horizontal="center">
    <color>blue</color>
    <transform>
        <position x="0" y="0" z="0" />
        <rotation x="0" y="0" z="0" w="1" />
        <scale x="1" y="1" z="1" />
    </transform>
    <entity>
        <cubemeshentity>
            <color>lightblue</color>
        </cubemeshentity>
    </entity>
</buttonentity>
```

### `<inputentity>`
An input field for text entry.

**No specific child elements or attributes**

### `<canvasentity>`
A canvas for UI elements.

**No specific child elements or attributes**

## Specialized Entity Elements

### `<characterentity>`
A character/avatar entity.

**Optional Child Elements:**
- `<mesh-name>` (0..1) - Character mesh identifier
- `<mesh-resource>` (0..∞) - Character mesh files
- `<mesh-offset>` (0..1) - Mesh position offset
- `<mesh-rotation>` (0..1) - Mesh rotation offset
- `<label-offset>` (0..1) - Name label position offset

**Example:**
```xml
<characterentity>
    <mesh-name>avatar-1</mesh-name>
    <mesh-resource>models/avatar.fbx</mesh-resource>
    <mesh-offset x="0" y="0" z="0" />
    <mesh-rotation x="0" y="0" z="0" w="1" />
    <label-offset x="0" y="2.2" z="0" />
</characterentity>
```

### `<lightentity>`
A light source.

**No specific child elements or attributes**

### `<terrainentity>`
A terrain system with height maps and texture layers.

**Required Child Elements:**
- `<layer>` (1..∞) - Terrain texture layers
- `<layer-masks>` - Layer mask texture

**Required Attributes:**
- `length` - Terrain length
- `width` - Terrain width
- `height` - Terrain height

**Optional Attributes:**
- `heights` - Height map data
- `type` (default: "heightmap") - Terrain type

**Example:**
```xml
<terrainentity length="50" width="50" height="5">
    <layer>
        <diffuse-texture>textures/grass.jpg</diffuse-texture>
        <normal-texture>textures/grass_normal.jpg</normal-texture>
        <mask-texture>textures/grass_mask.jpg</mask-texture>
        <specular>textures/grass_spec.jpg</specular>
        <metallic>0.0</metallic>
        <smoothness>0.2</smoothness>
    </layer>
    <layer-masks>textures/terrain_masks.png</layer-masks>
</terrainentity>
```

### `<waterentity>`
A water surface with realistic water effects.

**Required Child Elements:**
- `<shallow-color>` - Water color in shallow areas
- `<deep-color>` - Water color in deep areas
- `<specular-color>` - Specular reflection color
- `<scattering-color>` - Light scattering color
- `<deep-start>` - Depth where deep color starts
- `<deep-end>` - Depth where deep color is fully applied
- `<distortion>` - Water surface distortion
- `<smoothness>` - Surface smoothness
- `<num-waves>` - Number of wave patterns
- `<wave-amplitude>` - Wave height
- `<wave-steepness>` - Wave steepness
- `<wave-speed>` - Wave animation speed
- `<wave-length>` - Wave length
- `<wave-scale>` - Wave scale
- `<wave-intensity>` - Wave intensity

**Example:**
```xml
<waterentity>
    <shallow-color>#40E0D0</shallow-color>
    <deep-color>#006994</deep-color>
    <specular-color>#FFFFFF</specular-color>
    <scattering-color>#40E0D0</scattering-color>
    <deep-start>1.0</deep-start>
    <deep-end>3.0</deep-end>
    <distortion>0.1</distortion>
    <smoothness>0.9</smoothness>
    <num-waves>3.0</num-waves>
    <wave-amplitude>0.1</wave-amplitude>
    <wave-steepness>0.3</wave-steepness>
    <wave-speed>1.0</wave-speed>
    <wave-length>2.0</wave-length>
    <wave-scale>1.0</wave-scale>
    <wave-intensity>1.0</wave-intensity>
</waterentity>
```

### `<voxelentity>`
A voxel-based entity.

**No specific child elements or attributes**

### `<waterblockerentity>`
An invisible volume that blocks water effects.

**No specific child elements or attributes**

## Utility Elements

### `<placement-socket>`
Defines an attachment point for connecting entities.

**Required Child Elements:**
- `<position>` - Socket position
- `<rotation>` - Socket orientation
- `<connecting-offset>` - Connection offset

**Example:**
```xml
<placement-socket>
    <position x="0" y="1" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <connecting-offset x="0" y="0" z="0" />
</placement-socket>
```

### `<synchronizer>`
Network synchronization identifier.

**Content:** String synchronizer name

**Example:**
```xml
<synchronizer>player-sync</synchronizer>
```

## Color Values

Colors can be specified in several formats:

- **Named colors:** `red`, `blue`, `green`, `yellow`, `white`, `black`, `gray`, etc.
- **Hex colors:** `#FF0000` (red), `#00FF00` (green), `#0000FF` (blue)
- **Hex with alpha:** `#FF0000FF` (opaque red), `#FF000080` (semi-transparent red)
- **Color function:** `color(255 0 0 255)` (red with alpha)

## Coordinate System

VEML uses a right-handed coordinate system:
- **X-axis:** Left (-) to Right (+)
- **Y-axis:** Down (-) to Up (+)
- **Z-axis:** Back (-) to Forward (+)

## File Path Conventions

- Relative paths are relative to the VEML file location
- Forward slashes `/` are preferred for cross-platform compatibility
- Common file extensions: `.fbx`, `.obj` (models), `.jpg`, `.png` (textures), `.wav`, `.ogg` (audio)

This reference covers all VEML elements defined in schema version 2.3. For practical examples of these elements in use, see the [Complete Examples](VEML-Complete-Examples.md) documentation.