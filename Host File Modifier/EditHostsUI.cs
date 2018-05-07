using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Octokit;

using Newtonsoft.Json;

namespace Host_File_Modifier
{
    public partial class EditHostsUI : Form
    {
        public EditHostsUI()
        {
            InitializeComponent();
        }

        public HostVariables hostVar = new HostVariables();

        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

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

        static async Task<bool> checkRepository(string user, string repo)
        {
            var github = new GitHubClient(new ProductHeaderValue("HostsFileManager"));

            Repository repository = null;

            try
            {
                repository = await github.Repository.Get(user, repo);
            }
            catch 
            {
                Console.WriteLine("Can't find");
            }

            if (repository != null)
                return true;

            return false;
        }

        static async Task<IReadOnlyList<Repository>> getRepository(string user)
        {
            var github = new GitHubClient(new ProductHeaderValue("HostsFileManager"));

            IReadOnlyList<Repository> repository = null;

            try
            {
                repository = await github.Repository.GetAllForUser(user);
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(string.Format("Can't find the username on Github.\nAre you sure the naming is correct?\n\nDetailed Error :\n{0}", ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show(string.Format("Can't connect to Github. Are you sure you connected to the internet?\n\nDetailed Error :\n{0}", ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return repository;
        }

        static async Task<IReadOnlyList<RepositoryContent>> getFiles(string user, string path)
        {
            var github = new GitHubClient(new ProductHeaderValue("HostsFileManager"));

            IReadOnlyList<RepositoryContent> files = null;
            List<RepositoryContent> allFiles = null;

            try
            {
                files = await github.Repository.Content.GetAllContents(user, path);
            }
            catch (NotFoundException ex)
            {
                MessageBox.Show(string.Format("Can't find the repository on Github.\nAre you sure the naming is correct?\n\nDetailed Error :\n{0}", ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show(string.Format("Can't connect to Github. Are you sure you connected to the internet?\n\nDetailed Error :\n{0}", ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //foreach (RepositoryContent file in files)
            //{
            //    Debug.WriteLine(file.Name);
            //    if (file.Type == ContentType.File)
            //    {
            //        allFiles.Add(file);
            //    }
            //    if (file.Type == ContentType.Dir)
            //    {
            //        //Currently can't get files under dir. Will patch em up if i found a way to do it.
            //        //
            //        //var task = getFiles(user, path );
            //        //foreach (string dirFile in await task)
            //        //{
            //        //    allFiles.Add(dirFile);
            //        //}
            //        allFiles.Add(file);
            //    }
            //}

            return files;
        }

        static bool remoteFileExists(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        private void EditHostsUI_Load(object sender, EventArgs e)
        {
            repositoryLists.DropDownStyle = ComboBoxStyle.DropDownList;
            typeList.DropDownStyle = ComboBoxStyle.DropDownList;
            githubFilePath.DropDownStyle = ComboBoxStyle.DropDownList;

            typeList.SelectedIndex = 0;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            List<string> repoName = new List<string>();

            string username = usernameTextbox.Text;

            var getRepo = getRepository(username);

            IReadOnlyList<Repository> repositoriesList = await getRepo;

            if (repositoriesList != null)
            {
                Debug.WriteLine("This passed the null checking.");
                foreach (Repository repo in repositoriesList)
                { 
                    ComboboxItem item = new ComboboxItem();
                    item.Text = repo.Name;
                    item.Value = repo;
                    repositoryLists.Items.Add(item);
                }

                hostVar.repositoriesList = repositoriesList;

                hostVar.username = username;

                repositoryLists.SelectedIndex = 0;

            }

            this.UseWaitCursor = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = appDirectory;
            openFileDialog1.ShowDialog();

            filePath.Text = openFileDialog1.FileName;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (typeList.SelectedIndex)
            {
                case 0:
                    hostVar.hostType = HostType.Link;

                    linkGroup.Enabled = true;
                    githubGroup.Enabled = false;
                    fileGroup.Enabled = false;
                    break;
                case 1:
                    hostVar.hostType = HostType.GithubRepository;

                    linkGroup.Enabled = false;
                    githubGroup.Enabled = true;
                    fileGroup.Enabled = false;
                    break;
                case 2:
                    hostVar.hostType = HostType.File;

                    linkGroup.Enabled = false;
                    githubGroup.Enabled = false;
                    fileGroup.Enabled = true;
                    break;
            }
            
            this.UseWaitCursor = false;
        }

        private async void repositoryLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            Task<IReadOnlyList<RepositoryContent>> task = getFiles(hostVar.username, repositoryLists.Text);
            IReadOnlyList<RepositoryContent> repositoryFiles = await task;

            if (repositoryFiles != null)
            {
                githubFilePath.Items.Clear();
                foreach (RepositoryContent file in repositoryFiles)
                {
                    ComboboxItem item = new ComboboxItem
                    {
                        Text = file.Name,
                        Value = file
                    };
                    githubFilePath.Items.Add(item);
                }

                hostVar.repositoryFiles = repositoryFiles;
                hostVar.repositoryFileNameSelectedIndex = repositoryLists.SelectedIndex;
                githubFilePath.SelectedIndex = 0;
            }
            else
                MessageBox.Show("nasdoasd");

            this.UseWaitCursor = false;

            saveButton.Enabled = true;
        }

        private void checkLink_Click(object sender, EventArgs e)
        {
            if (remoteFileExists(linkBox.Text))
            {
                saveButton.Enabled = true;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            hostList hosts = new hostList();

            hosts.listOfHosts.Add(hostVar);

            using (System.IO.StreamWriter write = new System.IO.StreamWriter("test.json"))
            {
                write.Write(JsonConvert.SerializeObject(hostVar, Formatting.Indented));
            }
        }

        private void linkBox_TextChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
            hostVar.link = linkBox.Text;
        }

        private void usernameTextbox_TextChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
        }

        private void githubFilePath_SelectedIndexChanged(object sender, EventArgs e)
        {
            linkBox.Text = (githubFilePath.SelectedItem as ComboboxItem).returnDownloadLink();
            filePath.Text = appDirectory + "\\" + (githubFilePath.SelectedItem as ComboboxItem).returnName();
            hostVar.repositoryFileNameSelectedIndex = repositoryLists.SelectedIndex;
            saveButton.Enabled = true;
        }

        private void filePath_TextChanged(object sender, EventArgs e)
        {
            hostVar.filePath = filePath.Text;
        }

        private void loadJSON_Click(object sender, EventArgs e)
        {
            string result = File.ReadAllText("test.json");

            var convertedJSON = JsonConvert.DeserializeObject<HostVariables>(result);

            typeList.SelectedIndex = convertedJSON.hostType;
            linkBox.Text = convertedJSON.link;
            usernameTextbox.Text = convertedJSON.username;
            repositoryLists.Items.Clear();
            List<ComboboxItem> hostVarList = new List<ComboboxItem>();
            foreach (Repository repo in convertedJSON.repositoriesList)
            {
                ComboboxItem item = new ComboboxItem
                {
                    Text = repo.Name,
                    Value = repo
                };
                hostVarList.Add(item);
            }
            repositoryLists.Items.AddRange(hostVarList.ToArray());
            githubFilePath.Items.Clear();
            foreach (RepositoryContent file in convertedJSON.repositoryFiles)
            {
                ComboboxItem item = new ComboboxItem();

                item.Text = file.Name;
                item.Value = file;
                githubFilePath.Items.Add(item);
            }
            filePath.Text = convertedJSON.filePath;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public string returnDownloadLink()
        {
            return (Value as RepositoryContent)?.DownloadUrl;
        }

        public string returnName()
        {
            return (Value as RepositoryContent)?.Name;
        }
    }
}
