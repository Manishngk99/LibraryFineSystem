﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Infrastructure.Service
{
    public class Common
    {
        public class ServiceResult<t>
        {
            public StatusType Status { get; set; }
            public string Message { get; set; }
            public t Data { get; set; }
        }
    }
    public enum StatusType
    {
        Success,
        Failure,
    }
}

