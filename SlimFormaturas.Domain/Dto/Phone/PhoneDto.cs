﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SlimFormaturas.Domain.Dto.Phone {
    public class PhoneDto {
        public string PhoneId { get; set; }
        public bool Default { get; set; }
        public string Ddd { get; set; }
        public string PhoneNumber { get; set; }

        public string TypeGenericId { get; set; }
        public string GraduateId { get; set; }
    }
}
