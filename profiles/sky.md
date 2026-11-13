# The `sky` profile

**Status:** specified 2026-09-15 (#290) — the second profile to graduate
from §12's deferred "profile specifications themselves", after `lighting`.
Registry entry: draft §10, *"rich procedural sky, successor of 3.0's
lite-procedural-sky"*.

Unlike `lighting`, this profile **adds vocabulary**. That is the difference
worth stating up front: lights already had a format — glTF
`KHR_lights_punctual` — so the lighting profile declined to re-mint one
(§0.6). A procedural sky has no such format. glTF describes geometry and
materials, not an atmosphere model; an equirectangular image is a *picture*
of a sky, which core already covers with `background panorama`. So the
choice here is a small vocabulary or nothing, and §10 already promised the
vocabulary by name.

## Declaration

```xml
<metadata>
  <profile>sky</profile>
</metadata>
```

Profile vocabulary lives in its own namespace, so a core-schema validator
accepts a sky document without knowing this profile (see **Schema** below):

```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/4.0"
      xmlns:sky="http://www.fivesqd.com/schemas/veml/4.0/profiles/sky"
      version="4.0">
  <metadata>
    <title>Hillside</title>
    <profile>sky</profile>
  </metadata>
  <environment>
    <background color="#87ceebff"/>
    <sky:sky>
      <sky:atmosphere zenith="#08124100" horizon="#448bc5ff" blend="0.579"/>
      <sky:ground color="#07070700" height="-0.02" fade="0.02"/>
      <sky:sun diameter="2" color="#9e813aff" elevation="35" azimuth="120"/>
    </sky:sky>
  </environment>
</veml>
```

## Semantics

A document declaring `<profile>sky</profile>` and carrying a `sky:sky`
element in its `environment` asks the shell to render a **procedural sky**:
an atmosphere gradient, an optional ground plane below the horizon, and an
optional sun disc. Every child and every attribute is
optional; `<sky:sky/>` alone is a valid, fully-defaulted sky.

### `sky:atmosphere`

| attribute | type | default | meaning |
|---|---|---|---|
| `zenith` | §4.5 color | `#08124100` | the color directly overhead |
| `horizon` | §4.5 color | `#448bc5ff` | the color at the horizon line |
| `blend` | number `0.1`–`2` | `0.579` | how sharply zenith gives way to horizon; lower is a harder band |

Colors use the core §4.5 color syntax — no third grammar. Components above
1 are permitted and mean what HDR means to the shell; alpha is ignored (a
sky is opaque), which is a documented simplification rather than an
oversight.

### `sky:ground`

| attribute | type | default | meaning |
|---|---|---|---|
| `enabled` | boolean | `true` | draw anything below the horizon at all |
| `color` | §4.5 color | `#07070700` | the color below the horizon |
| `height` | number `-1`–`1` | `-0.02` | where the horizon sits relative to the eye; 0 is eye level |
| `fade` | number `0`–`1` | `0.02` | how softly ground meets atmosphere |

### `sky:sun`

| attribute | type | default | meaning |
|---|---|---|---|
| `enabled` | boolean | `true` | draw the disc |
| `diameter` | number ≥ 0, **degrees** | `2` | angular diameter. Earth's sun is ~0.53°; the default is deliberately larger because a physically-sized disc reads as a bright dot |
| `color` | §4.5 color | `#9e813aff` | the disc's color |
| `elevation` | number `-90`–`90`, degrees | `45` | angle above the horizon. Negative places the sun below it — night, disc hidden |
| `azimuth` | number `0`–`360`, degrees | `180` | compass bearing, clockwise from −Z seen from above: 90 is −X |

**The sun is self-contained, and this is a deliberate divergence from
3.0.** The 3.0 element bound its sun to a scene light by tag
(`sun-entity-tag`). VEML 4.0 has no light vocabulary — the `lighting`
profile settled that lights travel in the asset layer — so there is nothing
for a tag to name. A shell that also implements `lighting` MAY align a
directional light with `elevation`/`azimuth`; nothing here requires it, and
a sky never creates a light.

### Time of day

There is none, and that is a boundary rather than an omission. `elevation`
and `azimuth` describe **a** sky, not a cycle. Animating them needs a clock,
a statement about whether that clock is shared between visitors, and a
statement about what happens when someone joins an hour in — which is
session semantics, not environment markup. 3.0's `day-night-cycle-enabled`
answered none of those questions; it toggled a client-side animation, which
is script-tier work in 4.0 and a candidate for its own profile if real
content asks.

## Degradation

Three cases, and none of them is a broken world.

1. **A shell without this profile.** `sky:sky` is an unrecognized element in
   a foreign namespace; §2 requires it to be ignored with a warning. The
   shell renders `background` as it always would.
2. **A shell with this profile, and `background` also present.** The
   procedural sky is the visible backdrop. A `background panorama` still
   serves as image-based lighting if the shell uses one — a picture of a sky
   and a computed sky are not rivals for that job. A `background color`
   is the pre-sky backdrop and is simply covered.
3. **A shell with this profile and no `background`.** Renders the sky.
   Authors are nonetheless **RECOMMENDED** to supply a `background color`
   roughly matching the sky's horizon: it is what a shell without the
   profile will show, and choosing it deliberately is the difference between
   degrading and looking broken.

Out-of-range numbers are **clamped to the stated range, with a warning** —
never a refused document, never a refused sky. This is stated because 3.0's
implementation aborted the entire sky when one attribute fell outside its
gate, which turns a typo into a black world.

Diagnostics follow §2's form: `veml:sky:out-of-range`,
`veml:sky:unknown-attribute`, `veml:sky:profile-not-implemented`.

**A shell that implements this profile MUST NOT emit `veml:2:unknown-element`
for the elements it implements.** This is worth stating because the obvious
implementation emits it anyway: a parser recognizes core vocabulary, warns
about everything else, and profile support is layered on afterwards — so
the warning is already in the diagnostics channel before any profile logic
runs. Measured against this repository's own parser, a fully-supported sky
document produces `veml:2:unknown-element <sky>` and one more per child.
That is correct for a shell without the profile and wrong for a shell with
it, and a conformance suite should be able to tell those two shells apart
by their diagnostics.

## Schema

Profile vocabulary is in its own namespace,
`http://www.fivesqd.com/schemas/veml/4.0/profiles/sky`. That is what makes a
sky document safe for a shell that has never heard of this profile: §2's
tolerance rule ignores the unrecognized elements, and the namespace keeps
them from colliding with core vocabulary now or in any later core version.

The namespace is a property of the **document**, and shells are not
currently obliged to carry it into their object model — this repository's
parser preserves unrecognized elements with their attributes and children
but records only the local name, so `sky:sky` and a hypothetical core
`<sky>` arrive indistinguishable. That costs nothing while one profile
carries vocabulary and becomes a collision the day two do. A shell that
means to support several profiles SHOULD preserve the namespace; this
profile does not require it, because requiring it of every shell to serve
one element would be the wrong trade today.

**The core schema accepts it** through `environment`'s extension point,
`xs:any namespace="##other" processContents="lax"` (§3.2; #293, 2026-09-25).
This profile's example above validates against the core schema, and junk in
the **core** namespace is still rejected. Until that landed, `environment`
was a closed sequence and a validating pipeline rejected every sky document —
`lighting` never surfaced it because it added no vocabulary; `sky` was the first
profile that did.

A schema for this profile's own namespace is **deferred**. `lax` processing
means a validator checks profile elements only when it has their schema,
which is the behaviour we want while so few profiles carry vocabulary.

## Bounds

Outside this profile, and named so the line is visible:

- **Clouds, moon and stars.** 3.0 carried all three. They are omitted for
  the same reason `lighting` declined to mint light vocabulary: a profile
  that specifies more than any shell renders is a promise nobody keeps.
  This repository's shell states the line in its own source — *"the CORE
  sky (ground→horizon→zenith day gradient + a sun disc) renders; the moon,
  stars, clouds … are parsed-and-DEFERRED … the chunk stays a gradient +
  sun disc"*. When a shell renders clouds, a profile can describe them; a
  §10 registry entry costs nothing to add later and an unimplemented
  attribute costs every author who trusts it.
- **Weather.** Rain, snow, wind.
- **The 3.0 saturation and falloff knobs** — `horizon-saturation-*`,
  `sky-lighting-falloff-*`, `sunset-*`. They describe one renderer's
  internals rather than a sky, and no content in the corpus sets them. A
  shell may approximate their effect; nothing declares it.
- **Aerial perspective and fog.** `fog` is its own registry entry (§10).
- **Sky-driven scene lighting.** A sky does not light the world here.
  `lighting` and `background panorama` cover that, and a sky that silently
  became a light source would make two profiles fight over the same job.
