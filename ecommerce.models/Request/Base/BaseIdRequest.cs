﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.models.Request.Base
{
    public class BaseIdRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
