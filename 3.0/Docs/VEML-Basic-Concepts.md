# VEML Basic Concepts

This document explains the fundamental concepts and principles behind VEML (Virtual Environment Markup Language) to help you understand how to effectively create virtual environments.

## What is VEML?

VEML is an XML-based markup language specifically designed for describing virtual environments, 3D scenes, and interactive experiences. It provides a standardized, declarative way to define:

- **3D Objects and Geometry**: Primitive shapes, custom meshes, and complex models
- **Spatial Relationships**: Position, rotation, scale, and hierarchical arrangements
- **Interactive Elements**: Buttons, inputs, and user interface components
- **Media Integration**: Audio, images, video, and HTML content
- **Environmental Settings**: Lighting, atmosphere, weather, and background
- **Multi-User Support**: Synchronization and networking for shared experiences

## Core Principles

### 1. Declarative Approach
VEML is declarative rather than procedural. Instead of writing code that creates objects step-by-step, you describe what you want the final result to look like:

```xml
<!-- Declarative: Describe what you want -->
<entity tag="red-cube">
    <transform>
        <position x="0" y="1" z="0" />
        <scale x="1" y="1" z="1" />
    </transform>
    <entity>
        <cubemeshentity>
            <color>red</color>
        </cubemeshentity>
    </entity>
</entity>
```

### 2. Hierarchical Structure
VEML uses a hierarchical tree structure where entities can contain other entities. This enables:

- **Logical Grouping**: Related objects can be grouped together
- **Relative Positioning**: Child objects are positioned relative to their parents
- **Coordinated Transformation**: Moving a parent moves all its children
- **Organized Scenes**: Complex scenes remain manageable through organization

```xml
<entity tag="car">
    <transform>
        <position x="0" y="0" z="0" />
    </transform>
    <entity tag="body">
        <!-- Car body -->
    </entity>
    <entity tag="wheels">
        <entity tag="front-left-wheel">
            <!-- Position relative to car -->
        </entity>
        <entity tag="front-right-wheel">
            <!-- Position relative to car -->
        </entity>
    </entity>
</entity>
```

### 3. Separation of Structure and Behavior
VEML separates the structural description of the environment from its behavior:

- **Structure**: Defined in VEML XML (what objects exist and where)
- **Behavior**: Defined in JavaScript files (how objects behave and interact)

This separation makes it easier to:
- Modify visual layout without changing behavior
- Reuse behaviors across different visual layouts
- Collaborate between designers and programmers

### 4. Standards-Based
VEML is built on established standards:
- **XML Schema (XSD)**: Ensures structural validity
- **Standard Coordinate System**: Right-handed 3D coordinates
- **Web Technologies**: JavaScript for scripting, standard file formats
- **Industry Formats**: Support for FBX, OBJ, and other common 3D formats

## Key Concepts

### Entities and Entity Types

**Entity** is the fundamental building block in VEML. Every object in your virtual environment is an entity. Entities come in different types:

#### Container Entities
- **Purpose**: Organize and group other entities
- **Visual**: No visual representation by themselves
- **Usage**: Create logical groupings, hierarchical structures

```xml
<entity tag="living-room">
    <entity tag="furniture">
        <!-- All furniture entities -->
    </entity>
    <entity tag="lighting">
        <!-- All light entities -->
    </entity>
</entity>
```

#### Geometric Entities
- **Purpose**: Display 3D shapes and geometry
- **Types**: Cubes, spheres, cylinders, custom meshes, etc.
- **Properties**: Color, material, texture

```xml
<entity>
    <cubemeshentity>
        <color>blue</color>
    </cubemeshentity>
</entity>
```

#### Media Entities
- **Purpose**: Display or play media content
- **Types**: Audio, images, text, HTML
- **Integration**: Seamlessly blend with 3D content

```xml
<entity>
    <audioentity>
        <audio-file>background-music.ogg</audio-file>
        <auto-play>true</auto-play>
        <loop>true</loop>
    </audioentity>
</entity>
```

#### Interactive Entities
- **Purpose**: Enable user interaction
- **Types**: Buttons, input fields, interactive surfaces
- **Events**: Connect to JavaScript for custom behavior

```xml
<entity>
    <buttonentity on-click-event="handleButtonClick">
        <!-- Button appearance -->
    </buttonentity>
</entity>
```

### Transformations

Every entity has a **transformation** that defines its position, orientation, and size in 3D space.

#### Position
Where the entity is located in 3D space:
- **X**: Left (-) to Right (+)
- **Y**: Down (-) to Up (+)  
- **Z**: Back (-) to Forward (+)

#### Rotation
How the entity is oriented, specified as a quaternion:
- **Quaternions**: Mathematical representation of 3D rotations
- **Components**: x, y, z (axis) and w (amount)
- **Default**: (0, 0, 0, 1) means no rotation

