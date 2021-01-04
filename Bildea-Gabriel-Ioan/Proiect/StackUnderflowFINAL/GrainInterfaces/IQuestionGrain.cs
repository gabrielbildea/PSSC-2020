using System.Collections.Generic;
using System.Threading.Tasks;
using StackUnderflow.EF.Models;

namespace GrainInterfaces
{
    public interface IQuestionGrain : Orleans.IGrainWithIntegerKey
    {
        Task OnActivateAsync();
        Task<IEnumerable<string>> SendQuestionWithAnswers(StackUnderflowContext ctx);
    }
}