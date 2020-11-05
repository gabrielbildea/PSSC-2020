using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tema6.Inputs
{
    public class ReplyPublishedACKSentToReplyAuthorCmd
    {
        [Required]
        public int ReplyId { get; }

        [Required]
        public int QuestionId { get; }

        [Required]
        public string Reply { get; }
        public ReplyPublishedACKSentToReplyAuthorCmd(int ReplyId, int QuestionId, string Reply)
        {
            this.ReplyId = ReplyId;
            this.QuestionId = QuestionId;
            this.Reply = Reply;
        }
    }
}