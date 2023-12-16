module Test
open Parser
open System
open AST
open Evaluator

let parserTest =
    let input = "gold band in size 7 ring.stl"
    let parsed = parse(input)

    let expected = Some {color = Gold "gold"; design = Band "band"; size = 7; file = "ring.stl"}
    assert (expected = parsed)

    let input2 = "red band in size 2 ring.stl"
    let parsed2 = parse(input2)

    let expected2 = None
    assert (parsed2 = expected2)


let evalTest =
    let inputDetails = {color = Gold "gold"; design = Band "band"; size = 7; file = "ringTest.stl"}
    evalDetails inputDetails

    let expectedLines = System.IO.File.ReadLines("expectedRingTest.stl") |> Seq.toList
    let testLines = System.IO.File.ReadLines("ringTest.stl") |> Seq.toList

    let upper = expectedLines.Length - 1
    for i in 0 .. upper do
      assert (expectedLines[i] = testLines[i])
