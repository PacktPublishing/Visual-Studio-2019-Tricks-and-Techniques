using System;

namespace CitySelector.Wizard
{
    internal static class Consts
    {
        public static class DictionaryEntries
        {
            public const string ConnectionString = "$dbconnectionstring$";
            public const string DestinationDirectory = "$destinationdirectory$";
            public const string OriginalDestinationDirectory = "$origdestinationdirectory$";
            public const string ParentWizardName = "$parentwizardname$";
            public const string SafeProjectName = "$safeprojectname$";
            public const string SafeRootProjectName = "$saferootprojectname$";
            public const string SolutionDirectory = "$solutiondirectory$";
        }

        public static class ParentWizards
        {
            public const string RootWizard = "RootWizard";
        }

        public static class ProjectTemplates
        {
            public const string CitySelectorWebApi = "WebApi";
            public const string CitySelectorWpf = "WPF";
            public const string Root = "Root";
        }

        public static class ReadMeFiles
        {
            public const string Root = "ReadMe.md";
        }
    }
}