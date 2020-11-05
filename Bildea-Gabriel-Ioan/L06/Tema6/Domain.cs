using System;
using System.Collections.Generic;
using System.Text;
using Access.Primitives.IO;
using Tema6.Inputs;
using Tema6.Outputs;
using static PortExt;

namespace Tema6
{
    public static class Domain
    {
        public static Port<ValidateReplyResult.IValidateReplyResult> ValidateReply(int authorId, int questionId, string answer)
            => NewPort<ValidateReplyCmd, ValidateReplyResult.IValidateReplyResult>(new ValidateReplyCmd(authorId, questionId, answer));

        public static Port<CheckLanguageResult.ICheckLanguageResult> CheckLanguage(string text)
            => NewPort<CheckLanguageCmd, CheckLanguageResult.ICheckLanguageResult>(new CheckLanguageCmd(text));

        public static Port<ReplyReceivedACKSentToQuestionOwnerResult.IReplyReceivedAckSentToQuestionOwnerResult> QuestionOwnerAcknowledgement(int authorid, int questionid, string reply)
            => NewPort<ReplyReceivedACKSentToQuestionOwnerCmd, ReplyReceivedACKSentToQuestionOwnerResult.IReplyReceivedAckSentToQuestionOwnerResult>(new ReplyReceivedACKSentToQuestionOwnerCmd(authorid, questionid, reply));

        public static Port<ReplyPublishedACKSentToReplyAuthorResult.IReplyPublishedAckSentToReplyAuthorResult> ReplyAuthorAcknowledgement(int replyid, int questionid, string reply)
            => NewPort<ReplyPublishedACKSentToReplyAuthorCmd, ReplyPublishedACKSentToReplyAuthorResult.IReplyPublishedAckSentToReplyAuthorResult>(new ReplyPublishedACKSentToReplyAuthorCmd(replyid, questionid, reply));
    }
}
