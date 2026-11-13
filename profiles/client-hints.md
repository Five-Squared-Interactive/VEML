# The `client-hints` profile

**Status:** specified 2026-09-27 (#425) — the fourth profile to graduate
from §12's deferred "profile specifications themselves", after `lighting`,
`sky` and `environment`. Registry entry: draft §10, *"successor of 3.0's
controlflags"*.

A shell brings things a world never declared: a way to look like a person
to the others, a name over each head, chat, on-screen controls on a phone,
a settings menu, and VR controls. This profile is where a **world** says
how it wants those things: on, off, or replaced with its own. The founder's
direction, 2026-09-27: *"we want webverse to support these things, but it
goes through the veml/js apis. should not be hardcoded in"*.

Two things this profile is not:

- **It is not the visitor's settings.** A visitor's own choices (their name,
  where chat sits on their screen, how fast they turn) belong to the
  visitor, and a hint sits **under** them. A world may prefer chat hidden; a
  visitor who opens it has opened it.
- **It is not the network.** Every hint applies to the visitor whose shell
  reads it. Nothing here is sent to the others in the room, and nothing
  here changes the WorldSync grammar.

Hints are **author preferences, not commands**, in exactly the sense of
§5.1's `navigation`. A shell SHOULD honour them and MAY substitute where the
visitor's access or safety requires it. As with `modes="none"`, no hint may
take away the visitor's way to leave.

## Declaration

```xml
<metadata>
  <profile>client-hints</profile>
</metadata>
```

Profile vocabulary lives in its own namespace, so a core-schema validator
accepts a document that uses it without knowing this profile (see
**Schema** below):

```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/4.0"
      xmlns:hints="http://www.fivesqd.com/schemas/veml/4.0/profiles/client-hints"
      version="4.0">
  <metadata>
    <title>Gallery</title>
    <profile>client-hints</profile>
  </metadata>
  <environment>
    <background color="#20242cff"/>
    <hints:hints>
      <hints:people avatar="avatars/guide.glb" labels="show" label-height="2.1" tint="none"/>
      <hints:chat enabled="true" panel="hide"/>
      <hints:controls touch-buttons="jump chat" menu="show"/>
      <hints:vr turn="smooth" stick-motion="true" left-pointer="teleport" right-pointer="ui"/>
      <hints:planet zoom="on" air="9000" clouds="0.4 0.2" cloud-altitude="1500 6000" wind="6 20" cloud-color="#f4f6ffff"/>
      <hints:placements src="placements.json" edit-bar="auto"/>
    </hints:hints>
  </environment>
</veml>
```

## Semantics

A document declaring `<profile>client-hints</profile>` and carrying a
`hints:hints` element in its `environment` states the world's preferences
for the features below. Every child and every attribute is optional. An
absent attribute means **the shell's own default**, and the defaults below
are the behaviour of a shell for a world that says nothing, so
`<hints:hints/>` changes nothing. A document carries at most one
`hints:hints`; a shell uses the first and warns about the rest.

The order of precedence, lowest first: the shell's defaults, then these
hints, then the **visitor's own** settings, then an embedder's settings (a
page hosting the shell). A script's `ClientHints.Set` (below) changes the
world's layer, so it too sits under the visitor.

### `hints:people`

How the other people in the room are drawn, for this visitor.

| attribute | type | default | meaning |
|---|---|---|---|
| `avatar` | URI, as `asset`'s `src` (§4.4): absolute, document-relative, or a `manifest:` name | the shell's own default avatar | the model drawn for a person who brings none |
| `labels` | `show` \| `hide` | `show` | a name over each person |
| `label-height` | number `0`–`10`, metres above the feet | just above the avatar's head | where the name sits |
| `tint` | `per-person` \| `none` | `per-person` | colour each person's avatar distinctly, or draw the model as authored |
| `own-avatars` | `show` \| `hide` | `show` | draw a person who brings an avatar in it, or draw everyone in `avatar` |

`avatar` names a binary glTF (`.glb`), placed with its **feet at the origin
and facing +Z**. If it carries animation clips named `idle` and `walk`, a
shell SHOULD play `walk` while the person moves and `idle` while they stand;
without them the model is drawn still. It is fetched under the same rules as
an `asset`, the §1.4 scheme allowlist included. A model that cannot be fetched or
parsed falls back to the shell's default, with a warning. This is an
avatar **for the world**, the same for everyone the visitor sees.

**A person may bring their own avatar.** WorldSync carries it with them as
the `avatar` of their roster entry (WorldSync protocol §4.6). It meets the
same contract as `avatar`: feet at the origin, facing +Z, `idle` and
`walk`. With `own-avatars="show"`, a shell draws a person who brings one in
it. With `own-avatars="hide"`, everyone is drawn in `avatar`, so a world
that wants one look for everyone keeps it. A brought avatar is fetched
under the same rules as `avatar`, and only over `https:`. A shell MAY
refuse one larger than a size it sets. One that is refused, or cannot be
fetched or parsed, falls back to `avatar`, then to the shell's default,
with a warning, and the person is still drawn.

`tint="per-person"` is how a shell tells people apart when everyone wears
the same model. The colour goes on the model's material named **`tint`**,
or on the whole model when it has no material by that name. So an author
keeps a part its own colour (eyes, for instance) by giving the body the
`tint` material and the part another. With `tint="none"` the model is drawn
exactly as authored. A person's own avatar is never tinted: it is drawn in
the colours its owner chose.

### `hints:chat`

| attribute | type | default | meaning |
|---|---|---|---|
| `enabled` | boolean | `true` | visitors can send and read chat lines in this world |
| `panel` | `show` \| `hide` | `show` | the messages panel (who is here, who joined, chat) starts shown or hidden |

`enabled="false"` removes the shell's chat line, its chat key and its chat
button for this world. It does not stop the shell telling the visitor that
someone joined or left. `panel="hide"` only sets the panel's starting state
in this world: a visitor who shows the panel has shown it.

### `hints:controls`

| attribute | type | default | meaning |
|---|---|---|---|
| `touch-buttons` | space-separated list from `jump` `chat` `fly`, or `none` | `jump chat fly` | which on-screen buttons a touch shell draws beside its sticks |
| `menu` | `show` \| `hide` | `show` | the shell's settings menu button |

The list says which buttons the world *wants*; each one still obeys what
governs it. `fly` appears only where §5.1's `modes` allows flying (or the
visitor's own override does), and `chat` only where `hints:chat` leaves chat
enabled. Unknown tokens are ignored with a warning. The sticks are not in
the list: a touch shell without them cannot move, and a world that wants no
movement says so with `modes="none"`.

`menu="hide"` hides the button, not the visitor's settings. A shell MAY
offer them another way (a key, for instance), and MUST keep offering the
visitor a way to leave the world.

### `hints:vr`

The successor of 3.0's `controlflags`, carrying the four fields a shell
honoured.

| attribute | 3.0 field | type | default | meaning |
|---|---|---|---|---|
| `turn` | `turn-locomotion` | `snap` \| `smooth` \| `none` | `snap` | how the turn stick turns the visitor: 45° steps, a continuous turn, or not at all |
| `stick-motion` | `joystick-motion` | boolean | `false` | the move stick walks the visitor. Off by default, the comfort-safe baseline |
| `left-pointer` | `left-vr-pointer` | `none` \| `ui` \| `teleport` | `none` | the left hand's ray: nothing, point-and-select (portals, buttons), or aim at the floor to teleport |
| `right-pointer` | `right-vr-pointer` | `none` \| `ui` \| `teleport` | `ui` | the right hand's ray, the same choices |

These apply only while the visitor is in VR; a flat-screen shell ignores
them without a warning. The defaults are 3.0's, so a converted world
behaves as it did.

**3.0 fields not carried:** `left-vr-poker`, `right-vr-poker`,
`left-hand-interaction`, `right-hand-interaction`, `left-grab-move`,
`right-grab-move` and `two-handed-grab-move`. No 3.0 shell's successor ever
implemented them, and what they describe (poking, hand interaction,
grab-to-move) each needs a statement about hand tracking that this profile
does not make. The converter keeps them in its review comment (§11) rather
than inventing a mapping.

### `hints:planet`

How a shell presents a **planet world** (the planet profile): the view a
visitor may take of it, the editing it offers, and the air and clouds it is
drawn with.

| attribute | type | default | meaning |
|---|---|---|---|
| `zoom` | `on` \| `off` | `off` | the shell's zoom control (a mouse wheel, for instance) lifts the camera from the visitor's eyes to a view of the whole planet and back |
| `edit` | `auto` \| `none` \| `dig` \| `place` \| `both` | `auto` | what the shell offers on a planet whose description offers editing (planet profile, **The planet description**); `auto` offers digging and placing to a visitor who holds a session for the planet's issuer, and nothing to one who does not; `none` outlines nothing and sends nothing |
| `reach` | `auto` \| number `0`–`64`, metres | `auto` | how far the visitor points, for editing; `auto` is the planet's own `edit.reach`, and a larger value than the planet's is taken as the planet's |
| `blocks` | `auto` \| block names, space-separated | `auto` | the blocks offered for placing; `auto` is the planet's own list (`edit.blocks`); a name the planet does not offer is dropped, and where the shell draws no bar, the first of these is the block placed |
| `edit-bar` | `auto` \| `none` | `auto` | whether the shell draws its bar of nine blocks |
| `edit-menu` | `auto` \| `none` | `auto` | whether the shell offers its menu of every block offered |
| `edit-keys` | `auto` \| three inputs: dig, place, menu | `auto` (`mouse-left mouse-right e`) | the inputs that dig, place and open the menu: each a key, as `KeyboardEvent.key` gives it in lower case, or `mouse-left`, `mouse-middle` or `mouse-right`; a value that is not three such inputs is taken as `auto` |
| `air` | `auto` \| `none` \| number `0`–`1000000`, metres | `auto` | how high above altitude 0 the planet's air reaches, as the shell draws it: the sky, the haze over distant ground, and space past it |
| `clouds` | `auto` \| `none` \| layers of number `0`–`1` | `auto` | how many cloud layers there are, and the share of the sky each covers |
| `cloud-altitude` | layers of `auto` \| number `0`–`100000`, metres | `auto` | each layer's height above altitude 0 |
| `cloud-size` | layers of `auto` \| number `10`–`100000`, metres | `auto` | about how wide one cloud of each layer is |
| `wind` | layers of `auto` \| number `-200`–`200`, metres per second | `auto` | how fast each layer drifts eastward around the planet's axis; a negative speed drifts it westward |
| `cloud-color` | layers of `auto` \| a colour, as `primitive`'s `color` (§4.5) | `auto` | each layer's colour in full sunlight |
| `cloud-opacity` | layers of `auto` \| number `0`–`1` | `auto` | how much of the sky behind a layer's thickest cloud it hides |
| `cloud-shadow` | layers of `auto` \| number `0`–`1` | `auto` | how much of the sun's direct light a layer's thickest cloud keeps from the ground under it; `0` casts no shadow |
| `cloud-thickness` | layers of `auto` \| number `0`–`10000`, metres | `auto` | how deep each layer's clouds are, from their base up |
| `exposure` | `auto` \| number `0.1`–`8` | `auto` | how bright the view is drawn; `auto` lets the eye adapt to the light as it changes, a number holds it fixed |
| `adaptation-speed` | `auto` \| number `0.1`–`10` | `auto` | how fast the eye adapts, as a multiple of the shell's own |
| `exposure-min` | `auto` \| number `0.1`–`8` | `auto` | the least the adapting eye darkens the view |
| `exposure-max` | `auto` \| number `0.1`–`8` | `auto` | the most the adapting eye brightens it, so night stays night |
| `sunbeams` | `auto` \| `none` \| number `0`–`1` | `auto` | how strongly the sun's beams show through gaps in cloud and trees, where the visitor's device can draw them |
| `foam` | `auto` \| `none` \| number `0`–`4` | `auto` | how much foam the water shows where it is shallow at the shore |
| `foam-width` | `auto` \| number `0.01`–`5`, metres | `auto` | the depth of water over which the foam fades out |
| `caustics` | `auto` \| `none` \| number `0`–`4` | `auto` | how strongly the sun's light ripples on a shallow bed |
| `underwater-fog` | `auto` \| `none` \| number `0`–`1`, per metre | `auto` | how thick the water looks from under it; `none` leaves the view untinted |
| `underwater-color` | `auto` \| colour | `auto` | the colour the water takes from under it |
| `terrain-style` | `auto` \| `smooth` \| `blocks` | `auto` | the style the planet's ground is drawn in, near and far: its slopes as slopes, or as whole blocks (planet profile, **Styles**) |
| `things` | `auto` \| `none` | `auto` | whether the things that move on the planet's surface, where its surface scheme tells them, are drawn; `none` draws none |
| `things-color` | `auto` \| colour | `auto` | the colour a thing that moves is drawn in where it is drawn as its box |
| `things-glide` | `auto` \| `none` | `auto` | whether a thing that moves glides from one told place to the next; `none` sets it at each place as it is told |
| `site-distance` | `auto` \| number `10`–`100000`, metres | `auto` (`2000`) | how far from the eye a site's own declared bounds may be and still load (planet profile, **Sites: worlds on the surface**) |
| `site-limit` | `auto` \| number `16`–`64` | `auto` (`16`) | how many sites stand loaded at once, floored to a whole number, never under `16` and never above the profile's own tree-wide cap of `64`; the farthest let go first (planet profile, **Sites: worlds on the surface**) |
| `fly-speed` | `auto` \| number `0.1`–`10` | `auto` (the shell's own pace) | flight speed as that many times the visitor's height over the ground, a second — over the nearest planet's own surface where none holds them (planet profile, **Held by a planet**); never slower than if the world said nothing |
| `block-shading` | `auto` \| `none` \| number `0`–`1` | `auto` | how strongly blocks darken in the corners where they meet |
| `block-glow` | `auto` \| `none` \| number `0`–`2` | `auto` | how brightly blocks that give off light (lava, torches) light what is around them, seen most at night |
| `block-glow-color` | `auto` \| colour | `auto` | the colour of that light |
| `sky-fill` | `auto` \| `none` \| number `0`–`2` | `auto` | how strongly the light the sky gives the ground takes its colour from where it comes: the sky's from above, the sunlit ground's from below; `none` leaves the world's own sky light as it is |
| `block-relief` | `auto` \| `none` \| number `0`–`2` | `auto` | how much relief the near blocks' faces take from their own pictures, so the light catches their texture; `none` draws them flat |
| `grade` | `auto` \| `none` \| URI, as `asset`'s `src` (§4.4): absolute or document-relative | `auto` | the world's own colour grade: a 3D look-up table in the `.cube` format through which the shell draws the finished view; `auto` and `none` ask for no grade |
| `focus` | `auto` \| `on` \| `none` | `auto` | whether the far ground softens while the visitor's eye is high over the planet; only `on` asks for it, and `auto` and `none` keep everything sharp |
| `cloud-detail` | `auto` \| `full` \| `half` \| `quarter` | `auto` | how finely the shell draws the cloud volume, as a share of the screen's resolution; `auto` is the shell's choice for the visitor's device |
| `cloud-base-variety` | layers of `auto` \| number `0`–`1` | `auto` | how much each layer's cloud bases rise and fall from cloud to cloud; `0` is one level base across the sky |
| `cloud-volume` | `auto` \| `on` \| `off` | `auto` | whether the clouds are drawn as volumes, with depth the eye and the sun's light pass through, where the visitor's device affords it; `off` draws them flat |
| `sun-shadows` | `auto` \| `on` \| `off` | `auto` | the sun casts shadows from what stands on the planet near the visitor |
| `sky-light` | `auto` \| number `0`–`2` | `auto` | how much of the world's own sky light (its hemispheric lights) the planet's sky gives by day; less deepens the sun's shadows |
| `stars` | `auto` \| `none` \| number `0`–`1` | `auto` | how many stars the night sky holds |
| `star-seed` | `auto` \| number `0`–`1000000000` | `auto` | which sky of stars: the same seed, the same stars |
| `shadow-reach` | `auto` \| number `10`–`10000`, metres | `auto` | how far from the visitor the sun's shadows are drawn |
| `sun-flare` | `auto` \| `none` \| number `0`–`1` | `auto` | how strongly the sun flares in the visitor's eyes when it is in view and nothing stands in its way |
| `bloom` | `auto` \| `none` \| number `0`–`1` | `auto` | how strongly the brightest things glow into what is around them |
| `fog` | `auto` \| `none` \| number `0`–`1` | `auto` | how thick the planet's fog is, where and when it is thickest |
| `fog-height` | `auto` \| number `10`–`5000`, metres | `auto` | how deep the fog lies: its thickness falls by e for every `fog-height` metres above its floor |
| `fog-floor` | `auto` \| number `-10000`–`100000`, metres | `auto` | the altitude the fog lies on; `auto` leaves it to the shell (a shell might lay it on the lowest ground about the visitor, so a valley high in the hills fills as one by the sea does) |
| `haze` | `auto` \| `none` \| number `0`–`4` | `auto` | how thick the air's haze is, against the shell's own (`1`) |
| `fog-color` | `auto` \| a colour, as `primitive`'s `color` (§4.5) | `auto` | the fog's colour in full light |
| `fog-patchiness` | `auto` \| number `0`–`1` | `auto` | how patchy the fog is: `0` the same everywhere, `1` some places clear |
| `water-reflections` | `auto` \| `none` \| number `0`–`1` | `auto` | how strongly the planet's water mirrors its sky and the sun |
| `water-mirror` | `auto` \| `none` \| number `0`–`1` | `auto` | how strongly the water mirrors what stands over it, such as its shores, trees and cliffs, where the shell can see them drawn |
| `water-ripple` | `auto` \| number `0`–`4` | `auto` | how much the water's surface ripples, breaking up what it mirrors |
| `waves` | `auto` \| `none` \| number `0`–`4` | `auto` | how high the water's swell runs, as a multiple of the shell's own |
| `wave-length` | `auto` \| number `0.5`–`1000`, metres | `auto` | the distance from one crest of the swell to the next |
| `wave-direction` | `auto` \| number `0`–`360`, degrees from north | `auto` | the bearing the swell travels toward |
| `wave-speed` | `auto` \| number `0`–`4` | `auto` | how fast the swell and ripple move, as a share of the speed deep water gives a wave of their length |
| `wave-reach` | `auto` \| number `0`–`10000`, metres | `auto` | how far from the visitor the swell raises and lowers the surface itself; past it the swell shows in the water's shading only |
| `sway` | `auto` \| `none` \| number `0`–`4` | `auto` | how strongly the planet's plants and leaves sway in its wind, as a multiple of the shell's own |
| `sway-speed` | `auto` \| number `0`–`4` | `auto` | how fast they sway, as a multiple of the shell's own; `0` holds them still |
| `tide` | `auto` \| `none` \| number `0`–`50`, metres | `auto` | how far the water falls from its drawn level at low tide |
| `shallow-fall` | `auto` \| number `0`–`1` | `auto` | the most of its depth that water whose depth the shell knows falls with the tide and the swell together: `0` holds it at its drawn level, `1` lets it fall to its bed |
| `water-clarity` | `auto` \| number `0.1`–`1000`, metres | `auto` | how deep the visitor sees into the water before its bed is hidden |

`block-shading`, `block-glow` and `block-glow-color` apply where the
planet's ground is drawn as blocks. A shell drawing ground that is not made
of blocks ignores them.

`things`, `things-color` and `things-glide` say how the things that move on
the planet are drawn, where a surface scheme tells them (`cube-sphere/1`,
**Things that move**). A shell MAY let the visitor choose whether they are
drawn; their choice is then drawn in place of the world's.

`terrain-style` chooses among the styles the planet's host serves its ground
in (planet profile, **Styles**). `auto` takes the host's own style, the first
it lists. A style the host does not list is drawn as `auto`, without a
warning: the hint is the world's, the styles are its host's. The shell draws
every part of the ground in the one style, the terrain far off and any
surface scheme's content near the visitor alike, so the two meet. A shell
MAY let the visitor choose a style for themselves; their choice is then drawn
in place of the world's, as long as the host lists it, and the world's hint
applies again when the visitor leaves the choice to the world.

`site-distance` and `site-limit` narrow how the shell loads a planet's
sites (planet profile, **Sites: worlds on the surface**): how far a
site's own declared bounds may be and still load, and how many stand
loaded together. `fly-speed` sets how much faster a visitor flies the
higher they are over the ground (planet profile, **Held by a planet**),
never slower than if the world said nothing. All three are additive: a
shell that does not know them reads `<hints:planet>` exactly as before
(§2), and a world that sets one is read, by such a shell, as having set
none of them.

While zoomed, the visitor's feet stay where they were; the zoom moves only
the camera. A `zoom` turned `off` while the visitor is zoomed out returns
the camera to their eyes.

**Editing.** These hints only narrow what a planet's host allows; they
never widen it. A planet's `edit` (planet profile, **The planet
description**) decides whether a visitor may dig or place at all, and the
host refuses whatever its policy, reach or offered blocks do not allow,
whatever a hint asks for. `edit="none"` asks the shell to offer nothing —
no outline, no bar, no menu — even on a planet that offers editing; it
does not sign the visitor out. The outcome of every dig and place a
visitor makes is told to a world script as an action
(`planet-cube-sphere.md`, **Editing**).

`air="auto"` leaves the height, and whether to draw air at all, to the
shell. `air="none"` asks for no air: the shell draws the world's own
`background` and `fog` (§5) as for any world. A number asks for air
reaching that many metres; `0` is the same as `none`. The air is lit by the
world's sun, its first directional light (lighting profile), or the shell's
default light where the world declares none.

**Layers.** A planet may have several layers of clouds, one over another,
as Earth has low heaped cloud under thin high cloud. A value written as
**layers** is one entry per layer, lowest first, separated by spaces, at most
four; a colour with spaces in it (`color(…)`) is one entry. `clouds` says
how many layers there are: one entry for each. An entry of another attribute
applies to the layer at its place; a missing entry, or `auto`, leaves that
layer's value to the shell, and entries past the last layer are ignored. So
`clouds="0.5" wind="12"` is one layer drifting at 12 m/s, and
`clouds="0.4 0.2" cloud-altitude="auto 6000"` is two, the second at 6 km.
More than four entries keeps the first four, with a warning
(`veml:client-hints:out-of-range`); an entry that is not a value the
attribute takes stands as `auto`, with a warning
(`veml:client-hints:unknown-value`).

`auto` for any cloud attribute leaves that value to the shell, and
`clouds="auto"` leaves to it how many layers there are, if any.
`clouds="none"` asks for no clouds. The clouds live in the air: a planet
with `air="none"` has no clouds, whatever `clouds` says, and a shell keeps
every layer under the air's top however high `cloud-altitude` asks for it.
Sunlight lights the clouds, so a low sun darkens and warms `cloud-color`;
its alpha, where it has one, is ignored (`cloud-opacity` says how much a
layer hides). A layer's clouds shade the ground, and whatever stands on it,
from the sun's direct light, by `cloud-shadow` under their thickest part;
the sky's own light still reaches it.

**Day and night.** The hour, the length of a day and the season are the
planet's, not the visitor's: they are on `planet:planet` (planet profile,
**The planet's day**), so every visitor sees the same sun. The sun rises in
the east and sets in the west, and the side turned away from it has night.
The world's sun is its first
directional light (lighting profile), or the shell's default light where it
declares none: a shell turns it to the sun's direction and dims and colours
it by the air its light comes through, so that the light the world gives it
is the light at noon overhead, and it dims the world's sky light at night.
The stars hang in a sky that stays still while the planet turns, so they
wheel across the night as the sun crosses the day; a shell makes them from
`star-seed`, so a world's night sky is the same for every visitor.

**The sun's light.** Where `sun-shadows` is not `off`, the sun casts shadows
from what stands near the visitor, as far as `shadow-reach`, onto that and
the ground further off; a world whose sun already casts shadows (its light's
`cast-shadows`) keeps them, and the planet's own things join them. The
visitor's own shadow setting still decides whether any are drawn. The flare
shows only while the sun is above the horizon and in view, fainter through
air and cloud, and not at all with ground in the way. `sun-flare` and `bloom`
are strengths; `none` asks for none, and a shell may draw less bloom, or
none, where the visitor's device cannot afford it.

**The sky's light.** The world's own sky light (its hemispheric lights) lights
the ground's shaded sides. With `sky-fill`, a shell colours that light by where
it comes from: a face turned up takes the hue of the sky above it, a face turned
down the colour of the sunlit ground about the visitor, and a face between them
a blend of the two. It changes the light's colour, not how much of it there is,
and a face the sky does not reach, such as a cave's wall, takes none. `none`
leaves the world's sky light as it is, and a shell may draw it as `none` where
the visitor's device cannot afford it.

**Relief.** With `block-relief`, a shell draws the near blocks' faces with
relief taken from their own pictures: a block's texture reads as raised where
it is light and sunken where it is dark, so a low sun catches it. Each block's
relief comes from its own picture alone, never a neighbour's. A face of one
colour is drawn flat, as is every face at `none`, and a shell may draw it as
`none` where the visitor's device cannot afford it.

**Grade and focus.** With `grade`, a shell draws the finished view, after its
exposure, through the world's own colour table: a 3D look-up table in the
`.cube` format that colour-grading tools write, resolved as any other URI in
the document is. A table that cannot be fetched or read is ignored, with a
warning, and the view is drawn ungraded. With `focus="on"`, the far ground
softens as the visitor's eye rises high over the planet, as a lens focused on
the ground below would: never what stands near the eye, and nothing at all
while the visitor stands, walks or jumps. Neither is drawn unless the world
asks for it. A shell may draw neither where the visitor's device cannot afford
them, and may let the visitor turn the focus off, or on, for themselves.

**Fog and haze.** Both belong to the place, not to the eye: a shell sums
them along the line from the eye to what it sees. The haze is the air's own,
thinning with height as the air does, as thick as `haze` asks, and the
sky's colour the way the eye looks: brighter toward the sun, bluer away from
it. Far cloud is lost in it as far ground is. The fog lies low, pooling in hollows
and over water, thickest about sunrise and burnt off by midday, and comes in
patches as far as `fog-patchiness` asks. So the ground seen from orbit wears
the fog it wears seen from a hill, and the air around an eye in space holds
none. With `air="none"` there is neither, and the world's own `fog` (§5)
applies as for any world; while the air is drawn, it stands in for that
`fog`. Under the planet's water, the water's look is the visitor's.

**Water.** The planet's water mirrors its sky at the angle it is seen at,
more at a glance than looking straight down, and the sun as a glint, as
strongly as `water-reflections` asks; `water-ripple` breaks both up. A shell
may draw no reflections where the visitor's device cannot afford them.
With `water-mirror`, the water also mirrors what stands over it, its shores,
trees and cliffs, in place of the sky behind them, as far as the shell can see
them drawn; past that, and where nothing stands, it mirrors the sky, with no
edge between the two. `water-mirror` mirrors nothing where `water-reflections`
is `none`, and a shell may mirror only the sky where the visitor's device
cannot afford more.
The water moves: a swell travels across it as `waves`, `wave-length` and
`wave-direction` ask, at the pace `wave-speed` sets, and it falls by as much as `tide` and rises again once each tide, on the
planet's own tide clock (planet profile, **The planet's day**), so every visitor sees the same tide. Water never stands over the level the world draws it at, so
neither swell nor tide floods a bank. Where a shell knows how deep the water
is, it falls no further than `shallow-fall` of that depth: shallow water
rises and falls less, its waves keeping their shape, and never bares its bed.
Its bed fades from sight as the water deepens past `water-clarity`.

These apply only to a planet world; a shell ignores them, without a
warning, in a world that does not declare the planet profile.

### `hints:placements`

Where a world's **placements** live, and how a shell offers to edit them.
A placement is a thing a person put into a world after it was published —
a model from the world's palette at a position, rotation and scale — and
lives outside the document, in a store the world names, so that the
document stays what its author published and what people place in it
can change without republishing. The store, its wire forms and who may
write to it are **The placements description** below; this element is
how a document points at one, and the hints beside it.

| attribute | type | default | meaning |
|---|---|---|---|
| `src` | URI | none | the placements description (below), resolved against the document. Absent, the world has no placements and the other attributes mean nothing |
| `edit` | `auto` \| `none` | `auto` | what the shell offers in a world whose description offers editing; `auto` offers placing, moving and deleting to a visitor the description's `edit` allows, and nothing to one it does not; `none` offers nothing and sends nothing |
| `edit-bar` | `auto` \| `none` | `auto` | whether the shell draws its bar of palette pictures |
| `edit-keys` | `auto` \| three inputs: place, grab, delete | `auto` (`mouse-left mouse-right delete`) | the inputs that place the chosen palette item at the pointer, take hold of (and let go of) the placement under the pointer to move it, and delete the placement under the pointer; each as `hints:planet`'s `edit-keys` describes an input; a value that is not three such inputs is taken as `auto` |
| `shapes` | `auto` \| `none` | `auto` | whether the shell offers the language's own shapes (VEML core §4.5, `primitive`'s `shape`) as items to place beside the palette, in a colour the visitor chooses; `none` offers only the palette. A shape already placed is drawn either way (**The snapshot and the palette**) |
| `style` | `auto` \| a style name | `auto` | the style (**The snapshot and the palette**, **Styles**) the shell draws placements and the placing copy in: each item its model for that style, and an item without it its own `ref`; `auto` draws every item's own `ref`. A style name is a token of lower-case letters, digits and `-`, 1 to 32 characters; any other value is taken as `auto` |

