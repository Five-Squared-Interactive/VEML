# The `water` profile

**Status:** specified 2026-10-09. Registry entry: draft §10, listed since the
first draft; this page is what graduated from that line.

A body of water a visitor can see and enter: a lake, a sea, a pool. A surface
at a height, a colour that deepens with depth, a little movement, and one
rule about what happens to a visitor who walks into it. Core has no water;
a `primitive` plane with a blue colour is a floor, and a visitor walks across
it dry. This profile makes a surface **water**: not solid, swum in below,
drawn as water above.

The vocabulary is small by intent. 3.x's water carried fourteen attributes
that described one renderer's shader (`distortion`, `num-waves`,
`wave-steepness`, …); none of them says what the water *is*, and a profile
that specifies more than any shell renders is a promise nobody keeps
(`sky.md`, Bounds). What survives is what an author means: where the water
is, what colour it is, how clear, and how much it moves.

## Declaration

```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/4.0"
      xmlns:water="http://www.fivesqd.com/schemas/veml/4.0/profiles/water"
      version="4.0">
  <metadata>
    <title>Lakeside</title>
    <profile>water</profile>
  </metadata>
  <environment>
    <background color="#87ceebff"/>
    <entry-point id="default">
      <position x="0" y="1" z="30"/>
    </entry-point>
    <primitive tag="shore" shape="plane" color="#6b7a4aff">
      <transform><position x="0" y="0" z="40"/><scale x="80" y="1" z="20"/></transform>
    </primitive>
    <water:body color="#2a6f97ff" deep-color="#0b2a3aff" clarity="6" waves="0.2">
      <transform><position x="0" y="-0.5" z="-20"/><scale x="200" y="1" z="120"/></transform>
    </water:body>
    <entity tag="boathouse">
      <transform><position x="10" y="-0.5" z="-15"/></transform>
      <asset src="assets/boathouse.glb"/>
    </entity>
    <water:blocker>
      <transform><position x="10" y="-0.5" z="-15"/><scale x="8" y="4" z="6"/></transform>
    </water:blocker>
  </environment>
</veml>
```

## Semantics

A document declaring `<profile>water</profile>` may place any number of
water bodies in its `environment`, and may carve them with blockers.

### `water:body`

A horizontal surface, rectangular, filling the box its `transform` describes:
the surface is the box's top face, at the transform's y, and the water fills
the box below it. The `transform` is a core §4.2 transform without rotation
(a rotated body is accepted with its rotation ignored, `veml:water:rotated`);
`scale` is its extent in metres, and a body with no `transform` is 1 × 1 m
at the origin. A body is not an entity: nothing is parented to it and it is
not in the registry a script reads.

| attribute | type | default | meaning |
|---|---|---|---|
| `color` | §4.5 color | `#2a6f97ff` | the water's colour where it is shallow |
| `deep-color` | §4.5 color | `#0b2a3aff` | its colour where it is deep |
| `clarity` | number, metres, ≥ 0 | `4` | the depth at which the colour has become `deep-color`; `0` is opaque |
| `waves` | number, 0..1 | `0.1` | how much the surface moves; `0` is still |

Colours' alpha is the surface's alpha. Out-of-range numbers are clamped to
their range with a warning (`veml:water:out-of-range`), never a refused body.

### `water:blocker`

A box in which water does not apply: a boat's hull, a room below the
waterline, a pier's underside. A blocker sits in `environment` beside the
bodies, and its `transform` (a §4.2 transform; `scale` is its extent,
rotation honoured) is in the world frame. Inside a blocker a visitor is dry,
the surface is not drawn, and the rules below do not apply, whatever bodies
the box overlaps. A blocker outside any body does nothing.

### What water does

- **Not solid.** A body's surface and volume are never walkable. A visitor
  who steps off the shore enters the water; nothing holds them up.
