# VEML 4.0 — Virtual Environment Markup Language

**Draft Specification · Five Squared Interactive · August 2026**
**Status: Editor's Draft — not yet frozen**

Destination: `specs/veml/veml-4.0-draft.md` (Tier 1)
Sources reconciled: VEML 3.0 XSD · the 2026-07-14 design session (X3D lessons)
· the 2026-08-03 audit (`veml-audit-draft.md`, all judgment calls resolved)

---

## 1. Introduction

### 1.1 What VEML is

VEML is a declarative XML format describing a **world**: a navigable 3D
document at a URL. VEML occupies the *document layer* — composition,
navigation, arrival, synchronization, capabilities, and behavior binding. It
deliberately does **not** occupy the *asset layer*: geometry, materials,
animations, lights, and audio are expressed in glTF and its extensions, which
VEML documents reference.

The design analogy is HTML: HTML does not encode images or fonts; it composes
and links them. VEML does not encode meshes or materials; it composes and
links them. A world is to VEML what a page is to HTML.

Throughout this specification, a **shell** is the software that loads and
presents a VEML document on a user's behalf — a 3D browser, an authoring
tool, a headless indexer. §1.3 makes *shell* a conformance class;
user-facing materials may say "client," but this document says shell.

### 1.2 What VEML is not

- Not a scene format. Renderable content lives in referenced assets.
- Not a protocol. Documents travel over HTTP(S); synchronization uses
  whatever protocol a declared service names (§7). VEML only *names*
  endpoints; it defines no wire format and privileges no particular
  protocol.
- Not a runtime. Behavior beyond declarative bindings is supplied by
  scripts executing in a shell-provided sandbox: their loading, pinning,
  and gating are specified here (§8.3); the script API surface is
  specified separately (§12).

### 1.3 Conformance language

MUST, MUST NOT, SHOULD, MAY are used per RFC 2119. Two conformance classes
exist: **documents** (a VEML file) and **shells** (software that loads and
presents VEML documents). Conformance is testable via the VEML Conformance
Suite; conformance claims name a profile level (§10).

Authority is split once, cleanly: **the schema — `veml-4.0.xsd`, beside
this document — is the source of truth for the document grammar** (elements,
attributes, types, defaults, cardinality, child order); generated views
(types, element tables) derive from it. This document is the source of
truth for everything a schema cannot express: behavior, error recovery,
security, fetching, and conformance. Where this prose describes grammar,
it is informative and the schema prevails; where the schema's annotations
mention behavior, they are informative and this prose prevails.

### 1.4 Media type and file extension

`.veml` files. Proposed media type `application/veml+xml` (vendor-scoped
`application/vnd.fivesqd.veml+xml` until registration). Namespace:
`http://www.fivesqd.com/schemas/veml/4.0`. The `xmlns` declaration is
OPTIONAL at load time: a shell processing a root `veml` element that
carries `version` but no namespace declaration inserts the 4.0 namespace
into the infoset before any schema-based processing. The schema-valid form
carries the declaration — a conforming *document*, and any tool emitting
one, carries it; the explicit declaration also matters when VEML elements
travel inside foreign XML. A
root `veml` in any *other* namespace (a 3.0 document, a typo) is not a
VEML 4.0 document: a shell MUST NOT process it as one (warn); legacy
handling is shell discretion, outside this specification. The `version`
attribute is the discriminator for namespace-less documents: a
namespace-less root **without** `version` is the prototype-era wire form
— likewise not a VEML 4.0 document, treated exactly as a wrong namespace
(3.x documents never carried `version`; 4.0 documents always do).

Scheme allowlist: whenever a shell dereferences a URI on a document's
behalf — in **any** URI-bearing attribute (`href`, `src`, `url`, …), and
applied **after** `manifest:` resolution where §6.3 permits it (`src` and
`href` only) — the URI MUST be `http(s)`. A shell MUST NOT dereference any
other scheme (`javascript:`, `data:`, `file:`, …); such a value is a
document error (warn; the attribute is treated as absent — a `link`
without a dereferenceable `href` is an inert affordance). The allowlist
governs absolute URIs: a document-relative reference inherits its
document's base scheme, so a locally opened world may reference its
sibling files. A document's base URI is its retrieval URL, always —
`xml:base` is ignored (warn, per §2), while `xml:lang` on the root `veml`
is honored: it declares the document's text language and inherits per
XML 1.0 §2.12 (per-element granularity: §12). URI-bearing attributes
hold IRIs: parsing, resolution against the base, and the mapping to
fetchable URLs (punycode, percent-encoding) follow the WHATWG URL
Standard. One rule precedes that parsing: a URI-bearing attribute whose
value contains a C0 control character (U+0000–U+001F) or DEL (U+007F) is
refused exactly as a disallowed scheme is — `veml:1.4:bad-scheme`, the
attribute treated as absent — before any scheme is read. The URL parser
strips such characters, so `java&#9;script:` would read to this allowlist
as a relative reference and to the fetcher as `javascript:`; refusing the
character closes the gap without second-guessing the parser.

The one exemption is the `address` of a `synchronization-service` (§7). It
is never `manifest:`-resolvable; its scheme is governed by the named
protocol's own specification, not this allowlist; and a shell dereferences
it only when it implements that protocol. Even then, an `address` names a
**network endpoint**: a local-resource scheme (`file:`, `data:`, …) — or
any scheme the named protocol does not sanction — is a document error with
its own resolution: the *declaration* is unusable (warn), and fallback
proceeds within its group (§7).

A VEML document MUST NOT contain a document type declaration: a shell MUST
reject a document carrying a `DOCTYPE` at the parse layer and MUST NOT
process external entities. This — the XXE and entity-expansion defense —
is the one legal *policy* rejection of a well-formed document at the parse
layer (malformed XML naturally fails to parse); the tolerance rule (§2)
begins after parsing.

### 1.5 Design principles (inherited from the web)

Every construct in this specification is tested against the principles that
made the web work (Berners-Lee, *Weaving the Web*):

- **Universality** — any world at any URL is loadable by any conforming
  shell; no construct may require a particular vendor, directory, or service.
- **Decentralization** — no registries, no central authority. Prototypes are
  referenced by URL (§6.4), action names are an open namespace (§8.1),
  metadata names are author-defined (§4.9). Composition never requires
  consent from the composed origin.
- **Least power** — prefer the declarative form wherever it suffices; script
  is the escalation, never the default (§8).
- **Tolerance** — be liberal in what you accept; degrade, never break (§2).
- **User agency** — the shell is the user's agent. Presentation, navigation
  mode (§5.1), and consent (§9) ultimately belong to the user, not the
  document.

### 1.6 Value syntax

Where an element carries a vector as child elements (`position` and
`rotation` under `transform` and `entry-point`; `scale` and `size` under
`transform` only), each component is an attribute holding an XML double:
`<position x="0" y="1.5" z="-2"/>`; omitted components take their
element's defaults — except `transform`'s `size`, whose three components
are all required (a defaulted extent of 0 would be degenerate). Where this
specification gives a vector as a single attribute (`bounds`
`center`/`size`; `placement-socket` `position`/`rotation`/
`connecting-offset`), the value is three — four for quaternions —
space-separated doubles: `center="0 1.5 -2"`. Booleans are lowercase
`true`/`false`. List-valued attributes (e.g. `modes`) are space-separated.
Free-text values (`title`, `description`, `label`, `meta` `content`, …)
are Unicode; shells displaying them apply the Unicode Bidirectional
Algorithm with first-strong heuristics (an explicit direction attribute
is deferred, §12).

---

## 2. The tolerance rule (normative, global)

A shell encountering an element or attribute it does not recognize MUST
ignore it, MUST NOT treat the document as invalid, and MUST surface a
warning through its diagnostics channel. Unrecognized content MUST be
preserved if the shell re-serializes the document.

The diagnostics channel is normative for conformance: a shell MUST expose
warnings programmatically, each carrying a stable identifier of the form
`veml:<section>:<slug>` (the slug registry lives with the Conformance
Suite); user-facing *presentation* of warnings remains SHOULD. Every
"(warn …)" clause in this specification names such a diagnostic.
Recognized children appearing out of schema order are likewise accepted
with a warning — order violations are never fatal.

This rule is load-bearing: it is the extension mechanism (new elements and
future profiles degrade gracefully in older shells), the forward-compatibility
mechanism (a 4.0 shell opens documents containing 4.x additions), and the
reason removal-then-re-addition of features (e.g., `client-hints`) is safe.

A shell MUST NOT fail to load a document because of an unrecognized element,
an unrecognized attribute, an unrecognized asset `type`, or an unbound action
name. Each of these warns and degrades; none is fatal.

---

## 3. Document structure

