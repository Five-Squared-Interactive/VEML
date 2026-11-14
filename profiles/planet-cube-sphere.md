# The `cube-sphere/1` surface scheme

**Status:** specified 2026-10-09. An optional surface scheme of the `planet`
profile (`planet.md`, Surface schemes), named
`https://www.fivesqd.com/schemas/veml/4.0/profiles/planet/schemes/cube-sphere/1`.

A planet using this scheme lays its surface out as six square grids of
blocks, one per face of a cube puffed out onto the sphere. Parts of the grid
are **areas**, each a square of blocks that a visitor can walk among,
delivered as chunks of 16 × 16 × 16 blocks. Everywhere else, and from a
distance, the planet is its terrain tileset (`planet.md`, Terrain). A host
MAY let a visitor dig and place an area's blocks (**Editing**, below).

Key words MUST, MUST NOT, SHOULD and MAY are as in RFC 2119. Numbers and
lists are written as in `planet.md`.

## The description's `surface`

```json
"surface": {
  "scheme": "https://www.fivesqd.com/schemas/veml/4.0/profiles/planet/schemes/cube-sphere/1",
  "tilesPerFaceEdge": 32,
  "tileBlocks": 1227,
  "areasPerFaceEdge": 2,
  "altitudeZeroY": -0.5,
  "areas": [
    { "slot": "px,0,1", "chunks": "area/7f3a9c/chunk/{x}_{y}_{z}.glb" }
  ]
}
```

| field | meaning |
|---|---|
| `tilesPerFaceEdge` | T: a power of 2 |
| `tileBlocks` | B: blocks along a tile edge, a positive integer |
| `areasPerFaceEdge` | area slots along a face edge, dividing T |
| `altitudeZeroY` | the block y of altitude 0, the same in every area |
| `areas[]` | the slots that hold an area (`slot` is `face,ax,az`), and each area's chunk template |

`faceBlocks` = T × B, and an area slot is W = (T / `areasPerFaceEdge`) × B
blocks on a side. `faceBlocks` MUST be at most 2^24, so that every block
coordinate is exact in a 32-bit float, as glTF stores positions. Templates are RFC 6570 level 1, with `{x}`, `{y}` and `{z}` replaced
by decimal integers and `{style}` by the style the shell draws the ground in
(`planet.md`, **Styles**), and they stay on the description's origin
(`planet.md`); for example `area/7f3a9c/chunk/{x}_{y}_{z}.glb?style={style}`.
The chunks of one area in two styles are the same chunks, drawn differently:
a shell changing style keeps each chunk drawn, stood on and handed over from
(**The hand-over**) until the same chunk in the new style arrives. A shell MAY stop reading `areas` after the first 100,000. A
slot named twice keeps its first entry. Unknown fields are ignored.

An area a host lets visitors edit gives four more fields: `region`,
`nodes`, `icons` and `node` (**Editing**, below).

## The mapping

**Faces** are named `px nx py ny pz nz`. **Grid** (gx, gz) on a face is in
[0, `faceBlocks`) × [0, `faceBlocks`), in blocks, and grid block (i, k) covers
[i, i + 1) × [k, k + 1). A **grid position** is written `face,gx,gz` or
`face,gx,gz,alt`, with alt in metres above the radius.

**From the grid to the sphere.** Grid (gx, gz) is face coordinate
u = −1 + 2·gx/`faceBlocks`, v = −1 + 2·gz/`faceBlocks`, and the cube point is:

| face | cube point (x, y, z) |
|---|---|
| `px` | (1, v, −u) |
| `nx` | (−1, v, u) |
| `py` | (u, 1, −v) |
| `ny` | (u, −1, v) |
| `pz` | (u, v, 1) |
| `nz` | (−u, v, −1) |

The cube point's unit direction is (x·√(1 − y²/2 − z²/2 + y²z²/3),
y·√(1 − x²/2 − z²/2 + x²z²/3), z·√(1 − x²/2 − y²/2 + x²y²/3)). That is in a
right-handed frame with +y towards the north pole. In the planet frame
(`planet.md`, left-handed) it is (x, y, −z), and its latitude and longitude
follow from `planet.md`'s formula.