**Style.** A world may offer an item in several styles, and a visitor
chooses the style they see it in. The style is the visitor's: two
visitors in different styles see the same placements, each drawn in their
own style, and nothing the style chooses is ever written. A world's
`style` is the style a visitor sees until they choose one. A shell offers
the visitor only the styles the world's items name, and their choice sits
over the world's while they are in that world. A shell does not keep the
choice past the visit: the styles are the world's, and another world may
name none of them.

**Editing.** As for a planet, these hints only narrow what the world's
store allows; they never widen it. The description's `edit` decides who
may write at all, and the store refuses whatever its policy does not
allow, whatever a hint asks for. `edit="none"` asks the shell to offer
nothing even in a world that offers editing; it does not sign the
visitor out. A shell draws placements whether or not it offers editing,
and whether or not the visitor is signed in: reading is sessionless.

**Precedence.** `src` is a fact about the world, not a preference: it is
not among the hints a visitor's settings sit over (**Semantics**), and a
`ClientHints.Set` of `placements.src` is refused. The other five are
hints as every other attribute here is.

#### The placements description

A JSON document, `src`. URIs in it resolve against its own URL. Fields it
does not know are ignored (Principle 7).

```json
{
  "format": "placements/1",
  "entities": "https://store.example/w/garden/entities",
  "palette": "https://store.example/w/garden/palette",
  "edit": {
    "policy": "owner",
    "issuer": "https://passes.example",
    "signIn": { "clientId": "the-worlds-client" }
  },
  "changes": { "type": "worldsync", "address": "wss://sync.example/", "session": "garden-changes" }
}
```

