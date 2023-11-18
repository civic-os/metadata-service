namespace metadata_service.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open metadata_service
open Npgsql.FSharp

type Column = {
    Name: string
}

[<ApiController>]
[<Route("[controller]")>]
type ColumnsController (logger : ILogger<ColumnsController>) =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("{table_name}")>]
    member _.Get (table_name: string, config: Configuration) : Column list =
        config.ConnectionStrings.Database
        |> Sql.connect
        |> Sql.query "SELECT * FROM information_schema.columns WHERE table_schema = 'public' AND table_name = @table_name"
        |> Sql.parameters [ "table_name", Sql.text table_name ]
        |> Sql.execute (fun read ->
            {
                Name = read.text "column_name"
            })
