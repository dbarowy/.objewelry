open Combinator
open Parser
open Evaluator
open System

[<EntryPoint>]

let main args =
    let usage() =
        printfn "dotnet run <details>"
        printfn "\twhere <details> has the form <color> <design> in size <size> <file>"
        printfn "\tfor example, \"gold cat ears in size 7 file.txt\""
        exit 1
    
    if (Array.length args = 0) || (Array.length args > 1) then usage()
    else
        let input = args[0]
        let parsed = parse(input)

        match parsed with
        | Some details -> evalDetails details
        | None -> printfn "Not a valid ring specification."
    0