using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.IssueTracker
{
    public class IssueTracker : IIssueTracker
    {
        private Dictionary<string, Issue> IdIssues = new Dictionary<string, Issue>();

        //private HashSet<Issue> ToDo = new HashSet<Issue>();
        //private HashSet<Issue> InProgress = new HashSet<Issue>();
        //private HashSet<Issue> Done = new HashSet<Issue>();

        Dictionary<IssueStatus, HashSet<Issue>> IssuesByStatus = new Dictionary<IssueStatus, HashSet<Issue>>();

        public int Count => IdIssues.Count;

        //add
        public void AddIssue(Issue issue)
        {
            IdIssues.Add(issue.Id, issue);
            
         /*   if (issue.IssueStatus != IssueStatus.ToDo)
            {
                if (issue.IssueStatus == IssueStatus.InProgress)
                {
                     InProgress.Add(issue);
                }

                else if (issue.IssueStatus == IssueStatus.Done)
                {
                    Done.Add(issue);
                }
            }
            else
            {
                ToDo.Add(issue);
            }*/

            if (!IssuesByStatus.ContainsKey(issue.IssueStatus))
            {
                IssuesByStatus[issue.IssueStatus] = new HashSet<Issue>() { issue };
            }
            else
            {
                IssuesByStatus[issue.IssueStatus].Add(issue);
            }
        }

        public void Blocks(string issueId, string blockedIssueId)
        {
            NotContainsIssueId(issueId);
            NotContainsIssueId(blockedIssueId);

            //add to blocked by list
            IdIssues[issueId].BlockedIssues.Add(IdIssues[blockedIssueId]);
            IdIssues[blockedIssueId].BlockedByIssues.Add(IdIssues[issueId]);
        }

        public bool Contains(Issue issue)
        {
            return IdIssues.ContainsKey(issue.Id);
        }

        private void NotContainsIssueId(string id)
        {
            if (!IdIssues.ContainsKey(id))
            {
                throw new ArgumentException();
            }
        }

        public Dictionary<string, Dictionary<IssueStatus, List<Issue>>> GetAssigneeIssueGroupedByStatus()
        {
            return null;
        }

        public IEnumerable<Issue> GetBacklog()
        {
            var issuesToReturn = IdIssues.Values
                .Where(x => x.IssueStatus == IssueStatus.ToDo)
                .OrderBy(x => x.Priority)
                .ToList();

            /*            var issuesToReturn = IssuesByStatus[IssueStatus.ToDo]
                            .OrderBy(x => x.Priority)
                            .ToList();*/

            if (issuesToReturn.Count == 0)
            {
                return new List<Issue>();
            }    

            return issuesToReturn;
        }

        public IEnumerable<Issue> GetBlockedIssues()
        {
            var issuesToReturn = IdIssues.Values
                .Where(x => x.BlockedByIssues.Count != 0)
                .OrderBy(x => x.Priority)
                .ThenBy(x => x.Title)
                .ToList();

            if (issuesToReturn.Count == 0)
            {
                return new List<Issue>();
            }

            return issuesToReturn;
        }

        public IEnumerable<Issue> GetLongestBlockChain()
        {
            return null;
        }

        //editer
        public void MoveInDone(string issueId)
        {
            NotContainsIssueId(issueId);
            var temp = IdIssues[issueId];

            //remove & add status collections
            IssuesByStatus[temp.IssueStatus].Remove(temp);
            IssuesByStatus[IssueStatus.Done].Add(temp);
           
            //change issue status
            temp.IssueStatus = IssueStatus.Done;
        }
        //editer
        public void MoveInProgress(string issueId)
        {
            NotContainsIssueId(issueId);
            var temp = IdIssues[issueId];

            //NOT currently in status "To Do" 
            if (!IssuesByStatus[IssueStatus.ToDo].Contains(temp))
            {
                throw new ArgumentException();
            }

            //remove & add status collections
            IssuesByStatus[temp.IssueStatus].Remove(temp);
            IssuesByStatus[IssueStatus.InProgress].Add(temp);

            //change issue status
            temp.IssueStatus = IssueStatus.InProgress;
        }
        //remover
        public void RemoveIssue(string issueId)
        {
            NotContainsIssueId(issueId);

            var temp = IdIssues[issueId];

            IdIssues.Remove(issueId);
            //remove from status collection
            IssuesByStatus[temp.IssueStatus].Remove(temp);


        }
    }
}
