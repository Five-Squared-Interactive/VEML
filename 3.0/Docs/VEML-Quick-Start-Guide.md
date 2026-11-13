# VEML Quick Start Guide

This guide will help you create your first VEML file in just a few minutes. By the end, you'll understand the basic structure and be able to create simple virtual environments.

## What You Need

- A text editor (any XML or code editor)
- Basic understanding of XML syntax
- The VEML.xsd schema file (for validation)

## Basic VEML Structure

Every VEML file has the same basic structure:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <!-- Document information and settings -->
    </metadata>
    <environment>
        <!-- Scene content and objects -->
    </environment>
</veml>
```

## Your First VEML File

Let's create a simple scene with a colored cube floating in space:

### Step 1: Create the Basic Structure

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>My First Virtual Environment</title>
    </metadata>
    <environment>
        <background>
            <color>#87CEEB</color>
        </background>
    </environment>
</veml>
```

This creates an empty environment with a light blue sky color.

### Step 2: Add Your First Entity

Now let's add a red cube to the scene:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>My First Virtual Environment</title>
    </metadata>
    <environment>
        <background>
            <color>#87CEEB</color>
        </background>
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
    </environment>
</veml>
```

### Step 3: Understanding What We Created

- **`<metadata>`**: Contains information about our virtual environment
  - **`<title>`**: A human-readable name for the environment

- **`<environment>`**: Contains all the scene content
  - **`<background>`**: Sets the environment background to light blue
  - **`<entity>`**: A container for objects in the scene
    - **`tag`**: A friendly name for referencing this entity
    - **`id`**: A unique identifier for this entity
    - **`<transform>`**: Defines the position, rotation, and scale
    - **`<cubemeshentity>`**: Creates a cube with the specified color

## Adding More Objects

Let's create a more interesting scene with multiple objects:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>Multi-Object Scene</title>
    </metadata>
    <environment>
        <background>
            <color>#87CEEB</color>
        </background>
        
        <!-- Red cube -->
        <entity tag="red-cube" id="cube-001">
            <transform>
                <position x="-2" y="1" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <cubemeshentity>
                    <color>red</color>
                </cubemeshentity>
            </entity>
        </entity>
        
        <!-- Blue sphere -->
        <entity tag="blue-sphere" id="sphere-001">
            <transform>
                <position x="0" y="1" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <spheremeshentity>
                    <color>blue</color>
                </spheremeshentity>
            </entity>
        </entity>
        
        <!-- Green cylinder -->
        <entity tag="green-cylinder" id="cylinder-001">
            <transform>
                <position x="2" y="1" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="2" z="1" />
            </transform>
            <entity>
                <cylindermeshentity>
                    <color>green</color>
                </cylindermeshentity>
            </entity>
        </entity>
    </environment>
</veml>
```

## Understanding Transformations

### Position
- **x**: Left/Right (-x is left, +x is right)
- **y**: Up/Down (-y is down, +y is up)
- **z**: Forward/Back (-z is back, +z is forward)

### Rotation
VEML uses quaternions for rotation:
- **x, y, z**: The rotation axis components
- **w**: The rotation amount
- Default `(0, 0, 0, 1)` means no rotation

### Scale
- **x, y, z**: Scale factors in each direction
- `1` means normal size, `2` means double size, `0.5` means half size

## Common Shapes Available

VEML provides several built-in geometric shapes:

- **`<cubemeshentity>`**: Box/cube shapes
- **`<spheremeshentity>`**: Spheres and balls
- **`<cylindermeshentity>`**: Cylinders and tubes
- **`<capsulemeshentity>`**: Pill/capsule shapes
- **`<planemeshentity>`**: Flat surfaces
- **`<conemeshentity>`**: Cone shapes
- **`<torusmeshentity>`**: Donut/ring shapes

## Colors

Colors can be specified in several formats:
- **Named colors**: `red`, `blue`, `green`, `yellow`, `white`, `black`, etc.
- **Hex colors**: `#FF0000` (red), `#00FF00` (green), `#0000FF` (blue)
- **Hex with alpha**: `#FF0000FF` (opaque red), `#FF000080` (semi-transparent red)

## Next Steps

Now that you've created your first VEML file, you can:

1. **Validate your XML**: Use the VEML.xsd schema to ensure your file is valid
2. **Learn about [Basic Concepts](VEML-Basic-Concepts.md)**: Understand the underlying principles
3. **Explore [File Structure](VEML-File-Structure.md)**: Learn about the complete VEML structure
4. **Try [Complete Examples](VEML-Complete-Examples.md)**: See more complex scenarios

## Validation

To validate your VEML file against the schema:

1. Use an XML editor with schema validation
2. Point it to the VEML.xsd schema file
3. Open your VEML file - any errors will be highlighted

## Common Beginner Mistakes

1. **Forgetting the namespace**: Always include `xmlns="http://www.fivesqd.com/schemas/veml/2.3"`
2. **Incorrect nesting**: Remember that shape entities (like `<cubemeshentity>`) go inside `<entity>` containers
3. **Missing required elements**: Every entity needs a `<transform>`, every environment needs a `<background>`
4. **Invalid coordinates**: Remember that Y is up in VEML coordinate system

You're now ready to create basic VEML virtual environments! Continue with the other documentation sections to learn more advanced features.