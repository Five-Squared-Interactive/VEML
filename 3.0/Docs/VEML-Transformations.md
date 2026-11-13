# VEML Transformations

This guide provides detailed information about VEML's transformation system, which controls how entities are positioned, oriented, and sized in 3D space.

## Understanding Transformations

Every entity in VEML must have a `<transform>` element that defines its spatial properties. The transformation system is hierarchical, meaning child entities inherit transformations from their parents.

## Transform Types

VEML supports three types of transformations, determined by the child elements used:

### 1. Scale Transform (Most Common)

Used for most 3D objects where you want to scale from the original size.

```xml
<transform>
    <position x="0" y="1" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <scale x="1" y="1" z="1" />
</transform>
```

**When to use:**
- Most 3D objects and entities
- When you want proportional scaling
- When working with standard mesh entities

### 2. Size Transform

Used when you want to specify absolute dimensions rather than scale factors.

```xml
<transform>
    <position x="0" y="1" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <size x="2" y="1" z="1" />
</transform>
```

**When to use:**
- When you need exact dimensions
- For architectural elements (walls, floors)
- When dimensions are more meaningful than scale factors

### 3. Canvas Transform

Used for 2D UI elements positioned as percentages of their parent.

```xml
<transform>
    <position-percent x="0.5" y="0.5" />
    <size-percent x="0.2" y="0.1" />
</transform>
```

**When to use:**
- UI elements and overlays
- Canvas entities
- 2D layouts that should adapt to screen size

## Position

The `<position>` element defines where an entity is located in 3D space.

### Coordinate System

VEML uses a **right-handed coordinate system**:

```
     +Y (Up)
     |
     |
     |______ +X (Right)
    /
   /
  +Z (Forward/Toward viewer)
```

### Attributes

- **x**: Horizontal position (left ← → right)
- **y**: Vertical position (down ← → up)
- **z**: Depth position (back ← → forward)
- **Default**: All values default to 0

### Practical Examples

```xml
<!-- Object at world origin -->
<position x="0" y="0" z="0" />

<!-- Object 2 units to the right, 1 unit up -->
<position x="2" y="1" z="0" />

<!-- Object 3 units forward (toward viewer) -->
<position x="0" y="0" z="3" />

<!-- Object behind the origin -->
<position x="0" y="0" z="-5" />
```

### Common Positioning Patterns

#### Ground Level Objects
Place the Y coordinate so the bottom of the object sits on the ground:

```xml
<!-- Cube sitting on ground (assuming ground is at Y=0) -->
<entity tag="ground-cube">
    <transform>
        <position x="0" y="0.5" z="0" />  <!-- Half the cube's height -->
        <scale x="1" y="1" z="1" />
    </transform>
    <entity>
        <cubemeshentity><color>brown</color></cubemeshentity>
    </entity>
</entity>
```

#### Table Height Objects
Common table height is around 0.75 units:

```xml
<!-- Object on a table -->
<entity tag="table-object">
    <transform>
        <position x="0" y="0.75" z="0" />
    </transform>
</entity>
```

#### Eye Level Objects
For human-scale environments, eye level is typically 1.6-1.8 units:

```xml
<!-- Sign at eye level -->
<entity tag="eye-level-sign">
    <transform>
        <position x="0" y="1.7" z="0" />
    </transform>
</entity>
```

## Rotation

The `<rotation>` element defines how an entity is oriented in 3D space using quaternions.

### Quaternion Components

- **x, y, z**: The rotation axis components
- **w**: The rotation amount/angle component
- **Default**: (0, 0, 0, 1) represents no rotation

### Understanding Quaternions

Quaternions are a mathematical way to represent 3D rotations that avoid problems like gimbal lock. While the math is complex, here are common useful values:

#### Common Rotation Values

