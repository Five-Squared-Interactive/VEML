# The `planet` profile

**Status:** specified 2026-10-09.

A document declaring this profile **is a planet world**: a sphere a visitor
can stand on, fly over and see whole from orbit, in one zoom with no change
of world. A planet world carries other worlds on its surface (**sites**) and
other planet worlds in orbit (**bodies**, such as moons). Any world may
include planet worlds, so a solar system is an ordinary world that includes
its planets. A visitor can fly to any of them and land.

The profile assumes nothing about how a planet is made. It defines what every
planet shares: a sphere, places named by latitude, longitude and altitude,
and terrain as an OGC 3D Tiles tileset. How a planet's surface is laid out
in detail, for example as blocks a visitor can dig, is a **surface scheme**
that the planet names and a shell may implement. A shell that does not know a
planet's scheme still shows the whole planet, flies over it and stands on its
terrain.

Everything a shell reads is a plain file fetched over HTTP.

Key words MUST, MUST NOT, SHOULD and MAY are as in RFC 2119.

## Declaration

```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/4.0"
      xmlns:planet="http://www.fivesqd.com/schemas/veml/4.0/profiles/planet"
      version="4.0">
  <metadata>
    <title>Test planet</title>
    <profile>planet</profile>
  </metadata>
  <environment>
    <background color="#8cb8e8ff"/>
    <planet:planet src="planet.json" radius="25000" reach="30000" spawn="12.5,-30"/>
    <planet:entry id="harbour" at="12.48,-30.1" look="270,-10"/>
    <planet:site href="https://alice.example/harbour/world.veml"
                 at="12.481,-30.102,12.5" heading="90" radius="150"/>
    <entity tag="moon">
      <transform><position x="0" y="0" z="140000"/></transform>
      <include href="https://worlds.example/moon/world.veml"/>
    </entity>
  </environment>
</veml>
```

## The planet

### `planet:planet`

