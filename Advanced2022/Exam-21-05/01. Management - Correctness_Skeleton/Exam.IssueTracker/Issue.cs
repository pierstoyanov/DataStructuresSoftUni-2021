using System;
using System.Collections.Generic;

namespace Exam.IssueTracker
{
    public class Issue : IComparable
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int Priority { get; set; }

        public string Assignee { get; set; }

        public IssueStatus IssueStatus { get; set; }

        public List<Issue> BlockedIssues { get; set; } = new List<Issue>();

        public List<Issue> BlockedByIssues { get; set; } = new List<Issue>();

        public Issue(string id, string title, int priority, string assignee)
        {
            this.Id = id;
            this.Title = title;
            this.Priority = priority;
            this.Assignee = assignee;
            this.IssueStatus = IssueStatus.ToDo;
        }

        public override bool Equals(object obj)
        {
            var toCompare = (Issue) obj;
            return Id == toCompare.Id;
        }

        public int CompareTo(object obj)
        {
            var toCompare = (Issue)obj;
            return this.Priority.CompareTo(toCompare.Priority);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Priority, Assignee, IssueStatus, BlockedIssues, BlockedByIssues);
        }
    }
}
