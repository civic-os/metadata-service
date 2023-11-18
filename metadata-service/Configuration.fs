namespace metadata_service

open System.IO
open FsConfig
open Microsoft.Extensions.Configuration

type ConnectionStrings = {
    Database: string
}
type Configuration = { 
    ConnectionStrings: ConnectionStrings
}

module Configuration =
    let init =
        let configurationRoot =
            ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.Development.json", true, true)
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build()

        let appConfig = AppConfig(configurationRoot)
        appConfig.Get<Configuration>()