| field | meaning |
|---|---|
| `format` | `placements/1`. A shell refuses a format it does not know, with a warning (`veml:placements:unknown-format`) |
| `entities` | the **snapshot** (below): every placement, read by anyone. Writes go to `<entities>/<id>` |
| `palette` | optional: the **palette** (below), what may be placed. Absent, the snapshot's own prefab references are the palette, with no labels or pictures |
| `edit` | optional: who may write, and how they sign in (below). Absent, nobody writes through a shell |
| `edit.policy` | `owner`: the one identity the store was made for. `signed-in`: any visitor holding a pass from `issuer`. `grants`: whoever the store has granted writing, as its own records say; a shell asks `edit.grants` which this visitor is (**Who may write**, below) |
| `edit.grants` | with `grants`, and required by it: the address on the write origin that tells a visitor, by their pass, what they may do |
| `edit.issuer` | the **pass issuer** whose passes the store takes for writes, exactly as the planet profile's `edit.issuer` (planet profile, **Signing in to write**): any pass issuer, none special, and a shell's trust in one is the shell's own, never taken from a description |
| `edit.signIn` | optional: `{ "clientId": "<the store's client id at its pass issuer>" }` and nothing else |
| `changes` | optional: the room the store tells writes in: `type`, `address` and `session` as the planet profile's `changes` |
| `picture` | optional: where the **world's picture** (below) is kept and written, on the write origin. Absent, a shell offers no taking of it |

