﻿using System;

namespace ActionableMessagesBackend.Models
{
    
    /// <summary>
    /// Initial sequential card model class.
    /// </summary>
    public class InitialCard
    {
        public Action action { get; set; }
    }

    /// <summary>
    /// Action in adaptive card model class.
    /// </summary>
    public class Action
    {
        public string type { get; set; }

        public string title { get; set; }

        public Data data { get; set; }

        public string verb { get; set; }
    }

    /// <summary>
    /// Incident data model class.
    /// </summary>
    public class Data
    {
        public Guid IncidentId { get; set; }

        public string CreatedBy { get; set; }

        public string IncidentTitle { get; set; }

        public string AssignedTo { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string AssignedToName { get; set; }

        public string UserMRI { get; set; }
    }
}
