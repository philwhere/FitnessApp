using System;
using System.IO;

namespace FitnessApp.Resources
{
    public static class Shortcuts
    {
        public static string PersonalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public static string ReadOnlyDatabaseFile = Path.Combine(PersonalFolder, Constants.FitnessAppDbName);
        public static string PersonalDatabaseFile = Path.Combine(PersonalFolder, Constants.LocalDbName);
    }
}