A description that offers `edit` without both `policy` and `issuer`
present, each a value above allows, or with `grants` and no `edit.grants`,
is read as offering none (Principle 7): a shell that finds no usable `edit` draws the placements and offers
no editing, with no warning. `entities`, `palette`, `<entities>/<id>` and
`edit.grants` MUST be on one origin, the store's **write origin**, which MUST be
`https` except on `localhost`, `127.0.0.1` or `[::1]`; a description that
breaks this offers no editing and its placements are not drawn, with a
warning (`veml:placements:other-origin`). A `picture` on another origin is
ignored with the same warning, and the rest of the description stands.

Before offering sign-in for a description, or sending any write to its
store, a shell fetches `<write origin>/.well-known/placement-writes.json`,
`{ "descriptions": ["<absolute URL of a placements description>", …] }`,
and MUST NOT offer sign-in for, or send a write on behalf of, a
description whose own URL is not listed there exactly; a failure to
fetch or read the list vouches for nothing. The reason is the planet
profile's (**Signing in to write**): otherwise a description anywhere
could name an honest store's pass issuer and client and have its own
visitors edit that store.

#### The snapshot and the palette

`entities` answers one payload, so that a world is drawn in one round
trip:

```json
{ "world_seq": 41,
  "placements": [
    { "id": "table-1",
      "prefab_ref": "assets/cafe-table.glb",
      "transform": { "position": { "x": 0, "y": 0, "z": 0 }, "rotation": { "x": 0, "y": 0, "z": 0, "w": 1 }, "scale": { "x": 1, "y": 1, "z": 1 } },
      "overrides": {},
      "seq": 41, "wid": "6f1d0c2e-9a47-4b3f-8c15-2d7e4a9b0f63" } ],
  "tombstones": [ { "id": "table-2", "wid": "…", "seq": 44 } ],
  "tombstone_floor": 12,
  "retention_floor": 12 }
```

