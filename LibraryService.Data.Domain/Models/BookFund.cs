﻿using LibraryService.Data.Domain;

namespace LibraryService.Domain.Core.Entities
{
    public class BookFund : KeyedEntityBase
    {
        public virtual long BookInFundId { get; set; }
        public virtual Book BookInFund { get; set; }
        public virtual Library Library { get; set; }
        public long Amount { get; set; }
    }
}