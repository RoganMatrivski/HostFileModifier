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

            string filename = System.IO.Path.GetFileName(uri.LocalPath);

            return filename;
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

        static void Main(string[] args)
        {
            List<string> hosts_debug = debug_returnlistfromfiles("hosts_debug1");

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

            Console.WriteLine("Done downloading hosts file from gvoze32/unblockhostid repository. Reading file...");

            List<string> downloadedHosts = debug_returnlistfromfiles("hosts");

            List<string> mergedHosts = hosts_debug.Union(downloadedHosts).ToList();


            Console.WriteLine("Done merging any changes.");

            foreach (string line in mergedHosts)
                Console.WriteLine(line);

            Console.ReadKey();
            #region  

            string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\System32\drivers\etc\hosts";

            List<string> originalHosts = debug_returnorigfiles(fileLocation);

            bool getHost = false;

            int readPosition = 0 - 1;
            int startEditing = 0, endEditing = 0;
            foreach (string host in originalHosts)
            {
                readPosition++;
                if (host == "#[HOST_EDIT_START]")
                {
                    getHost = true;
                    startEditing = readPosition+1;
                    continue;
                }
                if (host == "#[HOST_EDIT_END]")
                {
                    endEditing = readPosition-1;
                    getHost = false;
                    continue;
                }

                if (getHost)
                {
                    Console.WriteLine(host);
                }
            }

            originalHosts.RemoveRange(startEditing, endEditing - startEditing + 1);

            foreach (string host in originalHosts)
                Console.WriteLine(host);

            Console.ReadKey();

            originalHosts.InsertRange(startEditing, mergedHosts.Except(originalHosts));

            foreach (string host in originalHosts)
                Console.WriteLine(host);
             

            Console.ReadKey();
#endregion
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