```xml
<!-- No rotation (default) -->
<rotation x="0" y="0" z="0" w="1" />

<!-- 90° rotation around Y axis (turning left) -->
<rotation x="0" y="0.707" z="0" w="0.707" />

<!-- 180° rotation around Y axis (turning around) -->
<rotation x="0" y="1" z="0" w="0" />

<!-- 270° rotation around Y axis (turning right) -->
<rotation x="0" y="-0.707" z="0" w="0.707" />

<!-- 90° rotation around X axis (tilting up) -->
<rotation x="0.707" y="0" z="0" w="0.707" />

<!-- 90° rotation around Z axis (rolling) -->
<rotation x="0" y="0" z="0.707" w="0.707" />
```

#### Practical Rotation Examples

```xml
<!-- Picture frame facing the viewer -->
<entity tag="picture-frame">
    <transform>
        <position x="0" y="1.5" z="2" />
        <rotation x="0" y="1" z="0" w="0" />  <!-- 180° turn to face viewer -->
    </transform>
</entity>

<!-- Door slightly ajar -->
<entity tag="door">
    <transform>
        <position x="2" y="0" z="0" />
        <rotation x="0" y="0.259" z="0" w="0.966" />  <!-- ~30° open -->
    </transform>
</entity>

<!-- Text tilted for artistic effect -->
<entity tag="tilted-text">
    <transform>
        <position x="0" y="2" z="0" />
        <rotation x="0" y="0" z="0.259" w="0.966" />  <!-- 30° roll -->
    </transform>
</entity>
```

### Converting from Euler Angles

If you're more comfortable with Euler angles (pitch, yaw, roll), you can convert using online calculators or 3D software, then use the resulting quaternion values.

## Scale and Size

### Scale Transform

Scale multiplies the original size of an entity:

```xml
<scale x="1" y="1" z="1" />      <!-- Original size -->
<scale x="2" y="2" z="2" />      <!-- Double size -->
<scale x="0.5" y="0.5" z="0.5" />   <!-- Half size -->
<scale x="1" y="2" z="1" />      <!-- Stretch vertically -->
```

#### Scale Examples

```xml
<!-- Make a cube twice as wide but same height -->
<entity tag="wide-cube">
    <transform>
        <position x="0" y="0.5" z="0" />
        <scale x="2" y="1" z="1" />
    </transform>
    <entity>
        <cubemeshentity><color>blue</color></cubemeshentity>
    </entity>
</entity>

<!-- Create a thin wall -->
<entity tag="wall">
    <transform>
        <position x="0" y="1.5" z="0" />
        <scale x="5" y="3" z="0.1" />  <!-- Wide, tall, thin -->
    </transform>
    <entity>
        <cubemeshentity><color>white</color></cubemeshentity>
    </entity>
</entity>
```

### Size Transform

Size specifies absolute dimensions in world units:

```xml
<size x="2" y="1" z="1" />  <!-- 2 units wide, 1 unit tall, 1 unit deep -->
```

#### Size vs Scale

```xml
<!-- These produce the same result for a unit cube -->
<scale x="2" y="1" z="1" />  <!-- Scale: 2x width, 1x height, 1x depth -->
<size x="2" y="1" z="1" />   <!-- Size: 2 units wide, 1 unit tall, 1 unit deep -->

<!-- But for a model that's originally 0.5 units wide: -->
<scale x="2" y="1" z="1" />  <!-- Result: 1 unit wide (0.5 * 2) -->
<size x="2" y="1" z="1" />   <!-- Result: 2 units wide (absolute) -->
```

## Hierarchical Transformations

Child entities inherit transformations from their parents. The child's transform is applied relative to the parent's transformed coordinate system.

### Basic Inheritance

```xml
<entity tag="table">
    <transform>
        <position x="5" y="0" z="0" />  <!-- Table at (5, 0, 0) -->
        <rotation x="0" y="0.707" z="0" w="0.707" />  <!-- Table rotated 90° -->
    </transform>
    
    <entity tag="cup-on-table">
        <transform>
            <position x="0" y="0.8" z="0.5" />  <!-- Cup relative to table -->
        </transform>
        <!-- Cup's final world position: table position + rotated cup position -->
    </entity>
</entity>
```

### Nested Hierarchies

