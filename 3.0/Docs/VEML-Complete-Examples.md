# VEML Complete Examples

This section provides complete, realistic VEML examples that demonstrate common use cases and scenarios. Each example is a complete, valid VEML file that you can use as a starting point for your own projects.

## Example 1: Simple Room with Furniture

This example creates a basic room with furniture pieces, demonstrating basic geometric shapes and positioning:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>Simple Room with Furniture</title>
        <capability>physics</capability>
    </metadata>
    <environment>
        <background>
            <color>#E6F3FF</color>
        </background>
        
        <!-- Room Floor -->
        <entity tag="floor" id="floor-001">
            <transform>
                <position x="0" y="0" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="8" y="0.1" z="8" />
            </transform>
            <entity>
                <cubemeshentity>
                    <color>#8B4513</color>
                </cubemeshentity>
            </entity>
        </entity>
        
        <!-- Room Walls -->
        <entity tag="walls" id="walls-001">
            <transform>
                <position x="0" y="0" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            
            <!-- North Wall -->
            <entity tag="north-wall">
                <transform>
                    <position x="0" y="1.5" z="4" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="8" y="3" z="0.2" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#F5F5DC</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <!-- South Wall -->
            <entity tag="south-wall">
                <transform>
                    <position x="0" y="1.5" z="-4" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="8" y="3" z="0.2" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#F5F5DC</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <!-- East Wall -->
            <entity tag="east-wall">
                <transform>
                    <position x="4" y="1.5" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.2" y="3" z="8" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#F5F5DC</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <!-- West Wall -->
            <entity tag="west-wall">
                <transform>
                    <position x="-4" y="1.5" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.2" y="3" z="8" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#F5F5DC</color>
                    </cubemeshentity>
                </entity>
            </entity>
        </entity>
        
        <!-- Table -->
        <entity tag="table" id="table-001">
            <transform>
                <position x="0" y="0" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            
            <!-- Table Top -->
            <entity tag="table-top">
                <transform>
                    <position x="0" y="0.8" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="2" y="0.1" z="1" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#8B4513</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <!-- Table Legs -->
            <entity tag="leg1">
                <transform>
                    <position x="0.9" y="0.4" z="0.4" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.1" y="0.8" z="0.1" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#654321</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <entity tag="leg2">
                <transform>
                    <position x="-0.9" y="0.4" z="0.4" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.1" y="0.8" z="0.1" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#654321</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <entity tag="leg3">
                <transform>
                    <position x="0.9" y="0.4" z="-0.4" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.1" y="0.8" z="0.1" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#654321</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <entity tag="leg4">
                <transform>
                    <position x="-0.9" y="0.4" z="-0.4" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.1" y="0.8" z="0.1" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#654321</color>
                    </cubemeshentity>
                </entity>
            </entity>
        </entity>
        
        <!-- Chairs -->
        <entity tag="chair1" id="chair-001">
            <transform>
                <position x="1.5" y="0" z="0.5" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            
            <!-- Chair Seat -->
            <entity tag="seat">
                <transform>
                    <position x="0" y="0.45" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.5" y="0.1" z="0.5" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#8B0000</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <!-- Chair Back -->
            <entity tag="back">
                <transform>
                    <position x="0" y="0.75" z="-0.2" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.5" y="0.6" z="0.1" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#8B0000</color>
                    </cubemeshentity>
                </entity>
            </entity>
        </entity>
        
        <entity tag="chair2" id="chair-002">
            <transform>
                <position x="-1.5" y="0" z="0.5" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            
            <!-- Chair Seat -->
            <entity tag="seat">
                <transform>
                    <position x="0" y="0.45" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.5" y="0.1" z="0.5" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#8B0000</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <!-- Chair Back -->
            <entity tag="back">
                <transform>
                    <position x="0" y="0.75" z="-0.2" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.5" y="0.6" z="0.1" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#8B0000</color>
                    </cubemeshentity>
                </entity>
            </entity>
        </entity>
    </environment>
