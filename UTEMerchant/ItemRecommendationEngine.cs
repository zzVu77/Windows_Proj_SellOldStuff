using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class ItemRecommendationEngine
    {
        private static readonly List<Item> Items = new Item_DAO().Load();

        public static List<Item> GetRecommendations(Item targetItem, int sellerID, int recommendationCount = 10)
        {
            if (targetItem == null)
            {
                throw new ArgumentNullException(nameof(targetItem));
            }

            List<Item> recommendations = new List<Item>();

            // 1. Type Matching: Find items of the same type (lower case for case-insensitive comparison)
            var typeMatches = Items.Where(i => i.Item_Id != targetItem.Item_Id && i.Type.ToLower() == targetItem.Type.ToLower());

            // 2. Condition Similarity: Prioritize items in similar condition.
            var conditionMatches = typeMatches
                .Select(i => new
                {
                    Item = i,
                    ConditionDifference = Math.Abs(i.Condition - targetItem.Condition)
                })
                .OrderBy(x => x.ConditionDifference)
                .Select(x => x.Item);

            // 3. Price Range: Consider items within a reasonable price range of the target item.
            decimal priceLowerBound = (decimal)(targetItem.Price * (double)0.8m); // 20% lower
            decimal priceUpperBound = (decimal)(targetItem.Price * (double)1.2m); // 20% higher

            // Note: If the list is too small, select all items without considering price
            if (conditionMatches.Count() < 10)
            {
                priceLowerBound = decimal.MinValue;
                priceUpperBound = decimal.MaxValue;
            }
            var priceMatches = conditionMatches
                .Where(i => i.Price >= (double)priceLowerBound && i.Price <= (double)priceUpperBound);

            // 4. Content-Based Filtering: Analyze item details (Name, Condition_Description, Detail_description)
            var contentMatches = priceMatches.Select(i => new
            {
                Item = i,
                SimilarityScore = CalculateContentSimilarity(targetItem, i) // Implement a similarity function
            })
            .OrderByDescending(x => x.SimilarityScore);

            // 5. Time-Based Decay: Give preference to newer items.
            var timeDecayMatches = contentMatches.Select(i => new
            {
                Item = i.Item,
                TimeScore = CalculateTimeDecay(i.Item.PostedDate)
            })
            .OrderByDescending(x => x.TimeScore);

            // Combine results and take top recommendations
            recommendations = timeDecayMatches
                .Select(x => x.Item)
                .Take(recommendationCount)
                .ToList();

            // Exclude items from the current seller
            return recommendations.Where(i => i.SellerID != sellerID).ToList();
        }

        // Helper functions for content-based filtering and time decay

        // Content similarity function: Keyword matching
        private static double CalculateContentSimilarity(Item item1, Item item2)
        {
            string item1Text = $"{item1.Name.ToLower()} {item1.Condition_Description.ToLower()} {item1.Detail_description.ToLower()}";
            string item2Text = $"{item2.Name.ToLower()} {item2.Condition_Description.ToLower()} {item2.Detail_description.ToLower()}";

            // Keyword matching
            string[] keywords = item1Text.Split(' ');
            int matchingKeywords = keywords.Count(k => item2Text.Contains(k));
            return (double)matchingKeywords / keywords.Length;
        }

        // Time decay function: Exponential decay over time
        private static double CalculateTimeDecay(DateTime postedDate)
        {
            TimeSpan timeElapsed = DateTime.Now - postedDate;
            return Math.Exp(-timeElapsed.TotalDays / 30);
        }
    }
}
