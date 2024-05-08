﻿using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class TeamRepository : ITeam
    {
        ApplicationDbContext context;
        public TeamRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Team team)
        {
            context.Teams.Add(team);
            context.SaveChanges();
        }

        public void Delete(Team team)
        {
            var delete = context.Teams.Find(team.Id);
            if (delete != null)
            {
                context.Teams.Remove(delete);
                context.SaveChanges();
            }
        }

        public List<Team> GetAll()
        {
            return context.Teams.ToList();
        }

        public Team GetById(int id)
        {
            return context.Teams.Find(id);
        }

        public void Update(Team team)
        {
            var edit = context.Teams.Find(team.Id);
            if (edit != null)
            {
                edit.TeamLogo = team.TeamLogo;
                edit.Name = team.Name;
                context.SaveChanges();
            }
        }
    }
}