| attribute | type | default | meaning |
|---|---|---|---|
| `src` | URI | required | the planet description (below), resolved against the document's URL |
| `radius` | number, metres, > 0 | required | the sphere's radius at altitude 0 |
| `reach` | number, metres, ≥ 0 | `radius` | how far above altitude 0 the planet holds a visitor (**Held by a planet**) |
| `spawn` | `lat,lon` | `0,0` | where a visitor arrives when nothing else says (**Arrival**) |
| `day-length` | number, seconds, ≥ 0 | none | how long the planet takes to turn once under its sun; `0` holds the sun still (**The planet's day**) |
| `time` | number, hours, in [0, 24) | `12` | the hour at longitude 0 at the clock's epoch (**The planet's day**) |
| `sun-latitude` | number, degrees, in [−90, 90] | `0` | the latitude the sun stands over at noon: the planet's season |
| `tide-period` | number, seconds, > 0 | half of `day-length`; none on a still day | the time from one high tide to the next (**The planet's day**) |

Everything a shell needs to place an arriving visitor on this planet is on
this element, so it can do so before fetching anything (§3.4); an `on` link
to a planet deeper in the world is the one exception (**Arrival**). A document has at most one
`planet:planet`; a second is ignored with a warning
(`veml:planet:duplicate`). A missing or invalid `src` or `radius` gives no
planet, and an invalid `reach`, `spawn`, `day-length`, `time`,
`sun-latitude` or `tide-period` takes its default; each warns
(`veml:planet:invalid`).

### The planet's day

The hour and the tide are the planet's, so every visitor sees the same sun
and the same water. A shell keeps no clock of its own for them and talks to
no one: it reads them from the visitor's own clock. Let *t* be the seconds
since 1970-01-01T00:00:00Z (UTC) by that clock.

- **The hour.** The sun stands still in the planet's frame, and the planet
  turns under it eastward about its axis (+y, the north pole). The hour at
  longitude 0 is (`time` + 24 · *t* / `day-length`) modulo 24; at a place
  it is an hour later for every 15° east of there. With `day-length="0"` it
  is always `time`.
- **The season.** The sun stands over latitude `sun-latitude` at noon.
- **The tide.** The water stands highest when *t* / `tide-period` is a whole
  number and lowest half a period later, the same over the whole planet. How
  far it falls is the visitor's (client hints, `tide`). On a still day
  (`day-length="0"`) with no `tide-period`, the sea stands still too: there
  is no tide.

Two visitors see the same hour and the same tide to within the difference
between their clocks. A planet that declares no `day-length` declares no day:
the hour, and a tide with no `tide-period`, are each shell's to choose, and
visitors may not agree. Changing the day while a world is open is outside
this revision.

### Numbers and positions

In this profile a **number** is written `-?[0-9]+(\.[0-9]+)?`: decimal, with
no exponent, infinity or NaN. Lists are comma-separated, with no spaces, so
that they can stand in a URL fragment unescaped. (Core VEML's vectors are
space-separated; this profile's are not.)

A **geographic position** is `lat,lon` or `lat,lon,alt`: latitude and
longitude in degrees, and altitude in metres above `radius`. Latitude must be
in [−90, 90], or the position is invalid. Longitude is taken modulo 360 into
(−180, 180].

### The frame

A planet document's frame is the planet's: its origin at the centre, VEML's
coordinates (§4.2: left-handed, +y up, metres), and **+y the north pole**. A
geographic position's point in the frame is d × (`radius` + alt), where the
unit direction is

d = (cos lat · cos lon, sin lat, cos lat · sin lon).

At a position, **up** points away from the centre, **north** is the
direction of increasing latitude, and **east** the direction of increasing
longitude. At the poles, north is the limit along the position's longitude.

The **local frame** at a position follows the compass VEML's `sky` profile
uses, in which a world's north is its −z: the local −z points north, −x
east, and +y up. It is left-handed, like the document's frame.

A **heading** is degrees clockwise from north, seen from above: 0 north, 90
east.

### What is placed how

- **On the surface**, things are placed by geographic position: the terrain,
  sites and `planet:entry`. They stand on the ground from every distance.
- **Off the surface**, core entities are placed in the frame, as in any world:
  stations, satellites and bodies. They are drawn where the frame puts them
  and do not follow the ground. Nothing on the surface is placed as a core
  entity.

### What an included planet world contributes

§6.2 says an included document contributes its entity content. A planet
world's `planet:planet`, `planet:site` and `planet:entry` are content in that
sense, so they come with it. While the planet holds a visitor, its own
`locomotion` (§5.2) applies, which is an exception to §6.2's rule that a
child's `locomotion` is ignored.

Its own `hints:planet` (client hints) come with it as well, because they
describe that planet: its air, its clouds, its water, its terrain style. A
`hints:planet` attribute the included planet world declares draws that
planet, whether or not it holds the visitor; one it leaves out is the root
world's. So a moon with `air="none"` is drawn airless in a system whose
planet has air. The included document's other client hints are not its to
set, and are ignored. The visitor's own settings still come above all of
them, as client hints' order of precedence says.

## Terrain

The description's `terrain` names an **OGC 3D Tiles 1.1 tileset**: the
planet's surface, from one coarse tile for the whole sphere down to the
finest detail its host provides.

- **Its frame.** A tileset's coordinates are right-handed with z up. A
  tileset point (x, y, z) is the planet frame's point (−x, z, −y). Tile
  content is glTF, which 3D Tiles first turns from y-up to z-up, so a glTF
  position (x, y, z) in a tile lands at (−x, y, z), exactly as §4.4 places a
  glTF asset. In tileset coordinates, the direction of (lat, lon) is
  (−cos lat · cos lon, −cos lat · sin lon, sin lat). A tileset written with
  Earth's convention (longitude 0 on +x) is 180° of longitude off unless its
  host accounts for this.
- **Bounding volumes** MUST be `box` or `sphere`. A `region` volume describes
  Earth's ellipsoid; a tile that uses one is skipped, with a warning
  (`veml:planet:region-volume`).
- **Refinement** MUST be `REPLACE`. A tile marked `ADD` is treated as
  `REPLACE`, with a warning (`veml:planet:refine-add`). `geometricError` is in
  metres. A shell picks tiles by how large their error would look on screen,
  so zooming is continuous at every distance.
- **On demand.** A tile's content may be an external tileset, so a host can
  hand out the tree as the visitor approaches and bake it on first request.
  A shell MUST bound how deep external tilesets nest, at least 32 deep
  (`veml:planet:tileset-depth` past it), and MUST NOT enter a tileset already
  on the current path (`veml:planet:tileset-cycle`).
- **Content** is glTF. Other tile formats are skipped, with a warning
  (`veml:planet:content-type`).
- **What is solid.** A glTF material is identified by its `name`. `ground` is
  solid, and a visitor stands on it. `water` is not solid; the sea's surface
  is `water`. Any other name is drawn and is not solid, unless a surface
  scheme says otherwise.
- **Standing.** A visitor stands on the most refined terrain a shell has
  loaded under them. Where a tile is answered 404, its parent stays drawn
  over that area.

### Answers a shell can rely on

For a tileset, a tile, or a surface scheme's content:

- **200**: the content.
- **204**, or a glTF with no meshes: nothing to draw there. A host on static
  storage writes the empty glTF.
- **503** or **429**: not ready yet. The shell keeps what it has, a coarser
  tile included, and asks again after `Retry-After` when given. Otherwise it
  backs off, waiting at least 1 second the first time and doubling each
  time, up to at least a minute.
- **404**, and any other 4xx except 429: there is no such thing. The shell does not ask
  again (`veml:planet:content-missing` for anything but 404).
- Anything else: a failure, warned (`veml:planet:content-failed`) and treated
  as 503.

Terrain and scheme content are assets, not subsidiary documents. §6.2's
reference budgets do not count them; a shell bounds them by distance and by
how many it fetches at once. Every file is fetched with CORS: the host
answers with `Access-Control-Allow-Origin`.

## Sites: worlds on the surface

### `planet:site`

| attribute | type | default | meaning |
|---|---|---|---|
| `href` | URI | required | the VEML document placed here |
| `at` | geographic position | required | where the site's origin stands |
| `heading` | number, degrees | `0` | the site's turn, clockwise seen from above |
| `radius` | number, metres, > 0 | required | the site's declared bounds: a shell uses it to decide when to load the site |
| `integrity` | SRI hash | none | as §6.2's `include` |

A site is an include (§6.2) placed by geographic position instead of a
transform. Its document's frame is the local frame at `at`, turned clockwise
by `heading`: its origin at `at`, its +y up, and its −z facing `heading`, so
at `heading="0"` the site's north is the planet's. A site's document is
ordinary VEML. It needs no profile and knows nothing of the planet, so one
file can stand on two planets or be a world of its own.

A site is drawn rigid and upright at `at`. Where a shell draws the ground
near the viewer differently from the frame (a surface scheme may ask it to,
so that blocks stay square), it draws sites the same way, so they stay on the
ground beneath them.

`radius` is a declaration in §4.8's sense: a shell decides when to load a
site from it, and a site whose content reaches past it costs performance,
never correctness. Loaded content is not clipped to it.

Every §6.2 rule applies to a site (relative URIs, any origin, its own
metadata and origin, cycles, depth, deduplication, integrity, no authority
crossing in either direction), with one change. **A planet may declare any
number of sites.** §6.2 caps a document's eligible references so that it
cannot make a shell fetch without bound. For sites the bound is spatial
instead:

- a shell MUST load only sites whose `radius` reaches within its load
  distance of the viewer;
- it MUST bound how many sites it holds loaded at once — at least `16`,
  never above the tree-wide cap below — and unloads the farthest first;
- sites do not count against a document's 16 eligible references, and the
  tree-wide cap of 64 counts loaded sites, not declared ones.

The load distance, and how many a shell holds up to that cap, are the
world's to set (client hints, `site-distance`, `site-limit`, `16`–`64`);
without either, 2000 m and 16 are the shell's own defaults. `site-limit`
never raises the tree-wide cap: a world asking for more than 64 gets 64.

Sites may be declared in the document and in the description's `sites` list;
both behave the same. A shell MAY stop reading a description's `sites` after
the first 100,000, with a warning (`veml:planet:too-many-sites`).

**A site holds no planet.** A site's document, and every document it
includes, MUST NOT be a planet world; such a document is omitted with a
warning (`veml:planet:planet-in-site`). A site's document MAY itself
include further documents (§6.2); a shell is no more required to resolve
them than it is the site itself, and MAY defer them for exactly as long
as it defers the site (§6.2's own latitude to defer-load).

## Bodies: planets in orbit

A planet world may include other planet worlds, its moons, with core
`include` on an entity (§6.2). Any world that is not a planet world may
include planet worlds the same way. That is how a solar system places its
planets.

- **Placement.** The included planet's centre is at the entity's origin, and
  its frame's axes are the entity's. The entity's scale, and any scale above
  it, does not apply: a planet is drawn at its own size, with a warning
  (`veml:planet:scaled`).
- **Carried along.** A body moves with whatever includes it, and a moon with
  its planet when a solar system includes the planet.
- **Its arrival does not apply** (§6.2): a visitor reaches a body by
  travelling to it, or by a link (**Arrival**).
- Bodies nest only as deep as §6.2's include depth allows.

## Held by a planet

A planet's **hold sphere** is its centre out to `radius` + `reach`.

- **Nested or apart.** Across a whole world, any two hold spheres MUST be
  either nested (one wholly inside the other) or apart (not meeting), and
  every hold sphere MUST lie wholly outside the `radius` of every planet
  whose hold sphere contains it. A shell checks this when planets load, and
  whenever an entity that carries a planet moves. A planet that breaks it,
  the later in the include tree's depth-first order or the one that moved, is not drawn and holds
  no one, with a warning (`veml:planet:body-overlaps`), until it no longer
  breaks it.
- **Held.** A visitor is held by the smallest hold sphere that contains
  them. While held, gravity points to that planet's centre at the strength
  its own `locomotion` gives, or the shell's default. They stand on that
  planet's terrain, and their position is kept in its frame, so a moving or
  turning parent carries them along.
- **Leaving** a hold sphere, the visitor is held by the next one out.
- **Outside every hold sphere**, the visitor is in the root world's frame,
  with no gravity unless the root world is not a planet world and its own
  `locomotion` gives some. A planet's gravity, the root planet's included,
  acts only on visitors it holds.
- **Flying.** A shell MAY let a flying visitor's speed grow with their
  height over the ground. Where it does, the speed is `fly-speed`
  (client hints) times that height, a second, never less than the
  shell's own flying speed with no world's opinion. The height is above
  the terrain of the planet holding the visitor; outside every hold
  sphere, it is above the nearest planet's own surface — the least
  distance from any planet's centre, less that planet's `radius`.
  Where there is no planet to measure a height from, the speed is the
  shell's own flying speed. A shell MAY fly slower than this, though never
  slower than its own flying speed, while the terrain where the visitor
  looks has not yet arrived, and back up to it once it has, so that a
  visitor never outruns the ground they fly toward.
- A shell MUST NOT confine a visitor so tightly that a planet in their world
  cannot be reached. Height bounds that a shell applies to worlds without
  planets do not apply in a world with one. Below, a planet's terrain bounds
  the visitor.

## Arrival

A world URL's fragment (§5) may name a spot on a planet. It is either an
entry id, percent-decoded as §5 says, or `&`-separated `name=value` pairs.
Pairs are split on `&` and on each pair's first `=` **before** each name
and value is percent-decoded, so a value may carry an encoded URL.

| pair | meaning |
|---|---|
| `at=lat,lon` or `at=lat,lon,alt` | the spot, a geographic position; with `alt`, metres over altitude 0, a place in the air |
| `look=heading,pitch` | facing: a heading, and a pitch in degrees above the horizon, in [−90, 90] |
| `on=URL` | which planet, when the root world includes planets: the planet world's own document URL, absolute, percent-encoded |

A fragment is read as pairs when its raw text, before any decoding, begins
with `at=`, `on=` or `look=`. Any other fragment is an entry id. An entry
whose `id` itself begins that way is named by percent-encoding its `=`
(`#on%3D…`). A pair whose value has a malformed escape or decodes to invalid
UTF-8 is invalid, with a warning (`veml:planet:unknown-fragment`). In pairs, a name
this profile does not define is ignored, and a repeated name keeps its first
value.

**Naming a planet with `on`.** A planet is named by its own absolute URL,
the one it would be opened by on its own, and never by where it sits in
another world: every world has its own URL, and no world is found by walking
through others. The value is parsed by the URL Standard, as §1.4
parses every URI, and MUST be an absolute http or https URL; one that is not
names no planet. The named planet is the planet world, anywhere in the root
world's include tree, whose **document URL** serializes to the same string as
the value, both without their fragments. A document URL is the URL the
document was finally retrieved from after redirects, which is how this
profile reads §1.4's retrieval URL. So an author writes the planet's final
URL: a link written to a URL that redirects does not match. Where it
is included more than once, the first in the tree's depth-first order is
meant. Its parents can be renamed or rearranged, and the
link still finds it while it is anywhere in the tree.

When a world loads as the root world, the visitor arrives at the first of
these that applies:

1. **The fragment.**
   - Pairs: on the planet named by `on`, or on the root planet world when
     there is no `on`, at `at`, facing `look`. Without `at`, at that planet's
     own arrival (steps 2 to 4 below). While an `on` planet is not yet found,
     the shell loads the tree's eligible core includes, deferred ones too
     (§6.2), within §6.2's budgets, until it finds it or they are all
     loaded. It does not search sites, which never hold a planet.
   - An invalid `look` is ignored, with a warning
     (`veml:planet:unknown-fragment`), and the arrival goes ahead without it.
   - `at` without `on` in a root world that is not a planet world: a
     warning (`veml:planet:unknown-fragment`), and the visitor arrives by the
     world's own entry points (§5).
   - An `on` planet that is not found once the tree is loaded: a warning (`veml:planet:unknown-fragment`), and they
     are not moved.
   - An entry id: a `planet:entry` with that id, then a core `entry-point`
     with it (§5).
   - An unknown entry id, an `on` that names no planet, or an invalid `at`
     warns (`veml:planet:unknown-fragment`) and continues with step 2.
2. A `planet:entry` named `default`.
3. A core `entry-point` named `default`. If its position is inside the
   planet's hold sphere, the visitor arrives on the ground below its
   direction from the centre, and its rotation is not used (a position within
   1 m of the centre is skipped). Outside the hold sphere, as a station or a
   ship might be, the visitor arrives at the entry point itself, as §5 says.
4. `planet:planet`'s `spawn`.

In a root world that is not a planet world, steps 2 to 4 do not apply to its
planets. The world's own entry points place the visitor (§5), and a fragment
with `on` lands them on a planet.

**An `on` link needs fetches.** The planet it names is found only once the
documents that include it have arrived, which §3.4's fetch-free arrival and
§6.2's "fragment navigation targets the root" do not allow for. This profile
makes an explicit exception to both. The visitor first arrives where the root
world's own arrival places them, and is moved once to the named spot when
the planet is found and every eligible include earlier in the depth-first
order, queued and deferred ones included, has loaded or failed, so that the move is to the planet the link means, as a comfort transition in the manner of §5, never a
silent hard teleport. A link with no `on` needs no fetch.

Except for step 3's arrival outside the hold sphere and an `at` with an
`alt`, the visitor's feet arrive **on the ground** at the arrival's latitude
and longitude. With an `alt`, their feet arrive at that altitude, flying
where the shell lets them fly; an `alt` under the ground there arrives on
the ground over it once the terrain has come. A shell that keeps the
visitor's place in the address (§5) writes an `alt` while they fly and none
while they stand, so that a link arrives where they were. Until
terrain there has arrived, the shell holds the visitor where they are, never letting them fall and never waiting for a bake. When finer
terrain moves the ground, the shell MAY refine their height once, as §5
allows.

### `planet:entry`

| attribute | type | default | meaning |
|---|---|---|---|
| `id` | string | required | the name a URL fragment uses |
| `at` | `lat,lon` | required | where the visitor's feet arrive, on the ground |
| `look` | `heading,pitch` | `0,0` | facing |

Two `planet:entry` elements with one `id`: the first wins, with a warning
(`veml:planet:duplicate-entry`). An `id` used by both a `planet:entry` and a
core `entry-point` names the `planet:entry`.

**Links.**
- A spot on a planet world opened directly:
  `https://worlds.example/test-planet/world.veml#at=12.48,-30.1&look=270,-10`.
- A spot on a moon of a planet included by a solar system:
  `https://worlds.example/sol/world.veml#on=https%3A%2F%2Fworlds.example%2Fmoon%2Fworld.veml&at=0.7,23.4`.
  The same spot without the solar system around it is the moon's own link:
  `https://worlds.example/moon/world.veml#at=0.7,23.4`.

The server never sees a fragment, so links work on any static host and do
not affect caching. A surface scheme MAY define another form of `at`, which a
shell tells apart because its first field is not a number. A shell that does
not know that form treats it as an invalid `at`.

## The planet description

A JSON document, `src`. URIs in it resolve against its own URL. Fields it
does not know are ignored (Principle 7).

```json
{
  "format": "planet/1",
  "seaLevel": 0,
  "floor": -130,
  "terrain": "terrain/tileset.json?style={style}",
  "styles": ["smooth", "blocks"],
  "surface": { "scheme": "https://www.fivesqd.com/schemas/veml/4.0/profiles/planet/schemes/cube-sphere/1" },
  "sites": [
    { "href": "https://bob.example/station/world.veml", "at": "40,100,30", "radius": 300 }
  ]
}
```

| field | meaning |
|---|---|
| `format` | `planet/1`. A shell refuses a format it does not know, with a warning (`veml:planet:unknown-format`). |
| `seaLevel` | optional: the sea's altitude, in metres |
| `floor` | optional: the lowest altitude that holds terrain, in metres |
| `terrain` | the URI of the terrain tileset |
| `styles` | optional: the styles the host draws its ground in, as names, its own style first (**Styles**) |
| `surface` | optional: a surface scheme, and its own fields |
| `sites[]` | optional: more sites, with a `planet:site`'s attributes as fields: `href`, `at` and `integrity` as strings, `heading` and `radius` as numbers |
| `edit` | optional: who may write to the planet's surface, and how they sign in (below) |
| `changes` | optional: the room the planet tells writes in, and what else its surface scheme tells there (below) |

The description is for visitors. It holds no server addresses, routing or
credentials. The terrain, and every URI a surface scheme adds, MUST stay on
the description's origin; one that does not is refused with a warning
(`veml:planet:other-origin`). Serving them from another operator's host is a
later revision, because it needs rules of trust between operators. Sites are
includes, and may be on any origin (§6.2).

### Styles

A host MAY draw a planet's ground in more than one style: smoothed, with
slopes as slopes, or as whole blocks, say. `styles` lists the names of the
styles it serves, its own first. A name is lower-case letters and digits in
hyphen-joined words (`[a-z][a-z0-9]*(-[a-z0-9]+)*`). This profile names two:

- `smooth`: the ground's slopes drawn as slopes.
- `blocks`: the ground drawn as whole blocks, each column's top at the
  ground's height there, rounded to the block.

A host may name others. A shell ignores an entry that is not a style's name,
and a name listed again after its first.

The terrain URI, and any URI a surface scheme names its content by, may hold
the RFC 6570 level 1 variable `{style}`. A shell fills it with the style it
draws: the one the world asks for (client hints, `terrain-style`) where
`styles` lists it, else the first listed. A description with no `styles` has
no style to fill, and `{style}` is filled with nothing. A shell that does not
know styles fills `{style}` with nothing too, as RFC 6570 level 1 expands an
undefined variable, so a host SHOULD serve its own style where the style is
empty, for example `terrain/tileset.json?style={style}`.

The tilesets of one host's styles SHOULD have one shape: the same tiles at
the same places, differing only in their content. A shell changing style
while a world runs then lets the tiles of the old style stand in, place for
place, for the new style's tiles until they arrive, as a coarser or finer
tile stands in for another. A shell MUST NOT leave ground undrawn while a new
style's content arrives.

A style is how the ground is drawn, not what the world is: every style shows
the same world, and an edit to it shows in each.

### Editing: `edit` and `changes`

```json
"edit": {
  "policy": "signed-in",
  "issuer": "https://passes.example",
  "reach": 5,
  "blocks": "blocks.json",
  "signIn": { "clientId": "the-planets-client" }
},
"changes": { "type": "worldsync", "address": "wss://sync.example/…", "session": "planet" }
```

| field | meaning |
|---|---|
| `edit.policy` | `signed-in`: any visitor holding a pass from `issuer` may write. `allow-list`: only identities the host lists, which this description never publishes. |
| `edit.issuer` | the **pass issuer** whose passes the host takes for writes (**Signing in to write**, below). Any pass issuer; none is special. |
| `edit.reach` | optional: metres a visitor reaches, for a surface scheme that reads it. Default `5`. |
| `edit.blocks` | a URI, resolved against the description, of `{ "blocks": [names…] }`: the blocks a visitor may place, named as a surface scheme names them. |
| `edit.signIn` | optional: `{ "clientId": "<the planet's client id at its pass issuer>" }` (**Signing in to write**, below). |
| `changes.type` | an open protocol identifier, as core VEML's own synchronization declaration (§7); `worldsync` is one such protocol. |
| `changes.address`, `changes.session` | the room's endpoint and session name, as §7's `address` and `session`. |

A planet that lets nobody write publishes no `edit`. A description that
offers `edit` without both `policy` and `blocks` present, each a value
above allows, is read as offering none (Principle 7): a shell that finds
no usable `edit` offers no editing, with no warning.

### Signing in to write

`edit.issuer` is a **pass issuer**: the one party whose tokens a planet
ever takes for a write. A pass issuer does not sign a visitor in itself;
it mints a **pass**, a short-lived token made for one planet and its
write origin, from a sign-in the visitor already holds elsewhere. A
planet never receives, and a shell never sends it, anything but a pass —
never the visitor's own sign-in, which may reach far more than this one
planet. `edit.signIn`, where given, is `{ "clientId": "<the planet's
client id at its pass issuer>" }` and nothing else — a shell ignores any
other member.

**Where a visitor signs in, and which pass issuers may be trusted with
that sign-in, are the shell's own** — set by whoever deploys or embeds
it, and never by a world, a planet, or a pass issuer. A shell holds an
upstream issuer and its own client there, and the pass issuers it trusts
with that issuer's tokens, each with the exact token endpoint it
exchanges at. A shell's sign-in configuration (its upstream issuer,
client and trusted pass issuers) comes from the shell's own deployment
or its embedder, and MUST NOT be taken from anything resolved against
the world document's URL or from any path a world's author controls. A
host that serves a shell's sign-in and authors' files on one origin MUST
serve the authors' files so that none of them runs as that origin (for
example with the Content-Security-Policy `sandbox` directive). A shell MUST
NOT take an upstream issuer, a client, or a pass issuer's token
endpoint from a planet, a world, or a pass issuer's own metadata. A
planet whose `edit.issuer` is not among a shell's trusted pass issuers
gets no editing in that shell, and the shell SHOULD tell the visitor so;
it MUST NOT fetch anything from such a pass issuer, nor send it any
token.

Before offering sign-in for a planet, or sending any write to it, a shell
fetches `<the planet's write origin>/.well-known/planet-writes.json`,
`{ "descriptions": ["<absolute URL of a planet description>", …] }` (the
write origin is defined below). It MUST NOT offer sign-in for, or send a
write on behalf of, a description whose own URL is not listed there
exactly; a failure to fetch or read the list vouches for nothing.
Otherwise a description anywhere could copy an honest planet's pass
issuer, client id and write origin, and have its own visitors edit the
honest planet in their names.

A shell that offers editing on a planet whose pass issuer it trusts:

1. Discovers its own upstream issuer's endpoints — OpenID Connect
   discovery (`<issuer>/.well-known/openid-configuration`), else OAuth
   2.0 Authorization Server Metadata (RFC 8414) — believed only when the
   document's own `issuer` is that upstream issuer exactly and its
   endpoints are `https` (plain `http` allowed only on `localhost`,
   `127.0.0.1` or `[::1]`, for a host on the visitor's own machine). It signs the
   visitor in there with the authorization code grant and PKCE (S256),
   as its own client, returning to a page of its own choosing, on its
   own origin. It takes the code back only from that page's origin, and
   only with the `state` it sent.
2. Keeps the token it redeems (the **subject token**) to itself, under
   the upstream issuer and its own client: never in a world's reach,
   never sent to any planet.
3. Exchanges it for a **pass**, at the trusted pass issuer's own
   declared token endpoint only — never one discovered from the pass
   issuer's metadata, nor named by a planet — with a form POST (OAuth
   2.0 Token Exchange, RFC 8693 §2.1) of
   `grant_type=urn:ietf:params:oauth:grant-type:token-exchange`,
   `subject_token=<the subject token>`,
   `subject_token_type=urn:ietf:params:oauth:token-type:access_token`,
   `audience=<edit.signIn.clientId>` and `resource=<the planet's write
   origin>`. The answer is RFC 8693 §2.2: the pass as `access_token`,
   with `expires_in`. `invalid_target` means the pass issuer makes no
   pass for that planet at that write origin.
4. Exchanges with a pass issuer only once the visitor has chosen, by an
   action of their own in the current tab, to edit a planet of that pass
   issuer. After that, while the subject token lasts, it MAY exchange
   again without asking — a lapsed pass renewed, or a further planet of
   the same pass issuer.
5. Keeps the pass **for that planet alone** — under the pass issuer, its
   client and the write origin together — and sends it as
   `Authorization: Bearer` only to a node route or a wake route
   (`planet-cube-sphere.md`, **An area's edit fields**) on that write
   origin, never to another
   planet's, even one naming the same pass issuer and client id.

A pass issuer MUST:

- mint a pass only for an `audience` it lists, and only for a `resource`
  it lists for that `audience`;
- take as a subject token only a live token of its own upstream issuer,
  issued to a client it lists, or to that issuer's own sign-in (which
  names no client), and never one of its own passes;
- sign a pass with a key named in its `jwks_uri`, by that key's `kid`. A
  pass is a JWT of `iss` (the pass issuer), `sub`, `aud` (the planet's
  client), `iat`, `exp` and `jti`, and nothing else;
- make `sub` **pairwise**: stable for one visitor at one planet, and
  different at every other planet, never the visitor's account id at
  the upstream issuer (OpenID Connect's pairwise subject identifiers are
  the model). It therefore names a visitor only as that one planet sees
  them: a host MUST NOT treat it as the visitor's identity anywhere
  else, and MUST NOT expect it to match across planets even for the
  same visitor. An `allow-list` (above) is written in a planet's own
  names.

A pass issuer publishes OAuth 2.0 Authorization Server Metadata
(RFC 8414): `issuer`, `jwks_uri`, `token_endpoint`, and
`grant_types_supported` naming
`urn:ietf:params:oauth:grant-type:token-exchange`. A planet, not a
shell, reads it — for `jwks_uri` alone, to verify the passes it takes
(`planet-cube-sphere.md`, **Writing a node**). It says nothing of where
a visitor signs in, and a shell takes nothing from it for trust.

A planet's **write origin** is the one origin every write a surface
scheme defines goes to; `cube-sphere/1` takes it from `node`
(`planet-cube-sphere.md`, **An area's edit fields**), which MUST
likewise be `https` (the same loopback exception). Without `signIn`, a
shell offers no sign-in of its own and sends no write.

A planet's document never names where a visitor signs in, nor where a
sign-in or a pass goes, and a pass issuer's own metadata says nothing of
sign-in either: a shell that takes either from the document or from
there, rather than from its own configuration and the write origin
above, is not conforming. A host takes no part in signing a visitor in
beyond trusting its pass issuer; it serves no return page of its own.

What may be written, and how, is each surface scheme's own; a scheme that
defines no writing offers none even where `edit` is published.
`cube-sphere/1` (`planet-cube-sphere.md`, **Editing**) does.

`changes` names the room a shell MAY join to hear of writes other
visitors made, and of anything else the planet's surface scheme tells
there, such as things that move; one that does not join it still draws
what it fetches itself. Only the planet's own host writes to this room; a
shell MUST NOT write to it. What a surface scheme tells there, and how a
shell acts on it, is that scheme's own.

## Surface schemes

A surface scheme adds what a particular planet's surface is made of, beyond
its terrain tileset: blocks a visitor can dig, a grid, finer content near the
viewer. A scheme is **named by an absolute URL** (`surface.scheme`), which its
author controls, as a namespace is. Nobody needs to ask anyone to define one.
A scheme's document says what its fields in `surface` mean and what a shell
does with them. It MUST keep the planet usable without it: terrain, sites and
arrival work in a shell that ignores the scheme.

A shell that does not implement `surface.scheme` ignores `surface`.
`planet-cube-sphere.md` is a scheme document, and an optional extension of
this profile.

## Degradation

1. **A shell without this profile.** The planet elements are unrecognized
   elements in a foreign namespace, ignored with a warning (§2). The world
   loads with its core entities: no ground, no sites. An author who wants such
   a shell to show something MAY give the world a viewpoint of its own.
2. **A shell without the planet's surface scheme.** The planet is drawn and
   stood on from its terrain tileset, and sites stand on it. Only the
   scheme's own content is missing.
3. **No usable description** (unreachable, not JSON, an unknown format): no
   terrain, with a warning (`veml:planet:description-unavailable`,
   `veml:planet:unknown-format`). The visitor arrives as above, over an empty
   sphere.
4. **An invalid site or entry** (no `href`, an invalid `at`, `heading`,
   `look` or `radius`): omitted, with `veml:planet:invalid-site` or
   `veml:planet:invalid-entry`.
5. **`planet:site` or `planet:entry` in a document without `planet:planet`:**
   ignored, with `veml:planet:not-a-planet`.

**A shell that implements this profile MUST NOT emit
`veml:2:unknown-element` for the elements it implements** (see `sky.md`,
Degradation, for why that needs saying).

## Schema

The vocabulary is in its own namespace,
`http://www.fivesqd.com/schemas/veml/4.0/profiles/planet`, accepted by the
core schema's `environment` extension point (`xs:any namespace="##other"
processContents="lax"`, §3.2). A schema for the namespace is deferred, as for
`sky`.

## Bounds

Outside this revision, and named so the line is visible:

- **Content on another operator's host.** It needs trust rules between
  operators.
- **Who may view a world.** Restricting a world to the people its owner gives
  a link to is a general mechanism for every world, not this profile's.
- **Shapes other than a sphere** (ellipsoids, irregular bodies).
- **Writing to a planet beyond a surface scheme's own ground.** Reading a
  planet stays sessionless. `edit` says who a host lets write and where
  they sign in; what may be written, and how, is each surface scheme's
  own (`planet-cube-sphere.md`, **Editing**, is one). Writing to the
  terrain tileset itself, or to a site, is outside this revision.
- **Caves and interiors** beyond what a surface scheme provides. The tileset
  is the surface.
