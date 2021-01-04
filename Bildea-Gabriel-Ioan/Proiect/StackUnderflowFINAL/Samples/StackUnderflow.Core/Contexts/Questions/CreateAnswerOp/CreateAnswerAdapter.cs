using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Questions.CreateAnswerOp;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Schema.Questions.CreateAnswerOp.CreateAnswerResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateAnswerOp
{
    public class CreateAnswerAdapter : Adapter<CreateAnswerCmd, ICreateAnswerResult, QuestionsWriteContext, QuestionsDependencies>
    {

        public async override Task<ICreateAnswerResult> Work(CreateAnswerCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddAnswerToQuestion(state, CreateAnswerFromCmd(cmd))
                           select t;
            
            //state.Replies.Add(new DatabaseModel.Models.Reply { AuthorUserId=55, Body="bodyyy 10", QuestionId=50, ReplyId=15});
            
            var result =  await workflow.Match(
                Succ: r => r,
                Fail: ex => new AnswerNotCreated(ex.Message)
                );

            return result;
        }

        private ICreateAnswerResult AddAnswerToQuestion(QuestionsWriteContext state, Post answer)
        {
            if (state.Questions.Any(p => p.Title.Equals(answer.Title)))
                return new AnswerNotCreated();
            
            if ( ! (state.Questions.Any(p => p.PostId.Equals(answer.ParentPostId))) )
                return new AnswerNotCreated();

            if (state.Questions.All(p => p.PostId != answer.PostId))
                state.Questions.Add(answer);

            return new AnswerCreated(answer);
        }

        private Post CreateAnswerFromCmd(CreateAnswerCmd cmd)
        {
            var answer = new Post()
            {
                TenantId = cmd.TenantId, //from base.TenantUser
                PostedBy = cmd.PostedBy, //from base.TenantUser
                ParentPostId = cmd.ParentPostId, // <---- the answer is for this question 
                Title = cmd.Title,
                PostText = cmd.PostText, //body of the answer
                PostTypeId = (byte)StackUnderflow.EF.Models.PostTypeValue.Answer, //PostTypeId for answer = 2

            };
            return answer;
        }
        public override Task PostConditions(CreateAnswerCmd cmd, ICreateAnswerResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
