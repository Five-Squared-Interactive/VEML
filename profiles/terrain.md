# The `terrain` profile

**Status:** specified 2026-10-09. Registry entry: draft §10, listed since the
first draft as "tracking OMI's glTF work"; this page is what graduated from
that line.

A world's ground, when it is too large to be one glTF asset. A hillside, a
valley, a landscape exported from an authoring tool: a visitor stands on it,
walks it, and sees as much of it as the distance warrants. Core already
covers ground that fits in one file: an `asset` is walkable, and that is how
most worlds are built. This profile is for the ground that does not fit, and
it borrows the mechanism the `planet` profile uses for a whole planet's
surface, an **OGC 3D Tiles 1.1 tileset**, placed in a flat world's frame. The
two profiles describe the same kind of file in the same way, so a host that
bakes one bakes the other.

No new geometry format is minted (§0.6). glTF describes the ground and its
materials; 3D Tiles describes how much of it to fetch for where the visitor
stands; this profile says where the tileset sits and what in it is solid.

## Declaration

```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/4.0"
      xmlns:terrain="http://www.fivesqd.com/schemas/veml/4.0/profiles/terrain"
      version="4.0">
  <metadata>
    <title>Valley</title>
    <profile>terrain</profile>
  </metadata>
  <environment>
    <background color="#87ceebff"/>
    <entry-point id="default">
      <position x="0" y="2" z="0"/>
    </entry-point>
    <terrain:terrain src="tiles/tileset.json"/>
    <entity tag="cabin">
      <transform><position x="40" y="12.5" z="-80"/></transform>
      <asset src="assets/cabin.glb"/>
    </entity>
  </environment>
</veml>
```

## Semantics

A document declaring `<profile>terrain</profile>` and carrying a
`terrain:terrain` element in its `environment` has **streamed ground**: a
tileset the shell fetches by how large each tile's error would look on
screen, so near ground is fine and far ground is coarse, and the visitor
stands on the most refined terrain loaded under them.

### `terrain:terrain`

| attribute | type | default | meaning |
|---|---|---|---|
| `src` | URI | required | the tileset's root JSON, resolved against the document's URL, under the §1.4 scheme allowlist |

A document has at most one `terrain:terrain`; a second is ignored with a
warning (`veml:terrain:duplicate`). A missing or disallowed `src` gives no
terrain, with a warning (`veml:terrain:invalid`). The element takes no
transform of its own: the tileset's origin is the world's origin, so an
author places everything else in the frame the terrain was baked in.

### The tileset

The rules are the `planet` profile's, restated for a flat frame so a shell
written against one serves the other:

- **Its frame.** A tileset's coordinates are right-handed with z up. A
  tileset point (x, y, z) is the world's point (−x, z, −y) (§4.2: left-handed,
  +y up, metres). Tile content is glTF, which 3D Tiles first turns from y-up
  to z-up, so a glTF position (x, y, z) in a tile lands at (−x, y, z), exactly
  as §4.4 places a glTF asset.
- **Bounding volumes** MUST be `box` or `sphere`. A `region` volume describes
  Earth's ellipsoid and means nothing in a flat world; a tile that uses one is
  skipped, with a warning (`veml:terrain:region-volume`).
- **Refinement** MUST be `REPLACE`. A tile marked `ADD` is treated as
  `REPLACE`, with a warning (`veml:terrain:refine-add`). `geometricError` is
  in metres. A shell picks tiles by how large their error would look on
  screen; the threshold is the shell's.
- **On demand.** A tile's content may be an external tileset. A shell MUST
  bound how deep external tilesets nest, at least 32 deep
  (`veml:terrain:tileset-depth` past it), and MUST NOT enter a tileset already
  on the current path (`veml:terrain:tileset-cycle`).
- **Content** is glTF. Other tile formats are skipped, with a warning
  (`veml:terrain:content-type`).
- **What is solid.** A glTF material is identified by its `name`. `ground` is
  solid, and a visitor stands on it. `water` is not solid; where the `water`
  profile is also declared, it is that profile's surface. Any other name is
  drawn and is not solid: a canopy, a fence, a distant mountain backdrop.
