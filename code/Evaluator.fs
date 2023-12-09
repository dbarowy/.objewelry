module Evaluator
open Parser
open System


let format (word: string) =
    if word.Contains(".") or word = "vertex" then
        word
    else
        word + ".0"


let evalDetails (details: Details) =
    System.IO.File.AppendAllText(details.file, details.ToString())

    let templateLines = System.IO.File.ReadLines("template.stl") |> Seq.toList
    let line = templateLines[4].ToString()
    let scale = 2.54

    if line.Contains("vertex") then
        let words = line.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries) |> Seq.toList
        printfn "%A" words
        let newWords = 
            match words with
            | [a; x; y; z] -> ["vertex"; (float x * scale).ToString(); (float y * scale).ToString(); (float z * scale).ToString()]
        
        printfn "%A" newWords

        let changedLine = "      " + newWords[0] + "   " + format newWords[1] + " " + format newWords[2] + " " + format newWords[3]
        printfn "%s" changedLine

            
    


let evalSquareDetails (details: Details) =
    let templateLines = System.IO.File.ReadLines("ringtemplate.stl") |> Seq.toList

    let scale = float details.size

    for line in templateLines do
        let text = line.ToString() + Environment.NewLine

        if text.Contains("vertex") then
            let words = line.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries) |> Seq.toList
            
            let newWords = 
                match words with
                | [a; x; y; z] -> ["vertex"; (float x * scale).ToString(); (float y * scale).ToString(); (float z * scale).ToString()]
            
            let changedLine = "      " + newWords[0] + "   " + format newWords[1] + " " + format newWords[2] + " " + format newWords[3]
            System.IO.File.AppendAllText(details.file, changedLine)

        else
            System.IO.File.AppendAllText(details.file, text)
        



