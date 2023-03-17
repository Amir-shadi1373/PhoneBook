using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Core.Domain.Models
{
    public class EmailVM:General
    {

        [Required]
        public string Value { get; set; }
        public long UserId { get; set; }
    }
}
