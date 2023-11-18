namespace metadata_service.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open metadata_service
open Microsoft.AspNetCore.Http
open Npgsql.FSharp


type Table = {
    Name: string
}

[<ApiController>]
[<Route("[controller]")>]
type TableController (logger : ILogger<TableController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get (config: Configuration) : Table list =
        config.ConnectionStrings.Database
        |> Sql.connect
        |> Sql.query "SELECT * FROM information_schema.tables WHERE table_schema = 'public'"
        |> Sql.execute (fun read ->
            {
                Name = read.text "table_name"
            })
