using System.Collections.Generic;

namespace Exam.IssueTracker
{
    public class IssueTracker : IIssueTracker
    {
        public int Count => 0;

        public void AddIssue(Issue issue)
        {
            
        }

        public void Blocks(string issueId, string blockedIssueId)
        {

        }

        public bool Contains(Issue issue)
        {
            return false;
        }

        public Dictionary<string, Dictionary<IssueStatus, List<Issue>>> GetAssigneeIssueGroupedByStatus()
        {
            return null;
        }

        public IEnumerable<Issue> GetBacklog()
        {
            return null;
        }

        public IEnumerable<Issue> GetBlockedIssues()
        {
            return null;
        }

        public IEnumerable<Issue> GetLongestBlockChain()
        {
            return null;
        }

        public void MoveInDone(string issueId)
        {
            
        }

        public void MoveInProgress(string issueId)
        {
            
        }

        public void RemoveIssue(string issueId)
        {
            
        }
    }
}
