using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;

namespace CitySelector.Wizard
{
    // Invoked by root project's vstemplate
    public class RootWizard : IWizard
    {
        // Makes values such as $saferootprojectname$ available to other/child IWizard implementations.
        public static Dictionary<string, string> RootDictionary = new Dictionary<string, string>();

        // Fields
        private DTE2 _dte2;

        private string _templateName;

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            // Add ReadMe file to solution folder.
            var solution = (Solution2)_dte2.Solution;
            solution.AddSolutionFolder("Solution Items");

            // Copy the readme file to the solution directory.
            var sourceReadMePath = GetTemplateReadMeFilePath(_templateName);
            var destReadMePath = GetDestReadMeFilePath(solution, _templateName);
            if (!string.IsNullOrWhiteSpace(destReadMePath))
            {
                File.Copy(sourceReadMePath, destReadMePath);

                // Add the readMe file to the solution folder.
                if (File.Exists(destReadMePath))
                {
                    _dte2.ItemOperations.AddExistingItem(destReadMePath);
                }
            }

            // Set startup project(s).
            if (_templateName == Consts.ProjectTemplates.Root)
            {
                var startupProjects = new List<object>();
                List<Project> solutionProjects = new List<Project>();
                foreach (var objProject in _dte2.Solution.Projects)
                {
                    if (objProject is Project solutionProject)
                    {
                        solutionProjects.Add(solutionProject);
                    }
                }

                Project webApiProject = GetProject(".WebApi", solutionProjects);
                Project wpfProject = GetProject(".WPF", solutionProjects);

                if (webApiProject != null)
                {
                    startupProjects.Add(webApiProject.UniqueName);
                }

                if (wpfProject != null)
                {
                    startupProjects.Add(wpfProject.UniqueName);
                }

                if (startupProjects.Any())
                {
                    _dte2.Solution.SolutionBuild.StartupProjects = startupProjects.ToArray(); // startupProject.UniqueName;
                }
            }
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind,
            object[] customParams)
        {
            _dte2 = (DTE2)automationObject;
            _templateName = Path.GetFileNameWithoutExtension((string)customParams[0]);

            // Place $parentwizardname$ in root dictionary
            RootDictionary[Consts.DictionaryEntries.ParentWizardName] = Consts.ParentWizards.RootWizard;

            // Place "$saferootprojectname$ in the global dictionary.
            RootDictionary[Consts.DictionaryEntries.SafeRootProjectName] = replacementsDictionary[Consts.DictionaryEntries.SafeProjectName];

            // Invoke child wizard to store connection string in RootDictionary.
            WebApiWizard.GetConnectionStringUserInput(_templateName);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private string GetDestReadMeFilePath(Solution2 solution, string templateName)
        {
            string retVal = null;
            var solutionPath = solution.Properties.Item("Path").Value as string;

            if (string.IsNullOrWhiteSpace(solutionPath))
            {
                return null;
            }

            var solutionDir = new FileInfo(solutionPath).DirectoryName;
            if (string.IsNullOrWhiteSpace(solutionDir))
            {
                return null;
            }

            var readMeFileName = GetReadMeFileName(templateName);
            if (!string.IsNullOrWhiteSpace(readMeFileName))
            {
                retVal = Path.Combine(solutionDir, readMeFileName);
            }

            return retVal;
        }

        private Project GetProject(string extension, IEnumerable<Project> projects)
        {
            Project retVal = null;
            string projectName = RootDictionary[Consts.DictionaryEntries.SafeRootProjectName] + extension;
            foreach (Project project in projects) // _dte2.Solution.Projects)
            {
                var projectFullName = Path.GetFileNameWithoutExtension(project.FullName);
                if (projectFullName.Equals(projectName))
                {
                    retVal = project;
                }
                else if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                {
                    var innerProjects = GetSolutionFolderProjects(project);
                    retVal = GetProject(extension, innerProjects);
                    if (retVal != null)
                    {
                        break;
                    }
                }
            }

            return retVal;
        }

        private string GetReadMeFileName(string templateName)
        {
            string retVal;

            switch (templateName)
            {
                case Consts.ProjectTemplates.Root:
                    retVal = Consts.ReadMeFiles.Root;
                    break;

                default:
                    retVal = null;
                    break;
            }

            return retVal;
        }

        private IEnumerable<Project> GetSolutionFolderProjects(Project project)
        {
            List<Project> retVal = new List<Project>();
            var numProjectItems = (project.ProjectItems as ProjectItems).Count;
            for (var i = 1; i <= numProjectItems; i++)
            {
                var subProject = project.ProjectItems.Item(i).SubProject;
                // var subProject = projectItem as Project;
                if (subProject != null)
                {
                    retVal.Add(subProject);
                }
            }

            return retVal;
        }

        private string GetTemplateFilePath(string fileName)
        {
            string dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(dirPath, fileName);
        }

        private string GetTemplateReadMeFilePath(string templateName)
        {
            string readMeFileName = GetReadMeFileName(templateName);
            if (readMeFileName == null)
            {
                return null;
            }

            var templateReadMePath = GetTemplateFilePath(readMeFileName);

            return templateReadMePath;
        }
    }
}