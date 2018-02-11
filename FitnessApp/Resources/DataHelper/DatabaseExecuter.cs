using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.Util;
using FitnessApp.Resources.Model;
using SQLite;
using static FitnessApp.Resources.Constants;
using static FitnessApp.Resources.Shortcuts;

namespace FitnessApp.Resources.DataHelper
{
    public class DatabaseExecuter
    {
        //public bool CreateDatabase()
        //{
        //    try
        //    {
        //        using (var connection = new SQLiteConnection(Path.Combine(Folder, FitnessAppDbName)))
        //            connection.CreateTable<Program>();
        //        return true;
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        Handle(ex);
        //        return false;
        //    }
        //}

        public List<Program> GetFitnessPrograms()
        {
            try
            {
                using (var connection = new SQLiteConnection(DatabaseFile))
                    return connection.Table<Program>().ToList();
            }
            catch (SQLiteException ex)
            {
                Handle(ex);
                return null;
            }
        }

        public List<Exercise> GetAllExercises()
        {
            try
            {
                using (var connection = new SQLiteConnection(DatabaseFile))
                    return connection.Table<Exercise>().ToList();
            }
            catch (SQLiteException ex)
            {
                Handle(ex);
                return null;
            }
        }

        public List<Program> GetFitnessProgram(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(DatabaseFile))
                    return connection.Table<Program>()
                        .Where(p => p.Id == id)
                        .ToList();
            }
            catch (SQLiteException ex)
            {
                Handle(ex);
                return null;
            }
        }

        public List<Exercise> GetProgramExercises(int programId)
        {
            try
            {
                using (var connection = new SQLiteConnection(DatabaseFile))
                {
                    //var exercises = connection.Table<Exercise>()
                    //    .AsEnumerable()
                    //    .Where(e => programExercises.Any(m => m.ExerciseId == e.Id))
                    //    .ToList();

                    var exercises = connection.Query<Exercise>(
                        "SELECT * FROM Exercise e " +
                        "JOIN ProgramExercise pe " +
                        "ON e.Id = pe.ExerciseId " +
                        "WHERE pe.ProgramId = ?", programId).ToList();

                    return exercises;
                }
            }
            catch (SQLiteException ex)
            {
                Handle(ex);
                return null;
            }
        }


        private static void Handle(SQLiteException ex)
        {
            Log.Info("SQLite Exception", ex.Message);
        }
    }
}