# Build self contained executable (no dependencies on .NET 6)
dotnet publish ./src/SSW.FireEmoji.Console/SSW.FireEmoji.Console.csproj -p:PublishSingleFile=true -r win-x64 -c Release --self-contained true -p:PublishTrimmed=true --output ./releases/latest/console

# Remove everything except the executable
Remove-Item -recurse releases/latest/console/* -exclude SSW.FireEmoji.Console.exe
