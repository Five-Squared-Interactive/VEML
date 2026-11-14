# VEML — Virtual Environment Markup Language

VEML describes a world: a navigable 3D document at a URL. A world is a
folder of files; its central document is VEML, which composes and links the
models, the sky, where visitors start and what the world is connected to.
VEML does not encode geometry or materials: those are glTF, which a VEML
document references. A world is to VEML what a page is to HTML.

## The specification

- [`VEML.md`](VEML.md) — the specification. Status: editor's draft; the
  text is complete and in use, and it is not yet frozen.
- [`VEML.xsd`](VEML.xsd) — the schema. The schema and the prose share
  authority: the schema says what is well-formed, the prose says what it
  means.
- [`profiles/`](profiles/) — the profile specifications the text names in
  §10: `lighting`, `sky`, `environment`, `client-hints`, `terrain`, `water`,
  `planet` and the cube-sphere surface scheme.

A world is a plain text file. Published worlds serve it as `index.veml`
beside their models, and any page that opens worlds reads it.

## Licence

MIT, see [`LICENSE.txt`](LICENSE.txt).
