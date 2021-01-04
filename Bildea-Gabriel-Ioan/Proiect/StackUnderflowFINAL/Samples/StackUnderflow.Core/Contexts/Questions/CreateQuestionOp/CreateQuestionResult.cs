using Access.Primitives.Extensions.Cloning;
using CSharp.Choices;
using LanguageExt;
using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CreateQuestionOp
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult : IDynClonable { }

        public class QuestionCreated : ICreateQuestionResult
        {
               
            public Post question { get; }
            //public int PostId { get; set; }
            //public string Title { get; set; }
            //public string PostText { get; set; }
            //public string Tags { get; set; }

            public QuestionCreated(Post Question)//int PostId, string Title, string PostText, string Tags)
            {
                this.question = Question;
                //this.PostId = PostId;
                //this.Title = Title;
                //this.PostText = PostText;
                //this.Tags = Tags;
            }

            public object Clone() => this.ShallowClone();
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get; set; }
            public QuestionNotCreated() { }
            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }

            ///TODO
            public object Clone() => this.ShallowClone();
        }

        public class InvalidRequest : ICreateQuestionResult
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
