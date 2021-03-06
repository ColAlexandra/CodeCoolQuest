﻿using Codecool.Quest.Models.ItemsCollection;

namespace Codecool.Quest.Models.Actors
{
    public class Player : Actor
    {
        public override string TileName => "player";

        //przeniesc do class Items Collection i tu wywolac//stworzyc ekwipunek i go przeszukiwac, czy ma czy nie
        public bool HasSword { get; set; }
        public bool HasKey { get; set; }

        public Player(Cell cell) : base(cell)
        {
            Health = 30;
            
        }

        public void MovePlayer(MoveDirection move)
        {
            var (x, y) = move.ToVector();
            Move(x, y);
        }

        public bool ActorFight(Cell cell)
        {
            var actorInCell = cell.ActorInCell();

            return actorInCell switch
            {
                null => false,
                _ => HandleActors(actorInCell, cell),
            };
        }

        public bool ActorCollectItem(Cell cell)
        {
            var itemInCell = cell.ItemInCell();
            return itemInCell switch
            {
                null => false,
                _ => HandleItems(itemInCell, cell),
            };
        }


        private bool HandleActors(Actor actor, Cell cell)
        {
            if (actor.TileName == "skeleton")
            {
                Fight(actor, cell);
            }
            else if(HasKey == true)
            {
                OpenTheDoor(cell);
            }
            return false;
        }

        private bool HandleItems(Item items, Cell cell)
        {
            return items.TileName switch
            {
                "key" => CollectSingleItem(items, cell),
                "sword" => CollectSingleItem(items, cell),
                _ => false,
            };
        }

        private bool Fight(Actor actor, Cell cell)
        {
            var isFree = false;
            if (HasSword)
            {
                Health -= 2;
                actor.Health -= 2;
                isFree = LifeOrDead(actor, cell);

            }

            return isFree;
        }

        private bool CollectSingleItem(IDrawable items, Cell cell)
        {
            switch (items.TileName)
            {
                case "sword":
                    HasSword = true;
                    cell.Items = null;
                    return true;

                case "key":
                    HasKey = true;
                    cell.Items = null;
                    return true;

                default:
                    return false;
            }

        }

        private void OpenTheDoor(Cell cell)
        {
            cell.Actor = null;
        }


        private static bool LifeOrDead(Actor actor, Cell cell)
        {
            if (actor.Health > 0) return false;
            cell.Actor = null;
            return true;

        }
    }
}