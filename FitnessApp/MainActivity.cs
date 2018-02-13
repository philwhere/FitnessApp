using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using FitnessApp.Resources;
using FitnessApp.Resources.DataHelper;
using FitnessApp.Resources.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FitnessApp
{
    [Activity(Label = "FitnessApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private static int LatestExerciseId => 15; // Leg Press
        private List<Button> Buttons { get; } = new List<Button>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            EnsureDatabaseCreated();
            EnsureDatabaseUpdated();

            RegisterButtons();
            HandleButtonClick();
            SetProgramNames();
        }

        private void HandleButtonClick() => Buttons.ForEach(b => b.Click += ShowExercises);

        private void RegisterButtons() => AddButtonsToList(
            Resource.Id.program1,
            Resource.Id.program2,
            Resource.Id.program3);

        private void SetProgramNames()
        {
            var programs = DatabaseExecuter.GetPrograms();
            Buttons.ForEach(b => b.Text = GetButtonText(programs, b));
        }

        private void AddButtonsToList(params int[] resourceIds)
        {
            foreach (var id in resourceIds)
                Buttons.Add(FindViewById<Button>(id));
        }

        private static string GetButtonText(List<Program> programs, Button button)
            => programs.First(p => $"{p.Id}" == $"{button.Tag}").Name;


        private void ShowExercises(object sender, EventArgs eventArgs)
        {
            var button = (Button) sender;
            var programId = ExtractProgramId(button);
            GoToExercises(programId);
        }

        private void GoToExercises(string programId)
        {
            var i = new Intent(this, typeof(Exercises));
            i.PutExtra("ProgramId", programId);
            StartActivity(i);
        }

        private static string ExtractProgramId(Button button) => (string)button.Tag;

        private void EnsureDatabaseCreated()
        {
            if (!File.Exists(Shortcuts.ReadOnlyDatabaseFile))
                CopyDb();
        }

        private void EnsureDatabaseUpdated()
        {
            var latestExerciseId = DatabaseExecuter.GetLatestExerciseId();
            if (latestExerciseId != LatestExerciseId)
                CopyDb();
        }

        private void CopyDb()
        {
            var dbStream = Assets.Open(Constants.FitnessAppDbName);
            var writeStream = new FileStream(Shortcuts.ReadOnlyDatabaseFile, FileMode.OpenOrCreate, FileAccess.Write);
            Common.ReadWriteStream(dbStream, writeStream);
        }
    }
}