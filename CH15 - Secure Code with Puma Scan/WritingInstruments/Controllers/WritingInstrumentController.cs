using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;

using WritingExample.Data;
using WritingExample.Models;
using System.Drawing;
using System.Collections.Generic;

namespace WritingExample.Controllers
{
    public class WritingInstrumentController : Controller
    {
        private readonly ILogger<WritingInstrumentController> logger;
        private readonly IMapper mapper;
        private WritingInstrumentDbContext context;

        public WritingInstrumentController(ILogger<WritingInstrumentController> logger, WritingInstrumentDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var instruments = await context.Crayons.ToListAsync();

            var ret = mapper.Map<List<CrayonViewModel>>(instruments);
            return View(ret);
        }

        public async Task<IActionResult> Crayon(int CrayonId = -1)
        {
            var instrument = await context.Crayons.FirstOrDefaultAsync(c => c.Id == CrayonId);
            if (instrument == null)
                instrument = getNewRandomCrayon();

            var ret = mapper.Map<CrayonViewModel>(instrument);
            return View(ret);

        }

        public async Task<IActionResult> CrayonSearch(string Color)
        {
            if (!string.IsNullOrEmpty(Color))
            {
                var instrument = await context.Crayons.FromSqlRaw("SELECT * FROM Crayons WHERE HTMLColor = '" + Color + "'").FirstOrDefaultAsync();

                if (instrument == null)
                {
                    instrument = new Crayon
                    {
                        Id = -1,
                        HTMLColor = "#000000"
                    };
                }

                var ret = mapper.Map<CrayonViewModel>(instrument);
                return View(ret);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveCrayon(CrayonViewModel model)
        {
            var instrument = await context.Crayons.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (instrument == null)
            {
                instrument = new Crayon
                {
                    Id = model.Id,
                    HTMLColor = model.Color
                };
                context.Crayons.Add(instrument);
            }
            else
                instrument.HTMLColor = model.Color;

            var changes = await context.SaveChangesAsync();
            var ret = mapper.Map<CrayonViewModel>(instrument);
            return RedirectToAction("Crayon", new { CrayonId = ret.Id });
        }

        private static Crayon getNewRandomCrayon()
        {
            Crayon instrument;
            var c = new Random((int)DateTime.Now.Ticks).Next(0x1000000);
            var color = ColorTranslator.FromHtml($"#{c:X6}");
            instrument = new Crayon(color);
            return instrument;
        }
    }
}