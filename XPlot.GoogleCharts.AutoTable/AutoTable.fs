module XPlot.GoogleCharts.AutoTable

open Newtonsoft.Json.Linq
open XPlot.GoogleCharts

let toToken o = JObject.FromObject o

let toSeries (objects: 'T list) properties =
    let series =
        properties
        |> Seq.map(fun prop ->
            objects |> Seq.map toToken |> Seq.mapi(fun ind i -> ind,i.[prop].ToString()) |> Seq.toList)
        |> Seq.toList
    series    

let toTable (objects: 'T list) =
    let head = objects.Head
    let properties = (toToken head).Properties() |> Seq.map(fun i -> i.Name) |> Seq.toList
    let series = toSeries objects properties
    series |> Chart.Table  |> Chart.WithLabels properties   