</veml>
```

## Example 2: Interactive VR Playground

This example demonstrates VR controls, interactive elements, and audio:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>Interactive VR Playground</title>
        <capability>vr</capability>
        <capability>physics</capability>
        <capability>audio</capability>
        <script>playground.js</script>
        <inputevent input="controller:trigger" event="grab" />
        <inputevent input="controller:grip" event="teleport" />
        <controlflags>
            <left-vr-pointer>ray</left-vr-pointer>
            <right-vr-pointer>ray</right-vr-pointer>
            <left-hand-interaction>true</left-hand-interaction>
            <right-hand-interaction>true</right-hand-interaction>
            <joystick-motion>true</joystick-motion>
            <turn-locomotion>snap</turn-locomotion>
        </controlflags>
    </metadata>
    <environment>
        <background>
            <lite-procedural-sky 
                ground-enabled="true" 
                ground-color="#90EE90"
                sun-enabled="true"
                day-sky-color="#87CEEB"
                clouds-enabled="true"
            />
        </background>
        <effects>
            <lite-fog enabled="false" />
        </effects>
        
        <!-- Interactive Button Platform -->
        <entity tag="button-platform" id="platform-001">
            <transform>
                <position x="0" y="0.1" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="3" y="0.2" z="3" />
            </transform>
            <entity>
                <cubemeshentity>
                    <color>#CD853F</color>
                </cubemeshentity>
            </entity>
        </entity>
        
        <!-- Interactive Buttons -->
        <entity tag="red-button" id="button-red">
            <transform>
                <position x="-1" y="0.3" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="0.3" y="0.1" z="0.3" />
            </transform>
            <entity>
                <buttonentity on-click-event="playRedSound">
                    <transform>
                        <position x="0" y="0" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="1" y="1" z="1" />
                    </transform>
                    <entity>
                        <cylindermeshentity>
                            <color>red</color>
                        </cylindermeshentity>
                    </entity>
                </buttonentity>
            </entity>
        </entity>
        
        <entity tag="blue-button" id="button-blue">
            <transform>
                <position x="0" y="0.3" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="0.3" y="0.1" z="0.3" />
            </transform>
            <entity>
                <buttonentity on-click-event="playBlueSound">
                    <transform>
                        <position x="0" y="0" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="1" y="1" z="1" />
                    </transform>
                    <entity>
                        <cylindermeshentity>
                            <color>blue</color>
                        </cylindermeshentity>
                    </entity>
                </buttonentity>
            </entity>
        </entity>
        
        <entity tag="green-button" id="button-green">
            <transform>
                <position x="1" y="0.3" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="0.3" y="0.1" z="0.3" />
            </transform>
            <entity>
                <buttonentity on-click-event="playGreenSound">
                    <transform>
                        <position x="0" y="0" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="1" y="1" z="1" />
                    </transform>
                    <entity>
                        <cylindermeshentity>
                            <color>green</color>
                        </cylindermeshentity>
                    </entity>
                </buttonentity>
            </entity>
        </entity>
        
        <!-- Audio Sources -->
        <entity tag="red-audio" id="audio-red">
            <transform>
                <position x="-1" y="1" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <audioentity>
                    <audio-file>sounds/red-sound.wav</audio-file>
                    <auto-play>false</auto-play>
                    <loop>false</loop>
                    <priority>1</priority>
                    <volume>0.8</volume>
                    <pitch>1.0</pitch>
                    <stereo-pan>0.0</stereo-pan>
                </audioentity>
            </entity>
        </entity>
        
        <entity tag="blue-audio" id="audio-blue">
            <transform>
                <position x="0" y="1" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <audioentity>
                    <audio-file>sounds/blue-sound.wav</audio-file>
                    <auto-play>false</auto-play>
                    <loop>false</loop>
                    <priority>1</priority>
                    <volume>0.8</volume>
                    <pitch>1.0</pitch>
                    <stereo-pan>0.0</stereo-pan>
                </audioentity>
            </entity>
        </entity>
        
        <entity tag="green-audio" id="audio-green">
            <transform>
                <position x="1" y="1" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <audioentity>
                    <audio-file>sounds/green-sound.wav</audio-file>
                    <auto-play>false</auto-play>
                    <loop>false</loop>
                    <priority>1</priority>
                    <volume>0.8</volume>
                    <pitch>1.0</pitch>
                    <stereo-pan>0.0</stereo-pan>
                </audioentity>
            </entity>
        </entity>
        
        <!-- Floating Shapes for Visual Interest -->
        <entity tag="floating-shapes" id="shapes-001">
            <transform>
                <position x="0" y="3" z="5" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            
            <entity tag="floating-sphere">
                <transform>
                    <position x="-2" y="0" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.5" y="0.5" z="0.5" />
                </transform>
                <entity>
                    <spheremeshentity>
                        <color>#FF69B4</color>
                    </spheremeshentity>
                </entity>
            </entity>
            
            <entity tag="floating-cube">
                <transform>
                    <position x="0" y="0" z="0" />
                    <rotation x="0" y="0.707" z="0" w="0.707" />
                    <scale x="0.5" y="0.5" z="0.5" />
                </transform>
                <entity>
                    <cubemeshentity>
                        <color>#FFD700</color>
                    </cubemeshentity>
                </entity>
            </entity>
            
            <entity tag="floating-torus">
                <transform>
                    <position x="2" y="0" z="0" />
                    <rotation x="0.707" y="0" z="0" w="0.707" />
                    <scale x="0.5" y="0.5" z="0.5" />
                </transform>
                <entity>
                    <torusmeshentity>
                        <color>#32CD32</color>
                    </torusmeshentity>
                </entity>
            </entity>
        </entity>
        
        <!-- Information Display -->
        <entity tag="info-display" id="display-001">
            <transform>
                <position x="0" y="2" z="-2" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <textentity text="Welcome to the VR Playground! Press the colored buttons to hear sounds." font-size="0.1">
                    <transform>
                        <position x="0" y="0" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="1" y="1" z="1" />
                    </transform>
                </textentity>
            </entity>
        </entity>
    </environment>
</veml>
```

