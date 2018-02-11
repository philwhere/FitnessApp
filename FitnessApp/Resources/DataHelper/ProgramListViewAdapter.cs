using Android.App;
using Android.Views;
using Android.Widget;
using FitnessApp.Resources.Model;
using System;
using System.Collections.Generic;
using Object = Java.Lang.Object;

namespace FitnessApp.Resources.DataHelper
{
    public class ProgramListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Program> fitnessProgramList;

        public ProgramListViewAdapter(Activity activity, List<Program> fitnessProgramList)
        {
            this.activity = activity;
            this.fitnessProgramList = fitnessProgramList;
        }

        public override Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return fitnessProgramList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ??
                       activity.LayoutInflater.Inflate(Resource.Layout.FitnessProgram_ListView_DataTemplate, parent, false);
            var programName = view.FindViewById<TextView>(Resource.Id.programNameTextView);
            programName.Text = $"{fitnessProgramList[position].Name}";

            return view;
        }

        public override int Count => fitnessProgramList.Count;
    }
}