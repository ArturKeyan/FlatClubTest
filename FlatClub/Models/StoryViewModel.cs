using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FlatClub.Models
{
    public class StoryVModel
    {
        public int Id { get; set; }
        public string Title { get; set; }       
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
    }

    public class StoryGroupedVModel
    {
        public IEnumerable<GroupVModel> Groups { get; set; }

        public IEnumerable<StoryVModel> Stories { get; set; }
    }

    public class StoryDetailsVModel : StoryVModel
    {
        public bool IsEditable { get; set; }
        public UserVModel Creator { get; set; }
        public GroupVModel[] Groups { get; set; }
    }

    public class StoryCreateVModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} lenght must be between {2} and {1}", MinimumLength = 4)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "You must select at least one group")]
        [Display(Name = "Select Groups")]
        public int[] GroupIds { get; set; }

        public IEnumerable<SelectListItem> GroupListItems { get; set; }
    }

    public class StoryEditVModel : StoryCreateVModel
    {
        [Required]
        public int Id { get; set; }
    }
}