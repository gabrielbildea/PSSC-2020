using System;
using System.Collections.Generic;
using System.Text;
using Access.Primitives.Extensions.Cloning;
using CSharp.Choices;
using LanguageExt;
using StackUnderflow.EF.Models;


namespace StackUnderflow.Domain.Schema.Questions.CreateAnswerOp
{
    [AsChoice]
    public static partial class CreateAnswerResult
    {
        public interface ICreateAnswerResult : IDynClonable { }

        public class AnswerCreated : ICreateAnswerResult
        {
            public AnswerCreated(Post answer)//int replyId, int questionId, int authorUserId, string body)
            {
                this.answer = answer;
                //ReplyId = replyId;
                //QuestionId = questionId;
                //AuthorUserId = authorUserId;
                //Body = body;
            }
            public Post answer { get; }
            //public int ReplyId { get; }
            //public int QuestionId { get; }
            //public int AuthorUserId { get; }
            //public string Body { get; }
            public object Clone() => this.ShallowClone();
        }

        public class AnswerNotCreated : ICreateAnswerResult
        {
            public AnswerNotCreated() { }
            public AnswerNotCreated(string message)
            {
                Message = message;
            }

            public string Message { get; }

            ///TODO
            public object Clone() => this.ShallowClone();
        }

        public class InvalidRequest : ICreateAnswerResult
        {
            public string Message { get; }

            public InvalidRequest(string message)
            {
                Message = message;
            }

            ///TODO
            public object Clone() => this.ShallowClone();
        }
    }

    
}
