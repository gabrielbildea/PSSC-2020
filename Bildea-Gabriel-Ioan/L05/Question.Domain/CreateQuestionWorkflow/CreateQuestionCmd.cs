﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    public struct CreateQuestionCmd
    {
        [Required(ErrorMessage = "Title is missing.")]
        [MinLength(15), MaxLength(150)]
        public string Title { get; private set; }


        [Required(ErrorMessage = "Body is missing.")]
        [MinLength(30), MaxLength(30000)]  //Body is limited to 30000 characters;
        public string Body { get; private set; }


        [Required(ErrorMessage = "Please enter at least one tag; see a list of popular tags.")]
        [MinLength(1), MaxLength(10)]
        public string[] Tags { get; set; }

        public CreateQuestionCmd(string Title, string Body, string[] Tags)
        {
            this.Title = Title;
            this.Body = Body;
            this.Tags = Tags;
        }
    }
}
