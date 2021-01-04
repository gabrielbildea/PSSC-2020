﻿using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public class QuestionsWriteContext
    {
        public ICollection<Post> Questions { get; }
        
        //public ICollection<Post> Replies { get; }
        public QuestionsWriteContext(ICollection<Post> questions)//, ICollection<Post> replies)
        {
            Questions = questions ?? new List<Post>(0);
            //Replies = replies ?? new List<Post>(0);
        }
        /*public QuestionsWriteContext(ICollection<Reply> replies)
        {
            Replies = replies ?? new List<Reply>(0);
        }*/

       
    }
}
