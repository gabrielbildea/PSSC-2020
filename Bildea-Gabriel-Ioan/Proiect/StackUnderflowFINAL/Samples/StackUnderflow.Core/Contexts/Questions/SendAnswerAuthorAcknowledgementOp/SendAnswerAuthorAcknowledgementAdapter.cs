using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Access.Primitives.IO;
using GrainInterfaces;
using Orleans;
using StackUnderflow.Domain.Schema.Questions.SendAnswerAuthorAcknowledgementOp;
using static StackUnderflow.Domain.Schema.Questions.SendAnswerAuthorAcknowledgementOp.SendAnswerAuthorAcknowledgementResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendAnswerAuthorAcknowledgementOp
{
    class SendAnswerAuthorAcknowledgementAdapter : Adapter<SendAnswerAuthorAcknowledgementCmd, ISendAnswerAuthorAcknowledgementResult, QuestionsWriteContext, QuestionsDependencies>
    {
        private readonly IClusterClient clusterClient;

        public SendAnswerAuthorAcknowledgementAdapter(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }
        public override Task PostConditions(SendAnswerAuthorAcknowledgementCmd cmd, ISendAnswerAuthorAcknowledgementResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ISendAnswerAuthorAcknowledgementResult> Work(SendAnswerAuthorAcknowledgementCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var helloGrain = this.clusterClient.GetGrain<IHello>(0);

            var helloResult = await helloGrain.SayHello("My hello greeting");

            return new AcknowledgementSent(1,2 );
        }
    }
}
