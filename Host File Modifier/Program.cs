using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Octokit;

namespace Host_File_Modifier
{
    class Program
    {
        static string returnFilename(string hreflink)
        {
            Uri uri = new Uri(hreflink);

            return Path.GetFileName(uri.LocalPath);
        }

        static async Task<IReadOnlyList<RepositoryContent>> returnURL(string filename)
        {
            var github = new GitHubClient(new ProductHeaderValue("HostsFileManager"));

            IReadOnlyList<RepositoryContent> file = null;

            try
            {
                file = await github.Repository.Content.GetAllContents("gvoze32", "unblockhostid", filename);
            }
            catch (NotFoundException)
            {
                Console.WriteLine("Can't find hosts file from gvoze32/unblockhostid repository. The file is probably deleted or moved by the owner. \nIf you are not the owner of this program, please request an issue to this program Github repository.");
            }

            return file;
        }

        static List<string> mergeLists(List<string> list1, List<string> list2)
        {
            List<string> merged = new List<string>();

            bool match = false;
            foreach (string host in list1)
            {
                var host_split = host.Split(' ');
                for (int i = 1; i < host_split.Length; i++)
                {
                    foreach (string x in list2)
                    {
                        var x_split = x.Split(' ');
                        for (int j = 1; j < x_split.Length; j++)
                        {
                            match = host_split[i] == x_split[j];
                            if (match)
                                break;
                        }
                        if (match)
                            break;
                    }
                    if (match)
                        break;
                }

                if (!match)
                {
                    merged.Add(host);
                    //Console.WriteLine(host);
                }

                match = false;
            }

            foreach (string host in list2)
            {
                var host_split = host.Split(' ');
                for (int i = 1; i < host_split.Length; i++)
                {
                    foreach (string x in list1)
                    {
                        var x_split = x.Split(' ');
                        for (int j = 1; j < x_split.Length; j++)
                        {
                            match = host_split[i] == x_split[j];
                            if (match)
                                break;
                        }
                        if (match)
                            break;
                    }
                    if (match)
                        break;
                }

                if (!match)
                {
                    merged.Add(host);
                    //Console.WriteLine(host);
                }

                match = false;
            }

            return merged;
        }

        static List<string> removeDuplicateFromLists(List<string> list1, List<string> list2)
        {
            List<string> removed = new List<string>();

            bool match = false;
            foreach (string host in list1)
            {
                var host_split = host.Split(' ');
                for (int i = 1; i < host_split.Length; i++)
                {
                    foreach (string x in list2)
                    {
                        var x_split = x.Split(' ');
                        for (int j = 1; j < x_split.Length; j++)
                        {
                            match = host_split[i] == x_split[j];
                            if (match)
                                break;
                        }
                        if (match)
                            break;
                    }
                    if (match)
                        break;
                }

                if (!match)
                {
                    removed.Add(host);
                    //Console.WriteLine(host);
                }

                match = false;
            }
            return removed;
        }

        static List<string> cleanupHosts (List<string> list)
        {
            return null;
        }

        static void Main(string[] args)
        {
            if (!File.Exists("hosts"))
            {
                Task<IReadOnlyList<RepositoryContent>> fileContent = returnURL("hosts");
                Console.WriteLine("Please Wait.");
                fileContent.Wait();
                var x = fileContent.Result;
                if (x != null)
                    foreach (RepositoryContent content in x)
                    {
                        Console.WriteLine(content.DownloadUrl);
                    }

                using (WebClient downloader = new WebClient())
                {
                    downloader.DownloadFile(x[0].DownloadUrl, returnFilename(x[0].DownloadUrl));
                }
            }
            else
                Console.WriteLine("Hosts file already downloaded.");

            string hostsFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\System32\drivers\etc\hosts";
            //List<string> originalHosts = debug_returnorigfiles(hostsFileLocation);
            List<string> originalHosts = debug_returnorigfiles("hosts_debug1");
            List<string> downloadedHosts = debug_returnlistfromfiles("hosts");
            List<string> mergedHosts = mergeLists(originalHosts, downloadedHosts);

            bool getHost = false;
            int readPosition = 0 - 1;
            int startEditing = 0, endEditing = 0;
            if (originalHosts.Contains("#[HOST_EDIT_START]"))
            { 
                foreach (string host in originalHosts)
                {
                    readPosition++;
                    if (host == "#[HOST_EDIT_START]")
                    {
                        getHost = true;
                        startEditing = readPosition + 1;
                        continue;
                    }
                    if (host == "#[HOST_EDIT_END]")
                    {
                        endEditing = readPosition - 1;
                        getHost = false;
                        continue;
                    }

                    if (getHost)
                    {
                        //Console.WriteLine(host);
                    }
                }

                originalHosts.RemoveRange(startEditing, endEditing - startEditing + 1);

                originalHosts.InsertRange(startEditing, downloadedHosts)  ;
            }
            else
            {
                originalHosts.Add("#[HOST_EDIT_START]");
                originalHosts.AddRange(downloadedHosts);
                originalHosts.Add("#[HOST_EDIT_END]");
            }


            foreach (string lines in originalHosts)
                Console.WriteLine(lines);

            using (StreamWriter write = new StreamWriter("hosts_debug1", false, Encoding.UTF8, 1024))
            {
                foreach (string line in originalHosts)
                {
                    write.WriteLine(line);
                }
            }

                Console.ReadKey();
        }

        static string debug_readandprintstuff(string filename)
        {
            string stringbundles = "";
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    stringbundles += line + "\n";
                }
            }
            return stringbundles;
        }

        static List<string> debug_returnlistfromfiles(string filename)
        {
            List<string> linelist = new List<string>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(string.Format("{0}, {1}, {2}", !line.StartsWith("#") , !string.IsNullOrWhiteSpace(line) , !line.Contains("#")));
                    if (!string.IsNullOrWhiteSpace(line))
                        if (!line.StartsWith("#") || line == "#[HOST_EDIT_END]" || line == "#[HOST_EDIT_END]")
                            linelist.Add(line);
                }
            }
            return linelist;
        }

        static List<string> debug_returnorigfiles(string filename)
        {
            List<string> linelist = new List<string>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    linelist.Add(line);
                }
            }
            return linelist;
        }

        static List<string> debug_randomNumber(int length)
        {
            Console.WriteLine("Writing stuff");

            var random = new Random();

            List<string> listOfStrings = new List<string>();

            for (int i = 0; i < length; i++)
            {
                int randNum = random.Next();

                listOfStrings.Add(randNum.ToString());
            }

            return listOfStrings;
        }

        static void debug_writestuff()
        {
            Console.WriteLine("Writing stuff");

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[128];
            var random = new Random();

            List<string> listOfStrings = new List<string>();

            for (int i = 0; i < 1000; i++)
            {
                for (int x = 0; x < stringChars.Length; x++)
                {
                    stringChars[x] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                listOfStrings.Add(finalString);
            }

            using (StreamWriter fileWriter = new StreamWriter("hosts"))
            {
                foreach (string randomLines in listOfStrings)
                {
                    fileWriter.WriteLine(randomLines);
                }
            }
        }
    }
}
