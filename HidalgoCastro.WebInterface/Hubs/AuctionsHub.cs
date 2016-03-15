using HidalgoCastro.WebInterface.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidalgoCastro.WebInterface.Hubs
{
    [HubName("AuctionsHub")]
    public class AuctionsHub: Hub
    {

        private static List<Auction> auctions = new List<Auction>()
        {
            new Auction
            {
                Id = 1,
                Name = "First Auction",
                Price = 0.01M,
                EndDateTime = new DateTime(2016, 01, 31, 23, 59, 59)
            },
            new Auction
            {
                Id = 2,
                Name = "Second Auction",
                Price = 0.01M,
                EndDateTime = new DateTime(2016, 01, 31, 23, 59, 59)
            }
        };

        public static List<Auction> GetAuctions()
        {
            return auctions;
        }

        public void send(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                Auction temp = JsonConvert.DeserializeObject<Auction>(message);

                Auction auction = auctions.Find(x => x.Id == temp.Id);
                auction.Price = temp.Price;
                auction.EndDateTime.AddSeconds(10);
            }

            ListAuctions result = new ListAuctions
            {
                Data = auctions,
                ServerDateTime = DateTime.Now
            };

            Clients.All.addMessage(result);
        }
    }
}