**From the sphere to the grid.** A direction belongs to the face of its
largest component by magnitude (in the right-handed frame above), taking x
before y before z on a tie. Its grid position is the one the forward mapping
sends to it. There is no closed form, so a shell finds it numerically to
within 10⁻⁴ of a block, for example by Newton's method from the gnomonic
estimate. The faces meet along their edges as the table implies.

## Areas and chunks

- **An area's blocks.** Block (x, y, z) of the area in slot (face, ax, az) is
  grid block (ax·W + ⌊W/2⌋ + x, az·W + ⌊W/2⌋ + z), centred at grid position
  (face, ax·W + ⌊W/2⌋ + x + ½, az·W + ⌊W/2⌋ + z + ½,
  y − `altitudeZeroY`), and spans ±½ about that centre. An area's
  (x, y, z) is left-handed with +y up; x grows with gx and z with gz.
- **A chunk** is the area's blocks with lowest corner (x, y, z), each a
  multiple of 16, as one glTF 2.0 binary from the area's template. A glTF
  position (x, y, z) in a chunk, after its nodes' transforms, is the area's
  point (−x, y, z), as §4.4 places any glTF asset. A host SHOULD put the
  chunk's corner in a node translation and keep the vertices relative to
  it, so they stay exact in 32-bit floats. The answers a shell relies on are `planet.md`'s,
  and a bad template is ignored with a warning
  (`veml:planet:cube-sphere-invalid`).
- **Materials.** Beyond `planet.md`'s `ground` and `water`, this scheme's
  chunks use `leaves`, which is solid (a visitor stands on it), and `plants`,
  which is not.
- **The slot.** A shell draws an area's blocks only inside its slot. A chunk
  at a slot's edge can hold blocks beyond it, and those belong to the next
  slot.
- **A column's chunks** are those whose y range meets the block heights
  between the `minY` and `maxY` of the finest terrain tile the shell has
  loaded over the column.
  A shell asks for those, and for others above and below where it needs them.
- **The hand-over.** Where chunks are drawn, the terrain tile beneath is cut
  away column by column, and only once every one of that column's chunks has
  arrived (200, or 204 or an empty glTF). A column with a chunk that did not
  (missing, refused, or not ready) keeps its tile, so a visitor never falls
  through a hole. Tile edges and chunk
  edges need not line up, because B may be any positive integer and ⌊W/2⌋
  need not be a multiple of 16.
- **One surface.** A host serving this scheme samples its terrain tiles inside
  an area from the same ground as the area's chunks, so the two agree on shape
  and on where water runs. A host gives each tile its `minY` and `maxY` in
  block y, in the `extras` of the glTF node that holds the tile's mesh.
- **Standing.** A visitor stands on a chunk's `ground` and `leaves` where
  chunks are drawn, and on the terrain elsewhere.

## Editing

A planet publishes who may write with `edit` (`planet.md`, **The planet
description**). This scheme is where a visitor's session writes: to a
node of an area's blocks, by digging or placing one.

### An area's edit fields

An area the host lets visitors edit gives four fields beyond `slot` and
`chunks`, each a string, all four together or none:

| field | meaning |
|---|---|
| `region` | the name the area's changes are told under (**The change notice**, below) |
| `nodes` | a URI template of a chunk's nodes (below), from the description's own origin |
| `icons` | a URI template of a block's picture (PNG, its side), from the description's own origin |
| `node` | a URI template of the node a session writes at (**Writing a node**, below) |

`nodes` and `icons` MUST stay on the description's origin, as `planet.md`
asks of every URI a surface scheme adds. `node` is the one exception: it
names the planet's own write server, which need not be the host serving
the description's files — but every area's `node` MUST be on the same
origin, the planet's **write origin** (`planet.md`, **Signing in to
write**), and that origin MUST be `https`, except on `localhost`,
`127.0.0.1` or `[::1]`. `node` MUST also end in `/area/<the area's
region>/node/{x}_{y}_{z}` — a **node route** — with no query or
fragment: a shell sends a pass (`planet.md`, **Signing in to write**) to
no other URL on the write origin, so a planet cannot read it back from
somewhere a pass was never meant for. An area whose `node` is on another
origin than the first area's, or fails either rule, offers no editing for
that area. An area missing any of the four fields is likewise read as
offering no editing for it; a shell still draws it, from `slot` and
`chunks`, as it would without `edit`.

