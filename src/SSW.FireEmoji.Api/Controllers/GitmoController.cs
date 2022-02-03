using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSW.FireEmoji.Core.MachineLearning;
using SSW.FireEmoji.Core.Models;

namespace SSW.FireEmoji.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GitmoController : ControllerBase
    {
        GitmoPredictor gitmoPredictor = new GitmoPredictor();
        private static string ModelStreamUri => "gitmo.mlnet";

        public GitmoController()
        {
            LoadModel();
        }

        private void LoadModel()
        {
            gitmoPredictor.LoadFromFilePath(ModelStreamUri);
        }

        [HttpGet()]
        public GitmojiPrediction Get(string commit)
        {
            return gitmoPredictor.Predict(new GitComment() { Comment = commit });
        }
    }
}