```xml
<?xml version="1.0" encoding="UTF-8"?>
<veml xmlns="http://www.fivesqd.com/schemas/veml/4.0" version="4.0">
  <metadata>
    <title>My World</title>
  </metadata>
  <environment>
    <!-- entities, assets, includes, links, entry-points -->
  </environment>
</veml>
```

The root element is `veml` with required attribute `version`. It contains
exactly one `metadata` and exactly one `environment`, in that order.

### 3.1 `metadata`

| Child | Count | Purpose |
|---|---|---|
| `title` | 1 | Human-readable world title |
| `description` | 0–1 | Short description (discovery, previews) |
| `author` | 0–n | Attribution; free text or URL |
| `preview` | 0–1 | URI of a preview image |
| `capability` | 0–n | Capability request (§9) |
| `script` | 0–n | URI of a behavior script (§8.3) |
| `input-binding` | 0–n | Input → action binding (§8.2) |
| `synchronization-service` | 0–n | Sync endpoint declaration (§7) |
| `manifest` | 0–1 | Logical-name indirection (§6.3) |
| `prototype` | 0–n | Component declaration or import (§6.4) |
| `profile` | 0–n | Extension profiles this document uses (§10) |

Unless shown otherwise, metadata children carry their value as text content
(`<title>My World</title>`); `preview` and `script` carry `src`, `manifest`
and `prototype` carry `href`, and `input-binding` (§8.2) and
`synchronization-service` (§7) carry the attributes their sections define.
Child order follows the schema; per §2, a shell accepts out-of-order
children with a warning.

### 3.2 `environment`

| Child | Count | Purpose |
|---|---|---|
| `background` | 0–1 | Thin environment: `color` or `panorama` (an IBL/equirect URI). Richer skies are the `sky` profile. |
| `navigation` | 0–1 | Locomotion preferences and avatar metrics (§5.1) |
| `locomotion` | 0–1 | Vertical movement hints: gravity and jump height (§5.2) |
| `entry-point` | 0–n | Named arrival locations (§5) |
| `entity` and its subtypes | 0–n | The world's content (§4) |
| profile vocabulary | 0–n | Elements a declared profile adds (§10), each in that profile's own namespace, anywhere among the world's content |

