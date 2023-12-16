module AST

type Color =
| Gold of string
| Black of string
| Silver of string

type Design =
| Heart of string
| Band of string
| MoonAndStars of string

type Details = {color: Color; design: Design; size: int; file: string}