| field | meaning |
|---|---|
| `world_seq` | an integer that grows with every write to the store; the snapshot is as of it |
| `placements[]` | every placement now in the world. `id` names it and is the tail of its write address. `prefab_ref` is what is placed: a reference resolved against the description's `palette` (or against `entities` where there is none), or `primitive:<shape>`, one of the language's own shapes (below). A reference the palette does not list, or that fails to load, or a shape the language does not have, is skipped with a warning (`veml:placements:unknown-prefab`) and the rest are drawn. `transform` is in the world's own frame, as a core entity's `transform` (§5.6). `overrides` carries what a placed copy has beyond its transform: for a shape, `color`, its colour (as `primitive`'s `color`, §4.5); every other key is reserved, and a shell ignores what it does not know. `seq` is the `world_seq` at the placement's last write, and is what a write quotes in `If-Match`. `wid` is an opaque string a store stamps on each write |
| `tombstones[]`, `tombstone_floor` | optional: recent deletions, and the `seq` below which they are no longer kept (`"none"` where the store keeps none). A shell that draws placements does not need them; they are for a client that writes and reconciles |
| `retention_floor` | the floor a **place** quotes (below): the store's own, as of this snapshot, or `"none"` where it keeps no tombstones. A store MAY move it with every write |

`palette` answers `{ "world_seq": <n>, "prefabs": [ { "ref": "assets/cafe-table.glb", "label": "Café Table", "thumbnail": "thumbs/cafe-table.png" } ] }`:
`ref` is what a placement's `prefab_ref` names, resolved as above;
`label` is optional and shown; `thumbnail` is optional, a picture, and a
shell draws a label where there is none. A palette MAY be served from the
same host as the world's own files; a shell reads it as data and never
runs anything from it. An entry MAY carry other fields; a shell ignores
those it does not know (Principle 7).

**Styles.** An entry MAY carry `styles`, the same item in several looks,
each a model of its own: `{ "basic": "library/bench.basic.glb", "mid":
"library/bench.mid.glb", "ultra": "library/bench.ultra.glb" }`. A key is a
style's name, a token of lower-case letters, digits and `-`, 1 to 32
characters; a value is a reference resolved as `ref` is. The names are the
palette's own, not a fixed list: whoever adds an item names its styles,
and may add a style no other item has. Styles are not ranked. A shell
draws an entry in the visitor's style (`hints:placements`' `style`) with
the model that style names, and an entry without that style, or without
`styles`, with the model `ref` names. A placement always names the
entry's `ref`, never a style's model: a store checks a write against
`ref`, and a style's reference is not one a placement may name. A key
that is not a style name is ignored, as is a value that is not a string.

