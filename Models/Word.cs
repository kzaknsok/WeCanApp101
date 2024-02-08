using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp2._1._7.Models
{
    public class Word
    {
        public int Id { get; set; }

        [Display(Name = "Engrish Word")]
        //半角英数字と半角スペースのみ受け付ける
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please enter only English alphabets.")]
        public string? EWord { get; set; }

        [Display(Name = "漢字 -Japanese Word")]
        //[RegularExpression(@"^[\p{IsCJKUnifiedIdeographs}]+$", ErrorMessage = "Please enter only Kanji characters.")]
        //漢字のみ受け付ける
        [RegularExpression(@"^[一-龯]+$", ErrorMessage = "Please enter only Kanji characters.")]
        public string? JWord { get; set; }

        public string? PostUserId { get; set; }
        public IdentityUser? PostUser { get; set; }

        public string? UpdateUserId { get; set; }
        public IdentityUser? UpdateUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? PostAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