An editable area MAY give a fifth field, **`wake`**: the URI a session
asks the area to start at, and to keep running (**Waking an area**,
below). Its `wake` MUST be on the write origin and MUST be exactly the
area's node route with `node/{x}_{y}_{z}` replaced by `wake` — that is,
ending in `/area/<the area's region>/wake` — a **wake route**, with no
query or fragment. A `wake` that fails either rule is read as absent;
the area's editing is unaffected. The node routes and the wake routes
are the only URLs on the write origin a shell sends a pass to.

### A chunk's nodes

Fetching `nodes` at a chunk's lowest corner (`{x}_{y}_{z}`, the same
16-block chunk **Areas and chunks** defines) answers:

```json
{ "names": ["air", "stone", "stone-ramp"], "nodes": "<base64>", "variant": "<base64>", "passable": ["air"], "partial": ["stone-ramp"] }
```

| field | meaning |
|---|---|
| `names` | the chunk's own names, by index. A name is an opaque string the host chooses; a shell compares names exactly, and never translates or guesses one |
| `nodes` | 4096 indices into `names`, little-endian uint16, base64: index (z·16 + y)·16 + x is the block at the chunk's corner plus (x, y, z), x fastest |
| `variant` | 4096 bytes, base64, the same index: an opaque byte per block, meaningful to the host (such as its facing); a shell MAY ignore it, and echoes it only where this spec says to |
| `passable` | the names in this chunk a pointer passes through without stopping (air, a liquid) |
| `partial` | optional: of `names`, the ground's part-blocks — less than a whole block (a ramp, a flat part-block, a shore cap) — that building against makes whole (**Writing a node**, below). Absent is read as none |
| `replaceable` | optional: of `names`, the blocks a placed block takes the place of (a plant, a part-block all but flat), rather than being built beside (**Writing a node**, below). Absent is read as none |

*Informative:* a name per block, and a byte of per-block state alongside
it, is a model common to block-grid engines, not invented for this
scheme. Using it here lets a host serve the blocks it already stores
without translating them to another model. This scheme names no such
engine and takes nothing else from one.

The answers a shell relies on are `planet.md`'s (**Answers a shell can
rely on**). A malformed `nodes` or `variant`, or an index past `names`, is
read as `air` there, with a warning (`veml:planet:cube-sphere-invalid`).

**Pointing at a block.** A block spans its node position ± 0.5 in the
area's blocks. The shell points at the first block, within reach, that
the eye's ray passes through for a length — an edge or a corner it only
touches is passed over.

### Writing a node

A session write, to the node `node` names.

- `PUT <node URI>` places a block: body `{ "node": "<name>", "variant":
  <0–255>, "from": [x, y, z], "on"?: { "at": [x, y, z], "node": "<name>" } }`.
  `variant` defaults to `0`.
- `DELETE <node URI>` digs it: body `{ "from": [x, y, z] }`.
- `from` is where the visitor's eye is, in the area's blocks, at the
  moment of the write.
- A place pointed at a node the chunk's nodes document lists in
  `replaceable` goes to that node's own URI, with `If-Match` naming it:
  the placed block takes its place, as a block set over a plant does in a
  block-grid engine. A host lists there only nodes it will let a place
  take the place of; a place at any other occupied node is the host's to
  refuse (`403 refused`, below).
- `on` (optional, a place only): the block the place is built against,
  and the name the visitor's shell saw there. `on.at` MUST be exactly
  one face away from the place — the sum of the absolute differences of
  its three coordinates from the place's position is `1`. A malformed
  `on`, or one that is not a face away, is refused: `400 { "reason":
  "invalid" }`. A shell sends `on` only for a node the chunk's nodes
  document lists in `partial`.