## Example 3: Natural Outdoor Scene

This example creates a natural outdoor environment with terrain, water, and atmospheric effects:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>Natural Outdoor Scene</title>
        <capability>terrain</capability>
        <capability>water</capability>
        <capability>weather</capability>
    </metadata>
    <environment>
        <background>
            <lite-procedural-sky 
                ground-enabled="true"
                ground-color="#228B22"
                ground-height="-0.1"
                sun-enabled="true"
                sun-diameter="3"
                clouds-enabled="true"
                cloudiness="0.4"
                clouds-opacity="0.8"
                day-sky-color="#87CEEB"
                day-horizon-color="#FFE4E1"
                stars-enabled="true"
                moon-enabled="true"
            />
        </background>
        <effects>
            <lite-fog enabled="true" color="#E0E0E0" density="0.02" />
        </effects>
        
        <!-- Terrain -->
        <entity tag="main-terrain" id="terrain-001">
            <transform>
                <position x="0" y="0" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <terrainentity length="50" width="50" height="5" type="heightmap">
                    <layer>
                        <diffuse-texture>textures/grass_diffuse.jpg</diffuse-texture>
                        <normal-texture>textures/grass_normal.jpg</normal-texture>
                        <mask-texture>textures/grass_mask.jpg</mask-texture>
                        <specular>textures/grass_specular.jpg</specular>
                        <metallic>0.0</metallic>
                        <smoothness>0.2</smoothness>
                    </layer>
                    <layer>
                        <diffuse-texture>textures/rock_diffuse.jpg</diffuse-texture>
                        <normal-texture>textures/rock_normal.jpg</normal-texture>
                        <mask-texture>textures/rock_mask.jpg</mask-texture>
                        <specular>textures/rock_specular.jpg</specular>
                        <metallic>0.1</metallic>
                        <smoothness>0.8</smoothness>
                    </layer>
                    <layer-masks>textures/terrain_masks.png</layer-masks>
                </terrainentity>
            </entity>
        </entity>
        
        <!-- Water Feature -->
        <entity tag="lake" id="water-001">
            <transform>
                <position x="10" y="0.5" z="10" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="8" y="1" z="8" />
            </transform>
            <entity>
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
            </entity>
        </entity>
        
        <!-- Trees -->
        <entity tag="forest" id="forest-001">
            <transform>
                <position x="0" y="0" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            
            <!-- Tree 1 -->
            <entity tag="tree1">
                <transform>
                    <position x="-5" y="0" z="8" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="1" y="1" z="1" />
                </transform>
                
                <!-- Tree Trunk -->
                <entity tag="trunk">
                    <transform>
                        <position x="0" y="2" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="0.3" y="4" z="0.3" />
                    </transform>
                    <entity>
                        <cylindermeshentity>
                            <color>#8B4513</color>
                        </cylindermeshentity>
                    </entity>
                </entity>
                
                <!-- Tree Foliage -->
                <entity tag="foliage">
                    <transform>
                        <position x="0" y="5" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="2" y="2" z="2" />
                    </transform>
                    <entity>
                        <spheremeshentity>
                            <color>#228B22</color>
                        </spheremeshentity>
                    </entity>
                </entity>
            </entity>
            
            <!-- Tree 2 -->
            <entity tag="tree2">
                <transform>
                    <position x="3" y="0" z="15" />
                    <rotation x="0" y="0.2" z="0" w="0.98" />
                    <scale x="1.2" y="1.2" z="1.2" />
                </transform>
                
                <!-- Tree Trunk -->
                <entity tag="trunk">
                    <transform>
                        <position x="0" y="2.4" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="0.35" y="4.8" z="0.35" />
                    </transform>
                    <entity>
                        <cylindermeshentity>
                            <color>#8B4513</color>
                        </cylindermeshentity>
                    </entity>
                </entity>
                
                <!-- Tree Foliage -->
                <entity tag="foliage">
                    <transform>
                        <position x="0" y="6" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="2.4" y="2.4" z="2.4" />
                    </transform>
                    <entity>
                        <spheremeshentity>
                            <color>#32CD32</color>
                        </spheremeshentity>
                    </entity>
                </entity>
            </entity>
            
            <!-- Tree 3 -->
            <entity tag="tree3">
                <transform>
                    <position x="-8" y="0" z="12" />
                    <rotation x="0" y="0.5" z="0" w="0.866" />
                    <scale x="0.8" y="0.8" z="0.8" />
                </transform>
                
                <!-- Tree Trunk -->
                <entity tag="trunk">
                    <transform>
                        <position x="0" y="1.6" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="0.24" y="3.2" z="0.24" />
                    </transform>
                    <entity>
                        <cylindermeshentity>
                            <color>#8B4513</color>
                        </cylindermeshentity>
                    </entity>
                </entity>
                
                <!-- Tree Foliage -->
                <entity tag="foliage">
                    <transform>
                        <position x="0" y="4" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="1.6" y="1.6" z="1.6" />
                    </transform>
                    <entity>
                        <spheremeshentity>
                            <color>#228B22</color>
                        </spheremeshentity>
                    </entity>
                </entity>
            </entity>
        </entity>
        
        <!-- Rocks -->
        <entity tag="rocks" id="rocks-001">
            <transform>
                <position x="0" y="0" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            
            <entity tag="rock1">
                <transform>
                    <position x="5" y="0.3" z="3" />
                    <rotation x="0.1" y="0.3" z="0.1" w="0.94" />
                    <scale x="0.8" y="0.6" z="1.2" />
                </transform>
                <entity>
                    <spheremeshentity>
                        <color>#696969</color>
                    </spheremeshentity>
                </entity>
            </entity>
            
            <entity tag="rock2">
                <transform>
                    <position x="7" y="0.2" z="5" />
                    <rotation x="0.2" y="0.7" z="0.1" w="0.67" />
                    <scale x="0.6" y="0.4" z="0.8" />
                </transform>
                <entity>
                    <spheremeshentity>
                        <color>#708090</color>
                    </spheremeshentity>
                </entity>
            </entity>
        </entity>
        
        <!-- Ambient Audio -->
        <entity tag="nature-sounds" id="audio-ambient">
            <transform>
                <position x="0" y="2" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <audioentity>
                    <audio-file>sounds/forest_ambient.ogg</audio-file>
                    <auto-play>true</auto-play>
                    <loop>true</loop>
                    <priority>0</priority>
                    <volume>0.3</volume>
                    <pitch>1.0</pitch>
                    <stereo-pan>0.0</stereo-pan>
                </audioentity>
            </entity>
        </entity>
    </environment>
