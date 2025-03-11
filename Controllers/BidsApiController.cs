using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FormBudAdmin.Data;
using FormBudAdmin.Models;
using Newtonsoft.Json;

namespace FormBudAdmin.Controllers
{
    [Route("api/bid")]
    [ApiController]
    public class BidsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BidsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/bid
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBid()
        {
            return await _context.Bid.ToListAsync();
        }

        // GET: api/bid/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBid(int id)
        {
            var bid = await _context.Bid.FindAsync(id);

            if (bid == null)
            {
                return NotFound();
            }

            return bid;
        }

        /*
            *   
            *   Om telefon finns i Buyer Tabell
            *      returnera buyerId
            *   
            *   Skapa ett Bid baserat på BuyerId, Bud och ProduktId
            *   
            *   
            *   
            */

        // POST: api/bid
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bid>> PostBid(BidRequest bidRequest)
        {
            if(bidRequest == null) {
                return BadRequest("BidRequest kan inte vara null");
            }

            var buyer = await _context.Buyer
                .FirstOrDefaultAsync(m => m.BuyerPhone == bidRequest.BuyerPhone);

            Console.WriteLine($"BidRequest: {JsonConvert.SerializeObject(bidRequest)}");

            if (buyer == null)
            {
                Buyer newBuyer = new Buyer();
                newBuyer.BuyerPhone = bidRequest.BuyerPhone;
                newBuyer.BuyerName = bidRequest.BuyerName;
                newBuyer.BuyerEmail = bidRequest.BuyerEmail;
                _context.Buyer.Add(newBuyer);

                //Spara buyer i databasen
                await _context.SaveChangesAsync();

                buyer = newBuyer;
            }

            Bid bid = new Bid();
            bid.ProductId = bidRequest.ProductId;

            if(buyer != null)
            {
                bid.BuyerId = buyer.Id;
            } else
            {
                return NotFound();
            }

                bid.Price = bidRequest.Price;

            _context.Bid.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBid", new { id = bid.Id }, bid);
        }

       
    }
}