**Profile vocabulary** is the extension point §2's tolerance rule promises:
an element in a namespace other than core's, placed among the content
(after `background`, `navigation`, `locomotion` and entry points), is
schema-valid whether or not a validator knows its profile, and a shell
without the profile ignores it with a warning. Core-namespace elements get no
such allowance — an unknown core element is still a schema error (#293).

`background` carries exactly one of the attributes `color` (§4.5 color
syntax) or `panorama` (URI of an equirectangular or IBL image): both
present is a document error resolved in favor of `panorama` (warn);
neither present is a document error treated as an absent `background`
(warn). If no `background` is present, the shell supplies its neutral
default environment.

### 3.3 HTML serialization

The **primary HTML serialization** of VEML is the mechanical `veml-`
prefixing of the 4.0 vocabulary: each element gains the prefix
(`<veml-world version="4.0">` for the root, then `<veml-entity>`,
`<veml-asset src="…">`, …), attributes and values
unchanged — a document in this form and its XML form are **the same
document**. A `veml-world` subtree is processed as a standalone
document at the page's URL for reference resolution, while remaining a
distinct consent subject (§9): capability prompts attribute the embedded
world separately from the page, the page's scripts hold no session, and
the world gains nothing from the page (§6.2's rule, again).

A **sugar tier** of ergonomic authoring forms (per-type tags, attribute
shorthands) is supported as a specified-later surface defined strictly by
**lossless expansion onto the primary form**: sugar is never an
independent vocabulary, and conformance is **judged on the expanded
form**. The **self-boot contract** (one script tag boots a world page) is
its own small deferred surface, orthogonal to dialect. What remains
deferred is named in §12: the primary-form parsing rules, the sugar
expansion table, and the self-boot contract.

### 3.4 Load order

The normative pipeline a shell follows, start to walkable:

1. Fetch the document; reject any `DOCTYPE` (§1.4); parse. Base URI is the
   retrieval URL.
2. Root check: `veml` with `version`; a missing namespace declaration means
   the 4.0 namespace (§1.4); an unknown `version` gets best effort under §2.
3. Read `metadata`. **Nothing is fetched yet** — the manifest, prototype
   definitions, and scripts are not fast-path work (§6.3, §6.4, §8.3).
4. Build `environment`: entities, transforms, declared `bounds` — which
   permit deferring everything beneath them before any asset fetch (§4.8).
5. Place the user: URL fragment → matching `entry-point`; unknown fragment
   → `default` with a warning; no entry point → derived (§5).
6. Everything else — assets, includes, lod levels, prototype definitions,
   the manifest, scripts, synchronization — loads as needed, under the
   scheme allowlist (§1.4) and the fetch eligibility rules (§6.2).

Steps 1–4 bind every shell; steps 5–6 bind presenting shells — a
non-presenting shell (an indexer) stops after step 4. Steps 1–5 are the
walkable path; nothing in them requires a subsidiary fetch.

---

## 4. Entities: placement and composition

### 4.1 `entity` (base)

The composition node. Every placeable thing is an entity or a subtype.

Attributes: `tag` (human-readable, non-unique), `id` (UUID; shell generates
if absent), `on-load` (action name fired when the entity's **eager**
content finishes loading — deferred content (inactive lod levels §4.10,
deferred includes §6.2) does not gate it, and it never fires before §8.3's
script gate; an action name, never code; see §8).

Children: zero or one `transform` (identity when absent, §4.2), zero or
more child entities, zero or more `instance` (§6.4), zero or one `link`
(§6.1), zero or one `include` (§6.2), zero or one `synchronizer` (naming a
declared synchronization service), zero or more `placement-socket`, zero or
one `bounds` (§4.8), zero or more `meta` (§4.9). Every entity subtype
(`container`, `asset`, `primitive`, `html`, `lod`) accepts this full base
child set except where its own section says otherwise.

### 4.2 `transform`

**Coordinates.** A world's coordinates are left-handed, with +y up, in
metres. (VEML 2.3's documentation described a right-handed system; the
shells known to implement 3.x and 4.0 use this one, and §11 notes it.)

One element, two forms: `position` + `rotation` + (`scale` | `size`).
`position` (x,y,z doubles, default 0), `rotation` (quaternion x,y,z,w,
default identity), `scale` (default 1,1,1) or `size` (meters). Supplying
both `scale` and `size` is a document error resolved in favor of `size`
with a warning. An absent `transform` is the identity transform. `size`
states the entity's target extent in meters: content is uniformly scaled so
its natural bounds fit that extent. Resolving `size` needs the content's
natural bounds — declared `bounds` (§4.8) satisfy this without loading;
otherwise resolution waits for load. If loaded geometry diverges from a
declaration used for early resolution, the shell re-fits: bounds
misdeclaration costs performance, never final size (§4.8).

### 4.3 `container`

An entity with no intrinsic content; grouping only.

### 4.4 `asset`

References external renderable content. **The only way external geometry
enters a world.**

```xml
<asset src="gallery.glb"/>
<asset src="room.spz" type="model/splat+spz"/>
<asset src="scan.ply" type="model/pointcloud+ply"
       integrity="sha256-Qg4…"/>
```

- `src` (required): absolute or document-relative URI, or a `manifest:` name
  (§6.3).
- `type` (optional): media type. Resolution order: explicit `type` → HTTP
  `Content-Type` → file-extension inference. Core vocabulary:
  `model/gltf-binary`, `model/gltf+json`, `model/splat+spz`,
  `model/splat+ply`, `model/pointcloud+ply`. Unrecognized types follow the
  tolerance rule: the shell warns and renders a bounding placeholder.
- `integrity` (optional): subresource-integrity hash. When present, a shell
  MUST verify and MUST treat mismatch as load failure of that asset (not the
  document). An empty `integrity` value is a mismatch (fail closed), never
  equivalent to omission — here and everywhere `integrity` appears (§6.2,
  §6.4, §8.3). A failed asset — fetch failure, integrity mismatch, or a
  `src` stripped by the scheme allowlist (§1.4) — renders the same
  bounding placeholder as an unrecognized `type`.

**Orientation.** glTF is right-handed. A glTF asset's point (x, y, z) is
the entity's local point (−x, y, z): mirrored on x into the world's
left-handed frame, so the asset's +y stays up and the side glTF calls its
front (+z) faces the entity's +z. This is what shells that implement VEML
already do.

glTF extensions travel with the asset; VEML takes no position on asset
internals. Lights, audio emitters, materials, animations, and avatar formats
(VRM) are asset-layer concerns.

### 4.5 `primitive`

Authoring sugar for simple geometry without an asset round-trip.

```xml
<primitive shape="cube" color="#c0ffee"/>
```

`shape` ∈ cube · sphere · capsule · cylinder · plane · torus · cone ·
rectangular-pyramid · tetrahedron · prism · arch. `color` accepts hex
(#rgb/#rrggbb/#rrggbbaa), `color(r g b a)` with components as doubles in
0–1, or ASCII-case-insensitive **CSS named colors**; absent `color` is the
shell's neutral material, and a recognized-but-unparseable value (bad
hex, out-of-range component) is a document error resolved as that same
neutral default with a warning. Primitives fit a unit (1 m) bounding cube at default
scale, centered at the local origin. Center anchoring is **normative**:
primitive geometry is centered on the entity's local origin at every
scale. The 3.0-era engines anchored most shapes at the minimum corner
instead — §11 carries the migration correction.

### 4.6 `html`

An embedded web surface — the entirety of VEML's 2D UI story.

```xml
<html url="https://example.org/panel/" on-message="panel.event"/>
```

Renders as a sandboxed browsing context (an iframe in web shells) textured
onto the entity's plane: a quad in the entity's local XY plane, facing +Z,
sized by the transform's `size` (1 m square at default scale), at
shell-chosen resolution. Shells SHOULD expose the embedded context's
accessibility tree alongside its rendered surface (the integration model
is deferred, §12). `on-message` names an **action** (§8) fired when the
embedded page posts a message; the message payload becomes the action's data.
The reverse direction (world → page) is script-tier. The `html` boundary is a
capability boundary: the embedded context gets standard web sandboxing and
none of the world's capabilities.

### 4.7 `placement-socket`

A named attachment affordance on an entity: `name` (required — the
socket's author-facing identifier, any non-empty string; how assembling
shells reference it), `position`, `rotation`, `connecting-offset`. Sockets are how annotated
assets and assembling shells compose objects predictably; they carry no
behavior themselves.

### 4.8 `bounds`

An author-declared axis-aligned bounding box in the entity's local space:
`center` (x,y,z, default 0) and `size` (x,y,z, required), meters.

`bounds` is a promise about extent, consumed before content exists: a shell
MAY trust it to cull, prioritize, and defer-load the entity's subtree
**before fetching any of its assets** — nothing has to be downloaded to
know where things are and how big they are. On entities carrying an
`include` or heavy `asset` content, declared bounds are what make deferred
loading (§6.2) effective on the fast path.

Declared bounds govern **deferral only**, never visibility: once an
entity's content is loaded, its actual geometry governs rendering — a shell
MUST NOT clip or hide loaded content to its declared bounds, and MAY
thereafter cull by computed bounds, warning at its discretion when loaded
content diverges from the declaration. A misdeclared `bounds` therefore costs loading
performance, not correctness, and cannot hide content. A `bounds` missing
`size` is a document error: the element is disregarded with a warning (§2
covers unrecognized content; this covers recognized-but-invalid). No
`bounds`, well-formed or not, is ever fatal.

### 4.9 `meta`

```xml
<meta name="artwork.artist" content="R. Vance"/>
```

Structured, non-visual metadata on any entity. `name` is a string in the
same open namespace as action names (§8.1) — dot-separated by convention;
`content` is free text.
`meta` carries no runtime semantics: a shell MUST ignore it when rendering,
MUST preserve it through re-serialization, and MUST NOT treat any
well-formed `meta` as fatal. Indexers, search tools, and directories read
it. Names are author-defined with no registration — decentralization
applies to vocabulary too (§1.5) — except the `veml.` prefix, reserved
for spec-defined metadata (extending §8.1's action-name reservation). One
reserved name is defined now: `veml.alt`, the entity's text alternative
for non-visual presentation — what a screen-reader shell announces (the
full non-visual presentation model is deferred, §12).

### 4.10 `lod`

```xml
<lod>
  <transform>…</transform>
  <level range="0"><asset src="hero.glb"/></level>
  <level range="40"><asset src="hero-mid.glb"/></level>
  <level range="150"><primitive shape="cube" color="#888888"/></level>
  <level range="400"/>
</lod>
```

An entity subtype selecting among alternative representations by viewer
distance. Alongside the standard entity children (§4.1) it carries one or
more `level` elements. Each `level` holds zero or more entities or
`instance` elements (§6.4; instantiation is deferred until the level first
becomes active) and a `range` attribute (meters): the distance at which
that level becomes the active representation, remaining active until the
next level's range. An empty `level` means "render nothing beyond this
distance."

Rules: levels MUST be declared in ascending `range` order — out-of-order is
a document error the shell resolves by sorting, with a warning. `range`
defaults to 0 on the first level and is required on the rest; a later level
missing it is warned and ignored. Duplicate ranges keep the first (warn). A
viewer nearer than the first level's range sees the first level. A shell
SHOULD defer fetching a level's assets until that level is, or is about to
become, active, and SHOULD combine `lod` with declared `bounds` (§4.8) to
defer even the active level while the entity is far or out of view — `lod`
plus `bounds` is how heavy content stays off the fast path.

Non-`level` entity children of a `lod` — expanded instances included —
render distance-independently, alongside whichever level is active. A `lod`
with no `level` children is a document error treated as a `container`
(warn). A shell MAY cap the number of levels, ignoring the excess with a
warning.

Distance-driven level activation is a coarse position signal: `on-load`
actions on level entities fire as the viewer approaches, which a script
could bucket into distances it was never granted. In documents not holding
the `position-reporting` capability (§9), a shell MAY quantize distances or
delay level activation — and the load events it triggers — to blunt this
side channel.

A shell renders exactly one level at a time (or none, while an empty level
is active); a shell that does not evaluate distance MUST render the first
**non-empty** level and ignore the rest — `lod` degrades to its best
representation, never to nothing.

---

## 5. Entry points and fragment navigation

```xml
<entry-point id="default" >
  <position x="0" y="0" z="5"/><rotation x="0" y="0" z="0" w="1"/>
</entry-point>
<entry-point id="gallery-east">…</entry-point>
```

An `entry-point` is a named arrival location with avatar-placement semantics
(position + facing). Its `id` is required, author-chosen, and unique among
the document's entry points (a duplicate is a document error: first wins,
warn) — an author-facing name, exempt from §4.1's UUID rule. Children: zero
or one `position` (default 0 0 0) and zero or one `rotation` (default
identity; facing uses its yaw). `position` locates the avatar's **feet**
(its ground point); a shell SHOULD ground-snap within a small tolerance
rather than embed or float the avatar. It MAY carry `mode` (§5.1). A document
SHOULD declare an entry point named `default`; if none exists, the shell
derives one from declared `bounds` where present — otherwise it places
provisionally (the origin) and MAY refine **once**, as a comfort
transition, when first geometry arrives; never a silent hard teleport.

**Fragment binding:** the URL `https://example.org/world.veml#gallery-east`
directs the shell to place the arriving user at that entry point. An unknown
fragment falls back to `default` with a warning. This is the `#anchor` of
spatial content: shareable, bookmarkable, linkable. Fragment comparison:
the shell percent-decodes the URL fragment as UTF-8, then compares by
exact codepoint equality (case-sensitive) against `id` — non-ASCII entry
point names are first-class.

### 5.1 `navigation`

```xml
<navigation modes="walk teleport" speed="1.5" avatar-height="1.75"/>
```

Declares the world's intended locomotion. `modes` is a space-separated,
preference-ordered list from the open set walk · fly · teleport · examine ·
none. `modes` is optional: absent `modes` — or no `navigation` element at
all — leaves locomotion entirely to shell defaults. `speed` (default 1)
multiplies per-mode baseline velocities of 1.4 m/s (walk) and 5 m/s (fly)
and is ignored for modes without a defined baseline; a shell MAY cap the
effective speed for comfort — a document cannot compel a velocity.
`avatar-height` and `avatar-width` (meters) size collision and eye height. An `entry-point` MAY carry a `mode` attribute,
which promotes that mode to first preference for arrivals there (adding it
if absent from `modes`) — including when the document declares no
`navigation` at all, where it acts as the sole declared preference.

Normative: these are author *preferences*, not commands. The shell — as the
user's agent (§1.5) — MAY substitute an equivalent mode for accessibility,
comfort, or device reasons (a seated user teleports through a walk world);
a document's `modes` MUST NOT override a substitution the user's
accessibility settings require; and a shell MUST NOT allow `none` to
disable the user's ability to leave via shell chrome. Unrecognized mode names follow §2.

### 5.2 `locomotion`

```xml
<locomotion gravity="1.62" jump-height="2"/>
```

Declares the world's intended *vertical* movement feel: `gravity` (m/s²,
a non-negative magnitude acting downward) and `jump-height` (meters, the
apex a jump reaches above the feet). Both are optional; an absent
attribute — or no `locomotion` element at all — leaves that quantity
entirely to shell defaults. Pace is deliberately not here: horizontal
speed belongs to `navigation`'s `speed` multiplier (§5.1), and the two
elements never overlap. Like `navigation`, these are **hints, not commands**:
a shell SHOULD honor declared locomotion values and MAY clamp
them for comfort, accessibility, or device reasons (an immersive shell in
particular MAY limit effective gravity and jump arcs to avoid
discomfort) — a document cannot compel a trajectory. A value outside the
valid form (negative, non-numeric) follows §2: warn, attribute treated as
absent. How a jump is *triggered* — keys, buttons, hold-to-repeat — is
shell policy and out of VEML's scope. `locomotion` is root-document-only:
under composition it joins `background` and `navigation` in applying only
when its document loads as the root world (§6.2).

---

## 6. The web layer: link, include, manifest

### 6.1 `link` — navigation

```xml
<entity tag="north-door">
  <transform>…</transform>
  <link href="https://friend.example/studio.veml#foyer" label="Sam's studio"/>
</entity>
```

A `link` makes its parent entity a navigation affordance to another world (or
any URL). `href` (required, absolute or relative URI, MAY carry a fragment),
`label` (SHOULD be present). Activation consumes the raw `interact`
**input** at the shell layer, before the action bus: a consumed press
MUST NOT also be delivered to the bus, and document bindings (§8.2) can
neither intercept nor suppress link traversal — no *binding* can render
links unreachable. Geometry legitimately gates them: a link on an entity
that renders nothing is unreachable in-world (give doors geometry).
Whether a press is link activation uses the shell's ordinary target
selection — if the selected target is a link-bearing entity's rendered
content, the press is traversal; otherwise it reaches the bus. An
entity's rendered content includes its subtree's (the door pattern: link
on the parent, geometry on a child); where link-bearing entities nest,
the innermost wins. In an
`href`-fetched template, link targets are subject to §6.4's overlay
defense. Shells MAY additionally surface links through chrome (lists,
minimaps).

Normative shell behavior:
- Traversal MUST require a deliberate user action. Collision or proximity
  alone MUST NOT trigger navigation.
- Before traversal, the shell MUST make the real destination available to the
  user (at minimum the host). A label is presentation; the destination is
  truth. This is the anti-phishing requirement. Disclosure and the
  affordance itself MUST be perceivable in the shell's current
  presentation modality — a non-visual shell announces the `label`,
  falling back to the `href` host.
- Linking requires no consent from the destination. The return path is the
  shell's history mechanism, not a reciprocal link.

### 6.2 `include` — transclusion

```xml
<entity tag="annex">
  <transform><position x="40" y="0" z="0"/>…</transform>
  <include href="https://alice.example/annex.veml"/>
</entity>
```

An `include` composes another VEML document into this one, placed by the
parent entity's transform. Normative:

- Each `href` is an independent URI; shells SHOULD fetch includes in
  parallel and MUST NOT resolve them as sequential path segments.
- Relative URIs inside an included document resolve against that document's
  own URL, never the includer's.
- Shells MUST deduplicate subsidiary document fetches by URL across the
  document tree; depth limits alone do not bound fan-out. Eligibility is
  **local and static**: each document's first **16** direct subsidiary
  references — includes and prototype declarations (§6.4), in document
  order — are eligible for fetching, whenever needed, eagerly or lazily; a
  later reference is never fetched and fails gracefully (include omitted,
  prototype undeclared — warn). Local eligibility is computable from a
  document alone. As a resource backstop, shells MUST additionally bound
  total subsidiary fetches per document tree (at least 64 MUST be
  supported; shells SHOULD prioritize shallower documents when nearing the
  cap), failing gracefully beyond it.
- An included document contributes only its entity content: its
  `background`, `navigation`, `locomotion`, and `entry-point` elements
  apply when it loads as a root world, never when included — fragment
  navigation (§5) always targets the root document's entry points, and a
  child's `locomotion` is ignored with a warning.
- The included document's action bus is its own (§8.1); actions never
  cross the include boundary.
- Cycles MUST be detected; a cyclic include fails gracefully (warning +
  omission), never hangs or crashes.
- Shells MUST enforce a nesting depth limit (at least 8 MUST be supported)
  with graceful failure beyond it.
- The included document's own metadata (capabilities, scripts, sync) applies
  to *its* content under *its* origin; **a parent gains no authority over an
  included child, and a child inherits no capabilities from its parent.**
- `integrity` MAY pin an include by hash (archival composition).
- Shells MAY defer-load includes (impostor until approached).

### 6.3 `manifest` — logical-name indirection

```xml
<metadata>
  <manifest href="manifest.json"/>
</metadata>
…
<asset src="manifest:hero-model"/>
```

The manifest is a JSON object mapping logical names to absolute `http(s)`
URIs; a value that is itself a `manifest:` name is invalid (warn, name
unresolved) — resolution is single-step. Names match the placeholder
grammar (§6.4); a non-string value is an invalid entry (warn, name
unresolved). `manifest:` URIs in `src`/`href` resolve through it. The
manifest is fetched once, before the first dependent resolution; it counts
against §6.2's tree-wide backstop but occupies no per-document eligibility
slot (that enumeration covers includes and prototype declarations only). A
document using no `manifest:` names never fetches it — the manifest is not
fast-path work. If the manifest fails to
fetch or parse, and for any name absent from the map, each use warns and
the referencing element degrades as a failed reference. One small mutable file
redirects; content stays immutable and cacheable — relocatability without
mount tables.

### 6.4 `prototype` and `instance` — composable components

The web's component move applied to worlds: declare a parameterized chunk of
world once, at a URL, and instantiate it anywhere. As with `link` and
`include`, no consent from — and no coordination with — the defining origin
is required (§1.5, decentralization).

Declaration lives in `metadata`, inline or by reference:

```xml
<prototype name="lamp">
  <param name="glow" default="#fff4e0"/>
  <template>
    <entity>
      <transform>…</transform>
      <primitive shape="sphere" color="{glow}"/>
    </entity>
  </template>
</prototype>

<prototype name="chair" href="https://parts.example/chair.vemlp"
           integrity="sha256-…"/>
```

Instantiation is custom-element-shaped — parameters are plain attributes:

```xml
<entity tag="reading-lamp">
  <transform>…</transform>
  <instance prototype="lamp" glow="#ff4040"/>
</entity>
```

**Content model.** A `template` holds exactly one entity subtree drawn from
a **closed allowlist**: `entity`, `container`, `asset`, `primitive`, `lod`
(and `level`), `link`, and the structural children `transform`, `bounds`,
`meta`, `placement-socket`. Nothing else — in particular not `script`,
`capability`, `synchronization-service`, `synchronizer`, `html`, `include`,
or `instance`. The template root MUST be an entity or entity subtype; any
other root is a document error that leaves the prototype undeclared (warn).
A disallowed element in a template is a document error: the shell warns and
omits it, so a fetched definition can never become a code, capability, or
session vector. Expansion happens inside the instantiating document (the
same rule as §6.2: composition confers no capability). A component needing
behavior fires actions (§8.1) — subject to the `veml.*` source restriction
there — and the host document decides what, if anything, handles them.
Because a template cannot contain `instance` or `include`, expansion is
linear in document size and no recursion is possible; nested composition is
deferred (§12).

In an `href`-fetched template, action sources fire only when
**user-initiated**: a deliberate, shell-recognized activation input
(`interact`/`use`, §8.2) targeting the content — movement, gaze, collision,
and proximity are never user-initiated. The `on-load` attribute MUST NOT
appear (document error: warn + strip); a shell MUST NOT fire actions from
non-user-initiated sources (timers, proximity, load events) carried by the
template's assets; and an interaction target — for action sources and for
`link` traversal (§6.1) alike — MUST be **presented**: in a visual
presentation, geometry contributing rendered output at the activation
point (fully transparent, non-rendered, or wholly occluded geometry MUST
NOT receive activation); in every presentation modality, the activation
must be directed at a target the shell actually presents to the user —
§6.1's chrome-surfaced links qualify. This is the overlay defense,
modality-neutral. Inline templates, host-authored, are unrestricted.

**Expansion.** Instantiation replaces the `instance` element with the
template's entity subtree, which becomes a child of the containing entity;
the template root's transform composes beneath the host entity's transform.
Substitution operates on the **parsed document**: a parameter value becomes
(part of) a single attribute value in the expanded infoset and MUST NOT
introduce elements, attributes, or any markup structure — the observable
that rules out textual splice-and-reparse implementations. `id` MUST NOT appear in a template (warn +
strip); the shell generates fresh ids per instantiation (§4.1).

Expansion is presentation-side only: re-serialization (§2, §4.9) emits the
**source** infoset — declarations and `instance` elements intact, nothing
expanded — so document consumers, indexers included, read source, and
anything a template contributes (its `meta` included) is attributed to the
defining origin, never the host.

**Parameters.** A placeholder is `{name}`, appearing in attribute values
only, where `name` matches `[a-z][a-z0-9-]*` — also the grammar for `param`
names (deliberately ASCII-only: these are machine identifiers, never
presented to users, unlike action, meta, and socket names, which are
open). Each is replaced by the matching `instance` attribute, else the
`param` default; an unbound placeholder warns and substitutes the empty
string. `{{` denotes a literal `{`; `}` is always literal; any other `{`
that begins no well-formed placeholder is a document error resolved as
literal text with a warning. Placeholders are valid only in
**string-typed** attributes (`color`, `src`, `href`, `label`, `tag`,
`content`, …); parameterizing enum- or numeric-typed attributes (`shape`,
`range`, vector components) is not expressible in the valid form — that
is the typed-parameters feature deferred in §12. `tag`, `id`, and `on-load` are reserved on
`instance` alongside `prototype` (a `param` so named is a document error:
warn, ignored — and `tag`, `id`, or `on-load` appearing as an attribute
on `instance` itself is likewise ignored with a warning; `prototype` is
the selector); authors tag and observe
instantiated content via the
containing entity, whose `on-load` fires only after its **eager**
instances finish expanding — instances deferred in inactive lod levels
(§4.10) do not gate it. Substitution is a single left-to-right pass:
substituted values are never re-scanned for placeholders or escapes.
Placeholders MUST NOT appear in `integrity` values — a document error that
fails closed as an integrity mismatch (§4.4). URI base resolution: template attribute
values — with or without placeholders — resolve, if relative after
substitution, against the **defining** document's base URL, as includes do
(§6.2); a parameter value intended as a complete URI SHOULD be absolute (or
a `manifest:` name), since a host cannot rely on the definition's base.
`manifest:` names MUST NOT appear in an `href`-fetched template's **source
text** — the fetched document has no manifest to resolve them (a document
error: warn, reference unresolved); a `manifest:` name arriving as a
**parameter value**, by contrast, resolves through the host's manifest —
the host authored the value. An inline template resolves `manifest:` names
through the host document's manifest, which *is* its defining document.

**Resolution.** An `instance` resolves its prototype name against the
`prototype` declarations of the document it lexically appears in. Declaring
one name twice is a document error: the first declaration wins, the shell
warns. A declaration carrying neither `href` nor an inline `template`
leaves its name undeclared (warn). An `instance` naming an undeclared prototype — including one whose
fetch failed — is warned and omitted (§2's ignore rule); if the containing
entity declares `bounds` (§4.8), a shell MAY render a placeholder of that
extent.

**Fetching.** An `href` prototype resolves to a document whose root element
is `prototype` in the VEML namespace (recommended extension `.vemlp`;
media-type registration is deferred alongside VEML's own, §12). A fetched
root MUST contain an inline `template` and MUST NOT itself carry `href` —
definitions never chain — so, with `instance` and `include` excluded from
templates, fetching one definition is the entire dependency chain. `href`
and an inline `template` are mutually exclusive on a declaration: carrying
both is a document error (the inline template wins, warn — and inline
semantics apply throughout, including the declaration's own `param`
children). The referencing declaration's `name` is authoritative; a `name`
on the fetched root is ignored. When a definition is actually fetched,
`param` declarations and their defaults come from the fetched root;
`param` children on the referencing declaration are ignored with a
warning. A fetch failure or `integrity` mismatch leaves
the name undeclared (see Resolution). Fetching is lazy: a shell MUST NOT fetch a declaration
that is never instantiated, and SHOULD fetch on (or just ahead of) first
instantiation — declarations are not fast-path work. `integrity` SHOULD be
present on cross-origin references: an unpinned definition is mutable by
its origin, and every instantiating world inherits whatever it becomes;
pinning by hash is the archival form.

Shells MUST deduplicate definition fetches by URL and SHOULD share
instanced geometry across all instantiations of a prototype — repetition
is the memory story, not a memory cost. Definition fetches count against
the subsidiary-fetch budget (§6.2).

---

## 7. Synchronization declaration

```xml
<synchronization-service type="worldsync" address="wss://sync.example/…"
                         id="9c1f…" session="lobby"/>
```

VEML names the endpoint; the named protocol's own specification defines
the wire — VEML privileges none. Attributes: `type` (open protocol
identifier, required; `worldsync` is one such protocol, shown here as an
example), `address` (endpoint URI, required), `id` (author-chosen name,
required), `session` (session name within the service, required),
`ice-servers` (optional; space-separated `stun:`, `stuns:`, `turn:` or
`turns:` URIs that help a peer-to-peer transport reach the other
participants, a TURN entry carrying its credentials as
`turn:user:secret@host:port`). A shell that implements a protocol with no
peer-to-peer leg ignores `ice-servers`; one that does MAY let its own
configuration replace the list. A credential written here is published
with the document: an operator that needs TURN hands out short-lived
credentials through the shell's configuration rather than here.
Declarations MAY share an `id`: same-`id` declarations form a fallback
group in document order, and a shell uses the first whose `type` it
implements. Declarations in one group MUST name the same `session`
(differing sessions: the first declaration's wins, warn) — fallback
changes transport, never the room. An entity opts in via its `synchronizer` child —
`<synchronizer service="…"/>` — naming a group's `id`; when no declaration
in the group is usable, the entity stays static (warn). Documents with no
declaration are fully static — a conforming world requires no server.

---

## 8. Behavior: the action bus and two tiers

### 8.1 Actions

An **action** is a named event on the world's bus. Names are strings in an
open namespace — dot-separated by convention, with no structural
constraint beyond being non-empty and containing no whitespace. The `veml.*` prefix is reserved for
spec-defined built-ins (initial set: `veml.character.move`,
`veml.character.look`, `veml.interact`); all other names are author-defined,
no registration required. Sources of actions include input bindings (§8.2),
asset-carried interactivity (an annotated lever fires `lever.pulled`),
`on-load` and `on-message` attributes, timers, and scripts. Firing an action
nobody handles is a legal no-op (warn).

The bus is **per-document**: a composed document (§6.2) fires and handles
actions on its own bus, invisible to its parent's, and vice versa —
distinct documents never share a bus. (A fetched template expands into its
host document and shares the host's bus; its action sources are restricted
accordingly, §6.4.) Actions carry no handler-visible provenance in core;
source attribution is deferred (§12). Cross-document action interaction is
likewise deferred (§12).

Reserved `veml.*` built-ins are shell-consumed and may be targeted **only
by input bindings** (§8.2): a shell MUST ignore, with a warning, any
`veml.*` action fired from any other source (`on-load`, `on-message`,
timers, asset-carried interactivity, scripts) — content, first- or
third-party, never steers the user or auto-triggers interaction. Script
access to built-ins is deferred to the script API specification (§12), as
are the built-ins' payload and consumption semantics (what
`veml.character.move` carries; how `veml.interact` selects its target) —
until then, input bindings targeting them work, but their data shape is
shell-internal. Asset-carried interactivity awaits a named asset-layer
extension (§12); shells MAY support vendor mechanisms meanwhile, subject
to §6.4's source restrictions.

Markup carries **only action names, never code**.

### 8.2 `input-binding` (declarative tier)

```xml
<input-binding input="move"     action="veml.character.move"/>
<input-binding input="interact" action="veml.interact"/>
<input-binding input="use"      action="myGame.fireWeapon"/>
```

Bindings from shell-abstracted inputs to actions. Built-in (`veml.*`) targets
give a fully functional world with zero script. The core input vocabulary
is `move` (2-axis locomotion), `look` (2-axis view), `interact` (primary
activation), and `use` (secondary activation): a Level 1 shell MUST provide
all four across its devices (keyboard, touch, XR controllers); further
names are shell extensions, and unbound inputs follow §2. Defaults are
**per input**: any core input the document leaves unbound receives its
default binding (`move → veml.character.move`, `look →
veml.character.look`, `interact → veml.interact`) — declaring one binding
never forfeits the others, and a binding-free world is walkable. `use`
has no default — unbound, it fires nothing **on the bus**, deliberately
(secondary activation is world-defined); user-initiated action sources
(§6.4, §8.1) respond to activation inputs regardless of bindings. A document suppresses a default by binding
that input to an action nothing handles.

Input bindings are active only in the **root** document: an included
document's `input-binding` declarations are inert (warn) — its
interactivity remains user-initiated action sources on its own bus.
Relaxing this is part of cross-document action interaction (§12).

### 8.3 `script` (sandboxed tier)

`<script src="behavior.js"/>` loads code into a shell-provided sandbox that
exposes the session API surface, including `world.actions.on(name, handler)`
and `world.actions.fire(name, data)`. `script` accepts `integrity` with
§4.4's semantics (MUST verify when present; mismatch or empty value is load
failure of the script). Because a script holds every capability the user
granted, a document MUST carry `integrity` on any cross-origin `src`, and
a shell MUST NOT load a cross-origin script without it (warn, script
skipped).

Scripts are not fast-path work: a shell loads them after arrival (§3.4),
and MUST bound each script load with a timeout (shell-chosen); a
timed-out script counts as failed. A shell MUST NOT fire an entity's
`on-load` action until every declared script has loaded, failed, or timed
out; other actions fired before handlers register are legal no-ops
(§8.1). Sandbox semantics, the capability model
runtime, and the full script API are specified separately; core requires only
that scripts execute isolated per-document and receive no capability not
granted under §9.

---

## 9. Capabilities

```xml
<capability>microphone</capability>
<capability>persistent-storage</capability>
```

A document declares what it requests; the shell mediates user consent; the
default is **nothing** — an undeclared capability is unavailable to scripts.
Consent is origin-attributed: the prompt MUST identify the requesting
document's origin, and a composed document's request (§6.2) MUST be
distinguishable from the root world's — the consent analogue of §6.1's
"the destination is truth."
Initial vocabulary: `microphone`, `camera`, `persistent-storage`,
`network:<origin>` (fetch beyond the document's own origin; `<origin>` is
an RFC 6454 ASCII origin — `scheme://host[:port]`, no path, no trailing
slash, no wildcards; any other form is an unrecognized capability name),
`position-reporting`. Unrecognized capability names follow tolerance
(warn, treat as denied). Included documents request independently (§6.2).

---

## 10. Profiles and conformance levels

Core (this document) is deliberately small: ~30 constructs. Heavier features
are **profiles** — named extension vocabularies a document declares
(`<profile>sky</profile>`) and a shell advertises. Initial profile registry:
`lighting` (asset-layer glTF `KHR_lights_punctual` honored on opt-in —
SPECIFIED, see `profiles/lighting.md`), `sky` (rich procedural sky,
successor of 3.0's lite-procedural-sky — SPECIFIED, see `profiles/sky.md`;
the first profile to carry its own vocabulary, in its own namespace),
`environment` (an image as the scene's reflections and ambient light without
being drawn as the sky — SPECIFIED, see `profiles/environment.md`), `fog`,
`terrain` (streamed ground as an OGC 3D Tiles tileset in a flat world's frame,
the mechanism `planet` uses for a sphere — SPECIFIED, see
`profiles/terrain.md`), `water` (bodies of water a visitor swims in, carved
by blockers — SPECIFIED, see `profiles/water.md`), `voxel` (unspecified: a
planet's blocks are a `planet` surface scheme's business, and no flat-world
author has asked), `vehicles` (unspecified: it waits on the physics engine
decision and on OMI's vehicle work, which it will track rather than lead),
`planet` (a world that is a planet: a sphere seen from the ground to orbit in
one zoom, its terrain an OGC 3D Tiles tileset, sites on its surface and bodies
in orbit — SPECIFIED, see `profiles/planet.md`; surface schemes such as
`profiles/planet-cube-sphere.md` add what a particular planet's surface is made of),
`client-hints` (successor of 3.0's controlflags, and where a world sets the
shell's visitor-facing features: avatar, name labels, chat, touch controls,
menu, VR controls — SPECIFIED, see `profiles/client-hints.md`). Profile specs live beside
this one; a shell lacking a declared profile warns and degrades per §2.

Conformance levels bind to the Conformance Suite: **Level 0 (Minimal)** —
parse + the §3.4 load order, tolerance, entities/assets/primitives,
background, entry points + fragments + navigation preferences, link,
include, manifest, prototypes, placement sockets, bounds + lod
(first-non-empty-level fallback at minimum), entity meta preservation. **Level 1
(Interactive)** — action bus, input bindings, html entity, capabilities,
script sandbox. **Level 2 (Collaborative)** — synchronization. Profiles are
orthogonal to levels.

The parse- and fetch-layer security rules — DTD rejection and the scheme
allowlist (§1.4), fetch eligibility and budgets (§6.2), and `integrity`
verification (§4.4, §8.3) — are Level 0 requirements. The fetched-template
action-source restrictions (§6.4) bind at Level 1, where the action bus
exists; a Level 0 shell satisfies them vacuously — as it does §8.3's
script-integrity rules, since scripts load only at Level 1. Requirements bind to the
Conformance Suite in three modes: **structural** (asserted on the loaded
scene graph and the §2 diagnostics channel), **network** (asserted on the
request log of a suite-controlled server: the scheme allowlist, budgets,
deduplication, laziness, DTD non-fetching), and **human-audited** (shell
UI obligations no black-box test can verify: §6.1 destination disclosure,
§9 consent attribution, §5.1 chrome exit — checked by review, not by the
suite).

---

## 11. Migration from 3.0

4.0 is not 3.0-compatible. Mechanical converter (`veml3to4`, backlog).

**Coordinates.** 4.0 states its coordinate convention (§4.2: left-handed,
+y up) and how a glTF asset enters it (§4.4). VEML 2.3's documentation
described a right-handed system, while the shells known to implement 3.x
use the left-handed one stated here. A 3.x world keeps its positions
unchanged.
This section's requirements bind the **converter** — a conformance class
that exists when `veml3to4` does; until then the section is normative for
that future tool and informative for shells and documents.

**Content.** Primitive types → `primitive[@shape]` (all eleven shapes map
1:1). `mesh` → `asset` — repackaging, not renaming: 3.0's multiple
`mesh-resource` files and its `mesh-name` selector have no `asset`
equivalent, so multi-resource meshes are bundled by the converter or
flagged. Canvas UI family → authors migrate to `html` (converter emits a
stub page). `audio`/`light`/`character` → glTF-side equivalents flagged
for manual review. `terrain`/`water`/`sky`/`vehicles`/`voxel` and the
`effects` fog block → the corresponding profiles (`terrain` `water` `sky`
`vehicles` `voxel` `fog`, §10). `controlflags` → the `client-hints`
profile's `hints:vr` (§10): its four honoured fields map one to one, and the
never-implemented poker, hand-interaction and grab-move fields stay in the
converter's review comment. `placement-socket` gains its required `name` (§4.7); the
converter synthesizes one.

**Prototype extensions.** Late-3.0-era prototypes carried two additive
extensions under a working-name namespace ("3.1", never released):

- `link` — an entity child whose element *text* was the label; converts
  to 4.0 `link` with `label` as an **attribute** (a same-shape trap:
  text becomes attribute), `href` preserved (§6.1).
- `spawn` — environment-level position + rotation; converts to
  `entry-point id="default"` (§5). A document without `spawn` gets no
  synthetic entry point — shell derivation covers it.

The converter accepts the working-name namespace as an alias of
3.0-plus-extensions; nothing canonizes it as a version.

**Primitive anchoring.** 4.0 primitive geometry is centered on the
entity's local origin (§4.5). The 3.0-era engine anchored most shapes at
the scaled shape's **minimum corner** instead — verified against the
prototype-era Unity runtime's **prefab vertex data** (each shape's
unit-space mesh bounds, applied by the VEML handler with no offset),
which is the authority for this table; the corpus itself had only
surfaced the scaled cube/plane/cylinder subset:

| 3.0 shape | min-anchored axes |
|---|---|
| cube, cylinder, torus, cone, rectangular-pyramid, tetrahedron, prism, arch | x, y, z |
| plane | x, z (flat — the surface sits at the authored y) |
| sphere | y only (x/z centered; bottom-anchored) |
| capsule | none (already centered) |

For an **identity-rotation scale transform**, a converter corrects the
instance's `position` by **+scale/2 on each min-anchored axis** and
changes nothing else. A rotated, scaled, min-anchored primitive's pivot
semantics are **under-determined in the 3.0 corpus** — a converter
MUST flag such an instance for review rather than guess.

**Same-name traps** — 3.0 constructs whose 4.0 namesake means something
else; a converter MUST NOT pass these through verbatim:

- 3.0 `size` is a per-axis extent in meters (components defaulting 1,
  potentially non-uniform); 4.0 `size` is a uniform-fit extent with all
  components required (§4.2). The converter emits `scale` computed against
  the content's natural bounds, or flags for review — never a bare rename,
  which would change rendered geometry.
- 3.0 `capability` declares a feature the world needs; 4.0 `capability`
  requests user consent (§9), where an unknown name is denied. 3.0 values
  are dropped with notice unless they match the §9 vocabulary.
- 3.0 `color()` accepts comma-separated components and HDR values above 1;
  4.0 `color(r g b a)` components are doubles in 0–1, and an out-of-range
  value resolves to the neutral default (§4.5). The converter rescales (or
  clamps, flagging HDR intent) — passed through verbatim, 3.0 colors
  silently degrade to the shell's neutral material.
- 3.0's `on-load-event` and `html` `on-message` carry **code strings**;
  the similarly named 4.0 attributes carry action names only (§8.1). Like
  `inputevent`, each becomes a generated named handler in a converted
  script plus the matching binding. Passed through verbatim, the behavior
  is silently lost either way: `on-message` code becomes an unbound no-op
  action name, while `on-load-event` — no 4.0 attribute of that name
  exists — is dropped as unrecognized (§2). (`button`'s `on-click-event` needs no
  trap entry: the canvas family, `button` included, migrates to the
  `html` stub, where the converter folds its code into the generated
  page.)

**Behavior and services.** `inputevent` code strings → generated named
handlers + `input-binding`; end-of-gesture inputs (3.0's
`endmove`/`endlook`) have no core counterpart and are flagged — as shell
extensions (§8.2), bindings for them work only where a shell provides
those inputs. Metadata `script` (text
content, unsandboxed engine API) → `script src`, flagged for manual review
in every case: the 4.0 sandbox (§8.3) exposes a different surface, and a
cross-origin `src` additionally requires `integrity`, which the converter
fetches-and-hashes or flags. `synchronizationservice`/`synchronizer` →
§7's model, flagged: 3.0's optional attributes are all required in 4.0;
3.0's `id` was a session UUID while 4.0's `id` is a fallback-group key,
so coincidentally equal 3.0 ids MUST NOT be merged into one group; and
the text-content `synchronizer` becomes `<synchronizer service="…"/>`.

Per §2, a 4.0 shell encountering un-migrated 3.0 elements warns and
ignores.

## 12. Deferred (named gaps, deliberately out of core)

Full script/session API spec · sandbox implementation requirements · ACL and
multi-author permission format (Level-2-adjacent; tracked in an external
strategy document outside this spec set) ·
the HTML primary-form parsing rules, the sugar
expansion table, and the self-boot contract (§3.3) · cinematic `viewpoint` ·
profile specifications themselves (graduated: `lighting`,
`profiles/lighting.md`, 2026-08-26; `sky`, `profiles/sky.md`, 2026-09-15;
`environment`, `profiles/environment.md`, 2026-09-25;
`client-hints`, `profiles/client-hints.md`, 2026-09-27;
`planet`, `profiles/planet.md`, 2026-10-09; `terrain`, `profiles/terrain.md`,
and `water`, `profiles/water.md`, 2026-10-09; `fog`, `voxel` and `vehicles`
remain deferred) · media-type registration (`.veml` and
`.vemlp`) · typed prototype parameters (beyond string substitution) ·
slot-style content projection into templates · nested prototype composition
(`instance` or `include` within templates) · script access to `veml.*`
built-ins (§8.1) · cross-document action interaction (§8.1) · action
provenance in the script API (§8.1) · built-in action payload
semantics and the asset-interactivity extension (§8.1) · per-element
language and explicit text direction (`xml:lang` granularity, `dir`;
§1.4, §1.6) · the non-visual presentation model (§4.6, §4.9, §6.4).

---

*Change log: 2026-08-03 first draft, from the resolved audit. 2026-08-14
composition & navigation amendment: web design principles named (§1.5);
entity `bounds`, `meta`, `lod` (§4.8–4.10); `navigation` preferences (§5.1);
composable `prototype`/`instance` (§6.4); conformance Level 0 extended to
match (§10). 2026-08-14 review hardening: template content allowlist,
no-recursion rule, infoset-level substitution + escaping, id and base-URI
rules (§6.4); lod ordering and non-empty fallback (§4.10); bounds validity
and reconciliation (§4.8); meta conformance MUSTs (§4.9); navigation
baselines, speed clamping, mode override semantics (§5.1);
`veml.character.*` source restriction (§8.1); transform default (§4.1–4.2);
service name removed from §4.7. 2026-08-14 second review round: URI scheme
allowlist (§1.4); `veml.*` source restriction widened beyond character
motion (§8.1); prototype fetching made lazy, definition chaining forbidden,
href/inline exclusivity and failure semantics (§6.4); defining-base URI
resolution, placeholder grammar, single-pass substitution, integrity
placeholder ban, source-infoset serialization rule (§6.4); bounds made
deferral-only, never visibility (§4.8); empty integrity fails closed
(§4.4); lod non-level children and empty-lod rules (§4.10); navigation
defaults and per-mode speed scope (§5.1); §4.1 children list completed
(`link`, `include`). 2026-08-14 third review round: scheme allowlist
generalized to every dereferenced URI, applied post-manifest, with a
`ws(s)` carve-out for sync addresses (§1.4); include base-URI rule, URL
dedup, and total-fetch budget (§6.2); `manifest:` banned in fetched
templates (§6.4); empty-integrity rule generalized (§4.4); entry-point
`mode` without `navigation` resolved (§5.1); subtype child-set inheritance
stated (§4.1); lod position side channel acknowledged (§4.10); §12
deferral scope corrected to all `veml.*` built-ins. 2026-08-14 fourth
review round: per-document action bus (§8.1, §6.2); `on-load` banned in
fetched templates, template root must be an entity (§6.4); fetch budget
extended to prototype definitions, definition dedup MUST (§6.2, §6.4);
manifest resolution scoped to `src`/`href` and single-step, values
`http(s)` only (§1.4, §6.3); relative references inherit the document's
base scheme (§1.4); host-supplied `manifest:` parameter values resolve
through the host manifest (§6.4); included documents contribute entity
content only (§6.2). 2026-08-14 fifth review round: input bindings
root-only (§8.2); auto-fire ban in fetched templates generalized to all
non-user-initiated sources (§6.4); per-document bus wording scoped to
documents, template-shares-host-bus stated (§8.1); deterministic
breadth-first budget accounting (§6.2); href-prototype `param` placement
(§6.4); §1.4 exception clause grammar. 2026-08-14 sixth review round: DTD
prohibited, external entities never processed, base URI = retrieval URL
with `xml:base` ignored (§1.4); `script` gains `integrity` (§8.3);
capability consent origin-attributed (§9); "user-initiated" defined and
interaction targets must be rendered geometry in fetched templates —
overlay defense (§6.4); fetch budget recast as static eligibility,
viewer-independent (§6.2); href+inline param collision resolved (§6.4);
content-model paragraph reordered; action provenance added to §12.
2026-08-17 seventh review round (testability + implementer lenses):
diagnostics channel made normative with `veml:<section>:<slug>`
identifiers (§2); value syntax defined (§1.6); element grammars completed —
metadata carriers (§3.1), `background` (§3.2), `transform` `size` (§4.2),
primitive dimensions and color ranges (§4.5), `html` surface (§4.6),
`entry-point` (§5), manifest failure and pipeline semantics (§6.3),
`synchronization-service`/`synchronizer` (§7); fetch budget recast as
local per-document eligibility (16) with a tree backstop (≥64), depth ≥8
(§6.2); parallel fetch relaxed to SHOULD; core input vocabulary and
default bindings defined (§8.2); built-in payloads and asset-interactivity
deferred explicitly (§8.1, §12); cross-origin script `integrity` upgraded
to MUST (§8.3); asset failure observable (§4.4); parse-layer rejection
scoped to well-formed documents, scheme-error scope, `instance` reserved
attributes, overlay-defense visibility defined, lod single-active-level
observable, bounds divergence warning discretionary (§1.4, §4.8, §4.10,
§6.4); §10 gains level assignments for security rules and the three
test-binding modes. 2026-08-17 editorial round: shell defined at first use
(§1.1); synchronization made protocol-neutral — `worldsync` an example,
not a privileged name (§1.2, §7); `xmlns` optional with `version` (§1.4);
HTML embedding direction sketched (§3.3); normative load order added
(§3.4); authority split defined in §1.3 — `veml-4.0.xsd` created as the
source of truth for document grammar (this prose informative on grammar,
normative on behavior); metadata child order bound to the schema.
2026-08-17 eighth review round (schema reconciliation): template allowlist
enforced structurally in the schema (restricted template-entity family, no
`id`), `size` components required, action/meta name grammar opened to any
non-whitespace string, `placement-socket` gains required `name` (schema +
§4.7), `version` pattern-constrained; `instance` sanctioned in lod levels
with deferred instantiation (§4.10); `synchronization-service` `type`
marked required and same-`id` fallback groups defined, replacing
per-declaration fall-through (§7); sync `address` exempted from the scheme
allowlist in favor of protocol-governed schemes (§1.4); xmlns-free
processing reconciled with the schema (namespace inserted pre-validation;
authoring tools MUST emit it, §1.4); script load timing and pre-handler
action policy (§8.3); §3.3 embedded world made a distinct consent
subject; §3.4 scoped to presenting shells for steps 5–6; derived
placement grounded in bounds or provisional origin (§5); empty prototype
declaration behavior (§6.4). 2026-08-18 ninth review round: sync `address`
restored to network endpoints only — local-resource schemes invalid
regardless of protocol (§1.4); `on-load` gates on eager content only,
after the script gate, with deferred lod/include content excluded (§4.1,
§6.4); script loads timeout-bounded (§8.3); non-4.0-namespace roots
defined as not-VEML-4.0 (§1.4); fallback groups share one `session` (§7);
provisional placement refines at most once, as a comfort transition (§5);
§1.6 `size` exception stated; name grammar opened in prose to match
schema (§8.1, §4.9); schema: socket `name` re-typed permissive
`identifier`, stale sync/prototype/entity annotations corrected, identity
constraints added (unique entry-point ids, keyed prototype names,
instance→prototype keyref; sync ids deliberately unconstrained).
2026-08-18 tenth review round (coherence): stale summaries updated —
"external geometry" (§4.4 + schema), scripts' loading/gating in-spec
(§1.2), `controlflags` → `client-hints` migration target (§11); §1.4
scheme rules reordered with per-case error resolutions and the `address`
exemption isolated; manifest fetch scoped to the tree backstop only
(§6.3); Level 0 completed with background, placement sockets, and the
§3.4 load order (§10); non-empty name constraint stated (§8.1); §1.6
vector-parent list corrected; provenance anchor added (§8.1) and §12
citations repaired; duplicate reserved-attribute sentence removed (§6.4);
socket name grammar stated (§4.7); schema: `.vemlp` root re-typed
`prototypeRoot` (template required, no href, name optional) and identity-
constraint annotation scoped to lexical reference.
2026-08-18 eleventh review round (migration audit + author walkthrough):
§11 rewritten to cover the behavior/services half of 3.0 — same-name
traps (`size`, `capability`, code-string `on-*` attributes) called out as
MUST-NOT-pass-through, `effects`→`fog`, `script`, sync, `mesh`
repackaging, end-of-gesture inputs, socket `name` synthesis; input-binding
defaults made per-input (§8.2); placeholders scoped to string-typed
attributes, closing the two-artifact undecidability (§6.4 + schema
annotation); entry-point `position` anchored at avatar feet with
ground-snap (§5); link activation defined as `interact` on rendered
content (§6.1); CSS named colors and unparseable-color recovery (§4.5);
stripped `src` mapped to the bounding placeholder (§4.4);
`network:<origin>` serialization fixed to RFC 6454 form (§9); out-of-order
recognized children globally accepted-with-warning (§2).
2026-08-18 twelfth review round: §11 corrections against 3.0 ground truth
— `size` described as per-axis extent in meters (remedy unchanged),
`color()` HDR/comma trap added, `on-click-event` moved out of the trap
list (folds into the `html` stub), end-of-gesture inertness restated as
extension-dependent; link traversal bound to the raw `interact` input at
the shell layer, immune to document rebinding or suppression (§6.1).
2026-08-18 thirteenth review round: overlay defense extended to `link`
traversal in fetched templates (§6.4, §6.1); consumed presses exclusive —
MUST NOT also reach the bus (§6.1); "unreachable" claim scoped to
bindings, with geometry gating legitimate (§6.1); activation boundary
bound to the shell's ordinary target selection (§6.1); §11
`on-load-event` failure mode corrected (unrecognized attribute, not
unbound action); `size` re-fit on divergence stated (§4.2); `use` has no
default binding, deliberately (§8.2); reserved attributes on `instance`
itself ignored with warning (§6.4).
2026-08-19 fourteenth review round (i18n/a11y horizontal + keyword
census): fragment comparison defined as UTF-8 percent-decode + codepoint
equality (§5); IRIs mapped via the WHATWG URL Standard, `xml:lang`
honored on the root — schema attribute added (§1.4, schema); overlay
defense made modality-neutral (§6.4); `veml.` prefix reserved for meta
names and `veml.alt` defined as the entity text alternative (§4.9); bidi
via UBA first-strong (§1.6); link disclosure and affordances perceivable
in the current modality (§6.1); html embeds SHOULD expose their
accessibility tree (§4.6); document `modes` cannot override
accessibility substitutions (§5.1); `vem:name` recorded as deliberately
ASCII-only; ASCII-case-insensitive colors (§4.5); §2 warning strength
unified to MUST-surface; §11 given a converter-class status note; xmlns
emission restated as document conformance (§1.4); §10 vacuity note
extended to script integrity; round-13 regressions fixed — reserved-set
pronoun scoped (§6.4), `use` scoped to the bus (§8.2), link reach over
descendant geometry + innermost-wins (§6.1); non-visual presentation and
language/direction granularity added to §12. 2026-08-24
implementation-contact patches (#31): `version` attribute stated as the
discriminator for namespace-less documents (§1.4); §11 gains the
prototype extensions — `link` label-as-text trap, `spawn` → default
entry point, working-name namespace accepted as an alias only.
2026-08-25 locomotion hints (#41): `locomotion` element added — gravity
and jump-height as hints, never commands (§5.2, schema); pace explicitly
left with `navigation` `speed` (§5.1); root-document-only under
composition (§6.2); `walk-speed` considered and dropped as a duplicate
surface.
2026-08-26 §3.3 promoted from sketch to the #33 decision (#40): the
mechanical `veml-` prefixing is the primary HTML serialization (a
prefixed document and its XML form are the same document); the
ergonomic sugar tier is named as a specified-later surface defined by
lossless expansion onto the primary form, conformance judged on the
expanded form; the self-boot contract named as its own deferred
surface; §12's monolithic serialization entry replaced by the three
named pieces.
2026-08-26 primitive anchoring contact patch (#43): §4.5 center
anchoring made explicitly normative; §11 gains the per-shape
min-anchored-axes table from prototype-era prefab vertex evidence
(wider than the corpus-observed cube/plane/cylinder subset — sphere
bottom-anchored, capsule already centered), the +scale/2
identity-rotation correction, and the rotated-instance MUST-flag rule.
2026-08-26 lighting profile specified (#69): first graduation from §12's
deferred profile specifications — `profiles/lighting.md` created; asset-
layer glTF `KHR_lights_punctual` honored on `<profile>lighting</profile>`
opt-in, shell default lighting stands down when declared lights are
present, no new VEML vocabulary (lights stay in glTF where authoring
tools already emit them); the without-profile stripping behavior restated
as the core default; §10 registry updated.
2026-10-08 `ice-servers` added to `synchronization-service` (§7): an
optional, additive attribute naming the STUN/TURN servers a
peer-to-peer transport may use, with the published-credential caveat;
`worldsync-protocol.md` §4.2.1 says how its rendezvous mode uses it.
2026-10-08 §1.4: a URI-bearing attribute containing a C0 control
character or DEL is refused as a bad scheme is, before the scheme is read
(the URL parser strips those characters and would read the scheme they
hide); conformance `net-006-control-char-scheme` pins it.
2026-10-09 `terrain` and `water` profiles specified (`profiles/terrain.md`,
`profiles/water.md`): streamed ground as a 3D Tiles tileset in the world
frame, sharing the `planet` profile's tile rules; water bodies with a
surface, colour, clarity and waves, blockers beside them, and the swim rule;
§10 says why `voxel` and `vehicles` stay unspecified.
2026-10-09 `planet` profile specified: a world that is a planet, placed by
latitude, longitude and altitude, its terrain an OGC 3D Tiles tileset chosen
by on-screen error from the ground to orbit; any world may include planets,
moons included, and a visitor is held by the smallest hold sphere that
contains them;
`planet:site` places any number of worlds on the surface, loaded by distance
in place of §6.2's reference cap; links name a spot by fragment
(`#at=lat,lon`, with `on=` for a planet inside another world); a surface
scheme, named by URL, adds what a particular surface is made of, the first
`cube-sphere/1`. §4.2 states the coordinate convention shells use
(left-handed, +y up, metres), §4.4 how a glTF asset enters it (mirrored on
x), and §11 notes that VEML 2.3's documentation said otherwise; `sky`'s
sun azimuth is stated as seen from above (90 is −X); §10 registry updated.*
