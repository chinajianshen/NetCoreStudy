﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Demo07
{
    class Program
    {
        static void Main(string[] args)
        {
           
            StartWith(LookingAround.Instance, new EmptyShoppingCart());

            When(LookingAround.Instance, (evt, state) =>
            {
                if (evt.FsmEvent is AddItem addItem)
                {
                    return GoTo(Shopping.Instance)
                        .Applying(new ItemAdded(addItem.Item))
                        .ForMax(TimeSpan.FromSeconds(1));
                }
                else if (evt.FsmEvent is GetCurrentCart)
                {
                    return Stay().Replying(evt.StateData);
                }

                return Stay();
            });

            When(Shopping.Instance, (evt, state) =>
            {
                if (evt.FsmEvent is AddItem addItem)
                {
                    return Stay()
                        .Applying(new ItemAdded(addItem.Item))
                        .ForMax(TimeSpan.FromSeconds(1));
                }
                else if (evt.FsmEvent is Buy)
                {
                    return GoTo(Paid.Instance).Applying(OrderExecuted.Instance)
                        .AndThen(cart =>
                        {
                            if (cart is NonEmptyShoppingCart nonShoppingCart)
                            {
                                reportActor.Tell(new PurchaseWasMade(nonShoppingCart.Items));
                                SaveStateSnapshot();
                            }
                            else if (cart is EmptyShoppingCart)
                            {
                                SaveStateSnapshot();
                            }
                        });
                }
                else if (evt.FsmEvent is Leave)
                {
                    return Stop().Applying(OrderDiscarded.Instance)
                        .AndThen(cart =>
                        {
                            reportActor.Tell(ShoppingCardDiscarded.Instance);
                            SaveStateSnapshot();
                        });
                }
                else if (evt.FsmEvent is GetCurrentCart)
                {
                    return Stay().Replying(evt.StateData);
                }
                else if (evt.FsmEvent is FSMBase.StateTimeout)
                {
                    return GoTo(Inactive.Instance).ForMax(TimeSpan.FromSeconds(2));
                }

                return Stay();
            });

            When(Inactive.Instance, (evt, state) =>
            {
                if (evt.FsmEvent is AddItem addItem)
                {
                    return GoTo(Shopping.Instance)
                        .Applying(new ItemAdded(addItem.Item))
                        .ForMax(TimeSpan.FromSeconds(1));
                }
                else if (evt.FsmEvent is FSMBase.StateTimeout)
                {
                    return Stop()
                        .Applying(OrderDiscarded.Instance)
                        .AndThen(cart => reportActor.Tell(ShoppingCardDiscarded.Instance));
                }

                return Stay();
            });

            When(Paid.Instance, (evt, state) =>
            {
                if (evt.FsmEvent is Leave)
                {
                    return Stop();
                }
                else if (evt.FsmEvent is GetCurrentCart)
                {
                    return Stay().Replying(evt.StateData);
                }

                return Stay();
            });
        }
    }

    public class Item
    {
        public Item(string id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public string Id { get; }

        public string Name { get; }

        public double Price { get; }
    }

    public interface IShoppingCart
    {
        IShoppingCart AddItem(Item item);
        IShoppingCart Empty();
    }

    public class EmptyShoppingCart : IShoppingCart
    {
        public IShoppingCart AddItem(Item item)
        {
            return new NonEmptyShoppingCart(ImmutableList.Create(item));
        }

        public IShoppingCart Empty()
        {
            return this;
        }
    }

    public class NonEmptyShoppingCart : IShoppingCart
    {
        public NonEmptyShoppingCart(ImmutableList<Item> items)
        {
            Items = items;
        }

        public IShoppingCart AddItem(Item item)
        {
            return new NonEmptyShoppingCart(Items.Add(item));
        }

        public IShoppingCart Empty()
        {
            return new EmptyShoppingCart();
        }

        public ImmutableList<Item> Items { get; }
    }

    public interface ICommand { }

    public class AddItem : ICommand
    {
        public AddItem(Item item)
        {
            Item = item;
        }

        public Item Item { get; set; }
    }

    public class Buy : ICommand
    {
        public static Buy Instance { get; } = new Buy();
        private Buy() { }
    }

    public class Leave : ICommand
    {
        public static Leave Instance { get; } = new Leave();
        private Leave() { }
    }

    public class GetCurrentCart : ICommand
    {
        public static GetCurrentCart Instance { get; } = new GetCurrentCart();
        private GetCurrentCart() { }
    }

    public interface IUserState : Akka.Persistence.Fsm.PersistentFSM.IFsmState { }

    public class LookingAround : IUserState
    {
        public static LookingAround Instance { get; } = new LookingAround();
        private LookingAround() {
         
        }
        public string Identifier => "Looking Around";
    }

    public class Shopping : IUserState
    {
        public static Shopping Instance { get; } = new Shopping();
        private Shopping() { }
        public string Identifier => "Shopping";
    }

    public class Inactive : IUserState
    {
        public static Inactive Instance { get; } = new Inactive();
        private Inactive() { }
        public string Identifier => "Inactive";
    }

    public class Paid : IUserState
    {
        public static Paid Instance { get; } = new Paid();
        private Paid() { }
        public string Identifier => "Paid";
    }

    public interface IDomainEvent { }
    public class ItemAdded : IDomainEvent
    {
        public ItemAdded(Item item)
        {
            Item = item;
        }
        public Item Item { get; set; }
    }

    public class OrderExecuted : IDomainEvent
    {
        public static OrderExecuted Instance { get; } = new OrderExecuted();
        private OrderExecuted() { }
    }

    public class OrderDiscarded : IDomainEvent
    {
        public static OrderDiscarded Instance { get; } = new OrderDiscarded();
        private OrderDiscarded() { }
    }

    public interface IReportEvent { }

    public class PurchaseWasMade : IReportEvent
    {
        public PurchaseWasMade(IEnumerable<Item> items)
        {
            Items = items;
        }

        public IEnumerable<Item> Items { get; }
    }

    public class ShoppingCardDiscarded : IReportEvent
    {
        public static ShoppingCardDiscarded Instance { get; } = new ShoppingCardDiscarded();
        private ShoppingCardDiscarded() { }
    }

}
