using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.SendAnswerAuthorAcknowledgementOp
{
    [AsChoice]
    public static partial class SendAnswerAuthorAcknowledgementResult
    {

        public interface ISendAnswerAuthorAcknowledgementResult { }

        public class AcknowledgementSent : ISendAnswerAuthorAcknowledgementResult
        {
            public AcknowledgementSent(int questionId, int questionOwnerId)
            {
                QuestionId = questionId;
                QuestionOwnerId = questionOwnerId;
            }

            public int QuestionId { get; }
            public int QuestionOwnerId { get; }
        }

        public class AcknowledgementNotSent : ISendAnswerAuthorAcknowledgementResult
        {
            public AcknowledgementNotSent(string message)
            {
                Message = message;
            }

            public string Message { get; }
        }
    }
}
