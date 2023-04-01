﻿using Microsoft.EntityFrameworkCore;


namespace NoteApp.Models
{
    public class Context : DbContext
    {
        // проверяем наличие БД
        public Context()
        {
            Database.EnsureCreated();
        }


        // настройка
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\App_Data\NoteUser.sqlite3");
        }


        // таблицы, которые будут map'иться в БД
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
