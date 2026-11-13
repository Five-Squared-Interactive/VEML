# VEML (Virtual Environment Markup Language) Documentation

Welcome to the comprehensive documentation for VEML (Virtual Environment Markup Language) version 2.3. This documentation will help you understand how to create, validate, and work with VEML XML files.

## What is VEML?

VEML is an XML-based markup language designed for describing virtual environments, 3D scenes, and interactive experiences. It provides a standardized way to define:

- 3D objects and their transformations
- Lighting and environmental effects
- Audio elements
- User interactions and controls
- Background settings and atmospheric effects
- Synchronization services for multi-user experiences

## Documentation Structure

This documentation is organized into the following sections:

### Getting Started
- **[Quick Start Guide](VEML-Quick-Start-Guide.md)** - Create your first VEML file in minutes
- **[Basic Concepts](VEML-Basic-Concepts.md)** - Understanding the fundamental principles of VEML
- **[File Structure](VEML-File-Structure.md)** - Overview of the VEML XML structure

### Core Elements
- **[Root Element](VEML-Root-Element.md)** - The main `<veml>` element
- **[Metadata Section](VEML-Metadata-Section.md)** - Document metadata, capabilities, and settings
- **[Environment Section](VEML-Environment-Section.md)** - Scene environment and background settings

### Entities and Objects
- **[Entity System](VEML-Entity-System.md)** - Understanding the entity hierarchy
- **[Transformations](VEML-Transformations.md)** - Position, rotation, scale, and sizing
- **[Mesh Entities](VEML-Mesh-Entities.md)** - 3D geometry and primitive shapes
- **[Media Entities](VEML-Media-Entities.md)** - Audio, images, and HTML content
- **[Interactive Entities](VEML-Interactive-Entities.md)** - Buttons, inputs, and user interaction
- **[Specialized Entities](VEML-Specialized-Entities.md)** - Terrain, water, characters, and more

### Advanced Features
- **[Background and Sky](VEML-Background-and-Sky.md)** - Procedural skies, panoramas, and lighting
- **[Effects and Atmosphere](VEML-Effects-and-Atmosphere.md)** - Fog, lighting effects, and ambiance
- **[Input and Controls](VEML-Input-and-Controls.md)** - VR controls, locomotion, and interaction
- **[Synchronization](VEML-Synchronization.md)** - Multi-user and networking features

### Reference and Examples
- **[Complete Examples](VEML-Complete-Examples.md)** - Full VEML file examples for common scenarios
- **[Element Reference](VEML-Element-Reference.md)** - Complete alphabetical reference of all elements
- **[Attribute Reference](VEML-Attribute-Reference.md)** - Complete reference of all attributes
- **[Best Practices](VEML-Best-Practices.md)** - Tips and recommendations for effective VEML usage

## Quick Example

Here's a simple VEML file that creates a basic scene with a blue cube:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>My First VEML Scene</title>
    </metadata>
    <environment>
        <background>
            <color>#87CEEB</color>
        </background>
        <entity tag="blue-cube">
            <transform>
                <position x="0" y="1" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <cubemeshentity>
                    <color>blue</color>
                </cubemeshentity>
            </entity>
        </entity>
    </environment>
</veml>
```

## Getting Help

- **Schema Validation**: Use the VEML.xsd schema file to validate your XML
- **Code Generation**: Generated language bindings are available in C#, JavaScript, and Visual Basic
- **Community**: Visit the [VEML GitHub repository](https://github.com/dyfios/VEML) for discussions and support

## Version Information

This documentation covers VEML schema version 2.3, namespace: `http://www.fivesqd.com/schemas/veml/2.3`

Start with the [Quick Start Guide](VEML-Quick-Start-Guide.md) to begin creating your first VEML virtual environment!