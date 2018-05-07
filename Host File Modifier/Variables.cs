using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Octokit;

namespace Host_File_Modifier
{
    public class HostVariables
    {
        public int hostType;

        public string link;

        public string username;
        public string repositoryName;
        public int repositoryNameSelectedIndex;
        public string repositoryFileName;
        public int repositoryFileNameSelectedIndex;
        public IReadOnlyList<RepositoryContent> repositoryFiles;
        public IReadOnlyList<Repository> repositoriesList;

        public string filePath;
    }

    public class hostList
    {
        public List<HostVariables> listOfHosts = new List<HostVariables>();
    }

    public class HostType
    {
        public static int Link = 0;
        public static int GithubRepository = 1;
        public static int File = 2;
    }
}
