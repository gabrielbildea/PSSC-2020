using Access.Primitives.IO;
using StackUnderflow.Domain.Schema.Questions.CreateAnswerOp;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using StackUnderflow.Domain.Schema.Questions.CheckLanguageOp;
using StackUnderflow.Domain.Schema.Questions.SendQuestionOwnerAcknowledgementOp;
using StackUnderflow.Domain.Schema.Questions.SendAnswerAuthorAcknowledgementOp;
using StackUnderflow.Domain.Schema.Questions.CreateQuestionOp;
using static StackUnderflow.Domain.Schema.Questions.CreateAnswerOp.CreateAnswerResult;
using static StackUnderflow.Domain.Schema.Questions.CheckLanguageOp.CheckLanguageResult;
using static StackUnderflow.Domain.Schema.Questions.SendQuestionOwnerAcknowledgementOp.SendQuestionOwnerAcknowledgementResult;
using static StackUnderflow.Domain.Schema.Questions.CreateQuestionOp.CreateQuestionResult;
using static StackUnderflow.Domain.Schema.Questions.SendAnswerAuthorAcknowledgementOp.SendAnswerAuthorAcknowledgementResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public static class QuestionsDomain
    {
        public static Port<ICreateQuestionResult> CreateQuestion(CreateQuestionCmd createQuestionCmd) =>
            NewPort<CreateQuestionCmd, ICreateQuestionResult>(createQuestionCmd);
        public static Port<ICreateAnswerResult> CreateAnswer(CreateAnswerCmd createAnswerCmd) =>
           NewPort<CreateAnswerCmd, ICreateAnswerResult>(createAnswerCmd);

        public static Port<ICheckLanguageResult> CheckLanguage(CheckLanguageCmd checkLanguageCmd) =>
            NewPort<CheckLanguageCmd, ICheckLanguageResult>(checkLanguageCmd);

        public static Port<ISendQuestionOwnerAcknowledgementResult> SendQuestionOwnerAcknowledgement(SendQuestionOwnerAcknowledgementCmd cmd) =>
            NewPort<SendQuestionOwnerAcknowledgementCmd, ISendQuestionOwnerAcknowledgementResult>(cmd);

        public static Port<ISendAnswerAuthorAcknowledgementResult> SendAnswerAuthorAcknowledgement(SendAnswerAuthorAcknowledgementCmd cmd) =>
           NewPort<SendAnswerAuthorAcknowledgementCmd, ISendAnswerAuthorAcknowledgementResult>(cmd);
    }
}
