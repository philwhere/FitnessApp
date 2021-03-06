﻿using System.Collections.Generic;
using System.Linq;
using Android.Util;
using FitnessApp.Resources.Model;
using SQLite;
using static FitnessApp.Resources.Shortcuts;

namespace FitnessApp.Resources.DataHelper
{
    public static class DatabaseExecuter
    {
        //public bool CreateDatabase()
        //{
        //    try
        //    {
        //        using (var connection = new SQLiteConnection(PersonalDatabaseFile))
        //        {
        //            connection.CreateTable<Program>();
        //        }
        //        return true;
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        Handle(ex);
        //        return false;
        //    }
        //}

        public static int GetLatestExerciseId()
        {
            try
            {
                using (var connection = new SQLiteConnection(ReadOnlyDatabaseFile))
                    return connection.Table<Exercise>().Last().Id;
            }
            catch (SQLiteException ex)
            {
                Handle(ex);
                return 0;
            }
        }

        public static List<Program> GetPrograms()
        {
            try
            {
                using (var connection = new SQLiteConnection(ReadOnlyDatabaseFile))
                    return connection.Table<Program>().ToList();
            }
            catch (SQLiteException ex)
            {
                Handle(ex);
                return null;
            }
        }

        public static List<Exercise> GetAllExercises()
        {
            try
            {
                using (var connection = new SQLiteConnection(ReadOnlyDatabaseFile))
                    return connection.Table<Exercise>().ToList();
            }
            catch (SQLiteException ex)
            {
                Handle(ex);
                return null;
            }
        }

        public static List<Program> GetProgram(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(ReadOnlyDatabaseFile))
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

        public static List<Exercise> GetProgramExercises(int programId)
        {
            try
            {
                using (var connection = new SQLiteConnection(ReadOnlyDatabaseFile))
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