using Android.App;
using Android.OS;
using Android.Widget;
using FitnessApp.Resources.DataHelper;

namespace FitnessApp
{
    [Activity(Label = "Exercises")]
    public class Exercises : Activity
    {
        private ListView ListData => FindViewById<ListView>(Resource.Id.fitnessProgramListView);
        private int ProgramId => int.Parse(Intent.GetStringExtra("ProgramId"));


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Exercises);
            LoadData(ProgramId);
        }

        private void LoadData(int programId)
        {
            var listSource = DatabaseExecuter.GetProgramExercises(programId);
            var adapter = new ExerciseListViewAdapter(this, listSource);
            ListData.Adapter = adapter;
        }
    }
}