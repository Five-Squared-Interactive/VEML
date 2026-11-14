# The `lighting` profile

**Status:** specified 2026-08-26 (#69) — the first profile to graduate from
§12's deferred "profile specifications themselves". Registry entry: draft
§10. Decision record (maintainer, 2026-08-26): world lighting travels in
the **asset layer** as glTF `KHR_lights_punctual`; there is
**no new VEML vocabulary** for lights — glTF expresses punctual lights natively,
authoring tools (Blender's glTF exporter among them) emit them without
extra work, and the document/asset layering stays clean (§0.6: no
re-minted vocabulary).

## Declaration

```xml
<metadata>
  <profile>lighting</profile>
</metadata>
```

## Semantics

A document declaring `<profile>lighting</profile>` **opts in** to the
punctual lights embedded in its glTF assets (`KHR_lights_punctual`:
directional, point, spot) being honored by the shell: the lights load
with their assets and illuminate the scene.

When the profile is declared **and** the loaded assets deliver at least
one light, the shell's default-lighting injection **stands down** — the
world's declared lights are authoritative, exactly as a 3.0-era
lights-declaring world suppressed the default profile. A profile-declaring
world whose assets carry **no** lights still receives the shell's default
lighting: opting in never produces a black world.

**Without the profile**, the core default is unchanged and remains
normative: embedded glTF lights are ignored (stripped at asset load) and
the shell's default lighting profile is authoritative. A shell that does
not implement this profile behaves identically to the no-profile case and
warns per §2/§10 — content degrades to default lighting, never to
darkness.

## Bounds

Punctual lights only. IES profiles, area lights, baked lightmaps, and
image-based lighting beyond the core background/IBL path are outside this
profile — future surfaces if real content demands them.
