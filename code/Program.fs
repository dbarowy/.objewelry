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
        // Handles direct specification on terminal, ex. dotnet run "black cat ears in size 5 catring.txt"
        if input.Contains(" ") then
            let parsed = parse(input)
            match parsed with
            | Some details -> evalSquareDetails details
            | None -> printfn "Not a valid ring specification."
        // Handles specification within a file,  ex. dotnet run example-2.txt
        else
            let fileInputList: string list =
                try
                    IO.File.ReadLines(input) |> Seq.toList
                with
                    | :? System.IO.FileNotFoundException -> 
                        printfn "The file you specified does not exist"
                        usage()

            let parsedFile = parse(fileInputList[0])
            match parsedFile with
            | Some details -> evalSquareDetails details
            | None -> printfn "Not a valid ring specification."   
    0