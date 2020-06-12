using System;
using System.Linq;
using Cuna.Mutual.Back.End.Exercise.Api.Controllers;
using Cuna.Mutual.Back.End.Exercise.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cuna.Mutual.Back.End.Exercise.Api.Data
{
    public interface IMacGuffinRepository
    {
        void AddNew(MacGuffin macGuffin);

        MacGuffin Get(int id);

        void Update(MacGuffin macGuffin);
    }

    public class MacGuffinRepository : IMacGuffinRepository
    {
        private readonly MacGuffinContext _context;

        public MacGuffinRepository(MacGuffinContext context)
        {
            _context = context;
        }
        public void AddNew(MacGuffin macGuffin)
        {
            _context.MacGuffin.Add(macGuffin);
            _context.SaveChanges();
        }

        public MacGuffin Get(int id)
        {
            return _context.MacGuffin.Include(M => M.Statuses).First(x => x.Id == id);
        }

        public void Update(MacGuffin macGuffin)
        {
          
            _context.MacGuffin.Update(macGuffin);
            
            _context.SaveChanges();
        }
    }


}