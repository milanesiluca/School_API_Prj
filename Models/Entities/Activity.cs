﻿namespace API_School_own_prj.Models.Entities
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public Guid TypeId { get; set; }
        public ActivityType? Type { get; set; }
        public Guid ModuleId { get; set; }
        public Module? Module { get; set; }
    }
}
