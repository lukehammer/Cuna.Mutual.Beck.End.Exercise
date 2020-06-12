﻿using System;
using System.Linq;
using Cuna.Mutual.Back.End.Exercise.Api.Controllers;

namespace Cuna.Mutual.Back.End.Exercise.Api.Data
{
    public interface IMacGuffinRepository
    {
        void AddNew(MacGuffin macGuffin);

        MacGuffin Get
            (Guid id);

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
            _context.SaveChangesAsync();
        }

        public MacGuffin Get(Guid id)
        {
            return _context.MacGuffin.First(x => x.Id == id);
        }

        public void Update(MacGuffin macGuffin)
        {
            _context.MacGuffin.Update(macGuffin);
            _context.SaveChangesAsync();
        }
    }


}