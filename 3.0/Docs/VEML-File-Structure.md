# VEML File Structure

This document provides a comprehensive overview of the VEML file structure, explaining how the different parts work together to create virtual environments.

## XML Declaration and Root Element

Every VEML file must start with the standard XML declaration and the root `<veml>` element:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <!-- Content goes here -->
</veml>
```

### Important Notes
- **Namespace**: The namespace `http://www.fivesqd.com/schemas/veml/2.3` is required
- **Encoding**: UTF-8 encoding is recommended for international character support
- **Schema Version**: This documentation covers VEML 2.3

## Top-Level Structure

The VEML root element contains exactly two main sections:

```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <!-- Document metadata and configuration -->
    </metadata>
    <environment>
        <!-- Scene content and objects -->
    </environment>
</veml>
```

Both sections are **required** and must appear in this order.

## Metadata Section

The metadata section contains information about the virtual environment and its configuration:

```xml
<metadata>
    <title>Environment Title</title>
    <capability>feature1</capability>
    <capability>feature2</capability>
    <script>script1.js</script>
    <script>script2.js</script>
    <inputevent input="key:space" event="jump" />
    <controlflags>
        <!-- VR and input control settings -->
    </controlflags>
    <synchronizationservice type="websocket" address="ws://server.com" />
</metadata>
```

### Required Elements
- **`<title>`**: A human-readable name for the environment (required)

### Optional Elements
- **`<capability>`**: Feature capabilities (0 or more)
- **`<script>`**: JavaScript file references (0 or more)
- **`<inputevent>`**: Input event mappings (0 or more)
- **`<controlflags>`**: Control and interaction settings (0 or 1)
- **`<synchronizationservice>`**: Multi-user synchronization (0 or more)

## Environment Section

The environment section contains the actual scene content:

```xml
<environment>
    <background>
        <!-- Background settings -->
    </background>
    <effects>
        <!-- Visual effects and atmosphere -->
    </effects>
    <entity>
        <!-- Scene objects and entities -->
    </entity>
    <!-- More entities... -->
</environment>
```

### Required Elements
- **`<background>`**: Background/sky settings (required)

### Optional Elements
- **`<effects>`**: Visual effects like fog (0 or 1)
- **`<entity>`**: Scene objects and entities (0 or more)

## Entity Hierarchy

Entities are the building blocks of VEML scenes. They can contain other entities, creating a hierarchical structure:

```xml
<entity tag="parent" id="parent-001">
    <transform>
        <!-- Position, rotation, scale -->
    </transform>
    <entity tag="child1">
        <transform>
            <!-- Child transformation relative to parent -->
        </transform>
        <entity>
            <cubemeshentity>
                <color>red</color>
            </cubemeshentity>
        </entity>
    </entity>
    <entity tag="child2">
        <!-- Another child entity -->
    </entity>
</entity>
```

### Entity Structure
Each entity contains:
1. **Transform**: Position, rotation, and scale (required)
2. **Child entities**: Nested entities (optional)
3. **Synchronizer**: Network synchronization (optional)
4. **Placement sockets**: Connection points (optional)
5. **Attributes**: tag, id, on-load-event (optional)

## Transform Types

VEML supports different types of transformations:

### Scale Transform (Most Common)
```xml
<transform>
    <position x="0" y="1" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <scale x="1" y="1" z="1" />
</transform>
```

### Size Transform
```xml
<transform>
    <position x="0" y="1" z="0" />
    <rotation x="0" y="0" z="0" w="1" />
    <size x="2" y="1" z="1" />
</transform>
```

### Canvas Transform (for UI elements)
```xml
<transform>
    <position-percent x="0.5" y="0.5" />
    <size-percent x="0.2" y="0.1" />
</transform>
```

## Entity Types

VEML supports many entity types, categorized as follows:

### Geometric Shapes
- `<cubemeshentity>` - Boxes and cubes
- `<spheremeshentity>` - Spheres
- `<cylindermeshentity>` - Cylinders
- `<capsulemeshentity>` - Capsules
- `<planemeshentity>` - Flat planes
- `<conemeshentity>` - Cones
- `<torusmeshentity>` - Toruses
- `<tetrahedronmeshentity>` - Tetrahedrons
- `<rectangularpyramidmeshentity>` - Pyramids
- `<prismmeshentity>` - Prisms
- `<archmeshentity>` - Arches

### Custom Meshes
- `<meshentity>` - Custom 3D models

### Media and Content
- `<audioentity>` - Audio sources
- `<imageentity>` - Image displays
- `<textentity>` - Text displays
- `<htmlentity>` - HTML content

### Interactive Elements
- `<buttonentity>` - Interactive buttons
- `<inputentity>` - Input fields
- `<canvasentity>` - Canvas/UI surfaces

### Special Entities
- `<characterentity>` - Character/avatar entities
- `<lightentity>` - Light sources
- `<terrainentity>` - Terrain systems
- `<waterentity>` - Water surfaces
- `<voxelentity>` - Voxel-based objects
- `<waterblockerentity>` - Water blocking volumes
- `<containerentity>` - Generic containers

## Complete Structure Example

Here's a complete VEML file showing the full structure:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>Complete Structure Demo</title>
        <capability>physics</capability>
        <capability>audio</capability>
        <script>main.js</script>
        <inputevent input="key:space" event="jump" />
        <controlflags>
            <left-vr-pointer>ray</left-vr-pointer>
            <right-vr-pointer>ray</right-vr-pointer>
            <joystick-motion>true</joystick-motion>
        </controlflags>
        <synchronizationservice type="websocket" address="ws://localhost:8080" />
    </metadata>
    <environment>
        <background>
            <lite-procedural-sky sun-enabled="true" />
        </background>
        <effects>
            <lite-fog enabled="true" color="white" density="0.1" />
        </effects>
        <entity tag="scene-root" id="root-001">
            <transform>
                <position x="0" y="0" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity tag="ground">
                <transform>
                    <position x="0" y="0" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="10" y="0.1" z="10" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>green</color>
                    </cubemeshentity>
                </entity>
            </entity>
            <entity tag="floating-sphere">
                <transform>
                    <position x="0" y="2" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.5" y="0.5" z="0.5" />
                </transform>
                <entity>
                    <spheremeshentity>
                        <color>blue</color>
                    </spheremeshentity>
                </entity>
            </entity>
        </entity>
    </environment>
</veml>
```

## Key Principles

1. **Hierarchy**: Entities can contain other entities, creating parent-child relationships
2. **Transforms**: Every entity must have a transform defining its position, rotation, and scale
3. **Inheritance**: Child entities inherit transformations from their parents
4. **Modularity**: Complex scenes are built from simple, reusable components
5. **Validation**: The XSD schema ensures structural correctness

## Best Practices

1. **Use meaningful tags and IDs**: Make your entities easy to identify and reference
2. **Organize hierarchically**: Group related objects under parent entities
3. **Keep transforms simple**: Use default values when possible
4. **Validate regularly**: Check your XML against the schema frequently
5. **Comment complex sections**: Use XML comments to explain complex arrangements

This structure provides a solid foundation for creating any type of virtual environment in VEML. Continue with the detailed sections to learn about specific elements and their capabilities.