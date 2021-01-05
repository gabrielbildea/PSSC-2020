using Access.Primitives.IO;
using GrainInterfaces;
using Orleans;
using StackUnderflow.Domain.Schema.Questions.SendAnswerAuthorAcknowledgementOp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Schema.Questions.SendQuestionOwnerAcknowledgementOp.SendQuestionOwnerAcknowledgementResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendReplyAuthorAcknowledgementOp
{
    class SendQuestionOwnerAcknowledgementAdapter: Adapter<SendAnswerAuthorAcknowledgementCmd, ISendQuestionOwnerAcknowledgementResult, QuestionsWriteContext, QuestionsDependencies>
    {
        private readonly IClusterClient clusterClient;

        public SendQuestionOwnerAcknowledgementAdapter(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }
        public override Task PostConditions(SendAnswerAuthorAcknowledgementCmd cmd, ISendQuestionOwnerAcknowledgementResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ISendQuestionOwnerAcknowledgementResult> Work(SendAnswerAuthorAcknowledgementCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var helloGrain = this.clusterClient.GetGrain<IHello>(0);

            var helloResult = await helloGrain.SayHello("My hello greeting");

            return new AcknowledgementSent(1, 2);
        }
    }
}
