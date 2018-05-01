/*
A Program to Patch "hosts" File.
Copyright (C) 2018  Rogan Matrivski Lartengalf
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Octokit;

namespace Host_File_Modifier
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
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

        static bool remoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "GET";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
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

        private void MainUI_Load(object sender, EventArgs e)
        {

        }
    }
}