#### Scale/Size
How big the entity is:
- **Scale**: Multiplier of original size (1 = normal, 2 = double, 0.5 = half)
- **Size**: Absolute dimensions in world units
- **Inheritance**: Child entities inherit parent transformations

### Coordinate System

VEML uses a **right-handed coordinate system**:

```
    Y (Up)
    |
    |
    |______ X (Right)
   /
  /
 Z (Forward)
```

This means:
- Positive X points to the right
- Positive Y points up
- Positive Z points toward the viewer
- This matches many 3D modeling applications

### Inheritance and Hierarchy

Child entities inherit transformations from their parents:

```xml
<entity tag="table">
    <transform>
        <position x="5" y="0" z="0" />
    </transform>
    <entity tag="cup">
        <transform>
            <position x="0" y="1" z="0" />
        </transform>
        <!-- Cup appears at world position (5, 1, 0) -->
    </entity>
</entity>
```

The cup's final position is the combination of:
- Table position: (5, 0, 0)
- Cup's relative position: (0, 1, 0)
- Final world position: (5, 1, 0)

### Document Structure

Every VEML document has a consistent structure:

1. **XML Declaration**: Standard XML header
2. **Root Element**: `<veml>` with namespace
3. **Metadata Section**: Document information and settings
4. **Environment Section**: Scene content

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <!-- Document info and global settings -->
    </metadata>
    <environment>
        <!-- Scene content -->
    </environment>
</veml>
```

### Metadata vs Environment

#### Metadata Section
Contains information **about** the environment:
- Title and description
- Required capabilities
- Script files to load
- Input mappings
- Global control settings
- Network configuration

#### Environment Section
Contains the actual scene **content**:
- Background and sky settings
- Visual effects
- All entities and objects
- Scene hierarchy

## Design Patterns

### Component-Based Design
Think of entities as composed of components:

```xml
<entity tag="interactive-lamp">
    <!-- Position component -->
    <transform>
        <position x="0" y="0" z="0" />
    </transform>
    
    <!-- Visual component -->
    <entity tag="lamp-model">
        <meshentity>
            <mesh-name>desk-lamp</mesh-name>
            <mesh-resource>models/lamp.fbx</mesh-resource>
        </meshentity>
    </entity>
    
    <!-- Light component -->
    <entity tag="lamp-light">
        <lightentity>
        </lightentity>
    </entity>
    
    <!-- Interaction component -->
    <entity tag="lamp-switch">
        <buttonentity on-click-event="toggleLamp">
        </buttonentity>
    </entity>
</entity>
```

### Scene Graph Organization
Organize your scene hierarchically:

```xml
<entity tag="scene-root">
    <entity tag="static-geometry">
        <entity tag="buildings">
            <!-- All building entities -->
        </entity>
        <entity tag="terrain">
            <!-- Terrain entities -->
        </entity>
    </entity>
    
    <entity tag="dynamic-objects">
        <entity tag="vehicles">
            <!-- Moving vehicles -->
        </entity>
        <entity tag="characters">
            <!-- NPCs and avatars -->
        </entity>
    </entity>
    
    <entity tag="ui-elements">
        <!-- User interface -->
    </entity>
</entity>
```

### Prefab-Like Reusability
Create reusable "templates" by defining complex entities that can be positioned in multiple places:

```xml
<!-- Define a street lamp template -->
<entity tag="street-lamp-template">
    <transform>
        <position x="0" y="0" z="0" />
    </transform>
    <!-- Lamp definition -->
</entity>

<!-- Use the template in multiple locations -->
<entity tag="lamp-1">
    <transform>
        <position x="10" y="0" z="0" />
    </transform>
    <!-- Copy the lamp template content here -->
</entity>

<entity tag="lamp-2">
    <transform>
        <position x="20" y="0" z="0" />
    </transform>
    <!-- Copy the lamp template content here -->
</entity>
```

## Performance Considerations

### Entity Count
- Keep entity count reasonable for your target platform
- Use level-of-detail (LOD) models when available
- Consider culling techniques for large scenes

### Hierarchy Depth
- Avoid extremely deep hierarchies (>10 levels)
- Balance organization with performance
- Group related objects efficiently

### Resource Management
- Reference external files efficiently
- Reuse textures and models when possible
- Consider compressed file formats

## Validation and Quality

### Schema Validation
Always validate your VEML against the XSD schema:
- Catches structural errors early
- Ensures compatibility with VEML processors
- Provides helpful error messages

### Incremental Development
Build your environments incrementally:
1. Start with basic structure
2. Add simple geometry
3. Add textures and materials
4. Add interactions and behavior
5. Optimize and polish

### Testing Strategy
Test your environments on target platforms:
- Validate visual appearance
- Test interactions and behavior
- Check performance metrics
- Verify multi-user functionality

Understanding these core concepts will help you create well-structured, maintainable, and effective VEML virtual environments. The hierarchical, declarative nature of VEML makes it powerful for describing complex 3D scenes while remaining human-readable and maintainable.