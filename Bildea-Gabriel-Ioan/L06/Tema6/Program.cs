using System;
using Tema6.Outputs;

namespace Tema6
{
    class Program
    {
        public static void Main(string[] args)
        {

            var wf = from createReplyResult in Domain.ValidateReply(99, 103, "answer")  //authorID = 99, //questionID=103
                let validReply = (ValidateReplyResult.ReplyValidated)createReplyResult
                from checkLanguageResult in Domain.CheckLanguage(validReply.Reply.Answer)
                from ownerAck in Domain.QuestionOwnerAcknowledgement(99, 103, "answer")
                     from authorAck in Domain.ReplyAuthorAcknowledgement(1, 103, "answer")
                     select (validReply, checkLanguageResult, ownerAck, authorAck);

            Console.WriteLine("Hello world!");
        }
    }
}
