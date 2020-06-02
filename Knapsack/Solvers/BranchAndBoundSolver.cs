﻿namespace Knapsack.Solvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Knapsack.Utils;

    public class BranchAndBoundSolver : KnapsackSolver
    {
        public BranchAndBoundSolver(IList<Item> items, int capacity)
            : base(items, capacity)
        {
        }

        public override KnapsackSolution Solve()
        {
            this.Items = this.Items.OrderByDescending(i => i.Ratio).ToList();

            Node best = new Node();
            Node root = new Node();

            root.ComputeBound(this.Items, this.Capacity);

            var queue = new PriorityQueue<Node>();
            queue.Enqueue(root);
            //Console.WriteLine("\nroot| height: " + root.Height + " value: " + root.Value + " weight: " + root.Weight + " bound: " + root.Bound);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                

                if (node.Bound > best.Value && node.Height < this.Items.Count)
                {
                    Node with = new Node(node);

                    var item = this.Items[node.Height];
                    with.Weight += item.Weight;

                    if (with.Weight <= this.Capacity)
                    {
                        with.TakenItems.Add(this.Items[node.Height]);
                        with.Value += item.Value;
                        with.ComputeBound(this.Items, this.Capacity);

                        if (with.Value > best.Value)
                        {
                            best = with;
                        }

                        if (with.Bound > best.Value)
                        {
                            queue.Enqueue(with);
                        }
                 //       Console.WriteLine("\nwith| height: " + with.Height + " value: " + with.Value + " weight: " + with.Weight + " bound: " + with.Bound);
                    }

                    var without = new Node(node);
                    without.ComputeBound(this.Items, this.Capacity);

                    if (without.Bound > best.Value)
                    {
                        queue.Enqueue(without);
                    }
                 //   Console.WriteLine("\nwithout| height: " + without.Height + " value: " + without.Value + " weight: " + without.Weight + " bound: " + without.Bound);
                }
            }

            var solution = new KnapsackSolution
            {
                Value = best.Value,
                TotalWeight = best.Weight,
                Items = best.TakenItems,
                Approach = "Best-First Search with Branch and Bound"
            };

            return solution;
        }
    }
}