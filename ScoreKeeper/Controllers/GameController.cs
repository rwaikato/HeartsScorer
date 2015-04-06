using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScoreKeeper.Models;
using ScoreKeeper.Dal;
using ScoreKeeper.ViewModels;

 
namespace ScoreKeeper.Controllers
{
	[Authorize]
	public class GameController : Controller
	{
		private ScoreKeeperContext db = new ScoreKeeperContext( );

		// GET: /Game/
		public ActionResult Index( )
		{
			return View( db.Games.ToList( ) );
		}

		// GET: /Game/Details/5
		public ActionResult Details( int? id )
		{
			if ( id == null )
			{
				return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
			}
			Game game = db.Games.Find( id );
			if ( game == null )
			{
				return HttpNotFound( );
			}
			return View( game );
		}

		#region Create

		// GET: /Game/Create
		public ActionResult Create( )
		{
			GameDetails game = new GameDetails( db );
			game.SelectedEndScore = 50;

			return View( game );
		}

		// POST: /Game/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create( GameDetails vm )
		{
			if ( vm.SelectedPlayerIds == null || vm.SelectedPlayerIds.Count( ) < 3 || vm.SelectedPlayerIds.Count( ) > 7 )
			{
				ModelState.AddModelError( "SelectedPlayerIds", "Limited to 3 - 7 Players" );
			}

			if ( ModelState.IsValid )
			{
				Game game = new Game( );
				game.MaxScore = vm.SelectedEndScore;
				game.DatePlayed = DateTime.Now;
				game.IsComplete = false;

				db.Games.Add( game );
				db.SaveChanges( );

				foreach ( var player in vm.SelectedPlayerIds )
				{
					PlayersGameMap pgm = new PlayersGameMap( );
					pgm.GameID = game.GameID;
					pgm.PlayerID = player;

					db.PlayersGameMaps.Add( pgm );
				}

				db.SaveChanges( );

				return RedirectToAction( "StartGame", new { id = game.GameID } );
			}

			vm.Players.AddRange( db.Players.ToList( ).Select( s => new PlayerGameDetails( s ) ) );

			foreach(var player in vm.Players)
			{
				if( vm.SelectedPlayerIds != null && vm.SelectedPlayerIds.Any( s =>s == player.Id ) )
				{
					player.Selected = true;
				}
			}

			return View( vm );
		}

		#endregion

		[HttpGet]
		public ActionResult StartGame( int id )
		{
			Game game = db.Games.Where( g => g.GameID == id ).First( );
			GameDetails vm = new GameDetails( game );

			return View( vm );
		}

		[HttpPost]
		public ActionResult StartGame(GameDetails vm)
		{ 
			return View( vm );
		}

		// GET: /Game/Edit/5
		public ActionResult Edit( int? id )
		{
			if ( id == null )
			{
				return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
			}
			Game game = db.Games.Find( id );
			if ( game == null )
			{
				return HttpNotFound( );
			}
			return View( game );
		}

		// POST: /Game/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit( [Bind( Include = "GameID,DatePlayed,MaxScore,IsComplete" )] Game game )
		{
			if ( ModelState.IsValid )
			{
				db.Entry( game ).State = EntityState.Modified;
				db.SaveChanges( );
				return RedirectToAction( "Index" );
			}
			return View( game );
		}

		// GET: /Game/Delete/5
		public ActionResult Delete( int? id )
		{
			if ( id == null )
			{
				return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
			}
			Game game = db.Games.Find( id );
			if ( game == null )
			{
				return HttpNotFound( );
			}
			return View( game );
		}

		// POST: /Game/Delete/5
		[HttpPost, ActionName( "Delete" )]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed( int id )
		{
			Game game = db.Games.Find( id );
			db.Games.Remove( game );
			db.SaveChanges( );
			return RedirectToAction( "Index" );
		}

		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				db.Dispose( );
			}
			base.Dispose( disposing );
		}
	}
}
