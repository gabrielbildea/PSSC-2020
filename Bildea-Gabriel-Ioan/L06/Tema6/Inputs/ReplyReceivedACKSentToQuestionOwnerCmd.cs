using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tema6.Inputs
{
    public class ReplyReceivedACKSentToQuestionOwnerCmd
    {
        [Required]
        public int AuthorId { get; }

        [Required]
        public int QuestionId { get; }

        [Required]
        public string Reply { get; }

        public ReplyReceivedACKSentToQuestionOwnerCmd(int AuthorId, int QuestionId, string Reply)
        {
            this.AuthorId = AuthorId;
            this.QuestionId = QuestionId;
            this.Reply = Reply;
        }
    }
}