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
        private Button Button1 => FindViewById<Button>(Resource.Id.program1);
        private Button Button2 => FindViewById<Button>(Resource.Id.program2);
        private Button Button3 => FindViewById<Button>(Resource.Id.program3);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            EnsureDatabaseCreated();

            LoadProgramNames();
            Button1.Click += ButtonOnClick;
            Button2.Click += ButtonOnClick;
            Button3.Click += ButtonOnClick;
        }


        private void LoadProgramNames()
        {
            var programs = DatabaseExecuter.GetPrograms();
            Button1.Text = GetButtonText(programs, Button1);
            Button2.Text = GetButtonText(programs, Button2);
            Button3.Text = GetButtonText(programs, Button3);
        }

        private static string GetButtonText(List<Program> programs, Button button) 
            => programs.First(p => $"{p.Id}" == $"{button.Tag}").Name;

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var button = (Button)sender;
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
            var dbStream = Assets.Open(Constants.FitnessAppDbName);
            if (!File.Exists(Shortcuts.ReadOnlyDatabaseFile) || !IsLatestDb)
            {
                var writeStream = new FileStream(Shortcuts.ReadOnlyDatabaseFile, FileMode.OpenOrCreate, FileAccess.Write);
                Common.ReadWriteStream(dbStream, writeStream);
            }
        }

        private static bool IsLatestDb => false;
    }
}

