using FineTable.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Application.DTO.Request
{
	public class FineCollectionDetailRequest
	{
		public int MemberID { get; set; }
		public int Amount { get; set; }
		public int Days { get; set; }
		public MemberType MemberType { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ReturnDate { get; set; }

		public FineStatus FineStatus { get; set; }
	}

	public class FineCollection
	{
        public int Id { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int IssuedStatus { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int StaffId { get; set; }
        public double FineRate { get; set; }
        public double FineAmount { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum IssuedStatus
    {
        Issued = 1,
        Returned = 2,
        //Available = 3
    }
}
