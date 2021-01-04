using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendAnswerAuthorAcknowledgementOp
{
    public class NotificationAcknowledgement
    {
        public string Receipt { get; private set; }

        public NotificationAcknowledgement(string receipt)
        {
            Receipt = receipt;

        }
    }
}