using System.ComponentModel.DataAnnotations;

namespace AycProjectBudgeting.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}