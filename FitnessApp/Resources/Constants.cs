using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FitnessApp.Resources
{
    public static class Constants
    {
        public static class TableNames
        {
            public const string Program = "Program";
            public const string Exercise = "Exercise";
            public const string ProgramExercise = "ProgramExercise";
            public const string ProgramHistory = "ProgramHistory";
            public const string ExerciseHistory = "ExerciseHistory";
            public const string SetHistory = "SetHistory";
        }

        public const string FitnessAppDbName = "FitnessApp.db";
        public const string LocalDbName = "FitnessHistory.db";
    }
}