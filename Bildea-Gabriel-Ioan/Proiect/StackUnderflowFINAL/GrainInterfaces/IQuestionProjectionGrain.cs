using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using StackUnderflow.EF.Models;

namespace GrainInterfaces
{
    public interface IQuestionProjectionGrain : IGrainWithStringKey
    {
        Task<IEnumerable<Post>> GetQuestionsAsync();
    }
}