```xml
<entity tag="room">
    <transform>
        <position x="0" y="0" z="0" />
    </transform>
    
    <entity tag="furniture-group">
        <transform>
            <position x="2" y="0" z="0" />  <!-- Offset furniture group -->
        </transform>
        
        <entity tag="desk">
            <transform>
                <position x="0" y="0" z="0" />  <!-- Desk relative to furniture group -->
            </transform>
            
            <entity tag="monitor">
                <transform>
                    <position x="0" y="0.8" z="0" />  <!-- Monitor relative to desk -->
                </transform>
                <!-- Monitor's final position: room + furniture-group + desk + monitor -->
            </entity>
        </entity>
    </entity>
</entity>
```

## Canvas Transformations (2D UI)

Canvas transformations use percentage positioning for 2D user interface elements.

### Position Percent

- **x, y**: Values from 0.0 to 1.0
- **0.0**: Left/bottom edge
- **0.5**: Center
- **1.0**: Right/top edge

### Size Percent

- **x, y**: Values from 0.0 to 1.0 representing portion of parent
- **0.1**: 10% of parent size
- **0.5**: 50% of parent size
- **1.0**: 100% of parent size (full size)

### Canvas Examples

```xml
<!-- Centered button taking 20% width, 10% height -->
<entity tag="center-button">
    <transform>
        <position-percent x="0.5" y="0.5" />
        <size-percent x="0.2" y="0.1" />
    </transform>
    <entity>
        <buttonentity on-click-event="centerButtonClick">
            <!-- Button content -->
        </buttonentity>
    </entity>
</entity>

<!-- Top-left corner info panel -->
<entity tag="info-panel">
    <transform>
        <position-percent x="0.05" y="0.95" />  <!-- 5% from left, 5% from top -->
        <size-percent x="0.3" y="0.4" />        <!-- 30% width, 40% height -->
    </transform>
</entity>

<!-- Bottom navigation bar -->
<entity tag="nav-bar">
    <transform>
        <position-percent x="0" y="0" />
        <size-percent x="1.0" y="0.1" />  <!-- Full width, 10% height -->
    </transform>
</entity>
```

## Advanced Transformation Techniques

### Creating Symmetrical Layouts

```xml
<!-- Left and right symmetrical objects -->
<entity tag="left-pillar">
    <transform>
        <position x="-3" y="1.5" z="0" />
    </transform>
</entity>

<entity tag="right-pillar">
    <transform>
        <position x="3" y="1.5" z="0" />   <!-- Mirrors the left pillar -->
    </transform>
</entity>
```

### Grid Layouts

```xml
<!-- 3x3 grid of objects -->
<entity tag="grid-objects">
    <transform>
        <position x="0" y="0" z="0" />
    </transform>
    
    <!-- Row 1 -->
    <entity tag="grid-1-1">
        <transform><position x="-2" y="0" z="-2" /></transform>
    </entity>
    <entity tag="grid-1-2">
        <transform><position x="0" y="0" z="-2" /></transform>
    </entity>
    <entity tag="grid-1-3">
        <transform><position x="2" y="0" z="-2" /></transform>
    </entity>
    
    <!-- Row 2 -->
    <entity tag="grid-2-1">
        <transform><position x="-2" y="0" z="0" /></transform>
    </entity>
    <entity tag="grid-2-2">
        <transform><position x="0" y="0" z="0" /></transform>
    </entity>
    <entity tag="grid-2-3">
        <transform><position x="2" y="0" z="0" /></transform>
    </entity>
    
    <!-- Row 3 -->
    <entity tag="grid-3-1">
        <transform><position x="-2" y="0" z="2" /></transform>
    </entity>
    <entity tag="grid-3-2">
        <transform><position x="0" y="0" z="2" /></transform>
    </entity>
    <entity tag="grid-3-3">
        <transform><position x="2" y="0" z="2" /></transform>
    </entity>
</entity>
```

### Circular Arrangements

