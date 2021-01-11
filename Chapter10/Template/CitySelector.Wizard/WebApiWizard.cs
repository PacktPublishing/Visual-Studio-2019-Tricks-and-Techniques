using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace CitySelector.Wizard
{
    public class WebApiWizard : IWizard
    {
        private static MainWindow _mainWindow;

#pragma warning disable IDE0052 // Remove unread private members

        // ReSharper disable once NotAccessedField.Local
        private static string _templateName;

#pragma warning restore IDE0052 // Remove unread private members

        public static void GetConnectionStringUserInput(string templateName)
        {
            _templateName = templateName;

            _mainWindow = new MainWindow();
            _mainWindow.ConnectionStringSet += MyWindow_ConnectionStringSet;
            _mainWindow.ShowDialog();
        }

        // This method is called before opening any item that has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        // This method is only called for item templates, not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        // This method is called after the project is created.
        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            // Obtain connection string value from RootDictionary and place it in the replacementsDictionary.
            if (RootWizard.RootDictionary.ContainsKey(Consts.DictionaryEntries.ConnectionString))
            {
                replacementsDictionary[Consts.DictionaryEntries.ConnectionString] = RootWizard.RootDictionary[Consts.DictionaryEntries.ConnectionString];
            }
        }

        // This method is only called for item templates, not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        private static void MyWindow_ConnectionStringSet(object sender, MainWindow.ConnectionStringSetEventArgs e)
        {
            // Place connection string input by user into the root dictionary
            RootWizard.RootDictionary[Consts.DictionaryEntries.ConnectionString] = e.ConnectionString;
            _mainWindow.Close();
        }
    }
}