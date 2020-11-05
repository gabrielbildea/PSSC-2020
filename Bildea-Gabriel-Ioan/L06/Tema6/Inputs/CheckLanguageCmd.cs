using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tema6.Inputs
{
    public class CheckLanguageCmd
    {
        [Required]
        public string Answer { get; }

        public CheckLanguageCmd(string answer)
        {
            Answer = answer;
        }
    }
}
