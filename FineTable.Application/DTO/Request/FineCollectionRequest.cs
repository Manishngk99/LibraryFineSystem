﻿using FineTable.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Application.DTO.Request
{
    public class FineCollectionRequest
    {
        public int MemberID { get; set; }
        //public int Amount { get; set; }
        //public int Days { get; set; }
        public MemberType MemberType { get; set; }
        public DateTime CreatedDate { get; set; }
       // public DateTime ReturnDate { get; set; }

        public FineStatus FineStatus { get; set; }
    }
}
