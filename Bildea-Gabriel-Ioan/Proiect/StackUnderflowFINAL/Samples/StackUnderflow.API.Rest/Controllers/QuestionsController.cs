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
using System.Linq;
using Microsoft.AspNetCore.Http;
using GrainInterfaces;
using Orleans.Streams;
using System.Text.Json;
using StackUnderflow.Domain.Core.Contexts.Questions.SendAnswerAuthorAcknowledgementOp;

namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        private readonly IClusterClient _client;

        public QuestionsController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
            _client = client;
        }

        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCmd cmd)
        {
            QuestionsWriteContext ctx = new QuestionsWriteContext(
                new EFList<Post>(_dbContext.Post));

            var dep = new QuestionsDependencies();

            var questions = await _dbContext.Post.ToListAsync();
            //var questions = await _dbContext.Post.Where(q => q.PostTypeId == 1).ToListAsync();
            _dbContext.Post.AttachRange(questions);

            var expr = from createQuestionResult in QuestionsDomain.CreateQuestion(cmd)
                           //let checkLanguageCmd = new CheckLanguageCmd()
                           //select CreateQuestionResult;
                       from checkLanguageResult in QuestionsDomain.CheckLanguage(new CheckLanguageCmd(cmd.PostText))
                           //from sendAckToQuestionOwnerCmd in QuestionsDomain.SendQuestionOwnerAcknowledgement(new SendQuestionOwnerAcknowledgementCmd())
                       select new { createQuestionResult, checkLanguageResult };

            var r = await _interpreter.Interpret(expr, ctx, dep);

            //var reply = await _dbContext.QuestionModel.Where(r => r.Title == "intrebTest").SingleOrDefaultAsync();
            //_dbContext.Question.Update(reply);
            //await _dbContext.SaveChangesAsync();

            _dbContext.SaveChanges();

            return r.createQuestionResult.Match(
                created => (IActionResult)Ok("PostID of question created:" + created.question.PostId),
                notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Question could not be created."),//todo return 500 (),
                invalidRequest => BadRequest("Invalid request."));

        }

        [HttpPost("createAnswer")]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerCmd cmd)
        {
            QuestionsWriteContext ctx = new QuestionsWriteContext(
                new EFList<Post>(_dbContext.Post));

            var dep = new QuestionsDependencies();
            //dep.GenerateConfirmationToken = () => Guid.NewGuid().ToString();
            //dep.SendConfirmationEmail = SendConfirmationEmail
            var answers = await _dbContext.Post.ToListAsync();
            _dbContext.Post.AttachRange(answers);


            var expr = from createAnswerResult in QuestionsDomain.CreateAnswer(cmd)
                           //let checkLanguageCmd = new CheckLanguageCmd()
                       from checkLanguageResult in QuestionsDomain.CheckLanguage(new CheckLanguageCmd(cmd.PostText))
                           //from sendAckAuthor in QuestionsDomain.SendAnswerAuthorAcknowledgement(new SendAnswerAuthorAcknowledgementCmd(Guid.NewGuid(), 1, 2))
                       select new { createAnswerResult, checkLanguageResult };

            var r = await _interpreter.Interpret(expr, ctx, dep);

            //_dbContext.Replies.Add(new DatabaseModel.Models.Reply { Body = cmd.PostText, AuthorUserId = 1, QuestionId = cmd.QuestionId, ReplyId = 4 });
            // var reply = await _dbContext.Replies.Where(r => r.ReplyId == 4).SingleOrDefaultAsync();
            //reply.Body = "Text updated";
            //_dbContext.Replies.Update(reply);
            //await _dbContext.SaveChangesAsync();

            _dbContext.SaveChanges();

            return r.createAnswerResult.Match(
                created => (IActionResult)Ok("PostID of answer created: " + created.answer.PostId),
                notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Answer could not be created."),//todo return 500 (),
                invalidRequest => BadRequest("Invalid request."));

        }
        private TryAsync<NotificationAcknowledgement> SendNotifyEmail(NotificationAnswer letter)
         => async () =>
         {
              var emailSender = _client.GetGrain<IEmailSender>(0);
              await emailSender.SendEmailAsync(letter.Letter);
              return new NotificationAcknowledgement(Guid.NewGuid().ToString());
         };


        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {

            _client.Connect();
            var questionSender = _client.GetGrain<IQuestionGrain>(questionId);
            questionSender.OnActivateAsync();
            //var stream = question.GetQuestionWithAnswers(Guid.NewGuid().ToString());
            //await questionSender.OnActivateAsync(_dbContext);
            //var streamProvider = _client.GetStreamProvider("SMSProvider");
            //var stream = streamProvider.GetStream<Post>(questionId, "QuestionAndAnswers");
            //stream.
            //var subscription = await stream.SubscribeAsync((IAsyncObserver<Post>)this);
            var response = await questionSender.SendQuestionWithAnswers(_dbContext);
            //var r = response.ToString();
            //return r.Match(
            //    created => (IActionResult)Ok("ok"),
            //    notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Answer could not be created."),//todo return 500 (),
            //    invalidRequest => BadRequest("Invalid request."));

            //string jsonString = JsonSerializer.Serialize(response.ToString());
            return (IActionResult)Ok(response);
        }
    }
}
