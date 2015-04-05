using ScoreKeeper.Dal;
using ScoreKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreKeeper.ViewModels
{
    public class GameDetails
    {
        public int Id { get; set; }
        public int[] SelectedPlayerIds { get; set; }
        public int SelectedEndScore { get; set; }
        public List<PlayerGameDetails> Players { get; set; }
		public List<GameRound> RoundList { get; set; }


        public GameDetails()
        {
			Players = new List<PlayerGameDetails>();
			RoundList = new List<GameRound>( );
        }

        //On Create
        public GameDetails(ScoreKeeperContext Dal)
            : this()
        {
            Players.AddRange(Dal.Players.ToList().Select(s => new PlayerGameDetails(s)));
			RoundList.Add( new GameRound( Players.Count( ) ) );
        }

		public GameDetails( Game g )
			: this( )
		{
			Id = g.GameID;
			Players.AddRange( g.PlayersGameMap.Select( s => s.Player ).ToList( ).Select( s => new PlayerGameDetails( s ) ) );
			RoundList.Add( new GameRound( Players.Count( ) ) );
			SelectedEndScore = g.MaxScore;
		}

    }

    public class PlayerGameDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
		public bool Selected { get; set; }
		public int CurrentScore { get; set; }

        public PlayerGameDetails() { }
        public PlayerGameDetails( Player p )
        {
            Id = p.PlayerID;
            Name = p.Name;
            ImagePath = p.ImageName;
			Selected = false;
        }
    }

	public class GameRound
	{
		public int Id { get; set; }
		public List<GameRoundListItem> GameRoundListItem { get; set; }

		public GameRound()
		{
			Id = 1;
			GameRoundListItem = new List<GameRoundListItem>( );
		}

		public GameRound( int count )
			: this( )
		{
			for ( int i = 0; i < count; i++ )
			{
				GameRoundListItem.Add( new GameRoundListItem( ) );
			}
		}
	}

	public class GameRoundListItem
	{
		public int Score { get; set; }

		public GameRoundListItem( )
		{
			Score = 0;
		}
	}
}