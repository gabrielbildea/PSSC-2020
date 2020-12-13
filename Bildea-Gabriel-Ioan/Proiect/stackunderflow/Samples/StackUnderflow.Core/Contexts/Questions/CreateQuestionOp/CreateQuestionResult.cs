using CSharp.Choices;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CreateQuestionOp
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }

        public class QuestionCreated : ICreateQuestionResult
        {
 
            public Guid QuestionId { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public string[] Tags { get; set; }

            public QuestionCreated(Guid QuestionId, string Title, string Body, string[] Tags)
            {
                this.QuestionId = QuestionId;
                this.Title = Title;
                this.Body = Body;
                this.Tags = Tags;
            }
        }
        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get; set; }

            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }
        }
    }
   
}
