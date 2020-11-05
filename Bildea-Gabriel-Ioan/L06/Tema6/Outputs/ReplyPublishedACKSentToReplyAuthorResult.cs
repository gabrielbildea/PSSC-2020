using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6.Outputs
{
    public static partial class ReplyPublishedACKSentToReplyAuthorResult
    {
        public interface IReplyPublishedAckSentToReplyAuthorResult { };
        public class ReplyPublished : IReplyPublishedAckSentToReplyAuthorResult
        {
            public string Text { get; }
            public ReplyPublished(string Text)
            {
                this.Text = Text;
            }
        }
        public class InvalidReplyPublished : IReplyPublishedAckSentToReplyAuthorResult
        {
            public string ErrorMessage { get; }
            public InvalidReplyPublished(string ErrorMessage)
            {
                this.ErrorMessage = ErrorMessage;
            }
        }
    }
}
