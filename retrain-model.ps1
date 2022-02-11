dotnet run --project ./src/SSW.FireEmoji.Trainer/SSW.FireEmoji.Trainer.csproj -- train -d Data/CommitMessages.csv -o Model/gitmo.mlnet -t 90
dotnet test ./src/SSW.FireEmoji.MachineLearning.Tests/SSW.FireEmoji.MachineLearning.Tests.csproj
