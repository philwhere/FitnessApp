using Android.App;
using Android.Views;
using Android.Widget;
using FitnessApp.Resources.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Graphics;
using Org.Json;
using Object = Java.Lang.Object;

namespace FitnessApp.Resources.DataHelper
{
    public class ExerciseListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Exercise> exerciseList;

        public ExerciseListViewAdapter(Activity activity, List<Exercise> exerciseList)
        {
            this.activity = activity;
            this.exerciseList = exerciseList;
        }

        public override Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return exerciseList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ??
                       activity.LayoutInflater.Inflate(Resource.Layout.Exercise_ListView_DataTemplate, parent, false);

            var image = view.FindViewById<ImageView>(Resource.Id.exerciseImage);
            var name = view.FindViewById<TextView>(Resource.Id.exerciseName);
            var reps = view.FindViewById<TextView>(Resource.Id.exerciseReps);
            var sets = view.FindViewById<TextView>(Resource.Id.exerciseSets);

            SetImage(exerciseList[position].Photo, image);
            name.Text = $"{exerciseList[position].Name}";
            reps.Text = $"Recommended Reps: {exerciseList[position].RecommendedReps}";
            sets.Text = $"Recommended Sets: {exerciseList[position].RecommendedSets}";
            if (exerciseList[position].Superset)
                view.FindViewById<TextView>(Resource.Id.exerciseSuperset).Visibility = ViewStates.Visible;
            return view;
        }

        private static void SetImage(byte[] photo, ImageView image)
        {
            var bitmap = BitmapFactory.DecodeByteArray(photo, 0, photo.Length);
            image.SetImageBitmap(bitmap);
        }

        public override int Count => exerciseList.Count;
    }
}