using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageExt;
using Orleans;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.EFCore;
using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF;
using StackUnderflow.Domain.Core;
using StackUnderflow.Domain.Schema.Backoffice.CreateTenantOp;
using StackUnderflow.Domain.Schema.Backoffice.InviteTenantAdminOp;
using StackUnderflow.Domain.Schema.Backoffice;
using StackUnderflow.Domain.Core.Contexts.Questions;
using StackUnderflow.Domain.Schema.Questions.CreateAnswerOp;
using StackUnderflow.Domain.Schema.Questions.CheckLanguageOp;
using StackUnderflow.Domain.Schema.Questions.SendReplyAuthorAcknowledgementOp;
using StackUnderflow.Domain.Schema.Questions.CreateQuestionOp;
using StackUnderflow.Domain.Schema.Questions.SendQuestionOwnerAcknowledgementOp;
using System.Collections.Generic;
using StackUnderflow.EF.Models;

namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;

        public QuestionsController(IInterpreterAsync interpreter, StackUnderflowContext dbContext)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
        }

        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCmd cmd)
        {
            var dep = new QuestionsDependencies();

            var questions = await _dbContext.Question.ToListAsync();
            _dbContext.Question.AttachRange(questions);

            var ctx = new QuestionsWriteContext(new List<Question>());

            var expr = from CreateQuestionResult in QuestionsContext.CreateQuestion(cmd)
                           //let checkLanguageCmd = new CheckLanguageCmd()
                           //select CreateQuestionResult;
                       from checkLanguageResult in QuestionsContext.CheckLanguage(new CheckLanguageCmd(cmd.Body))
                       from sendAckToQuestionOwnerCmd in QuestionsContext.SendQuestionOwnerAcknowledgement(new SendQuestionOwnerAcknowledgementCmd(1, 1))
                       select CreateQuestionResult;

            var r = await _interpreter.Interpret(expr, ctx, dep);


            _dbContext.Question.Add(new DatabaseModel.Models.Question{ QuestionId = Guid.NewGuid(), Title = cmd.Title, Description = cmd.Body, Tags = cmd.Tags });
            //var reply = await _dbContext.QuestionModel.Where(r => r.Title == "intrebTest").SingleOrDefaultAsync();
            //_dbContext.Question.Update(reply);
            await _dbContext.SaveChangesAsync();

            return r.Match(
                succ => (IActionResult)Ok("Question successfully added"),
                fail => BadRequest("Question could not be added")
                );
        }

        [HttpPost("createReply")]
        public async Task<IActionResult> CreateReply([FromBody] CreateReplyCmd cmd)
        {
            var dep = new QuestionsDependencies();
            
            var replies = await _dbContext.Replies.ToListAsync();
            _dbContext.Replies.AttachRange(replies);

            var ctx = new QuestionsWriteContext(new EFList<Reply>(_dbContext.Replies));

            var expr = from createTenantResult in QuestionsContext.CreateReply(cmd)
                       //let checkLanguageCmd = new CheckLanguageCmd()
                       from checkLanguageResult in QuestionsContext.CheckLanguage(new CheckLanguageCmd(cmd.Body))
                       from sendAckAuthor in QuestionsContext.SendReplyAuthorAcknowledgement(new SendReplyAuthorAcknowledgementCmd(Guid.NewGuid(), 1, 2))
                       select createTenantResult;

            var r = await _interpreter.Interpret(expr, ctx, dep);

            _dbContext.Replies.Add(new DatabaseModel.Models.Reply { Body = cmd.Body, AuthorUserId = 1, QuestionId = cmd.QuestionId, ReplyId = 4 });
            // var reply = await _dbContext.Replies.Where(r => r.ReplyId == 4).SingleOrDefaultAsync();
            //reply.Body = "Text updated";
            //_dbContext.Replies.Update(reply);
            await _dbContext.SaveChangesAsync();


            return r.Match(
                succ => (IActionResult)Ok(succ.Body),
                fail => BadRequest("Reply could not be added")
                );
        }
    }
}