- **`Authorization: Bearer <pass>`** is REQUIRED: a pass from `edit.issuer`
  (`planet.md`, **Signing in to write**), never the visitor's own sign-in.
  The host verifies it offline against the pass issuer's published keys
  (RS256 or ES256, the algorithm taken from the key, never the token).
  `iss` MUST be `edit.issuer` exactly — a pass of any other issuer is
  refused, whatever it names as its audience. `exp` is required; `exp`
  and `nbf` are held to a small clock skew. The pass MUST also be for the
  host's own client: its `client_id` if it has one, else its `azp`, else
  one of its `aud` values, matched against a client the planet registers
  with its pass issuer (`edit.signIn.clientId` is the one every shell
  sends as the pass's audience; a host MAY register more than one). The
  writer is `<pass issuer>#<sub>`, the pass's own, pairwise subject — not
  the visitor's identity at the issuer they signed in at first, and not
  necessarily the same at another planet even for the same visitor
  (`planet.md`, **Signing in to write**). Reading a chunk's nodes
  stays sessionless.
- **`If-Match: "<name>"`** is REQUIRED: the node the visitor's shell
  last saw there, quoted. A write against a stale view changes nothing.
- A preflight (`OPTIONS`) is answered for `PUT, DELETE` with
  `authorization, content-type, if-match`, and every answer, the
  preflight included, carries `Access-Control-Allow-Origin: *`: a
  visitor writes with their own pass, never a cookie.
- A body over 1 MiB is refused without being read in full.
- Where a place names `on` and the host applies it, the host MAY also
  make the block at `on.at` whole — its material's own whole block —
  in the same edit, naming the result in its answer's `filled` (below).
  It does this only where that block is still the one `on.node` named
  and the host's own rules let the writer have it; otherwise the block
  is left as it was, and the place itself still stands: a fill that
  cannot be made never refuses the place. **A shell MUST NOT assume a
  fill happened unless the answer names `filled`.** A shell building
  against a `partial` node SHOULD show it whole at once, and SHOULD let
  that go where the answer has no `filled`, or the place is refused.
- Answers:

| status | body | when |
|---|---|---|
| 200 | `{ "applied": true, "node": "<name now there>", "filled"?: "<name>" }` | applied; `filled` names the block `on` pointed at, once it was made whole |
| 202 | `{ "accepted": true }` | not answered within the host's wait; the write stays queued, and shows once the area's map does |
| 400 | `{ "code": "INVALID_RECORD" }` or `{ "reason": "invalid" }` | no `If-Match`, or a malformed body |
| 401 | `{ "code": "UNAUTHENTICATED" }` | no pass, one that does not verify, one not from `edit.issuer`, or one not issued to the planet's own client |
| 403 | `{ "reason": "not-allowed" \| "protected" \| "refused" }` | the planet's policy; the area's own protection; refused with no further reason |
| 404 | `{ "reason": "no-such-region" }` | not an area this planet writes to |
| 412 | `{ "reason": "changed", "node": "<name there>" }` | the node there is not the one `If-Match` named |
| 413 | `{ "code": "INVALID_RECORD" }` | the body is over 1 MiB |
| 422 | `{ "reason": "out-of-reach" \| "not-offered" }` | the block, or where `from` says the visitor stands, outside the planet's reach, outside the area's own blocks, or farther from the writer's last place than the host believes they could have moved since; or a block the planet does not offer (never `air`) |
| 429 | `{ "reason": "rate" }` | over a rate the host sets and does not publish |
| 503 | `{ "reason": "region-not-running" }`, `{ "reason": "region-ground-unknown" }` or `{ "code": "UNAVAILABLE" }` | the area is not running, and is being woken; the area's own ground is not known, so it takes no edits at all; or nothing answers for the planet's areas |
| anything else | | a failure, treated as 503 |

A shell that writes fires the action **`planet.edit`** on the world's
action bus (VEML core §8.1), whatever the answer:

| field | type | meaning |
|---|---|---|
| `op` | `"dig"` \| `"place"` | what was asked |
| `x`, `y`, `z` | integers | the node, in the area's blocks |
| `node` | string | the node dug, or placed |
| `result` | `"applied"` \| `"accepted"` \| `"refused"` | the 200 or 202 above; or refused |
| `filled` | string, optional | present only where a place's `on` named a block that was made whole |
| `reason` | string, with `refused` | `not-allowed`, `protected`, `out-of-reach`, `not-offered`, `changed`, `rate`, `sign-in`, `unavailable`, `region-not-running`, or `refused` |

A script hears it with `world.actions.on('planet.edit', handler)` (VEML
core §8.3). The shell gives `sign-in` for a write it never sent because
no pass verified (401 above), and `unavailable` for one a 5xx answered
or nothing answered at all; every other reason is the write's own, from
the status table above.

### Waking an area

A host may let an area that no one is writing to stop, and start it
again when it is written to: a write meanwhile is answered `503
region-not-running` while the area starts (above). An area with `wake`
lets a shell start it before the visitor's first write, and keep it
running while they are there.

- `POST <wake URI>`, with no body (a body is ignored), and the same
  **`Authorization: Bearer <pass>`** a write sends, verified the same way
  (**Writing a node**, above).
- A shell holding a pass for the planet SHOULD send it when the visitor
  arrives in the area, and again while they stay in it, at least every
  five minutes. It MUST NOT send it more than once a minute for one area,
  and sends it only while it holds a pass: never a visitor's own sign-in,
  and never to an area it is not in.
- Waking changes no node, and is no write: no action is fired for it.
- A preflight (`OPTIONS`) is answered for `POST` with `authorization`,
  and every answer carries `Access-Control-Allow-Origin: *`.
- Answers:

| status | body | when |
|---|---|---|
| 202 | `{ "waking": true }` | the area is not running, and is being started |
| 204 | | the area is running; it is kept running, as a write would keep it |
| 401 | `{ "code": "UNAUTHENTICATED" }` | as for a write |
| 403 | `{ "reason": "not-allowed" }` | the planet's policy does not let this writer edit |
| 404 | `{ "reason": "no-such-region" }` | not an area this planet writes to |
| 429 | `{ "reason": "rate" }` | over a rate the host sets and does not publish |
| 503 | `{ "reason": "region-ground-unknown" }` or `{ "code": "UNAVAILABLE" }` | as for a write |

A shell treats every answer but 202 and 204 as nothing to act on: it
neither tells the visitor nor retries before its next wake. A host that
never stops an area gives no `wake`, and a shell sends nothing.

### The change notice

An area with `region` tells its changes in the planet's `changes` room
(`planet.md`): one application message per chunk a save actually
changed (by name and `variant`, whoever changed it), naming
**`planet/area/<region>/chunk/<x>/<y>/<z>`** (in chunks) and
carrying **`{ "area": "<region>", "chunk": [x, y, z], "version": <n> }`**,
`version` growing with each save of that chunk. A chunk saved for
the first time is told only once an edit has lately applied in it; one
never written to is never told.

Only the planet's own host writes to this room; it holds the one
credential the room takes as its writer. A shell MAY join and be told
every change, but MUST NOT write to it itself: a change notice is never
a shell's to send, and the room refuses one that tries.

Each notice stands alone. A shell acts on every one it receives, in any
order and across any gap in the room's sequence, by fetching that
chunk's `nodes` and `chunks` again (no cache answers with the old
content) and the far tiles over it, keeping each drawn as it was until
its new self has arrived — but not more than once per newer `version` of
a chunk: a notice for one already being fetched again is acted on
once more, after the fetch under way finishes, not once for every
notice that arrived meanwhile.

