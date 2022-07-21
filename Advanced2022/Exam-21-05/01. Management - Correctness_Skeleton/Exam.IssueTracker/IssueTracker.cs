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

        private Dictionary<IssueStatus, List<Issue>> IssuesByStatus = new Dictionary<IssueStatus, List<Issue>>();

        private Dictionary<string, List<Issue>> AssigneeIssues = new Dictionary<string, List<Issue>>();

        private LinkedList<Issue> LongestBlocked = new LinkedList<Issue>();

        private List<LinkedList<Issue>> Blocked = new List<LinkedList<Issue>>();

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
                IssuesByStatus[issue.IssueStatus] = new List<Issue>() { issue };
            }
            else
            {
                IssuesByStatus[issue.IssueStatus].Add(issue);
            }

            if (!AssigneeIssues.ContainsKey(issue.Assignee))
            {
                AssigneeIssues[issue.Assignee] = new List<Issue>() { issue };
            }
            else
            {
                AssigneeIssues[issue.Assignee].Add(issue);
            }

        }

        public void Blocks(string issueId, string blockedIssueId)
        {
            NotContainsIssueId(issueId);
            NotContainsIssueId(blockedIssueId);

            //add to blocked by list
            IdIssues[issueId].BlockedIssues.Add(IdIssues[blockedIssueId]);
            IdIssues[blockedIssueId].BlockedByIssues.Add(IdIssues[issueId]);

            var blockChain = new LinkedList<Issue>();

            if (IdIssues[blockedIssueId].BlockedByIssues.Count != 0)
            {

            }
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
            var result = new Dictionary<string, Dictionary<IssueStatus, List<Issue>>>();

            foreach (var kvp in AssigneeIssues)
            {

                var issuesDict = new Dictionary<IssueStatus, List<Issue>>();

                foreach (var issue in kvp.Value)
                {
                    if (!issuesDict.ContainsKey(issue.IssueStatus))
                    { 
                        issuesDict.Add(issue.IssueStatus, new List<Issue> { issue });
                    }
                    else
                    {
                        issuesDict[issue.IssueStatus].Add(issue);
                    }
                }

                result.Add(kvp.Key, issuesDict);
            }

            var count = 0;
            foreach (var kvp in result)
            {
                if (kvp.Value.Count != 0)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                return new Dictionary<string, Dictionary<IssueStatus, List<Issue>>>();
            }    

            return result;
        }

        public IEnumerable<Issue> GetBacklog()
        {
            var issuesToReturn = IdIssues.Values
                .Where(x => x.IssueStatus == IssueStatus.ToDo)
                .OrderBy(x => x.Priority)
                .ToList();
/*
            var issuesToReturn = IssuesByStatus[IssueStatus.ToDo]
                .OrderBy(x => x.Priority)
                .ToList();*/

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
            List<Issue> LongestBlock = new List<Issue>();

            var blockedIssues = GetBlockedIssues();

            var temp = new List<Issue>();

            foreach (var blockedIssue in blockedIssues)
            {
                if (blockedIssue.BlockedByIssues.Count != 0)
                {

                }



            }

             return LongestBlock;

        }

        private List<Issue> addChildren (Issue issue, List<Issue> result, List<Issue> longest)
        {
            if (issue.BlockedIssues.Count == 0)
            {
                result.Add(issue);
                return result;
            }

            else 
            {
               
                foreach (var child in issue.BlockedIssues)
                {
                    addChildren(child, result, longest);

                    if (longest.Count < result.Count)
                    {
                         longest = result;
                    }
                }

                return result;
            }
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
            
            //remove from status collection
            IssuesByStatus[temp.IssueStatus].Remove(temp);

            AssigneeIssues[temp.Assignee].Remove(temp);

            IdIssues.Remove(issueId);


        }

        private void NotAsigneeExists(string assigneeName)
        {
            if (!AssigneeIssues.ContainsKey(assigneeName))
            {
                throw new ArgumentException();
            }
        }
    }
}
