﻿using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class MatchRepository : IMatch
    {
        ApplicationDbContext context;
        public MatchRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Match match)
        {
            context.Matchs.Add(match);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var delete = context.Matchs.Find(id);
            if (delete != null)
            {
                context.Matchs.Remove(delete);
                context.SaveChanges();
            }
        }

        public List<Match> GetAll()
        {
            return context.Matchs.Include(e=>e.Stadium).Include(e=>e.League).Include(e=>e.Teams).ToList();
        }

        public Match GetById(int id)
        {
            var match = context.Matchs.Include(e=>e.League).Include(e=>e.Stadium).Include(e=>e.Teams).FirstOrDefault(e=>e.Id==id);
            if (match != null)
            {
                return match;
            }
            return null;
        }

        public List<League> GetLeagues()
        {
            return context.Leagues.ToList();
        }

        public List<Stadium> GetStadiums()
        {
            return context.Stadiums.ToList();
        }

        public List<Team> GetTeams()
        {
            return context.Teams.ToList();
        }

        public List<Match> Search(string name)
        {
            return context.Matchs.Include(e=>e.League).Include(e=>e.Stadium).Include(e=>e.Teams).Where(e=>e.Name.Contains(name)).ToList();
        }
        public void Update(Match match)
        {
            context.Matchs.Update(match);
            context.SaveChanges();
        }

    }
}
