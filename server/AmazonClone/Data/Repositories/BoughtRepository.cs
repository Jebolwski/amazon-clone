﻿using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class BoughtRepository : Repository<Bought>, IBoughtRepository
    {
        public BoughtRepository(BaseContext db) : base(db)
        {
        }

        public bool checkIfThereIs(Guid userId)
        {
            List<Bought> boughts = dbset.Where(p => p.userId == userId).ToList();
            return boughts.Any();
        }

        public Bought getByUserId(Guid userId)
        {
            List<Bought> boughts = dbset.Where(p => p.userId == userId).ToList();
            if (boughts.Any())
            {
                return boughts[0];
            }
            else
            {
                return null;
            }
        }

        public List<Bought> getAllByUserId(Guid userId)
        {
            List<Bought> boughts = dbset.Where(p => p.userId == userId).Where(p => p.archived == false).ToList();
            if (boughts.Any())
            {
                return boughts;
            }
            else
            {
                return null;
            }
        }

        public List<Bought> getAllArchivedByUserId(Guid userId)
        {
            List<Bought> boughts = dbset.Where(p => p.userId == userId).Where(p => p.archived == true).ToList();
            if (boughts.Any())
            {
                return boughts;
            }
            else
            {
                return null;
            }
        }


    }
}
