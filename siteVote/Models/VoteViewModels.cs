using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace siteVote.Models
{
    public class VoteViewModels
    {
        [Display(Name="得分")]
        [Required]
        public int score { get; set; }
        [Display (Name="网站名称")]
        public string siteName { get; set; }
    }
}