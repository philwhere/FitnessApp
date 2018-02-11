using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.App;
using Android.Views;
using Android.Widget;
using FitnessApp.Resources.Extensions;
using FitnessApp.Resources.Model;
using Object = Java.Lang.Object;

namespace FitnessApp.Resources.DataHelper
{
    public class ExerciseListViewAdapter : BaseAdapter
    {
        private readonly Activity Activity;
        private readonly List<Exercise> ExerciseList;

        public ExerciseListViewAdapter(Activity activity, List<Exercise> exerciseList)
        {
            Activity = activity;
            ExerciseList = exerciseList;
        }

        public override Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return ExerciseList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? 
                Activity.LayoutInflater.Inflate(Resource.Layout.Exercise_ListView_DataTemplate, parent, false);

            if (convertView == null)
                view.Click += delegate { Debug.WriteLine("one"); };
            
            var exercise = ExerciseList[position];
            PopulateView(view, exercise);

            return view;
        }

        public override int Count => ExerciseList.Count;


        private static void PopulateView(View view, Exercise exercise)
        {
            var name = view.FindViewById<TextView>(Resource.Id.exerciseName);
            var image = view.FindViewById<ImageView>(Resource.Id.exerciseImage);
            var reps = view.FindViewById<TextView>(Resource.Id.exerciseReps);
            var sets = view.FindViewById<TextView>(Resource.Id.exerciseSets);
            var rest = view.FindViewById<TextView>(Resource.Id.exerciseRest);
            var superset = view.FindViewById<TextView>(Resource.Id.exerciseSuperset);

            name.Text = $"{exercise.Name}";
            image.SetImageBitmap(exercise.Photo.ToBitmap());
            reps.Text = $"Recommended Reps: {exercise.RecommendedReps}";
            sets.Text = $"Recommended Sets: {exercise.RecommendedSets}";
            rest.Visibility = RestSpecified(exercise) ? ViewStates.Visible : ViewStates.Gone;
            rest.Text = $"Rest: {exercise.RestTime} seconds";
            superset.Visibility = exercise.IsSuperset ? ViewStates.Visible : ViewStates.Gone;
        }

        private static bool RestSpecified(Exercise exercise) => !string.IsNullOrEmpty(exercise.RestTime);
    }
}