- **Standing.** A visitor stands on the most refined terrain a shell has
  loaded under them. Where a tile is answered 404, its parent stays drawn over
  that area. Until any terrain has loaded under an entry point, §5's
  provisional placement applies: the visitor is held at the entry point's
  height and settles onto the ground when it arrives, refined at most once.

Answers a shell can rely on, for the tileset and every tile: as the `planet`
profile states them (200 content; 204 or an empty glTF, nothing there; 503 or
429, not ready, retry with backoff; 404 and other 4xx, no such thing, asked
once). Diagnostics use this profile's prefix (`veml:terrain:content-missing`,
`veml:terrain:content-failed`).

Terrain tiles are assets, not subsidiary documents. §6.2's reference budgets
do not count them; a shell bounds them by distance and by how many it fetches
at once. Every file is fetched with CORS: the host answers with
`Access-Control-Allow-Origin`.

### Terrain and the rest of the world

Terrain is ground, not a container. Entities are placed in the world frame as
always; an author who wants a cabin on a hill writes the hill's height into
the cabin's transform, as the example does. Nothing is parented to the
terrain, and the terrain never moves. `bounds` (§4.8) and `lod` (§4.10) are
for entities; the tileset carries its own detail levels and needs neither.

## Degradation

1. **A shell without this profile.** `terrain:terrain` is an unrecognized
   element in a foreign namespace; §2 requires it to be ignored with a
   warning. The world loads with its core entities and no ground under them
   where the terrain would have been. Authors are **RECOMMENDED** to give the
   entry point solid core ground (a `primitive` plane, a small `asset`) or to
   accept that such a shell shows the world from the entry point's height,
   held there by §5's provisional placement.
2. **No usable tileset** (unreachable, not JSON, a root with no tiles): no
   terrain, with a warning (`veml:terrain:tileset-unavailable`). The world
   loads as in case 1.
3. **A tile that fails.** Its parent stays drawn; the shell asks again as the
   answers above say. A visitor never falls through a hole a missing tile
   left: the coarser parent is solid where the child would have been.

**A shell that implements this profile MUST NOT emit `veml:2:unknown-element`
for `terrain:terrain`** (see `sky.md`, Degradation, for why that needs
saying). Its other diagnostics follow §2's form:
`veml:terrain:profile-not-implemented` where a shell knows the profile and
does not implement it.

## Conformance

Structural (§10): a document with the profile and a reachable tileset has
walkable ground under its entry point once its first tile has loaded, and
emits no `veml:2:unknown-element` for `terrain:terrain`. Network: the shell
fetches the root tileset once; it fetches no tile whose bounding volume is
outside the distance it bounds by; a 404 tile is asked for once and its
parent remains. A shell without the profile fetches nothing under `src` and
warns once.

## Client story

A shell implements this profile with the loader it already has for glTF and
one more module: a tileset walker that chooses tiles by screen-space error,
fetches their content, places it in the world frame, and marks `ground`
meshes walkable. The `planet` profile needs the same walker over a sphere;
the two share it. Until a shell has the walker it declares nothing and the
world degrades as above.

## Schema

Profile vocabulary is in its own namespace,
`http://www.fivesqd.com/schemas/veml/4.0/profiles/terrain`, accepted by the
core schema's `environment` extension point (`xs:any namespace="##other"
processContents="lax"`, §3.2). A schema for the namespace is deferred, as for
`sky`.

## Migration from 3.x

3.x's `terrain` entity carried its own vocabulary: a heightmap, up to several
material layers with textures and masks, a size. None of it is carried here.
A 3.x heightmap terrain is **baked** to a tileset by a tool (the heights to
geometry, the layers to glTF materials named `ground`), which is the same
work a planet host does, and the document names the result. §11 says so;
this page is where the vocabulary it points at is defined.

## Bounds

Outside this profile, and named so the line is visible:

- **Editing the ground** (digging, raising, painting). Read is sessionless;
  writing to terrain is a `planet` surface scheme's business or a later
  revision.
- **Heightmap and layer vocabulary** in the document. The ground is baked
  geometry; its materials live in the glTF.
- **Vegetation, scatter and grass** as terrain features. They are entities or
  tile content.
- **Collision shapes other than the mesh.** A visitor stands on `ground`
  geometry as drawn.
- **A planet.** A world that is a sphere is the `planet` profile.
