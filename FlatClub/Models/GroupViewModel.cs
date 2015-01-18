using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlatClub.Models
{
    public class GroupVModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsJoined { get; set; }
        public int MembersCount { get; set; }
        public int StoryesCount { get; set; }
    }

    public class GroupDetailsVModel : GroupVModel
    {
        public IEnumerable<UserVModel> Members { get; set; }
        public IEnumerable<StoryVModel> Stories { get; set; }
    }

    public class GroupCreateVModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} lenght must be between {2} and {1}", MinimumLength = 4)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }        
    }

    public class GroupEditVModel : GroupCreateVModel
    {
        [Required]
        public int Id { get; set; }
    }
}