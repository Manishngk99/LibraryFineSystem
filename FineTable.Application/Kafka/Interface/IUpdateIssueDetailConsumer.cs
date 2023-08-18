using FineTable.Application.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTable.Application.Kafka.Interface
{
	public interface IUpdateIssueDetailConsumer
	{
		Task IssueDetailConsumer(FineCollectionDetailRequest request);
	}
}
