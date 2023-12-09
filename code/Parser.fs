module Parser
open Combinator

type Color =
| Gold of string
| Black of string
| Silver of string

type Design =
| CatEars of string
| Band of string
| MoonAndStars of string

type Details = {color: Color; design: Design; size: int; file: string}

// Parses a size specification and casts it to an int
let size: Parser<int> =
    pbetween pws0 ((pmany1 pdigit) |>> (fun x -> x |> stringify |> int)) pws0

// Parses a string specifying color and casts it into type Color
let color: Parser<Color> =
    ((pbetween pws0 (pstr "gold") pws0)  |>> (fun g -> Gold(g))) 
    <|> ((pbetween pws0 (pstr "black") pws0) |>> (fun b -> Black(b)))
    <|> ((pbetween pws0 (pstr "silver") pws0) |>> (fun s -> Silver(s)))

// Parses a string specifying design choice and casts it into type Design
let design: Parser<Design> =
    ((pbetween pws0 (pstr "cat ears") pws0) |>> (fun c -> CatEars(c)))
    <|> ((pbetween pws0 (pstr "band") pws0) |>> (fun b -> Band(b)))
    <|> ((pbetween pws0 (pstr "moon and stars") pws0) |>> (fun m -> MoonAndStars(m)))

let file: Parser<string> =
    pbetween pws0 (pseq (pmany1 pletter) (pstr ".stl") (fun (a,b) -> (a |> stringify) + ".stl")) pws0

let details: Parser<Details> =
    (pseq color 
        (pseq design 
            (pseq (pright (pstr "in size") size) file (fun (s,f) -> {size = s; file = f; color = Gold(""); design = CatEars("")}))
        (fun (d,sf) -> {design = d; size = sf.size; file = sf.file; color = Gold("")}))
    (fun (c,dsf) -> {color = c; design = dsf.design; size = dsf.size; file = dsf.file}))

let grammar = pleft details peof

let parse (s: string): Details option =
    match grammar (prepare s) with
    | Success(ws,_) -> Some ws
    | Failure(_,_) -> None