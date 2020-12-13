using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Questions.CreateQuestionOp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Schema.Questions.CreateQuestionOp.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public class CreateQuestionAdaptor : Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionsWriteContext, QuestionsDependencies>
    {
        public override Task PostConditions(CreateQuestionCmd cmd, ICreateQuestionResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ICreateQuestionResult> Work(CreateQuestionCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddQuestion(state, CreateQuestionFromCmd(cmd))
                           select t;
            
            var result =  await workflow.Match(
                Succ: r => r,
                Fail: ex => new QuestionNotCreated(ex.Message)
                );

            return result;
        }

        private ICreateQuestionResult AddQuestion(QuestionsWriteContext state, object v)
        {
            return new QuestionCreated(Guid.NewGuid(), "TitluIntrebare", "BodyIntrebare", new string[] { "Tag1", "Tag2", "Tag3" });
        }

        private object CreateQuestionFromCmd(CreateQuestionCmd cmd)
        {
            return new { };
        }
    }
}