## Things that move

An area with `region` MAY also tell what moves in it, such as vehicles, in
the planet's `changes` room. It sends one application message per area at a
time, naming **`planet/area/<region>/objects`** and carrying the area's
whole list of moving things at one moment:

```json
{
  "area": "<region>",
  "at": 1793638779149,
  "objects": [
    { "id": "w42", "kind": "locomotive", "pos": [12.5, 8.0, -3.25], "yaw": 1.5708, "pitch": 0, "vel": [-6, 0, 0], "box": [-0.9, -0.5, -2.4, 0.9, 2.5, 2.4] }
  ]
}
```

| field | meaning |
|---|---|
| `at` | when the list was true, in milliseconds since 1970 by the host's clock; it only ever grows for an area |
| `objects[].id` | a string naming the thing while it lasts; the same thing keeps its `id` from one message to the next |
| `objects[].kind` | a string naming what sort of thing it is, as the host names it |
| `objects[].pos` | its place, in the area's block coordinates (**Areas and chunks**): continuous, with a block's centre at whole numbers |
| `objects[].yaw`, `objects[].pitch` | how it is turned, in radians: first about its own x axis by `pitch`, then about +y by `yaw`. A positive `pitch` turns +z toward +y, and a positive `yaw` turns +z toward −x. Each defaults to 0. |
| `objects[].vel` | optional: its velocity, in blocks a second, in the area's coordinates |
| `objects[].box` | optional: its extent, `[x1, y1, z1, x2, y2, z2]` in blocks about `pos`, in its own frame before it is turned. Default `[-0.5, -0.5, -0.5, 0.5, 0.5, 0.5]`. |

