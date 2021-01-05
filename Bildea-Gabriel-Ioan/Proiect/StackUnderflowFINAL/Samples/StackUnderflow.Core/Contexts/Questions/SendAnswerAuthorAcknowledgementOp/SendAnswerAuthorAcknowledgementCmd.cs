using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.SendAnswerAuthorAcknowledgementOp
{
    public class SendAnswerAuthorAcknowledgementCmd
    {
        public SendAnswerAuthorAcknowledgementCmd(Guid replyAuthorId, int questionId, int answerId)
        {
            ReplyAuthorId = replyAuthorId;
            QuestionId = questionId;
            AnswerId = answerId;
        }

        public Guid ReplyAuthorId { get; }
        public int QuestionId { get; }
        public int AnswerId { get; }
    }
}
