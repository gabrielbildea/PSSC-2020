﻿using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;

namespace Tema6.Outputs
{
    [AsChoice]
    public static partial class CheckLanguageResult
    {
        public interface ICheckLanguageResult { }

        public class TextChecked : ICheckLanguageResult
        {
            public int Certainty { get; }

            public TextChecked(int certainty)
            {
                Certainty = certainty;
            }
        }

        public class CheckFailed : ICheckLanguageResult
        {
            public string ErrorMessage { get; }

            public CheckFailed(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
        }
    }
}
