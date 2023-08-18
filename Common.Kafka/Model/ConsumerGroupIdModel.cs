using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Kafka.Model
{
    public class ConsumerGroupIdModel
    {
        public string GroupId { get; set; }
        public bool EnableAutoCommit { get; set; }
        public AutoOffsetReset AutoOffsetReset { get; set; }
        public int? StatisticsIntervalMs { get; set; }
        public int? SessionTimeoutMs { get; set; }
        public bool IsEvent { get; set; }

    }
}
