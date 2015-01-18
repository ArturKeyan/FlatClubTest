using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Table("User")]
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        
        /// <summary>
        /// groups in which the user participates
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; }

        /// <summary>
        /// Stories that been created by user
        /// </summary>
        public virtual ICollection<Story> Stories { get; set; }
    }   
}