</veml>
```

## Example 4: Multi-User Conference Room

This example demonstrates multi-user synchronization and character entities:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/2.3">
    <metadata>
        <title>Multi-User Conference Room</title>
        <capability>networking</capability>
        <capability>voice</capability>
        <capability>avatar</capability>
        <script>conference.js</script>
        <synchronizationservice type="websocket" address="wss://conference.example.com" id="conf-room-1" session="meeting-2024" />
        <controlflags>
            <left-vr-pointer>ray</left-vr-pointer>
            <right-vr-pointer>ray</right-vr-pointer>
            <joystick-motion>true</joystick-motion>
            <turn-locomotion>smooth</turn-locomotion>
        </controlflags>
    </metadata>
    <environment>
        <background>
            <color>#F5F5F5</color>
        </background>
        
        <!-- Conference Room Floor -->
        <entity tag="floor" id="floor-001">
            <transform>
                <position x="0" y="0" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="12" y="0.1" z="8" />
            </transform>
            <entity>
                <cubemeshentity>
                    <color>#D2B48C</color>
                </cubemeshentity>
            </entity>
        </entity>
        
        <!-- Conference Table -->
        <entity tag="conference-table" id="table-main">
            <transform>
                <position x="0" y="0.75" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="6" y="0.1" z="3" />
            </transform>
            <entity>
                <cubemeshentity>
                    <color>#8B4513</color>
                </cubemeshentity>
            </entity>
        </entity>
        
        <!-- Presentation Screen -->
        <entity tag="presentation-screen" id="screen-001">
            <transform>
                <position x="0" y="2" z="2.5" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="4" y="2.25" z="0.1" />
            </transform>
            <entity>
                <htmlentity url="https://presentation.example.com/meeting-slides" on-message="handleSlideChange">
                    <transform>
                        <position x="0" y="0" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="1" y="1" z="1" />
                    </transform>
                </htmlentity>
            </entity>
        </entity>
        
        <!-- Meeting Participants (Character Entities) -->
        <entity tag="participant-1" id="user-001" synchronizer="user-sync">
            <transform>
                <position x="-2" y="0" z="-1" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <characterentity>
                    <mesh-name>avatar-business-1</mesh-name>
                    <mesh-resource>models/avatars/business-1.fbx</mesh-resource>
                    <mesh-offset x="0" y="0" z="0" />
                    <mesh-rotation x="0" y="0" z="0" w="1" />
                    <label-offset x="0" y="2.2" z="0" />
                </characterentity>
            </entity>
            
            <!-- Name Tag -->
            <entity tag="nametag">
                <transform>
                    <position x="0" y="2.5" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="1" y="1" z="1" />
                </transform>
                <entity>
                    <textentity text="John Smith" font-size="0.1">
                        <transform>
                            <position x="0" y="0" z="0" />
                            <rotation x="0" y="0" z="0" w="1" />
                            <scale x="1" y="1" z="1" />
                        </transform>
                    </textentity>
                </entity>
            </entity>
        </entity>
        
        <entity tag="participant-2" id="user-002" synchronizer="user-sync">
            <transform>
                <position x="2" y="0" z="-1" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <characterentity>
                    <mesh-name>avatar-business-2</mesh-name>
                    <mesh-resource>models/avatars/business-2.fbx</mesh-resource>
                    <mesh-offset x="0" y="0" z="0" />
                    <mesh-rotation x="0" y="0" z="0" w="1" />
                    <label-offset x="0" y="2.2" z="0" />
                </characterentity>
            </entity>
            
            <!-- Name Tag -->
            <entity tag="nametag">
                <transform>
                    <position x="0" y="2.5" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="1" y="1" z="1" />
                </transform>
                <entity>
                    <textentity text="Sarah Johnson" font-size="0.1">
                        <transform>
                            <position x="0" y="0" z="0" />
                            <rotation x="0" y="0" z="0" w="1" />
                            <scale x="1" y="1" z="1" />
                        </transform>
                    </textentity>
                </entity>
            </entity>
        </entity>
        
        <!-- Interactive Controls -->
        <entity tag="slide-controls" id="controls-001">
            <transform>
                <position x="-4" y="1" z="1" />
                <rotation x="0" y="0.707" z="0" w="0.707" />
                <scale x="1" y="1" z="1" />
            </transform>
            
            <!-- Previous Button -->
            <entity tag="prev-button">
                <transform>
                    <position x="0" y="0.5" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.3" y="0.1" z="0.2" />
                </transform>
                <entity>
                    <buttonentity on-click-event="previousSlide">
                        <transform>
                            <position x="0" y="0" z="0" />
                            <rotation x="0" y="0" z="0" w="1" />
                            <scale x="1" y="1" z="1" />
                        </transform>
                        <entity>
                            <cubemeshentity>
                                <color>#4CAF50</color>
                            </cubemeshentity>
                        </entity>
                    </buttonentity>
                </entity>
            </entity>
            
            <!-- Next Button -->
            <entity tag="next-button">
                <transform>
                    <position x="0" y="0.2" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="0.3" y="0.1" z="0.2" />
                </transform>
                <entity>
                    <buttonentity on-click-event="nextSlide">
                        <transform>
                            <position x="0" y="0" z="0" />
                            <rotation x="0" y="0" z="0" w="1" />
                            <scale x="1" y="1" z="1" />
                        </transform>
                        <entity>
                            <cubemeshentity>
                                <color>#2196F3</color>
                            </cubemeshentity>
                        </entity>
                    </buttonentity>
                </entity>
            </entity>
            
            <!-- Labels -->
            <entity tag="prev-label">
                <transform>
                    <position x="0.5" y="0.5" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="1" y="1" z="1" />
                </transform>
                <entity>
                    <textentity text="Previous" font-size="0.05">
                        <transform>
                            <position x="0" y="0" z="0" />
                            <rotation x="0" y="0" z="0" w="1" />
                            <scale x="1" y="1" z="1" />
                        </transform>
                    </textentity>
                </entity>
            </entity>
            
            <entity tag="next-label">
                <transform>
                    <position x="0.5" y="0.2" z="0" />
                    <rotation x="0" y="0" z="0" w="1" />
                    <scale x="1" y="1" z="1" />
                </transform>
                <entity>
                    <textentity text="Next" font-size="0.05">
                        <transform>
                            <position x="0" y="0" z="0" />
                            <rotation x="0" y="0" z="0" w="1" />
                            <scale x="1" y="1" z="1" />
                        </transform>
                    </textentity>
                </entity>
            </entity>
        </entity>
        
        <!-- Conference Room Lighting -->
        <entity tag="ceiling-light" id="light-001">
            <transform>
                <position x="0" y="3.5" z="0" />
                <rotation x="0" y="0" z="0" w="1" />
                <scale x="1" y="1" z="1" />
            </transform>
            <entity>
                <lightentity>
                    <transform>
                        <position x="0" y="0" z="0" />
                        <rotation x="0" y="0" z="0" w="1" />
                        <scale x="1" y="1" z="1" />
                    </transform>
                </lightentity>
            </entity>
        </entity>
    </environment>
</veml>
```

## Key Features Demonstrated

### Example 1 - Simple Room
- Basic geometric shapes (cubes, hierarchical entity structure)
- Color specification
- Positioning and scaling
- Creating furniture from primitives

### Example 2 - VR Playground
- VR controller configuration
- Interactive buttons with events
- Audio entities and sound management
- Multiple shape types
- Text display entities

### Example 3 - Natural Scene
- Procedural sky with atmospheric effects
- Terrain with texture layers
- Water entities with realistic parameters
- Complex object hierarchies (trees)
- Fog effects and ambient audio

### Example 4 - Conference Room
- Multi-user synchronization
- Character entities with avatars
- HTML content integration
- Interactive presentation controls
- Networked environment setup

Each example is complete and functional, demonstrating progressive complexity from basic static scenes to interactive multi-user environments.