using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("Stories")]
    public class Story : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public int CreatorId { get; set; }
                
        public User Creator { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
