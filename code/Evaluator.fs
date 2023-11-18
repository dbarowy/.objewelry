module Evaluator
open Parser
open System

let evalDetails (details: Details) =
    System.IO.File.AppendAllText(details.file, details.ToString())