- **Swimming.** A visitor whose feet are below a body's surface and not in a
  blocker swims: they move in all three axes at the shell's swim speed, which
  SHOULD be slower than walking; they rise gently toward the surface when
  idle; they leave the water onto any solid ground whose top is no higher
  than the shell's step height above the surface. The `jump` input at the
  surface is a stroke upward, not a jump. A shell keeps the visitor above the
  bed, which is whatever solid ground lies under the body (terrain, a
  primitive, an asset); a body with nothing under it is bottomless and a
  visitor swims there as anywhere else.
- **Seen from above**, the surface is drawn with `color` shading toward
  `deep-color` by the depth of what lies beneath it, moving by `waves`.
  **Seen from below**, the shell MAY tint the view toward `deep-color`; it
  MUST keep the world visible, so a visitor can find the surface.
- **The `water` material** in a `terrain` tileset (`terrain.md`, What is
  solid) is a body's surface where a body covers it and otherwise simply not
  solid; a tileset carries no colour or waves of its own.

### Sound, splashes and wakes

None, and that is a boundary rather than an omission: they are a shell's
presentation, and a world that needs a particular sound names an `audio`
entity.

## Degradation

1. **A shell without this profile.** `water:body` and `water:blocker` are
   unrecognized elements in a foreign namespace; §2 requires them to be
   ignored with a warning. The world shows no water, and a visitor walks on
   whatever ground lies under the body. Authors are **RECOMMENDED** to give a
   body a bed, so such a shell shows a dry lakebed rather than a hole.
2. **A shell with this profile that draws no water** (a device without the
   material support it needs): the surface is drawn as a flat `color` plane
   and the rules above still hold. Being wet does not depend on the shader.
3. **A `water:body` or `water:blocker` with an invalid transform**: ignored,
   with `veml:water:invalid`.

**A shell that implements this profile MUST NOT emit `veml:2:unknown-element`
for the elements it implements** (see `sky.md`, Degradation, for why that
needs saying). Where a shell knows the profile and does not implement it:
`veml:water:profile-not-implemented`.

## Conformance

Structural (§10): a visitor placed below a body's surface and outside its
blockers is swimming (moves on three axes, rises when idle); a visitor inside
a blocker stands on the ground as if there were no water; a body's surface is
not in the walkable set; no `veml:2:unknown-element` is emitted for the
profile's elements. Out-of-range attributes clamp and warn once each. A shell
without the profile emits one warning per element and nothing else changes.

## Client story

A shell implements this profile with one material (a flat colour that
deepens with depth and moves by `waves`), one volume test in its visitor
controller (below a surface, not in a blocker), and a swim mode for that
controller. Nothing is fetched. The `terrain` profile's `water` material
reuses the same material where a body covers it.

## Schema

Profile vocabulary is in its own namespace,
`http://www.fivesqd.com/schemas/veml/4.0/profiles/water`, accepted by the core
schema's `environment` extension point for both elements (`xs:any
namespace="##other" processContents="lax"`, §3.2). A schema for the namespace
is deferred, as for `sky`.

## Migration from 3.x

3.x's `water` entity's fourteen shader attributes map to four: its
`shallow-color` is `color`, `deep-color` is `deep-color`, its `deep-start`
and `deep-end` become `clarity` (`deep-end`), and its wave amplitude becomes
`waves` (clamped to 0..1 after normalising by the renderer's full amplitude);
the rest describe one shader and are dropped by the migration tool with a
note. 3.x's `waterblocker` is `water:blocker`, at the world position the
entity it belonged to resolved to.

## Bounds

Outside this profile, and named so the line is visible:

- **Flow, rivers and currents.** A body is still water that moves in place.
- **Buoyant objects.** An entity that floats needs a physics engine; that is
  the physics story's.
- **Shaped bodies** (a winding river's outline, a round pool). A body is a
  box; an author composes shapes from several, or carves them with blockers.
- **Underwater fog, caustics, refraction.** Presentation; a shell may add
  them, nothing declares them.
- **Tides and levels that change**, and **a blocker that rides a moving
  entity** (a boat's hull). Both need a blocker or body to follow an entity,
  which the core schema's `entity` content model does not yet admit; a later
  revision can add it with the extension point.
