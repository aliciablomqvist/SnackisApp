using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SnackisApp.Models
{
  public class Message
  {
    public int Id { get; set; }

    [Required]
    public string SenderId { get; set; }

    [ValidateNever]
    public SnackisUser Sender { get; set; }

    [Required]
    public string RecipientId { get; set; }

    [ValidateNever]
    public SnackisUser Recipient { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Message cannot be longer than 500 characters.")]
    public string Content { get; set; }

    [Required]
    public DateTime DateSent { get; set; }
  }
}