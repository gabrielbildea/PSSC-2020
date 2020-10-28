using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CSharp.Choices;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }

        public class QuestionCreated : ICreateQuestionResult
        {
            public Guid QuestionId { get; private set; }
            public string QuestionTitle { get; private set; }
            public string AskerEmail { get; private set; }
            public bool IsMLApproved { get; set; }

            public QuestionCreated(Guid questionId, string questionTitle, string askerEmail, bool ismlApproved)
            {
                QuestionId = questionId;
                QuestionTitle = questionTitle;
                AskerEmail = askerEmail;
                IsMLApproved = ismlApproved;
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

        public class QuestionValidationFailed : ICreateQuestionResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }

            public QuestionValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
        }

    }
}
