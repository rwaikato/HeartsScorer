using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ScoreKeeper.Models
{
    public class Game
    {
        public int GameID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Played")]
        public DateTime DatePlayed { get; set; }

        [Range(25, 100)]
        [Display(Name = "Max Score")] 
        public int MaxScore { get; set; }

        public bool IsComplete { get; set; }

        public virtual ICollection<PlayersGameMap> PlayersGameMap { get; set; }
    }

    public class Player
    {
        public int PlayerID { get; set; }
        [Required]
        [Display(Name = "Player Name")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        public int LowestScore { get; set; }
        public int HighestScore { get; set; }
        public bool IsActive { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<PlayersGameMap> PlayersGameMap { get; set; }
    }

    public class PlayersGameMap
    {
        public int ID { get; set; }

        public int PlayerID { get; set; }
        public int GameID { get; set; }

        public virtual Player Player { get; set; }
        public virtual Game Game { get; set; }
    }
}