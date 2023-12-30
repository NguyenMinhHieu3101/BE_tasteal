using OpenAI_API;
using OpenAI_API.Completions;

namespace BE_tasteal.Persistence.Repository.KeyWordRepo
{
    public class KeyWordRepo
    {
        public async Task<string> useGpt(string query)
        {
            var openAi = new OpenAIAPI("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");

            CompletionRequest request = new CompletionRequest();

            request.Prompt = query;
            request.Model = OpenAI_API.Models.Model.DavinciText;

            var completions = await openAi.Completions.CreateCompletionAsync(request);

            string result = "";
            foreach (var completion in completions.Completions)
            {
                result += completion.Text;
            }

            return result;
        }

    }
}
