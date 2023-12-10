module Evaluator
open Parser
open System
open AST


let format (word: string) =
    if word.Contains(".") or word = "vertex" then
        word
    else
        word + ".0"

let createInstructionManual (details: Details) =
    let fileName = "instructionManual.txt"
    let instructionManual = System.IO.File.ReadLines("instructionmanualtemplate.txt") |> Seq.toList
    for line in instructionManual do
        let text = line.ToString() + Environment.NewLine
        if text.Contains("(COLOR)") then
            let newLine = "While you are working with the lovely people at the Makerspace, be sure to specify that you would like to print a " + details.color.ToString() + " ring." + Environment.NewLine
            System.IO.File.AppendAllText(fileName, newLine)
        else
            System.IO.File.AppendAllText(fileName, text)

let evalDetails (details: Details) =
    let templateLines = System.IO.File.ReadLines("bandsize5template.stl") |> Seq.toList

    // Scale is determined from standard ring sizing
    let scale = 1.05449**(float details.size - 5.0)
    //let scale = 2

    for line in templateLines do
        let text = line.ToString() + Environment.NewLine

        if text.Contains("vertex") then
            let words = line.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries) |> Seq.toList
            
            let newWords = 
                match words with
                | [a; x; y; z] -> ["vertex"; (float x * scale).ToString(); (float y * scale).ToString(); (float z * scale).ToString()]
            
            let changedLine = "      " + newWords[0] + "   " + format newWords[1] + " " + format newWords[2] + " " + format newWords[3] + Environment.NewLine
            System.IO.File.AppendAllText(details.file, changedLine)

        else
            System.IO.File.AppendAllText(details.file, text)
    createInstructionManual details