- **Each message is the whole list.** A thing missing from a message has
  gone. An empty `objects` says nothing moves in the area. A message with an
  `at` no later than the last one a shell took for that area is stale, and a
  shell ignores it.
- **How often.** A host tells an area's things as often as it chooses, but
  not more than 10 times a second. After the last thing in an area has gone,
  it sends one empty list, then nothing until something moves there again.
- **Drawing.** A shell that draws them draws each thing as its `box`, turned
  and placed as above, under the area as the area's chunks are, unless it
  draws something it knows for the `kind`. Between messages it SHOULD move
  each thing smoothly from where one message put it to where the next does,
  drawing a little behind the latest message rather than jumping. The
  world's client hints `things`, `things-color` and `things-glide` say
  whether they are drawn, in what colour, and whether they glide. A shell
  that has heard nothing for an area for 5 seconds removes that area's
  things: the host may have stopped the area.
- **What is told is what moves.** A host tells the things it chooses, and a
  shell treats `kind` as a name only. A thing a shell does not recognise is
  still drawn as its box.
- As for change notices, only the planet's own host writes these. A shell
  MUST NOT write them, and the room refuses one that tries.

## Drawing near the viewer

A face grid does not lie flat on a sphere. Drawn exactly through the mapping,
blocks change size and shape across a face: on a face whose blocks are about
a metre wide along its centre line, a block is about 0.9 m square at the
centre and 1.27 × 0.74 m at the middle of an edge, and blocks shear near the
cube's corners. (Blocks are a metre tall everywhere: altitude is in metres.) So near the viewer, a shell
SHOULD draw **all ground** in grid geometry: the areas, the terrain tiles
around them, and sites. That means one block one metre square, no stretch,
with the grid mapped onto the sphere around the viewer's own grid position,
and the map recomputed as they move. The error goes into the distance, where
it cannot be seen. Drawing everything near the viewer through one map keeps
areas, tiles and sites in step, with no crack at an area's edge.

From altitude, the planet is drawn through the mapping. A shell blends the
two over a band of height above the ground. Near a cube's corners, where
three faces meet and no grid lies flat, a shell MAY draw through the mapping
throughout.

## Grid positions in links

A fragment's `at` (`planet.md`, Arrival) MAY be a grid position:
`#at=px,9800,21800`. Its first field is a face name, not a number, which is
how a shell tells it apart. A shell that implements this scheme arrives
there. One that does not treats it as an invalid `at`.

## Degradation

- A shell without this scheme ignores `surface` and draws the planet from its
  terrain tileset (`planet.md`, Degradation case 2).
- An invalid `surface` (T not a power of 2 or past its bound, an
  `areasPerFaceEdge` that does not divide T, an invalid slot) is ignored with
  a warning (`veml:planet:cube-sphere-invalid`), and the planet is drawn from
  its terrain tileset.
- A shell that implements this scheme but not **Editing** ignores an
  area's `region`, `nodes`, `icons` and `node`, without a warning, and
  offers no editing: the area still draws and is stood on as above.
- A shell that does not implement **Things that move** ignores the room's
  `planet/area/<region>/objects` messages, without a warning. The area's
  blocks draw as before; only the things that move are missing.

## Bounds

- **Editing anything but an area's own blocks** — the terrain tileset, a
  site, or another area's `region`. Each area's write is its own.
- **Drawing styles** other than blocks, such as smoothing natural ground.
- **Collision** other than standing on the drawn chunks.
- **A model for a thing that moves**, such as a glTF per `kind`: a thing is
  drawn as its box, or as what a shell already knows for its `kind`.
- **Standing on or riding a thing that moves**: a thing is drawn, not stood on.
