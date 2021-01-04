using EarlyPay.Primitives.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CreateQuestionOp
{
    public class CreateQuestionCmd
    {
        //public CreateQuestionCmd() { }
        public CreateQuestionCmd(int TenantId, Guid PostedBy, string Title, string PostText, string Tags)
        {
            this.TenantId = TenantId;
            this.PostedBy = PostedBy;
            this.Title = Title;
            this.PostText = PostText; //body
            this.Tags = Tags;
        }

        [Required]
        public int TenantId { get; set; }

        [Required]
        [GuidNotEmpty]
        public Guid PostedBy { get; set; }

        [Required(ErrorMessage = "Title is missing.")]
        [MinLength(15), MaxLength(150)]
        public string Title { get; set; }


        [Required(ErrorMessage = "PostText (question Body) is missing.")]
        [MinLength(30), MaxLength(30000)]  //Body is limited to 30000 characters;
        public string PostText { get; set; }


        [Required(ErrorMessage = "Please enter at least one tag; see a list of popular tags.")]
        [MinLength(1), MaxLength(10)]
        public string Tags { get; set; }


    }
}