```xml
<!-- Objects arranged in a circle -->
<entity tag="circular-arrangement">
    <transform>
        <position x="0" y="0" z="0" />
    </transform>
    
    <!-- 8 objects around a circle, each rotated 45° from the previous -->
    <entity tag="circle-object-1">
        <transform>
            <position x="3" y="0" z="0" />  <!-- Radius of 3 units -->
            <rotation x="0" y="0" z="0" w="1" />  <!-- 0° -->
        </transform>
    </entity>
    
    <entity tag="circle-object-2">
        <transform>
            <position x="2.12" y="0" z="2.12" />  <!-- 45° position -->
            <rotation x="0" y="0.383" z="0" w="0.924" />  <!-- 45° rotation -->
        </transform>
    </entity>
    
    <!-- Continue for remaining objects... -->
</entity>
```

## Common Transformation Patterns

### Architectural Elements

```xml
<!-- Wall aligned along X-axis -->
<entity tag="wall-x">
    <transform>
        <position x="0" y="1.5" z="0" />
        <scale x="10" y="3" z="0.2" />  <!-- Long, tall, thin -->
    </transform>
</entity>

<!-- Wall aligned along Z-axis -->
<entity tag="wall-z">
    <transform>
        <position x="0" y="1.5" z="0" />
        <rotation x="0" y="0.707" z="0" w="0.707" />  <!-- 90° turn -->
        <scale x="10" y="3" z="0.2" />
    </transform>
</entity>

<!-- Floor tile -->
<entity tag="floor">
    <transform>
        <position x="0" y="0" z="0" />
        <scale x="10" y="0.1" z="10" />  <!-- Wide, thin, deep -->
    </transform>
</entity>
```

### Furniture Arrangements

```xml
<!-- Conference table with chairs -->
<entity tag="conference-setup">
    <transform>
        <position x="0" y="0" z="0" />
    </transform>
    
    <!-- Central table -->
    <entity tag="table">
        <transform>
            <position x="0" y="0.75" z="0" />
            <scale x="3" y="0.1" z="1.5" />
        </transform>
    </entity>
    
    <!-- Chairs around table -->
    <entity tag="chair-head">
        <transform>
            <position x="0" y="0" z="1" />  <!-- At head of table -->
        </transform>
    </entity>
    
    <entity tag="chair-foot">
        <transform>
            <position x="0" y="0" z="-1" />  <!-- At foot of table -->
        </transform>
    </entity>
    
    <entity tag="chair-left">
        <transform>
            <position x="-1.5" y="0" z="0" />  <!-- Left side -->
            <rotation x="0" y="0.707" z="0" w="0.707" />  <!-- Face table -->
        </transform>
    </entity>
    
    <entity tag="chair-right">
        <transform>
            <position x="1.5" y="0" z="0" />  <!-- Right side -->
            <rotation x="0" y="-0.707" z="0" w="0.707" />  <!-- Face table -->
        </transform>
    </entity>
</entity>
```

## Troubleshooting Transformations

### Common Issues

1. **Objects appearing at wrong locations**
   - Check parent entity transformations
   - Verify coordinate system understanding
   - Ensure all transforms are properly nested

2. **Objects appearing with wrong orientation**
   - Double-check quaternion values
   - Consider using common rotation values as starting points
   - Test with simple rotations first

3. **Objects appearing with wrong size**
   - Verify scale vs size usage
   - Check if parent entity affects sizing
   - Ensure original model/primitive size is as expected

4. **UI elements not appearing correctly**
   - Verify using canvas transform for UI elements
   - Check percentage values are between 0 and 1
   - Ensure parent entity supports canvas transforms

### Debugging Tips

1. **Start simple**: Begin with basic positions and default rotations
2. **Use round numbers**: Start with positions like 0, 1, 2, 5, 10
3. **Test incrementally**: Add one transformation at a time
4. **Use visual references**: Place reference objects to verify positioning
5. **Check the hierarchy**: Ensure entity nesting matches your intentions

Understanding VEML's transformation system is key to creating well-positioned, properly oriented virtual environments. Start with simple transformations and gradually build up to more complex arrangements as you become comfortable with the coordinate system and transformation inheritance.