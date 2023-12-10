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

let evalTest =
    let inputDetails = {color = Gold "gold"; design = Band "band"; size = 7; file = "ringTest.stl"}
    evalDetails inputDetails

    let expectedLines = System.IO.File.ReadLines("expectedRingTest.stl") |> Seq.toList
    let testLines = System.IO.File.ReadLines("ringTest.stl") |> Seq.toList

    let upper = expectedLines.Length - 1
    for i in 0 .. upper do
      assert (expectedLines[i] = testLines[i])
