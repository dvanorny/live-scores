
using System;

namespace LiveScores.Entities
{
    public class PageViewItem
    {
        public int LogId { get; set; }
        public DateTime LogTimestamp { get; set; }
        public string Username { get; set; }
        public string PageView { get; set; }
    }
}