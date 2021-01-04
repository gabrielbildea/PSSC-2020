using EarlyPay.Primitives.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CreateAnswerOp
{
    public class CreateAnswerCmd
    {
        public CreateAnswerCmd(int TenantId, Guid PostedBy, int ParentPostId, string Title, string PostText)
        {
            this.TenantId = TenantId;
            this.PostedBy = PostedBy;
            this.ParentPostId = ParentPostId;
            this.Title = Title;
            this.PostText = PostText;
        }

        [Required]
        public int TenantId { get; set; }

        [Required]
        [GuidNotEmpty]
        public Guid PostedBy { get; set; }

        [Required]
        public int ParentPostId { get; set; }

        [Required(ErrorMessage = "Title is missing.")]
        [MinLength(15), MaxLength(70)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string PostText { get; set; }
    }
}
