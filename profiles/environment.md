# The `environment` profile

**Status:** specified 2026-09-25 (#76) — the third profile to graduate from
§12's deferred "profile specifications themselves", after `lighting` and
`sky`, and the second to carry vocabulary. Registry entry: draft §10.

A world can light itself from an image: an equirectangular picture of its
surroundings supplies reflections on glass and metal and the soft ambient
light that fills shadows. Core already has one place for such an image —
`background panorama` — but there the image is also **drawn** as the sky.
Authoring tools separate the two routinely. Blender's Material Preview lights
and reflects a studio HDRI while showing a plain backdrop (World opacity 0);
an export that must choose between "draw the HDRI as the sky" and "lose the
reflections" cannot carry what was authored. This profile carries it: an
image that lights the world **without being the visible sky**, while
`background` keeps deciding what a visitor sees.

## Declaration

```xml
<metadata>
  <profile>environment</profile>
</metadata>
```

Profile vocabulary lives in its own namespace, like `sky`'s, so a core-schema
validator accepts the document without knowing this profile (§3.2's
extension point, #293):

```xml
<veml xmlns="http://www.fivesqd.com/schemas/veml/4.0"
      xmlns:env="http://www.fivesqd.com/schemas/veml/4.0/profiles/environment"
      version="4.0">
  <metadata>
    <title>Living Room</title>
    <profile>lighting</profile>
    <profile>environment</profile>
  </metadata>
  <environment>
    <background color="#404040ff"/>
    <entry-point id="default">
      <position x="2.4" y="0" z="4"/>
    </entry-point>
    <env:environment-map src="assets/sky.cbc1d3c0ef.png" intensity="3" diffuse="1.5"
                         specular="0.15" glazing-boost="6.7" glazing-f0="0.6"
                         punctual-lights="exclude"/>
    <asset tag="model" src="assets/model.243298f3fb.glb"/>
  </environment>
</veml>
```

Like all profile vocabulary it sits among the world's content — after
`background`, `navigation`, `locomotion` and any `entry-point` — anywhere in
that run (§3.2).

## Semantics

A document declaring `<profile>environment</profile>` and carrying an
`env:environment-map` in its `environment` asks the shell to use the
referenced image as the scene's **environment**: the source of image-based
reflections and ambient light. The image is **not drawn** as the sky. The
visible backdrop is still `background` (core §3.2), or the `sky` profile's
sky where that is declared; the environment map never replaces either.

At most one `env:environment-map` is honoured; a second is ignored with a
warning. It has no children.

### `env:environment-map`

| attribute | type | default | meaning |
|---|---|---|---|
| `src` | URI, required | — | an equirectangular image (PNG or JPEG), resolved against the document like any asset (§6). Without it the element is ignored with a warning |
| `intensity` | number ≥ 0 | `1` | the scene's overall environment strength |
| `diffuse` | number ≥ 0 | `1` | strength of the ambient fill derived from the image — the soft light in shadows |
| `specular` | number ≥ 0 | `1` | strength of the image in reflections only |
| `glazing-boost` | number ≥ 0 | `1` | an extra reflection multiplier on glazing (thin transparent surfaces such as windows), which an unlit environment otherwise renders nearly invisible. `1` means none |
| `glazing-f0` | number `0`–`1` | `0` | a reflectance floor for glazing — how mirror-like coated glass is at normal incidence. `0` keeps the material's own value |
| `punctual-lights` | `keep` \| `exclude` | `keep` | `exclude` states that the world was authored with the environment as its only light, so punctual lights carried in its assets (the `lighting` profile's `KHR_lights_punctual`) are not rendered. Lights declared in VEML itself are never excluded |

Numbers that are not finite, or below zero, are ignored with a warning and
the default applies — never a refused document, never a refused environment.
`glazing-f0` above `1` is clamped to `1`, with a warning.

The tuning attributes exist because one image, used as the only light, looks
different across renderers; they are the author's (or the exporter's)
statement of the look they saw, not shell constants. A shell MAY approximate
any of them and SHOULD document where it does.

## Degradation

1. **A shell without this profile.** `env:environment-map` is an unrecognised
   element in a foreign namespace; §2 requires it to be ignored with a
   warning. The shell renders `background` as it always would, with its
   default lighting. The world is plainer, never broken.
2. **A shell with this profile, and a `background panorama`.** The panorama
   is drawn as the sky, as core says; the environment map lights and reflects.
   They are allowed to be different images.
3. **A shell with this profile that cannot load the image** (a failed fetch,
   or a device without the texture support the shell needs). The shell falls
   back to its neutral environment, still renders the world, and warns. It
   never shows a black world for want of an environment.

Diagnostics follow §2's form: `veml:environment:missing-src`,
`veml:environment:out-of-range`, `veml:environment:duplicate`,
`veml:environment:load-failed`, `veml:environment:profile-not-implemented`.

**A shell that implements this profile MUST NOT emit `veml:2:unknown-element`
for `env:environment-map`** (the rule `sky` states, for the same reason).

## Schema

Profile vocabulary is in its own namespace,
`http://www.fivesqd.com/schemas/veml/4.0/profiles/environment`, and the core
schema accepts it through `environment`'s extension point
(`xs:any namespace="##other" processContents="lax"`, #293). A schema for
this namespace itself is deferred, as for `sky`: `lax` processing checks
profile elements only where a validator has their schema.

**Earlier exports.** Before this profile was specified, the reference exporter
wrote an un-namespaced `<environment-map>` as the first child of
`environment`, which fails core-schema validation. A shell MAY keep reading
that form so worlds already published keep their look; exporters MUST write
the namespaced form.

## Bounds

Outside this profile, and named so the line is visible:

- **Rotating or blurring the image** (Blender's `studiolight_rotate_z`,
  `studiolight_blur`). A shell cannot guarantee either identically, and no
  content yet needs them.
- **HDR delivery formats** (EXR, HDR). The profile names PNG and JPEG; turning
  a 32-bit source into one is an exporter's job.
- **Reflection probes and local cubemaps.** One environment per world.
- **Exposure and tone mapping** — a separate question, not an environment one.