**The language's shapes as items.** A `prefab_ref` of the form
`primitive:<shape>`, where `<shape>` is one of `primitive`'s shapes (VEML
core §4.5: `cube`, `sphere`, `capsule`, `cylinder`, `plane`, `torus`,
`cone`, `rectangular-pyramid`, `tetrahedron`, `prism`, `arch`), names that
primitive and is never looked up in the palette. A shell draws it as the
core would draw a `primitive` of that shape, one metre across, in the
colour its placement's `overrides.color` carries, or a plain grey where
it carries none. A shell that offers editing offers the shapes as items
beside the palette, in a colour the visitor chooses, unless the world's
`shapes` hint is `none`; placing one writes `prefab_ref` and
`overrides: { "color": "<the colour>" }`. A palette need not list the
shapes, and a store treats a shape placement as any other.

#### What a palette item does

An entry MAY carry **`bindings`**: what a placed copy of it does when
something happens to it. Each binding is an object:

| field | values | meaning |
|---|---|---|
| `on` | `click` \| `touch` \| `place` | the visitor clicks the placed copy while not placing or moving; the visitor's pointer comes onto it; this visitor places it (once) |
| `animate` | `play` \| `stop` \| `toggle` | optional: start the model's own animation named `clip`, stop it, or start it if stopped and stop it if playing |
| `clip` | string | the name of an animation in the item's model (glTF `animations[].name`); required with `animate`, and a binding that has `animate` but no `clip` is ignored |
| `loop` | `true` \| `false` | optional, with `animate` `play` or `toggle`: whether the clip repeats; default `false` |
| `action` | string | optional: the name of an action fired on the world's action bus (VEML core §8.1), with `{ "id": "<the placement>", "prefab_ref": "<the item>", "on": "<what happened>" }` |

A binding acts on **one placed copy**: its animation plays on that copy
alone, and two copies of one item are never moved together. A binding
whose `on` or `animate` a shell does not know is ignored, as is a `clip`
the model does not have (Principle 7). Bindings fire only from the
visitor's own input, never from another visitor's or a script's, so an
item can no more act unbidden than a fetched template can (VEML core
§6.4). A palette still carries no code: `action` names an action the
world's own scripts may or may not handle.

#### Writing a placement

