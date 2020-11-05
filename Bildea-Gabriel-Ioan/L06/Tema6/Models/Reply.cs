using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6.Models
{

    public class Reply
    {
        public int ReplyId { get; }
        public int QuestionId { get; }
        public int AuthorId { get; }
        public string Answer { get; }

        public Reply(int replyId, int questionId, int authorId, string answer)
        {
            ReplyId = replyId;
            QuestionId = questionId;
            AuthorId = authorId;
            Answer = answer;
        }
    }
}
