using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using OpenAI_API.Completions;

namespace BE_tasteal.Persistence.Repository.KeyWordRepo
{
    public class KeyWordRepo:  GenericRepository<KeyWordEntity>, IKeyWordRepo
    {
        public KeyWordRepo(MyDbContext context,
        ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
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
        public List<String> GetKeyWord()
        {
            Random random = new Random();

            int totalCount = _context.KeyWords.Count();
       
            int offset = random.Next(0, totalCount - 5);
            List<KeyWordEntity> randomEntities = _context.KeyWords
                .OrderBy(x => Guid.NewGuid())
                .Skip(offset)
                .Take(5)
                .ToList();

            List<string> key = new List<string>();
            foreach( var entity in randomEntities)
            {
                key.Add(entity.keyword);
            }

            return key;
        }
    }
}
