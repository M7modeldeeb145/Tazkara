﻿using Microsoft.EntityFrameworkCore;
using Tazaker.Models;
using Tazkara.Data;
using Tazkara.IRepository;
using Tazkara.Models;

namespace Tazkara.Repository
{
    public class LeagueRepository : ILeague
    {
        ApplicationDbContext context;
        public LeagueRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(League league)
        {
            context.Leagues.Add(league);
            context.SaveChanges();  
        }

        public void Delete(int id)
        {
            var league = context.Leagues.Find(id);
            if (league != null)
            {
                context.Remove(league);
                context.SaveChanges();
            }
        }

        public List<League> GetAll()
        {
            return context.Leagues.ToList();
        }

       

        public League GetById(int id)
        {
            return context.Leagues.Find(id);
        }

        public List<Team> GetTeamsWithLeague(int id)
        {
            return context.Teams.Include(e=>e.Leagues).Include(e=>e.Matches).Where(e=> e.Id== id).ToList();
        }
      
        public void Update(League league)
        {
            var edit = context.Leagues.Find(league.Id);
            if (edit != null)
            {
                
                edit.Name = league.Name;
                context.SaveChanges();
            }
        }
    }
}
