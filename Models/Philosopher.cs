using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SnackisApp.Models
{
    public class Philosopher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quote { get; set; }
    }
}