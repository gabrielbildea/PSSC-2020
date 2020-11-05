using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;
using Tema6.Inputs;
using Tema6.Models;

namespace Tema6.Outputs
{
    [AsChoice]
    public static partial class ValidateReplyResult
    {
        public interface IValidateReplyResult { }

        public class ReplyValidated : IValidateReplyResult
        {
            public Reply Reply { get; }

            public ReplyValidated(Reply reply)
            {
                Reply = reply;
            }
        }

        public class InvalidReply : IValidateReplyResult
        {
            public string Reasons { get; }

            public InvalidReply(string reasons)
            {
                Reasons = reasons;
            }
        }

        public class InvalidRequest : IValidateReplyResult
        {
            public string ValidationErrors { get; }
            public ValidateReplyCmd Cmd { get; }

            public InvalidRequest(string validationErrors, ValidateReplyCmd cmd)
            {
                ValidationErrors = validationErrors;
                Cmd = cmd;
            }
        }

    }
}
