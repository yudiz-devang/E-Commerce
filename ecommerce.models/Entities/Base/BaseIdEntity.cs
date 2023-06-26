using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.models.Entities.Base
{
    public class BaseIdEntity
    {
        [Key, Required]
        public Guid Id { get; set; }

        public BaseIdEntity() => Id = Guid.NewGuid();
    }
}
