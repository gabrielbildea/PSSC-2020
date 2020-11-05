using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;

namespace Tema6.Outputs
{
    [AsChoice]
    public static partial class ReplyReceivedACKSentToQuestionOwnerResult
    {
        public interface IReplyReceivedAckSentToQuestionOwnerResult { }
        public class ReplyReceived : IReplyReceivedAckSentToQuestionOwnerResult
        {
            public string Reply { get; }
            public ReplyReceived(string Reply)
            {
                this.Reply = Reply;
            }
        }
        public class InvalidReplyReceived : IReplyReceivedAckSentToQuestionOwnerResult
        {
            public string ErrorMessage { get; }
            public InvalidReplyReceived(string ErrorMessage)
            {
                this.ErrorMessage = ErrorMessage;
            }
        }
    }
}
