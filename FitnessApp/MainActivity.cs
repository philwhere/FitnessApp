using Android.App;
using Android.OS;
using Android.Widget;
using FitnessApp.Resources;
using FitnessApp.Resources.DataHelper;
using FitnessApp.Resources.Model;
using System.Collections.Generic;
using System.IO;

namespace FitnessApp
{
    [Activity(Label = "FitnessApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ListView ListData => FindViewById<ListView>(Resource.Id.fitnessProgramListView);
        private DatabaseExecuter db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            db = new DatabaseExecuter();
            //db.CreateDatabase();
            EnsureDatabaseCreated();

            //listData;
            LoadData();
        }

        private void LoadData()
        {
            //listSource = db.GetAllExercises();
            var listSource = db.GetProgramExercises(1);
            var adapter = new ExerciseListViewAdapter(this, listSource);
            ListData.Adapter = adapter;
        }

        private void EnsureDatabaseCreated()
        {
            if (!File.Exists(Shortcuts.ReadOnlyDatabaseFile))
            {
                var dbStream = Assets.Open(Constants.FitnessAppDbName); // DATA FILE RESOURCE ID
                var writeStream = new FileStream(Shortcuts.ReadOnlyDatabaseFile, FileMode.OpenOrCreate, FileAccess.Write);
                Common.ReadWriteStream(dbStream, writeStream);
            }
        }
    }
}

