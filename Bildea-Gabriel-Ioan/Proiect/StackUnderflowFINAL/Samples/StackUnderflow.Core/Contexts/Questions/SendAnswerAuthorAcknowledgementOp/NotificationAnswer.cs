using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendAnswerAuthorAcknowledgementOp
{
    public class NotificationAnswer
    {
        public string Email { get; private set; }

        public string Letter { get; private set; }

        public NotificationAnswer(string email, string letter)
        {
            Email = email;
            Letter = letter;
        }
    }
}
