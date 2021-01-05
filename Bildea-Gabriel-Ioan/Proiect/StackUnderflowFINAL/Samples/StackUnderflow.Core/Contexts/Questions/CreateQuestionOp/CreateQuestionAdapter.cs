using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Questions.CreateQuestionOp;
using StackUnderflow.EF.Models;
using static StackUnderflow.Domain.Schema.Questions.CreateQuestionOp.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public class CreateQuestionAdapter : Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionsWriteContext, QuestionsDependencies>
    {
        public async override Task<ICreateQuestionResult> Work(CreateQuestionCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddQuestionIfNotExist(state, CreateQuestionFromCommand(cmd))
                           select t;
            
            var result =  await workflow.Match(
                Succ: r => r,
                Fail: ex => new QuestionNotCreated(ex.ToString()));

            return result;
        }

        private ICreateQuestionResult AddQuestionIfNotExist(QuestionsWriteContext state, Post question)
        {
            if (state.Questions.Any(p => p.Title.Equals(question.Title)))
                return new QuestionNotCreated();

            if (state.Questions.All(p => p.PostId != question.PostId))
                state.Questions.Add(question);

            return new QuestionCreated(question);
        }

        private Post CreateQuestionFromCommand(CreateQuestionCmd cmd)
        {
            //var reply =  cmd.QuestionModel.Where(r => r.Title == "intrebTest").SingleOrDefaultAsync();
            var question = new Post()
            {
                TenantId = cmd.TenantId, //from base.TenantUser
                PostedBy = cmd.PostedBy, //from base.TenantUser
                Title = cmd.Title,
                PostText = cmd.PostText, //body of the question
                PostTypeId = (byte)StackUnderflow.EF.Models.PostTypeValue.Question, //PostTypeId for question = 1

            };
            
            question.PostTag.Add(new PostTag()
            {
                TenantId = cmd.TenantId,
                PostId = question.PostId,
                T  = new Tag()
                {
                    TenantId = cmd.TenantId,
                    Name = cmd.Tags,
                    Description = "NoDescription",
                },

            }) ;

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            question.PostView.Add(new PostView()
            {
                TenantId = cmd.TenantId,
                PostId = question.PostId,
                UserId = cmd.PostedBy,
                Viewed = DateTime.Parse(sqlFormattedDate),
        });
            return question;
        }

        public override Task PostConditions(CreateQuestionCmd cmd, ICreateQuestionResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