A shell writes with a **pass** (planet profile, **Signing in to write**;
a store never receives the visitor's own sign-in):

- **place**: `PUT <entities>/<id>` with a body of `prefab_ref`,
  `transform` and `overrides` (`{}`), and `?if_absent=<retention_floor>`
  quoting the snapshot's `retention_floor` exactly, `?if_absent=none`
  where it is `"none"`: the store refuses the write if a placement with
  this `id` exists, was deleted since that floor, or the floor has moved
  since (412, and the shell reads the snapshot again). A `PUT` with
  neither `if_absent` nor `If-Match` is unconditional, and never a place;
- **move**: `PUT <entities>/<id>` with the same body and
  `If-Match: "<seq>"`, the placement's `seq` as a quoted tag;
- **delete**: `DELETE <entities>/<id>` with `If-Match: "<seq>"`.

Every write carries `Authorization: Bearer <pass>` and MAY carry
`X-Rid: <a short string the shell chose>`, which the store echoes in its
answer and uses to recognise a retry of the same write.

| status | body | when |
|---|---|---|
| 200 | `{ "seq": <n>, "wid": "<stamp>", "precondition": null \| <what was checked> }` | applied; `seq` is the placement's new `seq` |
| 400 | `{ "code": "INVALID_RECORD" }` | a malformed body or precondition, or both `If-Match` and `if_absent` |
| 401 | `{ "code": "UNAUTHENTICATED" }` | no pass, one that does not verify, one not from `edit.issuer`, or one not issued to the store's own client |
| 403 | `{ "code": "FORBIDDEN" }` | a pass that verifies, from an identity the policy does not let write |
| 404 | `{ "code": "NOT_FOUND" }` | a move or delete of a placement that is not there |
| 412 | `{ "code": "PRECONDITION_FAILED", "seq": <n> }` | `If-Match` named a `seq` that is not the placement's now, or `if_absent` and the placement exists or was deleted since the floor quoted; `seq` is the placement's now, where it exists |
| 413 | `{ "code": "INVALID_RECORD" }` | the body is over 1 MiB |
| anything else | | a failure, treated as 503 |

A shell shows the visitor the placement as written before the answer
comes (**prediction**), and on 401, 403, 404 or 412 puts it back as it
was and says why; on 412 it then reads the snapshot again. A shell MUST
send a pass to no URL on the write origin but `entities`, `palette`, `picture`,
`<entities>/<id>`, `edit.grants`, and the palette files below.

#### Who may write

Under `grants`, who may write is the store's own record, kept by the
store and changed by whoever the store lets change it; this profile says
only how a shell learns where its visitor stands. Once the visitor has a
pass, a shell sends `GET <edit.grants>` with `Authorization: Bearer
<pass>`, and the store answers `200` with `{ "scopes": [ … ] }`, the
visitor's own:

| scope | means |
|---|---|
| `placement` | the visitor may place, move and delete placements |
| `palette` | the visitor may change what can be placed |
| `owner` | the visitor owns the world, and may set its picture |

Scopes a shell does not know are ignored (Principle 7). A shell offers
placing, moving and deleting only to a visitor whose scopes include
`placement`, and to the rest says that the world's owner has not given
them editing. A `401` there is a pass the store does not take, and the
shell signs in again; any other failure offers no editing, and is not
retried until the visitor signs in again. The answer differs from
visitor to visitor, so a store sends it with `Cache-Control: private,
no-store` and a shell keeps it no longer than the pass it was asked
with. It is a courtesy and never a permission: every write is still
decided when it is made, and a write the scopes promised can still be
refused with `403`.

#### Changing the palette

A visitor whose scopes include `palette` adds an item by writing its
files where the palette's references resolve: the address of a file
named `assets/<name>.<ext>` is that name resolved against the `palette`
URL, as a `ref` is. `<name>` is a letter or digit followed by up to 63
letters, digits, `_` or `-`; any other name is not a palette file. Three
kinds of file make an item:

| file | what it is |
|---|---|
| `assets/<name>.glb` | the item's model, a binary glTF; it is the entry's `ref` |
| `assets/<name>.png`, `.jpg`, `.webp` | a picture of it |
| `assets/<name>.glb.veml` | its **description**: a VEML document about that one model, giving its label, its picture and its bindings |

A shell writes each with `PUT <address>`, the bytes as the body, and
removes one with `DELETE <address>`, each carrying `Authorization: Bearer
<pass>`. It writes the picture first, then the model, then the
description, so that what a description names is there when it is read.

The description is a VEML document declaring this profile, whose
`metadata` has a `title` (the entry's `label`) and, optionally, a
`preview` whose `src` is the picture, resolved against the description
(the entry's `thumbnail`), and whose `environment` holds the item's
bindings, each a `binding` element of this profile's namespace:

```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/4.0"
      xmlns:hints="http://www.fivesqd.com/schemas/veml/4.0/profiles/client-hints" version="4.0">
  <metadata>
    <title>Door</title>
    <preview src="door.png"/>
    <profile>client-hints</profile>
  </metadata>
  <environment>
    <hints:binding on="click" animate="toggle" clip="Open"/>
  </environment>
</veml>
```

Each `hints:binding`'s attributes are the fields of a binding (**What a
palette item does**); `loop` is the text `true` or `false`. A model with
no description is still an item, labelled by its name. A description
that cannot be read describes nothing, and its model stays an item as if
it had none.

| status | body | when |
|---|---|---|
| 200 | `{ "ref": "assets/<name>.<ext>" }` | written, or removed |
| 401 | `{ "code": "UNAUTHENTICATED" }` | as for a placement |
| 403 | `{ "code": "FORBIDDEN" }` | a pass whose holder may not change the palette |
| 404 | `{ "code": "NOT_FOUND" }` | not a palette file's name, or removing a file that is not there |
| 413 | `{ "code": "INVALID_RECORD" }` | larger than the store allows; how large is the store's own choice |
| 415 | `{ "code": "INVALID_RECORD" }` | not what its name says: a model that is not binary glTF, a picture that is not that kind of picture, a description that is not a document |
| anything else | | a failure, treated as 503 |

A store judges a file by its bytes, never by a type the request claims.
Removing a model does not remove its placements: they name a reference
the palette no longer lists, and are skipped as above. A palette change
does not advance `world_seq`.

#### The world's picture

Where a description names `picture`, the world's own picture is kept
there: `GET <picture>` answers it, a PNG, JPEG or WebP image, or `404`
before there is one. A store answers it with `Cache-Control: no-cache`, so
a page showing it fetches a new one on its next load, and lets a page on
any origin read it. A world's document MAY name the same address as its
`preview` (VEML core §3.1), so the picture its owner takes is the one a
directory shows.

A shell MAY offer a visitor whose scopes include `owner` to take it: the
world's current view, rendered without the shell's own panels, at 1280 by
720. The shell shows the visitor the picture and writes it only if they
keep it, with `PUT <picture>`, the image as the body and `Authorization:
Bearer <pass>`. A new picture replaces the last.

| status | body | when |
|---|---|---|
| 200 | `{ "type": "<the image's type>" }` | kept |
| 401 | `{ "code": "UNAUTHENTICATED" }` | as for a placement |
| 403 | `{ "code": "FORBIDDEN" }` | a pass whose holder does not own the world |
| 413 | `{ "code": "INVALID_RECORD" }` | over 2 MiB |
| 415 | `{ "code": "INVALID_RECORD" }` | not a PNG, JPEG or WebP image, judged by its bytes |
| anything else | | a failure, treated as 503 |

#### The change notice

A store with `changes` tells each write in that room: one application
message per write, at the path **`placements`** and carrying
**`{ "description": "<the description's own URL>", "world_seq": <n> }`**,
the store's `world_seq` after the write. (The description's URL rides in
the message, not the path: a path is made of identifiers, which a URL is
not.) One room MAY carry the notices of many worlds; a shell acts only
on those naming the description it loaded. Only the store writes to the room; a shell MAY join and
be told every change, but MUST NOT write to it itself. A shell acts on a
notice by reading the snapshot again and redrawing what changed — once
per newer `world_seq`, not once per notice: a notice for a snapshot
already being fetched is acted on once more, after that fetch finishes.
A store without `changes` is read once, as the world loads; a shell MAY
read it again on its own occasions.

A change to the palette is told the same way, at the path **`palette`**,
carrying **`{ "description": "<the description's own URL>", "kind":
"prefab-changed", "ref": "<the file's ref>", "world_seq": <n> }`**. It is
a separate path because a palette change does not advance `world_seq`,
so a shell that counted it among placement notices would drop it. A
shell acts on every palette notice naming its description by reading the
palette again.

A shell that writes fires the action **`placements.edit`** on the
world's action bus (VEML core §8.1), whatever the answer, as a planet's
`planet.edit` is fired (`planet-cube-sphere.md`, **Editing**):

| field | type | meaning |
|---|---|---|
| `op` | `"place"` \| `"move"` \| `"delete"` | what was asked |
| `id` | string | the placement |
| `prefab_ref` | string | what is placed there, as the palette names it |
| `result` | `"applied"` \| `"refused"` | the 200 above; or refused |
| `reason` | string, with `refused` | `sign-in`, `not-allowed`, `not-found`, `changed`, `invalid`, or `unavailable` |

A script hears it with `world.actions.on('placements.edit', handler)`
(VEML core §8.3). The shell gives `sign-in` for a write it never sent
because no pass verified (401 above), `not-allowed` for 403, `not-found`
for 404, `changed` for 412, `invalid` for 400 and 413, and `unavailable`
for a 5xx or no answer at all.

### Script surface

A world script reads and changes the same hints at run time, and hears the
room's chat. These objects join the script API (§8.3), which is otherwise
specified separately. They follow its existing shape: PascalCase methods on
a global object, and callbacks passed as a function or a function's name.

| call | returns | meaning |
|---|---|---|
| `ClientHints.Get(name)` | string, or `null` | the world's preference in force for `name`: the shell's default where the world set none. `null` for a name the shell does not implement |
| `ClientHints.Set(name, value)` | boolean | set the world's preference for `name`. Validated exactly as the attribute is; `false`, with a warning, when the value is refused |
| `Chat.Post(text)` | boolean | show `text` as a line from the world on this visitor's messages panel. It is not sent to the room |
| `Chat.OnMessage(callback)` | a handle | call `callback(name, text)` for each chat line this visitor receives, including their own |
| `Dock.AddTool(name, picture, callback)` | a string, or `null` | a button of the world's in the shell's bar and its catalog of items (**`hints:placements`**): `name` is its label, `picture` the URL of its picture or `null` for a plain button, and `callback()` runs on this visitor's shell when they press it or choose its slot. Returns the tool's id. `null`, with a warning, where this world has no bar (no placements, or editing not offered to this visitor) or the bar's slots are full: a bar has nine, shared with the items, and a tool never takes one an item holds |
| `Dock.RemoveTool(id)` | boolean | take the tool out of the bar and the catalog; `false` for an id that is not a tool of this script's |
| `Dock.ClearTools()` | nothing | take out every tool this script added |

A tool is the world's own button, as its scripts are the world's own
code: pressing one does what the script says and nothing else, and a
visitor who may not edit the world sees no bar and so no tools. Tools
are per shell and per visit; they are not written to the store and
nobody else sees them.

`name` is the element and attribute joined by a dot, without the prefix:
`people.labels`, `chat.enabled`, `controls.touch-buttons`, `vr.turn`,
`planet.zoom`, `placements.edit`, and so on for every attribute above (`placements.src` excepted, as **`hints:placements`** says). A `Set` takes effect for
this visitor at once (people already drawn are redrawn), and lasts until the
world is left. `planet.air` is the exception: it is read once, as the world
loads, so a `Set` of it is reported by `Get` but does not change the air
already drawn. A `Set` of `planet.terrain-style` takes effect while the
world runs: the ground already drawn stays until the ground in the new style
replaces it, piece by piece, so there is no moment without ground. The
visitor's own settings are not the world's to read or change; `Get` does not
report them.

A script cannot send a chat line to the room in the visitor's name. That
would let a world speak for a person, which needs a consent statement this
profile does not make.

## Degradation

1. **A shell without this profile.** `hints:hints` is an unrecognized
   element in a foreign namespace. §2 requires it to be ignored with a
   warning, and the shell behaves as for a world that says nothing. A script
   calling `ClientHints` or `Chat` finds them undefined; a portable script
   checks with `typeof ClientHints !== 'undefined'` first.
2. **A shell with this profile but without a feature.** A shell with no
   chat, no touch controls or no VR ignores the hints for that feature,
   without a warning: there is nothing to configure. `ClientHints.Get`
   returns `null` for it.
3. **An unusable value.** Out-of-range numbers are **clamped to the stated
   range, with a warning**. An unknown enumerated value or list token is
   ignored and the default stands, with a warning. It is never a refused
   document, and never a refused world.

Diagnostics follow §2's form: `veml:client-hints:out-of-range`,
`veml:client-hints:unknown-value`, `veml:client-hints:unknown-attribute`,
`veml:client-hints:duplicate` (a second `hints:hints`),
`veml:client-hints:avatar-unloadable`,
`veml:client-hints:profile-not-implemented`. An unknown child of
`hints:hints` keeps §2's own `veml:2:unknown-element`.

**A shell that implements this profile MUST NOT emit
`veml:2:unknown-element` for the elements it implements**, for the reason
`sky` gives: the obvious implementation emits it anyway, and a conformance
suite must be able to tell a shell with the profile from one without it.

## Schema

The vocabulary is in its own namespace,
`http://www.fivesqd.com/schemas/veml/4.0/profiles/client-hints`, and sits
in `environment` through the core schema's extension point (§3.2), as
`sky` and `environment` do. 3.0 put `controlflags` in `metadata`; 4.0's
`metadata` has no extension point, and `environment` is where every profile
with vocabulary lives, so this profile follows them rather than widening
core. A per-profile schema is deferred, as it is for the others.

## Bounds

Outside this profile, and named so the line is visible:

- **A person's name and own avatar travelling to the others.** That is the
  WorldSync roster (protocol §4.6). This profile says only how a world lets
  a brought avatar be drawn (`own-avatars`, above). A person's own colour
  does not travel.
- **Hand tracking**, and the 3.0 poker, hand-interaction and grab-move fields
  that would need it.
- **Layout of the shell's chrome** (where the panel sits, how big the
  buttons are). That is the visitor's and the shell's, not the world's.
- **Scripts speaking in the room as the visitor** (see Script surface).
